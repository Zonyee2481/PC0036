using Infrastructure;
using MotionIODevice;
using SeqServer.Utilities.ModEventArg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MotionIODevice.Common;

namespace SeqServer
{
    public class GeneralControl : BaseFunc
    {
        private RunSeq m_Temp;
        private RunSeq m_StepChange;
        private ManualRunSeq m_ManualRunSeq;
        private StationCfg m_StationCdf;
        public tagMyVar m_MyFlag = new tagMyVar();
        int t = 0;
        private RunSeq m_RunSeq
        {
            get
            {
                return m_StepChange;
            }
            set
            {
                if (m_StepChange != value)
                {
                    m_StepChange = value;
                    Log($"{m_StationCdf.StationName}", $"{m_StepChange}");
                }

            }
        }

        public struct tagMyVar
        {
            internal MessageEventArg MsgArg;
        }

        public GeneralControl(List<StationCfg> StationData, TotalModule runmodule, List<ErrData> AllErrorData, List<BitCodeInfo> BitCode)
        : this(StationData, runmodule, BitCode)
        {
            InitData();            
        }

        private void InitData()
        {
            m_ModFlag = new MyVariable();
            m_MyFlag = new tagMyVar();
            m_MyFlag.MsgArg = new MessageEventArg();
        }

        private enum IN
        {
            AUTO,
            START_TIMER,
            END_LOT,
        }

        private enum OUT
        {
            TIMESUP,
            BITCODE_1,
            BITCODE_2,
            BITCODE_4,
            BITCODE_8,
            START_CONTROL_RELAY,
            START_BUTTON_LED,
        }

        private enum TimeOut
        {

        }

        private enum ErrCode
        {

        }

        private enum RunSeq
        {
            EOS = -1,
            // Init Sequence
            Init = 50,
            Reset,
            InitDone,
            // Run Sequence
            Begin = 1,
            TurnOnBitCode,
            TurnOnStartControl,
            WaitStartTimer,
            TurnOffStartControl,
            SetTimer,
            CheckTimesUp,
            TurnOnTimesUp,
            WaitEndLot,

            MC_ResumeReq,
        }

        private enum ManualRunSeq
        {
            StartProcess
        }

        private void Run()
        {
            try
            {
                while (true)
                {
                    lock (m_SyncSN)
                    {
                        switch (m_RunSeq)
                        {
                            #region Init Sequence
                            case RunSeq.Init:
                                {
                                    m_RunSeq = RunSeq.InitDone;
                                }
                                break;
                            case RunSeq.InitDone:
                                {
                                    m_MyFlag.MsgArg.StationName = m_StationCdf.StationName;
                                    m_MyFlag.MsgArg.MachineStatus = eMcState.MC_INITIALIZED;
                                    m_SeqEventPool.FireEvent(EV_TYPE.InitDone, this);
                                    FireEvent2UI(m_MyFlag.MsgArg);
                                    SM.McProcessTime = 0;
                                    TimesUp = false;
                                    StartCtrlRelay = false;
                                    StartBtnLED = false;
                                    ClearBitCode();
                                    m_RunSeq = RunSeq.EOS;
                                }
                                break;
                            #endregion

                            #region Run Sequence
                            case RunSeq.Begin:
                                {
                                    m_RunSeq = RunSeq.TurnOnBitCode;
                                }
                                break;
                            case RunSeq.TurnOnBitCode:
                                {
                                    SetBitCode();
                                    m_RunSeq = RunSeq.TurnOnStartControl;
                                }
                                break;
                            case RunSeq.TurnOnStartControl:
                                {
                                    StartCtrlRelay = true;
                                    StartBtnLED = true;
                                    SM.McProcessTime = m_LotInfo.InitialRun ? (m_LotInfo._RecipeInfo.TimeLimit_1st / 1000) : (m_LotInfo._RecipeInfo.TimeLimit_2nd / 1000);
                                    m_RunSeq = RunSeq.WaitStartTimer;
                                }
                                break;
                            case RunSeq.TurnOffStartControl:
                                {
                                    StartCtrlRelay = false;
                                    StartBtnLED = false;
                                    m_RunSeq = RunSeq.SetTimer;
                                }
                                break;
                            case RunSeq.SetTimer:
                                {
                                    t = Environment.TickCount + (m_LotInfo.InitialRun ? m_LotInfo._RecipeInfo.TimeLimit_1st : m_LotInfo._RecipeInfo.TimeLimit_2nd);
                                    SM.StartCount = true;
                                    m_RunSeq = RunSeq.CheckTimesUp;
                                }
                                break;
                            case RunSeq.CheckTimesUp:
                                {
                                    if (Environment.TickCount > t)
                                    {
                                        SM.StartCount = false;
                                        m_RunSeq = RunSeq.TurnOnTimesUp;
                                    }
                                }
                                break;
                            case RunSeq.TurnOnTimesUp:
                                {
                                    TimesUp = true;                                    
                                    m_RunSeq = RunSeq.WaitEndLot;
                                }
                                break;
                            case RunSeq.WaitEndLot:
                                {

                                }
                                break;
#endregion

                            case RunSeq.MC_ResumeReq:
                                {
                                    if (m_ModFlag.MCResumeSeq)
                                    {
                                        m_ModFlag.MCResumeSeq = false;
                                        m_RunSeq = m_Temp;
                                    }
                                }
                                break;
                        }
                    }
                    Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {

            }
        }

#region IO Inputs

#endregion

#region IO Outputs
        private bool BitCode_1
        {
            set
            {
                OutBit(OUT.BITCODE_1.ToString(), (TOutputStatus)(value ? 1 : 0));
            }
        }

        private bool BitCode_2
        {
            set
            {
                OutBit(OUT.BITCODE_2.ToString(), (TOutputStatus)(value ? 1 : 0));
            }
        }

        private bool BitCode_4
        {
            set
            {
                OutBit(OUT.BITCODE_4.ToString(), (TOutputStatus)(value ? 1 : 0));
            }
        }

        private bool BitCode_8
        {
            set
            {
                OutBit(OUT.BITCODE_8.ToString(), (TOutputStatus)(value ? 1 : 0));
            }
        }

        private void SetBitCode()
        {
            int index = m_LotInfo.InitialRun ? m_LotInfo._RecipeInfo.RunHz_1st : m_LotInfo._RecipeInfo.RunHz_2nd;
            bool bitCode1 = m_BitCode[index].BitCode_1;
            bool bitCode2 = m_BitCode[index].BitCode_2;
            bool bitCode4 = m_BitCode[index].BitCode_4;
            bool bitCode8 = m_BitCode[index].BitCode_8;

            BitCode_1 = bitCode1;
            BitCode_2 = bitCode2;
            BitCode_4 = bitCode4;
            BitCode_8 = bitCode8;
        }

        private void ClearBitCode()
        {
            BitCode_1 = false;
            BitCode_2 = false;
            BitCode_4 = false;
            BitCode_8 = false;
        }

        private bool StartCtrlRelay
        {
            set
            {
                OutBit(OUT.START_CONTROL_RELAY.ToString(), (TOutputStatus)(value ? 1 : 0));
            }
        }

        private bool StartBtnLED
        {
            set
            {
                OutBit(OUT.START_BUTTON_LED.ToString(), (TOutputStatus)(value ? 1 : 0));
            }
        }

        private bool TimesUp
        {
            set
            {
                OutBit(OUT.TIMESUP.ToString(), (TOutputStatus)(value ? 1 : 0));
            }
        }
#endregion

        internal override void StartThread()
        {
            ThreadDeclaration_Normal(Run);
        }

        internal override void OnBegin(object sender, EventArgs args)
        {
            lock (m_SyncEvent)
            {
                m_RunSeq = RunSeq.Begin;
            }
        }

        internal override void OnInit(object sender, EventArgs args)
        {
            InitData();
            m_RunSeq = RunSeq.Init;
        }

        internal override void OnWorkReq(object sender, EventArgs args)
        {
            lock (m_SyncEvent)
            {
                if (m_RunSeq == RunSeq.WaitStartTimer)
#if !SIMULATION
                    if (ReadBit(IN.START_TIMER.ToString()))
#endif
                {
                    m_RunSeq = RunSeq.TurnOffStartControl;
                }
            }
        }

        internal override void OnFarProcComp(object sender, EventArgs args)
        {
            lock (m_SyncEvent)
            {
                if (m_RunSeq == RunSeq.WaitEndLot)
#if !SIMULATION
                    if (ReadBit(IN.END_LOT.ToString()))
#endif
                {
                    //m_MyFlag.MsgArg.MachineStatus = eMcState.MC_INITIALIZING;
                    //FireEvent2UI(m_MyFlag.MsgArg);
                    m_RunSeq = RunSeq.Init;
                }
            }
        }

        internal override void OnKill(object sender, EventArgs args)
        {
            lock (m_SyncEvent)
            {
                m_RunSeq = RunSeq.EOS;
            }
        }

        internal override void OnMCStopSeq(object sender, EventArgs args)
        {

            lock (m_SyncEvent)
            {
                //Store the running Sequence
                //Wait for Resume request
                m_Temp = m_RunSeq;
                m_RunSeq = RunSeq.MC_ResumeReq;
            }
        }

        internal override string GetSeqNum()
        {
            return m_RunSeq.ToString();
        }

        internal override void OnManualSeq(object sender, EventArgs args)
        {
            lock (m_SyncEvent)
            {
                var arg = args as ManualSeqEventArgs;
                if (m_RunSeq == RunSeq.EOS)
                {
                    goto _Run;
                }
                else if (m_RunSeq == 0)
                {
                    goto _Run;
                }
                else return;

                _Run:

                if (arg.StationName != m_StationCdf.StationName) return;

                //Check SeqName Available in Enum ManualRunSeq
                if (!Enum.IsDefined(typeof(ManualRunSeq), arg.SeqName)) return;
                m_ManualRunSeq = (ManualRunSeq)Enum.Parse(typeof(ManualRunSeq), arg.SeqName);

                switch (m_ManualRunSeq)
                {

                }
            }
        }

        private GeneralControl(List<StationCfg> StationData, TotalModule runmodule, List<BitCodeInfo> BitCode)
        {
            m_StationData = StationData;
            m_StationCdf = m_StationData.FirstOrDefault(x => x.SeqID == (int)runmodule);
            m_BitCode = BitCode;

#region IO Modules
            foreach (var iocontrol in m_iocontrols)
            {
                foreach (var input in iocontrol.GetInputList)
                {
                    if (m_StationCdf.SeqID == input.Module)
                    {
                        m_Inputs.Add(input);
                    }
                }

                foreach (var output in iocontrol.GetOutputList)
                {
                    if (m_StationCdf.SeqID == output.Module)
                    {
                        m_Outputs.Add(output);
                    }
                }
            }
#endregion
        }
    
        private void ConvertToMilliseconds(double timeLimit, out int result)
        {
            result = Convert.ToInt32(timeLimit * (60 * 60)) * 1000;
        }
    }
}
