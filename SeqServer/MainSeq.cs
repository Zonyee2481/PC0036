using MotionIODevice;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MotionIODevice.Common;
using Infrastructure;
using KAEventPool;
using System.IO;

namespace SeqServer
{
    public class MainSeq : IMainSeq
    {
        private string StnCfgfilepath = @"..\..\..\Stations\";
        private string ErrCfgfilepath = @"..\..\..\Error Code\";
        private int m_TotalModule = (int)TotalModule.MaxModule; /*Enum.GetValues(typeof(TotalModule)).Length;*/
        private BaseFunc[] m_BaseSequence;
        private List<ErrData> ErrorData;
        private List<StationCfg> StationData;
        private List<BitCodeInfo> BitCode;

        void IMainSeq.BuildSequence()
        {
            m_BaseSequence = new BaseFunc[m_TotalModule];

            #region Instantiate Sequence
            m_BaseSequence[(int)TotalModule.GeneralControl] = new GeneralControl(StationData, TotalModule.GeneralControl, ErrorData, BitCode);

            #endregion

            #region Instantiate Thread
            Debug.Assert(m_BaseSequence.Count() == m_TotalModule, $"Sequence Instantiation and total modules counts not equal! m_BaseSequence ={m_BaseSequence.Count()}, m_TotalModule = {m_TotalModule}");
            for (int i = 0; i < m_TotalModule; i++)
            {
                m_BaseSequence[i].StartThread();
            }
            #endregion
        }

        void IMainSeq.BuildData()
        {
            StationData = new List<StationCfg>() { };
            ErrorData = new List<ErrData>() { };
            BitCode = new List<BitCodeInfo>() { };
            BaseFunc.ConstructAllStationData(StnCfgfilepath, StationData);
            BaseFunc.ConstructErrorData(ErrCfgfilepath, ErrorData);
            BaseFunc.ConstructBitCode(BitCode);
        }

        List<IMotionControl> IMainSeq.SetMotionBoardList
        {
            set
            {
                BaseFunc.m_motionBoards = value;
            }
        }

        List<IBoardIO> IMainSeq.SetIOBoardInputList
        {
            set
            {
                BaseFunc.m_iocontrols = value;
            }
        }

        IMainConnection IMainSeq.SetMainConnection
        {
            set
            {
                BaseFunc.m_communicationcontrols = value;
            }
        }

        BaseFunc[] IMainSeq.GetSeqModule
        {
            get
            {
                return m_BaseSequence;
            }
        }

        EventPool IMainSeq.SetEventPool_UI2Seq
        {
            set
                {
                for (int i = 0; i < m_BaseSequence.Count(); i++)
                {
                    m_BaseSequence[i].m_UIEventPool = value;
                }
            }
        }

        void IMainSeq.RecipeInfo(LotInfo lotinfo)
        {
            BaseFunc.m_LotInfo = lotinfo;
        }

        void IMainSeq.EnableStation(int station, bool enable)
        {
            m_BaseSequence[station].BypassStation(enable);
        }

        tagSeqFlag IMainSeq.MachineConfig
        {
            set
            {
                BaseFunc.m_SeqFlag = value;
            }
        }

        public LotCounter LotCounter { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<string> UnloadProdList { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<FixZone> FixZoneData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<UnloadStation> UnloadStationData { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        List<BitCodeInfo> IMainSeq.BitCode
        {
            get { return BitCode; }
            set { BitCode = value; }
        }

        List<string> IMainSeq.GetSeqNum()
        {
            List<string> _listofstring = new List<string>();
            for (int i = 0; i < m_BaseSequence.Count(); i++)
            {
                _listofstring.Add(m_BaseSequence[i].GetSeqNum());
            }

            return _listofstring;
        }

        int IMainSeq.GetProdCntNum(int station)
        {
            return m_BaseSequence[station].GetProdCntNum();
        }

        void IMainSeq.ResetProdCntNum(int station)
        {
            m_BaseSequence[station].SetProdCntNum();
        }

        double IMainSeq.GetCycleTime(int station)
        {
            return m_BaseSequence[station].GetCycleTime();
        }

        public List<CycletimeInfo> CycleTime(int station)
        {
            throw new NotImplementedException();
        }

        public void SaveFixZoneData()
        {
            throw new NotImplementedException();
        }
    }
}

