using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Advantech.Motion;

namespace MotionIODevice
{
    public class P1265 : Common
    {
        public bool Offline = false;

        bool[] Opened = new bool[4];

        const int MAX_DEVICE = 4;
        const int MAX_AXIS = 8;

        uint DevCount = 0;
        DEV_LIST[] AvailDevs = new DEV_LIST[Motion.MAX_DEVICES];
        IntPtr[] p_DeviceHandle = new IntPtr[MAX_DEVICE] { IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero };
        IntPtr[,] p_AxisHandle = new IntPtr[MAX_DEVICE, MAX_AXIS];
        IntPtr[,] p_GroupHandle = new IntPtr[MAX_DEVICE, 4];

        uint[] AxesPerDev = new uint[MAX_DEVICE];

        double[,] UPP = new double[MAX_DEVICE, MAX_AXIS];// { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } };
        bool[,] InvertPulse = new bool[MAX_DEVICE, MAX_AXIS];// { { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false } };

        double[,] StartV = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[,] DriveV = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[,] Accel = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[,] Decel = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[,] Jerk = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };

        //static bool DependenciesChecked = false;
        public P1265()
        {
            for (int i = 0; i < MAX_DEVICE; i++)
                for (int j = 0; j < MAX_AXIS; j++)
                {
                    UPP[i, j] = 1;
                    InvertPulse[i, j] = false;
                    StartV[i, j] = 0;
                    DriveV[i, j] = 0;
                    Accel[i, j] = 0;
                    Decel[i, j] = 0;
                }

            //if (!DependenciesChecked)
            //{
            //    CheckDependencies();
            //    DependenciesChecked = true;
            //}
        }

        #region Common
        public string GetErrorMessage(uint ErrCode)
        {
            StringBuilder ErrMsg = new StringBuilder(100);

            try
            {
                Motion.mAcm_GetErrorMessage((uint)ErrCode, ErrMsg, 100);
            }
            catch { throw; }

            return "0x" + ErrCode.ToString("X") + " " + ErrMsg.ToString();
        }
        public void DeviceAvailable(ref uint DeviceCount)
        {
            if (Offline) return;

            DeviceCount = 0;
            int Res;
            Res = Motion.mAcm_GetAvailableDevs(AvailDevs, Motion.MAX_DEVICES, ref DeviceCount);
            if (Res != (int)ErrorCode.SUCCESS)
            {
                throw new Exception(GetErrorMessage((uint)Res));
            }
        }
        #endregion

        #region Device
        public bool OpenDeviceLtype(byte DeviceNo)
        {
            if (Offline) return true;

            //            LoadConfig();

            try
            {
                DeviceAvailable(ref DevCount);

                uint Res = Motion.mAcm_DevOpen(AvailDevs[DeviceNo].DeviceNum, ref p_DeviceHandle[DeviceNo]);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                uint buffLen = 4;
                Res = Motion.mAcm_GetProperty(p_DeviceHandle[DeviceNo], (uint)PropertyID.FT_DevAxesCount, ref AxesPerDev[DeviceNo], ref buffLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                for (ushort i = 0; i < AxesPerDev[DeviceNo]; i++)
                {
                    Res = Motion.mAcm_AxOpen(p_DeviceHandle[DeviceNo], i, ref p_AxisHandle[DeviceNo, i]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }
                }

                Reset(DeviceNo);

                Opened[DeviceNo] = true;

                string FileName = "";// @"C:\29000000.cfg"; //@"C:\2d000000.cfg";
                if (DeviceNo == 0)
                {
                    FileName = @"C:\29000000.cfg";
                }
                else if (DeviceNo == 1)
                {
                    FileName = @"C:\29001000.cfg";
                }
                else if (DeviceNo == 2)
                {
                    FileName = @"C:\2d002000.cfg";
                }
                else if (DeviceNo == 3)
                {
                    FileName = @"C:\2d003000.cfg";
                }
                else if (DeviceNo == 4)
                {
                    FileName = @"C:\2d004000.cfg";
                }
                //if (OpenConfigFile.ShowDialog() != DialogResult.OK)
                //return;
                //Set all configurations for the device according to the loaded file
                Res = Motion.mAcm_DevLoadConfig(p_DeviceHandle[DeviceNo], FileName);
                if (Res != (uint)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                return true;
            }
            catch { throw; }
        }

        public bool OpenDevice(byte DeviceNo)
        {
            if (Offline) return true;

            //            LoadConfig();

            try
            {
                DeviceAvailable(ref DevCount);

                uint Res = Motion.mAcm_DevOpen(AvailDevs[DeviceNo].DeviceNum, ref p_DeviceHandle[DeviceNo]);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                uint buffLen = 4;
                Res = Motion.mAcm_GetProperty(p_DeviceHandle[DeviceNo], (uint)PropertyID.FT_DevAxesCount, ref AxesPerDev[DeviceNo], ref buffLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                for (ushort i = 0; i < AxesPerDev[DeviceNo]; i++)
                {
                    Res = Motion.mAcm_AxOpen(p_DeviceHandle[DeviceNo], i, ref p_AxisHandle[DeviceNo, i]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }
                }

                Reset(DeviceNo);

                Opened[DeviceNo] = true;

                string FileName = "";// @"C:\29000000.cfg";
                if (DeviceNo == 0)
                {
                    FileName = @"C:\29000000.cfg";
                }
                else if (DeviceNo == 1)
                {
                    FileName = @"C:\29001000.cfg";
                }
                else if (DeviceNo == 2)
                {
                    FileName = @"C:\29002000.cfg";
                }
                else if (DeviceNo == 3)
                {
                    FileName = @"C:\29003000.cfg";
                }
                else if (DeviceNo == 4)
                {
                    FileName = @"C:\29004000.cfg";
                }
                //if (OpenConfigFile.ShowDialog() != DialogResult.OK)
                //return;
                //Set all configurations for the device according to the loaded file
                Res = Motion.mAcm_DevLoadConfig(p_DeviceHandle[DeviceNo], FileName);
                if (Res != (uint)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                return true;
            }
            catch { throw; }
        }
        public bool OpenDevice(TDevice Device)
        {
            try
            {
                return OpenDevice(Device.ID);
            }
            catch { throw; }
        }
        public bool OpenDeviceLtype(TDevice Device)
        {
            try
            {
                return OpenDeviceLtype(Device.ID);
            }
            catch { throw; }
        }

        public void CloseDevice(byte DeviceNo)
        {
            if (Offline) return;

            //SaveConfig();

            uint Res = 0;
            for (uint i = 0; i < AxesPerDev[DeviceNo]; i++)
            {
                try
                {
                    Res = Motion.mAcm_AxClose(ref p_AxisHandle[DeviceNo, i]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }
                }
                catch { };
            }

            for (int i = 0; i < 3; i++)
            {
                try
                {
                    Res = Motion.mAcm_GpClose(ref p_GroupHandle[DeviceNo, i]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                    p_GroupHandle[DeviceNo, i] = IntPtr.Zero;
                }
                catch { };
            }

            Res = Motion.mAcm_DevClose(ref p_DeviceHandle[DeviceNo]);
            Opened[DeviceNo] = false;
        }
        public void CloseDevice(TDevice Device)
        {
            try
            {
                CloseDevice(Device.ID);
            }
            catch { throw; }
        }

        public bool DeviceOpened(byte DeviceNo)
        {
            return Opened[DeviceNo];
        }
        public bool DeviceOpened(TDevice Device)
        {
            return Opened[Device.ID];
        }

        public void Reset(byte DeviceNo)
        {
            if (Offline) return;

            try
            {
                uint BlendTime = 10;
                uint BufLen = 8;

                Motion.mAcm_SetProperty(p_DeviceHandle[DeviceNo], (uint)PropertyID.CFG_GpBldTime, ref BlendTime, BufLen);
            }
            catch { throw; }

            try
            {
                uint SFEnable = 1;
                uint BufLen = 8;

                Motion.mAcm_SetProperty(p_DeviceHandle[DeviceNo], (uint)PropertyID.CFG_GpSFEnable, ref SFEnable, BufLen);

                //uint Data = (uint)

                uint EmgLogic = 1;//Logic Act High
                Motion.mAcm_SetProperty(p_DeviceHandle[DeviceNo], (uint)PropertyID.CFG_DevEmgLogic, ref EmgLogic, 8);

                UInt32[] AxEvtStatusArray = new UInt32[32];
                UInt32[] GpEvtStatusArray = new UInt32[3];

                AxEvtStatusArray[0] = 0x01;
                AxEvtStatusArray[1] = 0x01;
                AxEvtStatusArray[2] = 0x01;
                AxEvtStatusArray[3] = 0x01;
                AxEvtStatusArray[4] = 0x01;
                AxEvtStatusArray[5] = 0x01;
                GpEvtStatusArray[0] = 0x01;
                //
                //GpEvtStatusArray[1] = 0x01;
                //GpEvtStatusArray[2] = 0x01;
                uint Res = Motion.mAcm_EnableMotionEvent(p_DeviceHandle[DeviceNo], AxEvtStatusArray, GpEvtStatusArray, 4, 1);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch { throw; }

            uint Data = 1;
            for (int i = 0; i < AxesPerDev[DeviceNo]; i++)
            {
                try
                {
                    Data = (uint)GenDoEnable.GEN_DO_EN;
                    Motion.mAcm_SetProperty(p_AxisHandle[DeviceNo, i], (uint)PropertyID.CFG_AxGenDoEnable, ref Data, 8);

                    Data = (uint)AlarmEnable.ALM_DIS;//EN;
                    Motion.mAcm_SetProperty(p_AxisHandle[DeviceNo, i], (uint)PropertyID.CFG_AxAlmEnable, ref Data, 8);

                    Data = (uint)AlarmLogic.ALM_ACT_LOW;//HIGH;
                    Motion.mAcm_SetProperty(p_AxisHandle[DeviceNo, i], (uint)PropertyID.CFG_AxAlmLogic, ref Data, 8);

                    Data = (uint)InPositionEnable.INP_DIS;//INP_EN
                    Motion.mAcm_SetProperty(p_AxisHandle[DeviceNo, i], (uint)PropertyID.CFG_AxInpEnable, ref Data, 8);

                    Data = (uint)InPositionLogic.NOT_SUPPORT;
                    Motion.mAcm_SetProperty(p_AxisHandle[DeviceNo, i], (uint)PropertyID.CFG_AxInpLogic, ref Data, 8);

                    Data = (uint)LatchEnable.NOT_SUPPORT;
                    Motion.mAcm_SetProperty(p_AxisHandle[DeviceNo, i], (uint)PropertyID.CFG_AxLatchEnable, ref Data, 4);

                    Data = (uint)LatchLogic.LATCH_ACT_LOW;
                    Motion.mAcm_SetProperty(p_AxisHandle[DeviceNo, i], (uint)PropertyID.CFG_AxLatchLogic, ref Data, 4);

                    Motion.mAcm_AxResetError(p_AxisHandle[DeviceNo, i]);
                }
                catch { throw; }
            }
        }
        public int AxisCount(byte DeviceNo)
        {
            return (int)AxesPerDev[DeviceNo];
        }

        public void LoadConfig(string FullFilename)
        {
            ES.Net.IniFile Inifile = new ES.Net.IniFile();

            Inifile.Create(FullFilename);
            PathDelay = Inifile.ReadInteger("P1245", "PathDelay", 2);
        }
        public void SaveConfig(string FullFilename)
        {
            ES.Net.IniFile Inifile = new ES.Net.IniFile();

            Inifile.Create(FullFilename);
            Inifile.WriteInteger("P1245", "PathDelay", PathDelay);
        }

        private uint MaxGroup(TDevice Device)
        {
            switch (Device.Type)
            {
                default:
                case EDeviceType.P1245:
                    return 2;
                case EDeviceType.P1265:
                    return 3;
                case EDeviceType.P1285:
                    return 4;
            }
        }
        #endregion

        //default
        //EMO logic act high
        //MtrAlm disabled
        //MtrAlm logic act low
        //InPos disabled
        //SwLmt disabled
        //SwLmt react force stop
        //HWLmt disabled
        //HWLmt logic act high
        //HWLmt react force stop

        #region Configuration
        public int PathDelay = 0;

        public void EmgLogicActHigh(TDevice Device, bool LogicHigh)//default ActHigh
        {
            uint EmgLogic = 1;//Logic Act High
            if (!LogicHigh) EmgLogic = 0;
            Motion.mAcm_SetProperty(p_DeviceHandle[Device.ID], (uint)PropertyID.CFG_DevEmgLogic, ref EmgLogic, 4);
        }

        public void MotorAlarmEnabled(TAxis Axis, out bool Enable)
        {
            uint Data = 0;
            uint Length = 0;
            Motion.mAcm_GetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxAlmEnable, ref Data, ref Length);

            Enable = (Data == (uint)AlarmEnable.ALM_EN);
        }
        public void MotorAlarmLogicActHigh(TAxis Axis, bool LogicHigh)
        {
            uint Data = (uint)AlarmLogic.ALM_ACT_HIGH;
            if (!LogicHigh) Data = (uint)AlarmLogic.ALM_ACT_LOW;
            Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxAlmLogic, ref Data, 4);
        }
        public void MotorAlarmEnable(TAxis Axis, bool Enable)
        {
            uint Data = (uint)AlarmEnable.ALM_EN;
            if (!Enable) Data = (uint)AlarmEnable.ALM_DIS;
            try
            {
                uint Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxAlmEnable, ref Data, 4);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    //Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch { throw; }
        }

        public void SoftwareLimitEnable(TAxis Axis, bool Enable)//default Disabled
        {
            try
            {
                uint Data = (uint)SwLmtEnable.SLMT_EN;
                if (!Enable) Data = (uint)SwLmtEnable.SLMT_DIS;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwPelEnable, ref Data, 4);
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwMelEnable, ref Data, 4);
            }
            catch { throw; }
        }
        public void SoftwareLimitReactForceStop(TAxis Axis, bool ForceStop)//default ForceStop
        {
            uint Data = (uint)SwLmtReact.SLMT_IMMED_STOP;
            if (!ForceStop) Data = (uint)SwLmtReact.SLMT_DEC_TO_STOP;
            Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwPelReact, ref Data, 4);
            Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwMelReact, ref Data, 4);
        }

        public void HardwareLimitEnable(TAxis Axis, bool Enable)//default Disabled
        {
            try
            {
                uint Data = (uint)HLmtEnable.HLMT_EN;
                if (!Enable) Data = (uint)HLmtEnable.HLMT_DIS;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxElEnable, ref Data, 4);
            }
            catch { throw; }
        }
        public void HardwareLimitLogicActHigh(TAxis Axis, bool LogicHigh)//default ActHigh
        {
            uint Data = (uint)HLmtLogic.HLMT_ACT_HIGH;
            if (!LogicHigh) Data = (uint)HLmtLogic.HLMT_ACT_LOW;
            Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxElLogic, ref Data, 4);
        }
        public void HardwareLimitReactForceStop(TAxis Axis, bool ForceStop)//default ForceStop
        {
            uint Data = (uint)HLmtReact.HLMT_IMMED_STOP;
            if (!ForceStop) Data = (uint)HLmtReact.HLMT_DEC_TO_STOP;
            Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxElReact, ref Data, 4);
        }

        public void CfgLatchEnable(TAxis Axis, bool Enable)//default disabled
        {
            try
            {
                uint Data = (uint)LatchEnable.LATCH_EN;
                if (!Enable) Data = (uint)LatchEnable.LATCH_DIS;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxLatchEnable, ref Data, 4);
            }
            catch { throw; }
        }
        public void CfgLatchLogic(TAxis Axis, bool HighAct)//default LowAct
        {
            try
            {
                uint Data = (uint)LatchLogic.LATCH_ACT_HIGH;
                if (!HighAct) Data = (uint)LatchLogic.LATCH_ACT_LOW;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxLatchLogic, ref Data, 4);
            }
            catch { throw; }
        }

        public void CfgCmpEnable(TAxis Axis, bool Enable)//default disabled
        {
            try
            {
                //uint Data = (uint)GenDoEnable.GEN_DO_DIS;
                ////if (!Enable) Data = (uint)GenDoEnable.GEN_DO_EN;
                //Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxGenDoEnable, ref Data, 4);


                uint Data = (uint)CmpEnable.CMP_EN;
                if (!Enable) Data = (uint)CmpEnable.CMP_DIS;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxCmpEnable, ref Data, 4);
            }
            catch { throw; }
        }
        public void CfgCmpLogic(TAxis Axis, bool HighAct)//default LowAct
        {
            try
            {
                uint Data = (uint)CmpPulseLogic.CP_ACT_HIGH;
                if (!HighAct) Data = (uint)CmpPulseLogic.CP_ACT_LOW;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxCmpPulseLogic, ref Data, 4);
            }
            catch { throw; }
        }
        /// <summary>
        /// Set Compare Pulse Width
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="HighAct">Width (5~1000 us)</param>
        public void CfgCmpSetPulseWidth(TAxis Axis, int Value)//default 5us
        {
            try
            {
                List<int> list = new List<int> { 5, 10, 20, 50, 100, 200, 500, 1000 };
                int Closest = list.OrderBy(item => Math.Abs(Value - item)).First();

                uint Data = 100;
                //switch (Closest)
                //{
                //    case 5:
                //    default:
                //        Data = 0; break;
                //    case 10:
                //        Data = 1; break;
                //    case 20:
                //        Data = 2; break;
                //    case 50:
                //        Data = 3; break;
                //    case 100:
                //        Data = 4; break;
                //    case 200:
                //        Data = 5; break;
                //    case 500:
                //        Data = 6; break;
                //    case 1000:
                //        Data = 7; break;
                //}
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxCmpPulseWidth, ref Data, 4);
            }
            catch { throw; }
        }
        public enum ECmpSource { Command, Actual }
        public void CfgCmpSource(TAxis Axis, ECmpSource Source)
        {
            try
            {
                uint Data = (uint)Source;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxCmpSrc, ref Data, 4);
            }
            catch
            {
                throw;
            }
        }
        public enum ECmpMethod { EqualOrMore, EqualOrLess }
        public void CfgCmpMethod(TAxis Axis, ECmpMethod Method)
        {
            try
            {
                uint Data = (uint)Method;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxCmpMethod, ref Data, 4);
            }
            catch
            {
                throw;
            }
        }
        public enum ECmpPulseMode { Pulse, Toggle }
        public void CfgCmpPulseMode(TAxis Axis, ECmpPulseMode Mode)
        {
            try
            {
                uint Data = (uint)Mode;
                Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxCmpPulseMode, ref Data, 4);
            }
            catch
            {
                throw;
            }
        }

        #endregion

        Mutex Mutex = new Mutex();

        #region Para
        public void SetUPP(TAxis Axis, double Value)
        {
            try
            {
                uint PPU = (uint)(1 / Value);

                uint BufLen = 4;
                uint Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxPPU, ref PPU, BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                UPP[Axis.Device.ID, Axis.Mask] = Value;
            }
            catch { throw; }
        }
        public void SetInvertPulse(TAxis Axis, bool Invert)
        {
            InvertPulse[Axis.Device.ID, Axis.Mask] = Invert;
        }
        #endregion

        #region Motion Para
        public void GetSpeedMinMax(TAxis Axis, ref double Min, ref double Max)
        {
            try
            {
                uint BufLen = 8;
                double MaxSpeed = 0;
                uint Res = Motion.mAcm_GetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.FT_AxMaxVel, ref MaxSpeed, ref BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                Min = 1 * UPP[Axis.Device.ID, Axis.Mask];
                Max = MaxSpeed * UPP[Axis.Device.ID, Axis.Mask];
            }
            catch { throw; }
        }
        public void GetAccelMinMax(TAxis Axis, ref double Min, ref double Max)
        {
            try
            {
                uint BufLen = 8;
                double MaxAcc = 0;
                uint Res = Motion.mAcm_GetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.FT_AxMaxAcc, ref MaxAcc, ref BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                Min = 1 * UPP[Axis.Device.ID, Axis.Mask];
                Max = MaxAcc * UPP[Axis.Device.ID, Axis.Mask];
            }
            catch { throw; }
        }
        public void GetDecelMinMax(TAxis Axis, ref double Min, ref double Max)
        {
            try
            {
                uint BufLen = 8;
                double MaxDec = 0;
                uint Res = Motion.mAcm_GetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.FT_AxMaxDec, ref MaxDec, ref BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                Min = 1 * UPP[Axis.Device.ID, Axis.Mask];
                Max = MaxDec * UPP[Axis.Device.ID, Axis.Mask];
            }
            catch { throw; }
        }
        public void GetJerkMinMax(TAxis Axis, ref double Min, ref double Max)
        {
            try
            {
                uint BufLen = 0;
                double MaxJerk = 0;
                uint Res = Motion.mAcm_GetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.FT_AxMaxJerk, ref MaxJerk, ref BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                Min = 0;
                Max = MaxJerk;
            }
            catch { throw; }
        }

        public void SetStartV(TAxis Axis, double Value)
        {
            try
            {
                uint BufLen = 8;
                uint Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.PAR_AxVelLow, ref Value, BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                StartV[Axis.Device.ID, Axis.Mask] = Value;
                Axis.Para.Psnt.StartV = Value;
            }
            catch { throw; }
        }
        public void SetStartV(TAxis Axis, TAxis Axis2, double Value)
        {
            uint Res = 0;
            try
            {
                #region Set Group
                for (int i = 0; i < MaxGroup(Axis.Device); i++)
                {
                    if (p_GroupHandle[Axis.Device.ID, i] == (IntPtr)0) continue;

                    uint AxisInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint AxisMask = 0x001;
                    AxisMask = AxisMask << Axis.Mask;

                    if ((AxisInGroup & AxisMask) == AxisMask)
                    {
                        Res = Motion.mAcm_SetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.PAR_GpVelLow, ref Value, 8);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                    }
                }
                #endregion
            }
            catch { throw; }
        }

        public void SetDriveV(TAxis Axis, double Value)//StartV is suppressed
        {
            try
            {
                if (Value < StartV[Axis.Device.ID, Axis.Mask]) SetStartV(Axis, Value);

                uint BufLen = 8;
                uint Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.PAR_AxVelHigh, ref Value, BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                DriveV[Axis.Device.ID, Axis.Mask] = Value;
                Axis.Para.Psnt.DriveV = Value;

            }
            catch { throw; }
        }
        public void SetDriveV(TAxis Axis, TAxis Axis2, double Value)
        {
            uint Res = 0;
            try
            {
                #region Set Group
                for (int i = 0; i < MaxGroup(Axis.Device); i++)
                {
                    if (p_GroupHandle[Axis.Device.ID, i] == (IntPtr)0) continue;

                    uint AxisInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint AxisMask = 0x001;
                    AxisMask = AxisMask << Axis.Mask;

                    if ((AxisInGroup & AxisMask) == AxisMask)
                    {
                        Res = Motion.mAcm_SetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.PAR_GpVelHigh, ref Value, 8);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                    }
                }
                #endregion
            }
            catch { throw; }
        }

        public void SetAccel(TAxis Axis, double Value)
        {
            uint Res = 0;
            try
            {
                uint BufLen = 8;
                Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.PAR_AxAcc, ref Value, BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                Accel[Axis.Device.ID, Axis.Mask] = Value;
                Axis.Para.Psnt.Accel = Value;

                SetDecel(Axis, Value);
            }
            catch { throw; }
        }
        public void SetAccel(TAxis Axis, TAxis Axis2, double Value)
        {
            uint Res = 0;
            try
            {
                for (int i = 0; i < MaxGroup(Axis.Device); i++)
                {
                    if (p_GroupHandle[Axis.Device.ID, i] == (IntPtr)0) continue;

                    uint AxisInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint AxisMask = 0x001;
                    AxisMask = AxisMask << Axis.Mask;

                    if ((AxisInGroup & AxisMask) == AxisMask)
                    {
                        Res = Motion.mAcm_SetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.PAR_GpAcc, ref Value, 8);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                    }
                }

                SetDecel(Axis, Axis2, Value);
            }
            catch { throw; }
        }

        public void SetDecel(TAxis Axis, double Value)
        {
            try
            {
                uint BufLen = 8;
                uint Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.PAR_AxDec, ref Value, BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                Decel[Axis.Device.ID, Axis.Mask] = Value;
                Axis.Para.Psnt.Decel = Value;
            }
            catch { throw; }
        }
        public void SetDecel(TAxis Axis, TAxis Axis2, double Value)
        {
            uint Res = 0;
            try
            {
                #region Set Group
                for (int i = 0; i < MaxGroup(Axis.Device); i++)
                {
                    if (p_GroupHandle[Axis.Device.ID, i] == (IntPtr)0) continue;

                    uint AxisInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint AxisMask = 0x001;
                    AxisMask = AxisMask << Axis.Mask;

                    if ((AxisInGroup & AxisMask) == AxisMask)
                    {
                        Res = Motion.mAcm_SetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.PAR_GpDec, ref Value, 8);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                    }
                }
                #endregion
            }
            catch { throw; }
        }

        public void SetJerk(TAxis Axis, double Value)
        {
            try
            {
                uint BufLen = 8;
                uint Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.PAR_AxJerk, ref Value, BufLen);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                Jerk[Axis.Device.ID, Axis.Mask] = Value;
            }
            catch { throw; }
        }
        public void SetJerk(TAxis Axis, TAxis Axis2, double Value)
        {
            uint Res = 0;
            try
            {
                for (int i = 0; i < MaxGroup(Axis.Device); i++)
                {
                    if (p_GroupHandle[Axis.Device.ID, i] == (IntPtr)0) continue;

                    uint AxisInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint AxisMask = 0x001;
                    AxisMask = AxisMask << Axis.Mask;

                    if ((AxisInGroup & AxisMask) == AxisMask)
                    {
                        Res = Motion.mAcm_SetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.PAR_GpJerk, ref Value, 8);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                    }
                }
            }
            catch { throw; }
        }

        private void SetSLmt(TAxis Axis, bool Positive, double Value)
        {
            try
            {
                int PValue = (int)(Value / UPP[Axis.Device.ID, Axis.Mask]);

                uint BufLen = 4;
                if (Axis.Device.EType)
                {
                    BufLen = 8;
                }
                else
                {
                    BufLen = 4;
                }
                uint Res = 0;
                if (Positive)
                {
                    if (!InvertPulse[Axis.Device.ID, Axis.Mask])
                    {
                        Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwPelValue, ref PValue, BufLen);
                    }
                    else
                    {
                        PValue = -PValue;
                        Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwMelValue, ref PValue, BufLen);
                    }
                }
                else
                {
                    if (!InvertPulse[Axis.Device.ID, Axis.Mask])
                    {
                        Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwMelValue, ref PValue, BufLen);
                    }
                    else
                    {
                        PValue = -PValue;
                        Res = Motion.mAcm_SetProperty(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)PropertyID.CFG_AxSwPelValue, ref PValue, BufLen);
                    }
                }
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception("SetSLMT " + GetErrorMessage(Res));
                }
            }
            catch { throw; }
        }
        public void SetSLmtP(TAxis Axis, double Value)
        {
            try
            {
                SetSLmt(Axis, true, Value);//InvertPulse[Axis.Device.ID, Axis.Mask], Value);
            }
            catch { throw; }
        }
        public void SetSLmtN(TAxis Axis, double Value)
        {
            try
            {
                SetSLmt(Axis, false, Value);//InvertPulse[Axis.Device.ID, Axis.Mask], Value);
            }
            catch { throw; }
        }
        #endregion

        #region Motion
        //const uint EVT_AX_MOTION_DONE = 0x0001;
        //const uint EVT_AX_COMPARED = 0x0002;
        //const uint EVT_AX_LATCHED = 0x0004;
        //const uint EVT_AX_ERROR = 0x0008;
        //const uint EVT_VH_START = 0x0010;
        //const uint EVT_VH_END = 0x0020;

        //public void EnableMotionEvent(byte DeviceID)
        //{
        //    UInt32[] AxEvtEnableArray = new UInt32[32];
        //    UInt32[] GpEvtEnableArray = new UInt32[32];

        //    AxEvtEnableArray[0] = EVT_AX_MOTION_DONE | EVT_AX_ERROR;
        //    AxEvtEnableArray[1] = EVT_AX_MOTION_DONE | EVT_AX_ERROR;
        //    AxEvtEnableArray[2] = EVT_AX_MOTION_DONE | EVT_AX_ERROR;
        //    AxEvtEnableArray[3] = EVT_AX_MOTION_DONE | EVT_AX_ERROR;
        //    GpEvtEnableArray[0] = EVT_AX_MOTION_DONE | EVT_AX_ERROR;

        //    try
        //    {
        //        uint Res = Motion.mAcm_EnableMotionEvent(p_DeviceHandle[DeviceID], AxEvtEnableArray, GpEvtEnableArray, 4, 3);
        //        if (Res != (int)ErrorCode.SUCCESS)
        //        {
        //            throw new Exception(GetErrorMessage(Res));
        //        }
        //    }
        //    catch { throw; }
        //}

        //public bool MotionReady(TAxis Axis)
        //{
        //    //if (_AxisReady[Axis.Mask])
        //    //{
        //    //    _AxisReady[Axis.Mask] = false;
        //    //    return true;
        //    //}
        //    UInt32[] AxEvtStatusArray = new UInt32[32];
        //    UInt32[] GpEvtStatusArray = new UInt32[32];

        //    try
        //    {
        //        uint Res = Motion.mAcm_CheckMotionEvent(p_DeviceHandle[Axis.Device.ID], AxEvtStatusArray, GpEvtStatusArray, 4, 3, 10);
        //        if (Res != (int)ErrorCode.SUCCESS)
        //        {
        //            throw new Exception(GetErrorMessage(Res));
        //        }

        //        //for (int i = 0; i < 2; i++)
        //        //{
        //        //    //if ((GroupAxis[i] & (int)Math.Pow(2, Axis.Mask)) > 0)
        //        //    //{
        //        //    //    //return ((GpEvtStatusArray[i] & 0x1) > 0);
        //        //    //    if ((GpEvtStatusArray[0] & (i + 1)) > 0)
        //        //    //    {
        //        //    //        for (int j = 0; j < 4; j++)
        //        //    //        {
        //        //    //            if ((GroupAxis[i] & (int)Math.Pow(2, j)) == (int)Math.Pow(2, j))
        //        //    //            {
        //        //    //                _AxisReady[j] = true;
        //        //    //            }
        //        //    //        }
        //        //    //        _AxisReady[Axis.Mask] = false;
        //        //    //        return true;
        //        //    //    }
        //        //    //}
        //        //}        
        //        return ((AxEvtStatusArray[Axis.Mask] & EVT_AX_MOTION_DONE) > 0);
        //    }
        //    catch { throw; }
        //}
        //public bool MotionBusy(TAxis Axis)
        //{
        //    try
        //    {
        //        return !MotionReady(Axis);
        //    }
        //    catch { throw; }
        //}
        //public void MotionWait(TAxis Axis)
        //{
        //    try
        //    {
        //        while (!MotionReady(Axis))
        //        {
        //            Application.DoEvents();
        //        }
        //    }
        //    catch { throw; }
        //}

        private AxisState AxisState(TAxis Axis)
        {
            try
            {
                Mutex.WaitOne();
                UInt16 State = 0;
                uint Res = Motion.mAcm_AxGetState(p_AxisHandle[Axis.Device.ID, Axis.Mask], ref State);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                Mutex.ReleaseMutex();
                return (Advantech.Motion.AxisState)State;
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
        }
        public bool AxisReady(TAxis Axis)
        {
            try
            {
                Advantech.Motion.AxisState State = AxisState(Axis);
                return (State == Advantech.Motion.AxisState.STA_AX_READY);
            }
            catch { throw; };
        }
        public bool AxisBusy(TAxis Axis)
        {
            try
            {
                Advantech.Motion.AxisState State = AxisState(Axis);
                switch (State)
                {
                    case Advantech.Motion.AxisState.STA_AX_DISABLE:
                    case Advantech.Motion.AxisState.STA_AX_READY:
                    case Advantech.Motion.AxisState.STA_AX_ERROR_STOP:
                        return false;
                    case Advantech.Motion.AxisState.STA_AX_BUSY:
                    case Advantech.Motion.AxisState.STA_AX_CONTI_MOT:
                    case Advantech.Motion.AxisState.STA_AX_EXT_JOG:
                    case Advantech.Motion.AxisState.STA_AX_EXT_MPG:
                    case Advantech.Motion.AxisState.STA_AX_HOMING:
                    case Advantech.Motion.AxisState.STA_AX_PAUSE:
                    case Advantech.Motion.AxisState.STA_AX_PTP_MOT:
                    case Advantech.Motion.AxisState.STA_AX_STOPPING:
                    case Advantech.Motion.AxisState.STA_AX_SYNC_MOT:
                    default:
                        return true;
                }
            }
            catch
            {
                throw;
            };
        }
        public bool AxisError(TAxis Axis)
        {
            try
            {
                Advantech.Motion.AxisState State = AxisState(Axis);
                return (State == Advantech.Motion.AxisState.STA_AX_ERROR_STOP);
            }
            catch { throw; };
        }
        public void ClearAxisError(TAxis Axis)
        {
            uint Res = 0;
            try
            {
                int MaxGroup = 2;
                if (Axis.Device.Type == EDeviceType.P1265) MaxGroup = 3;
                if (Axis.Device.Type == EDeviceType.P1285) MaxGroup = 4;

                for (int i = 0; i < MaxGroup; i++)
                {
                    if (p_GroupHandle[Axis.Device.ID, i] == (IntPtr)0) continue;

                    uint Data = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref Data, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint AxisMask = 0;
                    if (Axis.Mask == 1) AxisMask = 0x0001;
                    if (Axis.Mask == 2) AxisMask = 0x0002;
                    if (Axis.Mask == 3) AxisMask = 0x0004;
                    if (Axis.Mask == 4) AxisMask = 0x0008;
                    if (Axis.Mask == 5) AxisMask = 0x0010;
                    if (Axis.Mask == 6) AxisMask = 0x0020;

                    if ((Data & AxisMask) == AxisMask)
                    {
                        Res = Motion.mAcm_GpResetError(p_GroupHandle[Axis.Device.ID, i]);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                    }
                }

                Res = Motion.mAcm_AxResetError(p_AxisHandle[Axis.Device.ID, Axis.Mask]);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch
            {
                throw;
            }
        }

        private GroupState GroupState(TAxis MasterAxis)
        {
            uint Res = 0;
            try
            {
                Mutex.WaitOne();
                UInt16 State = 0;
                for (int i = 0; i < MaxGroup(MasterAxis.Device); i++)
                {

                    if (p_GroupHandle[MasterAxis.Device.ID, i] != (IntPtr)0)
                    {
                        UInt32 AxesInGroup = 0;
                        uint BufLen = 4;
                        Res = Motion.mAcm_GetProperty(p_GroupHandle[MasterAxis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInGroup, ref BufLen);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }

                        uint Mask = 0x0001;
                        Mask = Mask << MasterAxis.Mask;

                        if ((AxesInGroup & Mask) == Mask)
                        {
                            Res = Motion.mAcm_GpGetState(p_GroupHandle[MasterAxis.Device.ID, i], ref State);
                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }
                            break;
                        }
                    }
                }
                Mutex.ReleaseMutex();
                return (Advantech.Motion.GroupState)State;
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
        }
        public bool AxisBusy(TAxis Axis, TAxis Axis2)
        {
            try
            {
                Advantech.Motion.GroupState State = GroupState(Axis);
                switch (State)
                {
                    case Advantech.Motion.GroupState.STA_GP_AX_MOTION:
                    case Advantech.Motion.GroupState.STA_GP_BUSY:
                    case Advantech.Motion.GroupState.STA_Gp_Motion:
                    case Advantech.Motion.GroupState.STA_GP_Pathing:
                    case Advantech.Motion.GroupState.STA_GP_PAUSE:
                    case Advantech.Motion.GroupState.STA_Gp_Stopping:
                        return true;
                    case Advantech.Motion.GroupState.STA_Gp_ErrorStop:
                    case Advantech.Motion.GroupState.STA_Gp_Disable:
                    case Advantech.Motion.GroupState.STA_Gp_Ready:
                    default:
                        return false;
                }
            }
            catch { throw; };

            uint Res = 0;
            try
            {
                Mutex.WaitOne();

                //Advantech.Motion.GroupState State = 0;
                //= GroupState.STA_Gp_Disable;

                //UInt16 State = 0;
                //Thread.Sleep(1);

                for (int i = 0; i < MaxGroup(Axis.Device); i++)
                {
                    //if (i == MaxGroup(Axis.Device) + 1)
                    //{
                    //    throw new Exception("No Path Created.");
                    //}

                    if (p_GroupHandle[Axis.Device.ID, i] != (IntPtr)0)
                    {
                        UInt32 AxesInGroup = 0;
                        uint BufLen = 4;
                        Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInGroup, ref BufLen);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }

                        uint Mask = 0x0001;
                        Mask = Mask << Axis.Mask;

                        if ((AxesInGroup & Mask) == Mask)
                        {
                            //Res = Motion.mAcm_GpMovePath(p_GroupHandle[MasterAxis.Device.ID, i], IntPtr.Zero);
                            //if (Res != (int)ErrorCode.SUCCESS)
                            //{
                            //    throw new Exception(GetErrorMessage(Res));
                            //}
                            //break;
                            UInt16 State = 0;
                            Res = Motion.mAcm_GpGetState(p_GroupHandle[Axis.Device.ID, i], ref State);
                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }
                            switch ((Advantech.Motion.GroupState)State)
                            {
                                case Advantech.Motion.GroupState.STA_Gp_Disable:
                                    Mutex.ReleaseMutex();
                                    return false;
                                case Advantech.Motion.GroupState.STA_Gp_Ready:
                                    Mutex.ReleaseMutex();
                                    return false;
                                case Advantech.Motion.GroupState.STA_Gp_ErrorStop:
                                    Mutex.ReleaseMutex();
                                    return false;
                                default:
                                    Mutex.ReleaseMutex();
                                    return true;
                            }
                        }
                    }
                }
                Mutex.ReleaseMutex();
                return false;
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }

        }

        private void Stop(TAxis Axis, bool Emg)
        {
            uint Res = 0;
            try
            {
                UInt16 State = 0;
                Res = Motion.mAcm_AxGetState(p_AxisHandle[Axis.Device.ID, Axis.Mask], ref State);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                if (State == (uint)Advantech.Motion.AxisState.STA_AX_SYNC_MOT)
                {
                    int MaxGroup = 2;
                    if (Axis.Device.Type == EDeviceType.P1265) MaxGroup = 3;
                    if (Axis.Device.Type == EDeviceType.P1285) MaxGroup = 4;

                    for (int i = 0; i < MaxGroup; i++)
                    {
                        if (p_GroupHandle[Axis.Device.ID, i] == (IntPtr)0) continue;

                        uint Data = 0;
                        uint BufLen = 4;
                        Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref Data, ref BufLen);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }

                        uint AxisMask = 0;
                        if (Axis.Mask == 1) AxisMask = 0x0001;
                        if (Axis.Mask == 2) AxisMask = 0x0002;
                        if (Axis.Mask == 3) AxisMask = 0x0004;
                        if (Axis.Mask == 4) AxisMask = 0x0008;
                        if (Axis.Mask == 5) AxisMask = 0x0010;
                        if (Axis.Mask == 6) AxisMask = 0x0020;

                        if ((Data & AxisMask) == AxisMask)
                        {
                            if (Emg)
                                Res = Motion.mAcm_GpStopEmg(p_GroupHandle[Axis.Device.ID, i]);
                            else
                                Res = Motion.mAcm_GpStopDec(p_GroupHandle[Axis.Device.ID, i]);

                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }
                            break;
                        }
                    }
                }
                else
                {
                    if (Emg)
                        Res = Motion.mAcm_AxStopEmg(p_AxisHandle[Axis.Device.ID, Axis.Mask]);
                    else
                        Res = Motion.mAcm_AxStopDec(p_AxisHandle[Axis.Device.ID, Axis.Mask]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public void DecelStop(TAxis Axis)
        {
            try
            {
                Stop(Axis, false);
            }
            catch
            { throw; }
        }
        public void ForceStop(TAxis Axis)
        {
            try
            {
                Stop(Axis, true);
            }
            catch
            { throw; }
        }

        private void Jop(TAxis Axis, bool Positive)
        {
            if (AxisBusy(Axis)) new Exception("Axis Busy.");

            try
            {
                ushort Dir = 0;

                if (Positive & !InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Dir = 0;
                }
                if (!Positive & !InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Dir = 1;
                }
                if (Positive & InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Dir = 1;
                }
                if (!Positive & InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Dir = 0;
                }

                Mutex.WaitOne();
                uint Res = Motion.mAcm_AxMoveVel(p_AxisHandle[Axis.Device.ID, Axis.Mask], Dir);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
                Mutex.ReleaseMutex();
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
        }
        public void JogP(TAxis Axis)
        {
            try
            {
                Jop(Axis, true);
            }
            catch { throw; }
        }
        public void JogN(TAxis Axis)
        {
            try
            {
                Jop(Axis, false);
            }
            catch { throw; }
        }

        enum EMoveType { Rel, Abs };
        private void MovePtp(TAxis Axis, EMoveType MoveType, double Value)
        {
            if (AxisBusy(Axis))
            {
                new Exception("Axis Busy.");
            }
            //_Retry:
            //            try
            //            {
            //                Advantech.Motion.AxisState State = AxisState(Axis);

            //                while (AxisState(Axis) != Advantech.Motion.AxisState.STA_AX_READY)
            //                {
            //                    Thread.Sleep(0);
            ////                    Application.DoEvents();
            //                }
            //            }
            //            catch { throw; };


            //try
            //{
            //    Advantech.Motion.AxisState State = AxisState(Axis);

            //_Retry:
            //    //State = AxisState(Axis);
            //    //if (State != Advantech.Motion.AxisState.STA_AX_READY) goto _Retry;
            //    switch (State)
            //    {
            //        case Advantech.Motion.AxisState.STA_AX_DISABLE:
            //            throw new Exception("Axis Disabled.");
            //            //break;
            //        case Advantech.Motion.AxisState.STA_AX_READY:
            //            break;
            //        case Advantech.Motion.AxisState.STA_AX_ERROR_STOP:
            //            throw new Exception("Axis Error.");
            //            //break;
            //        //case (Advantech.Motion.AxisState)11:
            //        //    goto _Retry;
            //        //    break;
            //        default:
            //            //throw new Exception("Axis Busy." + State.ToString());
            //            //goto _Retry;    
            //        break;
            //    }
            //}
            //catch { throw; };

            try
            {
                uint Res = 0;

                Mutex.WaitOne();
                if (InvertPulse[Axis.Device.ID, Axis.Mask]) Value = -Value;
                if (MoveType == EMoveType.Abs)
                {
                    //double Val = 0;
                    //GetLCntr(Axis, ref Val);
                    //if (Value == Val)
                    //{
                    //    _AxisReady[Axis.Mask] = true;
                    //    Mutex.ReleaseMutex();
                    //    return;
                    //}
                    Res = Motion.mAcm_AxMoveAbs(p_AxisHandle[Axis.Device.ID, Axis.Mask], Value);
                    //if (Value == 0) _AxisReady[Axis.Mask] = false;
                }
                else
                {
                    //if (Value == 0)
                    //{
                    //    _AxisReady[Axis.Mask] = true;
                    //    Mutex.ReleaseMutex();
                    //    return;
                    //}
                    Res = Motion.mAcm_AxMoveRel(p_AxisHandle[Axis.Device.ID, Axis.Mask], Value);
                    //if (Value == 0) _AxisReady[Axis.Mask] = false;
                }
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    //Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
                //Mutex.ReleaseMutex();
                Thread.Sleep(PathDelay);
            }
            catch
            {
                //Mutex.ReleaseMutex();
                throw;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }
        }
        public void MovePtpRel(TAxis Axis, double Value)
        {
            try
            {
                MovePtp(Axis, EMoveType.Rel, Value);
            }
            catch { throw; }
        }
        public void MovePtpAbs(TAxis Axis, double Value)
        {
            try
            {
                MovePtp(Axis, EMoveType.Abs, Value);
            }
            catch
            {
                throw;
            }
        }

        private void MoveLine(TAxis Axis1, TAxis Axis2, EMoveType MoveType, double Value1, double Value2)
        {
            if (AxisBusy(Axis1) || AxisBusy(Axis2)) new Exception("Axis Busy.");

            if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) Value1 = -Value1;
            if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) Value2 = -Value2;

            Mutex.WaitOne();

            try
            {
                int MaxGroup = 2;
                if (Axis1.Device.Type == EDeviceType.P1265) MaxGroup = 3;
                if (Axis1.Device.Type == EDeviceType.P1285) MaxGroup = 4;

                uint Res = 0;
                //close all unused groups
                for (int i = 0; i < MaxGroup; i++)
                {
                    #region
                    UInt16 GpState = 0;
                    //int Retried = 0;
                    if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
                    {
                        Res = Motion.mAcm_GpGetState(p_GroupHandle[Axis1.Device.ID, i], ref GpState);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            Mutex.ReleaseMutex();
                            throw new Exception(GetErrorMessage(Res));
                        }

                        switch (GpState)
                        {
                            case (UInt16)Advantech.Motion.GroupState.STA_Gp_Disable:
                            case (UInt16)Advantech.Motion.GroupState.STA_Gp_Ready:
                                Res = Motion.mAcm_GpClose(ref p_GroupHandle[Axis1.Device.ID, i]);
                                if (Res != (int)ErrorCode.SUCCESS)
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception(GetErrorMessage(Res));
                                }
                                p_GroupHandle[Axis1.Device.ID, i] = (IntPtr)0;
                                break;
                            default:
                                uint AxisBit = 0;
                                uint DataLen = 4;
                                if (Axis1.Device.EType)
                                {
                                    DataLen = 8;
                                }
                                else
                                {
                                    DataLen = 4;
                                }
                                Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis1.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisBit, ref DataLen);
                                if (Res != (int)ErrorCode.SUCCESS)
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception(GetErrorMessage(Res));
                                }
                                if (((AxisBit & Axis1.Mask) == Axis1.Mask) && ((AxisBit & Axis2.Mask) == Axis2.Mask))
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception("Axis is busy");
                                }
                                break;
                        }
                    }
                    #endregion
                }

                //add to group
                for (int i = 0; i < MaxGroup + 1; i++)
                {
                    if (i == MaxGroup)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception("No avail group.");
                    }

                    //UInt16 GpState = 0;
                    if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
                    {
                        continue;
                    }

                    #region Add Axis1 and Axis2
                    Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis1.Device.ID, i], p_AxisHandle[Axis1.Device.ID, Axis1.Mask]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }

                    Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis2.Device.ID, i], p_AxisHandle[Axis2.Device.ID, Axis2.Mask]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                    //Thread.Sleep(2);
                    //GpState = 0;
                    //while (GpState != 1)
                    //{
                    //    Res = Motion.mAcm_GpGetState(p_GroupHandle[Axis1.Device.ID, i], ref GpState);
                    //    if (Res != (int)ErrorCode.SUCCESS)
                    //    {
                    //        Mutex.ReleaseMutex();
                    //        throw new Exception(GetErrorMessage(Res));
                    //    }
                    //}
                    #endregion

                    double[] PosArray = new double[2] { Value1, Value2 };
                    uint Elements = 2;

                    if (MoveType == EMoveType.Abs)
                    {
                        #region MoveAbs
                        Res = Motion.mAcm_GpMoveLinearAbs(p_GroupHandle[Axis1.Device.ID, i], PosArray, ref Elements);
                        #endregion
                    }
                    else
                    {
                        #region MoveRel
                        Res = Motion.mAcm_GpMoveLinearRel(p_GroupHandle[Axis1.Device.ID, i], PosArray, ref Elements);
                        #endregion
                    }
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                    break;
                }
                Mutex.ReleaseMutex();
                Thread.Sleep(PathDelay);
            }
            catch
            {
                throw;
            }
        }


        private void MoveLineSingleBoard(TAxis Axis1, TAxis Axis2, EMoveType MoveType, double Value1, double Value2)
        {
            if (AxisBusy(Axis1) || AxisBusy(Axis2)) new Exception("Axis Busy.");

            if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) Value1 = -Value1;
            if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) Value2 = -Value2;

            Mutex.WaitOne();

            try
            {
                int MaxGroup = 1;


                uint Res = 0;
                //close all unused groups
                for (int i = 0; i < MaxGroup; i++)
                {
                    #region
                    UInt16 GpState = 0;
                    //int Retried = 0;
                    if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
                    {
                        Res = Motion.mAcm_GpGetState(p_GroupHandle[Axis1.Device.ID, i], ref GpState);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            Mutex.ReleaseMutex();
                            throw new Exception(GetErrorMessage(Res));
                        }

                        switch (GpState)
                        {
                            case (UInt16)Advantech.Motion.GroupState.STA_Gp_Disable:
                                {

                                }
                                break;
                            case (UInt16)Advantech.Motion.GroupState.STA_Gp_Ready:
                                Res = Motion.mAcm_GpClose(ref p_GroupHandle[Axis1.Device.ID, i]);
                                if (Res != (int)ErrorCode.SUCCESS)
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception(GetErrorMessage(Res));
                                }
                                p_GroupHandle[Axis1.Device.ID, i] = (IntPtr)0;
                                break;
                            default:
                                uint AxisBit = 0;
                                uint DataLen = 4;
                                Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis1.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisBit, ref DataLen);
                                if (Res != (int)ErrorCode.SUCCESS)
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception(GetErrorMessage(Res));
                                }
                                if (((AxisBit & Axis1.Mask) == Axis1.Mask) && ((AxisBit & Axis2.Mask) == Axis2.Mask))
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception("Axis is busy");
                                }
                                break;
                        }
                    }
                    #endregion
                }

                //add to group
                for (int i = 0; i < MaxGroup; i++)
                {
                    //UInt16 GpState = 0;
                    if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
                    {
                        continue;
                    }

                    #region Add Axis1 and Axis2
                    Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis1.Device.ID, i], p_AxisHandle[Axis1.Device.ID, Axis1.Mask]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }

                    Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis2.Device.ID, i], p_AxisHandle[Axis2.Device.ID, Axis2.Mask]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }

                    #endregion

                    double[] PosArray = new double[2] { Value1, Value2 };
                    uint Elements = 2;

                    if (MoveType == EMoveType.Abs)
                    {
                        #region MoveAbs
                        Res = Motion.mAcm_GpMoveLinearAbs(p_GroupHandle[Axis1.Device.ID, i], PosArray, ref Elements);
                        #endregion
                    }
                    else
                    {
                        #region MoveRel
                        Res = Motion.mAcm_GpMoveLinearRel(p_GroupHandle[Axis1.Device.ID, i], PosArray, ref Elements);
                        #endregion
                    }
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }

                    //Close Group
                    //Res = Motion.mAcm_GpClose(ref p_GroupHandle[Axis1.Device.ID, i]);
                    //if (Res != (int)ErrorCode.SUCCESS)
                    //{
                    //    Mutex.ReleaseMutex();
                    //    throw new Exception(GetErrorMessage(Res));
                    //}

                    //p_GroupHandle[Axis1.Device.ID, i] = IntPtr.Zero;
                    break;
                }
                Mutex.ReleaseMutex();
                Thread.Sleep(PathDelay);
            }
            catch
            {
                throw;
            }
        }
        public void MoveLineRel(TAxis Axis1, TAxis Axis2, double Value1, double Value2)
        {
            try
            {
                MoveLineSingleBoard(Axis1, Axis2, EMoveType.Rel, Value1, Value2);
            }
            catch { throw; }
        }
        public void MoveLineAbs(TAxis Axis1, TAxis Axis2, double Value1, double Value2)
        {
            try
            {
                MoveLineSingleBoard(Axis1, Axis2, EMoveType.Abs, Value1, Value2);
            }
            catch { throw; }
        }

        public enum ECircDir { CW, CCW };
        public enum ECircType { CenterEnd, ThruEnd, CenterAngle }
        private void MoveArc(TAxis Axis1, TAxis Axis2, ECircType CircType, EMoveType MoveType, ECircDir Dir, double RefPt1, double RefPt2, double EndPt1, double EndPt2, ushort Angle_Deg)
        {
            if (AxisBusy(Axis1) || AxisBusy(Axis2)) new Exception("Axis Busy.");

            if (InvertPulse[Axis1.Device.ID, Axis1.Mask])
            {
                RefPt1 = -RefPt1;
                EndPt1 = -EndPt1;
            }
            if (InvertPulse[Axis2.Device.ID, Axis2.Mask])
            {
                RefPt2 = -RefPt2;
                EndPt2 = -EndPt2;
            }

            Mutex.WaitOne();

            #region
            //try
            //{
            //    int MaxGroup = 2;
            //    if (Axis1.Device.Type == EDeviceType.P1265) MaxGroup = 3;
            //    if (Axis1.Device.Type == EDeviceType.P1285) MaxGroup = 4;

            //    uint Res = 0;

            //    for (int i = 0; i < MaxGroup + 1; i++)
            //    {
            //        if (i == MaxGroup)
            //        {
            //            Mutex.ReleaseMutex();
            //            throw new Exception("No avail group.");
            //        }

            //        UInt16 GpState = 0;
            //        int Retried = 0;
            //        if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
            //        {
            //        _Retry:
            //            Res = Motion.mAcm_GpGetState(p_GroupHandle[Axis1.Device.ID, i], ref GpState);
            //            if (Res != (int)ErrorCode.SUCCESS)
            //            {
            //                Mutex.ReleaseMutex();
            //                throw new Exception(GetErrorMessage(Res));
            //            }

            //            switch (GpState)
            //            {
            //                case (UInt16)(UInt16)Advantech.Motion.GroupState.STA_Gp_Disable:
            //                case (UInt16)(UInt16)Advantech.Motion.GroupState.STA_Gp_Ready:
            //                //case (UInt16)(UInt16)Advantech.Motion.GroupState.STA_Gp_ErrorStop:
            //                    Res = Motion.mAcm_GpClose(ref p_GroupHandle[Axis1.Device.ID, i]);
            //                    if (Res != (int)ErrorCode.SUCCESS)
            //                    {
            //                        Mutex.ReleaseMutex();
            //                        throw new Exception(GetErrorMessage(Res));
            //                    }
            //                    break;
            //                default:
            //                    //Continue;
            //                    Thread.Sleep(1);
            //                    if (Retried < 3)
            //                    {
            //                        Retried++;
            //                        goto _Retry;
            //                    }
            //                    else
            //                        continue;

            //                    //{
            //                    //    Mutex.ReleaseMutex();
            //                    //    throw new Exception("Group Status Error.");
            //                    //}
            //            }
            //        }

            //        #region Add Axis1 and Axis2
            //            Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis1.Device.ID, i], p_AxisHandle[Axis1.Device.ID, Axis1.Mask]);
            //            if (Res != (int)ErrorCode.SUCCESS)
            //            {
            //                Mutex.ReleaseMutex();
            //                throw new Exception(GetErrorMessage(Res));
            //            }

            //            Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis2.Device.ID, i], p_AxisHandle[Axis2.Device.ID, Axis2.Mask]);
            //            if (Res != (int)ErrorCode.SUCCESS)
            //            {
            //                Mutex.ReleaseMutex();
            //                throw new Exception(GetErrorMessage(Res));
            //            }
            //            #endregion

            //            double[] CenterArray = new double[2] { CenterPt1, CenterPt2 };
            //            double[] EndArray = new double[2] { EndPt1, EndPt2 };
            //            uint AxisNum = 2;
            //            if (MoveType == EMoveType.Abs)
            //                Res = Motion.mAcm_GpMoveCircularAbs(p_GroupHandle[Axis1.Device.ID, i], CenterArray, EndArray, ref AxisNum, (short)Dir);
            //            else
            //                Res = Motion.mAcm_GpMoveCircularRel(p_GroupHandle[Axis1.Device.ID, i], CenterArray, EndArray, ref AxisNum, (short)Dir);
            //            if (Res != (int)ErrorCode.SUCCESS)
            //            {
            //                Mutex.ReleaseMutex();
            //                throw new Exception(GetErrorMessage(Res));
            //            }
            //            break;
            //    }
            //    Mutex.ReleaseMutex();
            //    Thread.Sleep(PathDelay);
            //}
            //catch
            //{
            //    Mutex.ReleaseMutex();
            //    throw;
            //}
            #endregion

            try
            {
                int MaxGroup = 2;
                if (Axis1.Device.Type == EDeviceType.P1265) MaxGroup = 3;
                if (Axis1.Device.Type == EDeviceType.P1285) MaxGroup = 4;

                uint Res = 0;

                //close all unused groups
                for (int i = 0; i < MaxGroup; i++)
                {
                    #region
                    UInt16 GpState = 0;
                    if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
                    {
                        Res = Motion.mAcm_GpGetState(p_GroupHandle[Axis1.Device.ID, i], ref GpState);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            Mutex.ReleaseMutex();
                            throw new Exception(GetErrorMessage(Res));
                        }

                        switch (GpState)
                        {
                            case (UInt16)(UInt16)Advantech.Motion.GroupState.STA_Gp_Disable:
                            case (UInt16)(UInt16)Advantech.Motion.GroupState.STA_Gp_Ready:
                                Res = Motion.mAcm_GpClose(ref p_GroupHandle[Axis1.Device.ID, i]);
                                if (Res != (int)ErrorCode.SUCCESS)
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception(GetErrorMessage(Res));
                                }
                                p_GroupHandle[Axis1.Device.ID, i] = (IntPtr)0;
                                break;
                            default:
                                uint AxisBit = 0;
                                uint DataLen = 4;
                                Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis1.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxisBit, ref DataLen);
                                if (Res != (int)ErrorCode.SUCCESS)
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception(GetErrorMessage(Res));
                                }
                                if (((AxisBit & Axis1.Mask) == Axis1.Mask) && ((AxisBit & Axis2.Mask) == Axis2.Mask))
                                {
                                    Mutex.ReleaseMutex();
                                    throw new Exception("Axis is busy");
                                }
                                break;
                        }
                    }
                    #endregion
                }

                //add to group
                for (int i = 0; i < MaxGroup + 1; i++)
                {
                    if (i == MaxGroup)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception("No avail group.");
                    }

                    if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
                    {
                        continue;
                    }

                    #region Add Axis1 and Axis2
                    Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis1.Device.ID, i], p_AxisHandle[Axis1.Device.ID, Axis1.Mask]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }

                    Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis2.Device.ID, i], p_AxisHandle[Axis2.Device.ID, Axis2.Mask]);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                    #endregion

                    double[] CenterArray = new double[2] { RefPt1, RefPt2 };
                    double[] ThruArray = new double[2] { RefPt1, RefPt2 };
                    double[] EndArray = new double[2] { EndPt1, EndPt2 };
                    uint AxisNum = 2;

                    if (CircType == ECircType.CenterEnd)
                    {
                        if (MoveType == EMoveType.Abs)
                            Res = Motion.mAcm_GpMoveCircularAbs(p_GroupHandle[Axis1.Device.ID, i], CenterArray, EndArray, ref AxisNum, (short)Dir);
                        else
                            Res = Motion.mAcm_GpMoveCircularRel(p_GroupHandle[Axis1.Device.ID, i], CenterArray, EndArray, ref AxisNum, (short)Dir);
                    }
                    if (CircType == ECircType.ThruEnd)
                    {
                        if (MoveType == EMoveType.Abs)
                            Res = Motion.mAcm_GpMoveCircularAbs_3P(p_GroupHandle[Axis1.Device.ID, i], ThruArray, EndArray, ref AxisNum, (short)Dir);
                        else
                            Res = Motion.mAcm_GpMoveCircularRel_3P(p_GroupHandle[Axis1.Device.ID, i], ThruArray, EndArray, ref AxisNum, (short)Dir);
                    }
                    if (CircType == ECircType.CenterAngle)
                    {
                        if (MoveType == EMoveType.Abs)
                            Res = Motion.mAcm_GpMoveCircularAbs_Angle(p_GroupHandle[Axis1.Device.ID, i], CenterArray, Angle_Deg, ref AxisNum, (short)Dir);
                        else
                            Res = Motion.mAcm_GpMoveCircularRel_Angle(p_GroupHandle[Axis1.Device.ID, i], CenterArray, Angle_Deg, ref AxisNum, (short)Dir);
                    }

                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                    break;
                }
                Mutex.ReleaseMutex();
                Thread.Sleep(PathDelay);
            }
            catch
            {
                throw;
            }
        }
        public void MoveArcCenterEndRel(TAxis Axis1, TAxis Axis2, ECircDir Dir, double CenterPt1, double CenterPt2, double EndPt1, double EndPt2)
        {
            try
            {
                MoveArc(Axis1, Axis2, ECircType.CenterEnd, EMoveType.Rel, Dir, CenterPt1, CenterPt2, EndPt1, EndPt2, 0);
            }
            catch { throw; }
        }
        public void MoveArcCenterEndAbs(TAxis Axis1, TAxis Axis2, ECircDir Dir, double CenterPt1, double CenterPt2, double EndPt1, double EndPt2)
        {
            try
            {
                MoveArc(Axis1, Axis2, ECircType.CenterEnd, EMoveType.Abs, Dir, CenterPt1, CenterPt2, EndPt1, EndPt2, 0);
            }
            catch { throw; }
        }
        public void MoveArcThruEndRel(TAxis Axis1, TAxis Axis2, ECircDir Dir, double ThruPt1, double ThruPt2, double EndPt1, double EndPt2)
        {
            try
            {
                MoveArc(Axis1, Axis2, ECircType.ThruEnd, EMoveType.Rel, Dir, ThruPt1, ThruPt2, EndPt1, EndPt2, 0);
            }
            catch { throw; }
        }
        public void MoveArcThruEndAbs(TAxis Axis1, TAxis Axis2, ECircDir Dir, double ThruPt1, double ThruPt2, double EndPt1, double EndPt2)
        {
            try
            {
                MoveArc(Axis1, Axis2, ECircType.ThruEnd, EMoveType.Abs, Dir, ThruPt1, ThruPt2, EndPt1, EndPt2, 0);
            }
            catch { throw; }
        }
        public void MoveArcCenterAngleRel(TAxis Axis1, TAxis Axis2, ECircDir Dir, double CenterPt1, double CenterPt2, ushort Angle_Deg)
        {
            try
            {
                MoveArc(Axis1, Axis2, ECircType.CenterAngle, EMoveType.Rel, Dir, CenterPt1, CenterPt2, 0, 0, Angle_Deg);
            }
            catch { throw; }
        }
        public void MoveArcCenterAngleAbs(TAxis Axis1, TAxis Axis2, ECircDir Dir, double CenterPt1, double CenterPt2, ushort Angle_Deg)
        {
            try
            {
                MoveArc(Axis1, Axis2, ECircType.CenterAngle, EMoveType.Abs, Dir, CenterPt1, CenterPt2, 0, 0, Angle_Deg);
            }
            catch { throw; }
        }

        public enum EMoveCmd
        {
            EndPath = 0,
            Abs2DLine = 1,
            Rel2DLine = 2,
            Abs2DArcCW = 3,
            Abs2DArcCCW = 4,
            Rel2DArcCW = 5,
            Rel2DArcCCW = 6,
        }

        public void PathAdd(TAxis Axis1, TAxis Axis2, EMoveCmd MoveCmd, double StartV, double DriveV, double EndPt1, double EndPt2, double Center1, double Center2)
        {
            uint Res = 0;

            if (InvertPulse[Axis1.Device.ID, Axis1.Mask])
            {
                Center1 = -Center1;
                EndPt1 = -EndPt1;
            }
            if (InvertPulse[Axis2.Device.ID, Axis2.Mask])
            {
                Center2 = -Center2;
                EndPt2 = -EndPt2;
            }

            double[] CenterArr = new double[2] { Center1, Center2 };
            double[] EndPtArr = new double[2] { EndPt1, EndPt2 };
            uint AxisNum = 2;

            try
            {
                Mutex.WaitOne();

                bool b_AxisInGroup = false;
                int i_GroupIndex = 0;

                //check group handle and add to existing group handle
                for (int i = 0; i < MaxGroup(Axis1.Device); i++)
                {
                    //ushort GpState = 0;
                    //Res = Motion.mAcm_GpGetState(p_GroupHandle[Axis1.Device.ID, i], ref GpState);
                    //if (Res != (int)ErrorCode.SUCCESS)
                    //{
                    //    //Mutex.ReleaseMutex();
                    //    // throw new Exception(GetErrorMessage(Res));
                    //}
                    if (p_GroupHandle[Axis1.Device.ID, i] != (IntPtr)0)
                    {
                        UInt32 AxesInGroup = 0;
                        uint BufLen = 4;
                        Res = Motion.mAcm_GetProperty(p_GroupHandle[Axis1.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInGroup, ref BufLen);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }

                        //check Axis1(primary axis) in group
                        uint Mask1 = 0x0001;
                        Mask1 = Mask1 << Axis1.Mask;
                        if ((AxesInGroup & Mask1) != Mask1) continue;

                        if (MoveCmd == 0)
                        {
                            Res = Motion.mAcm_GpAddPath(p_GroupHandle[Axis1.Device.ID, i], (ushort)MoveCmd, 0, DriveV, StartV, EndPtArr, CenterArr, ref AxisNum);
                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }
                            Mutex.ReleaseMutex();
                            return;
                        }

                        //check Axis2 in group, add if none
                        uint Mask2 = 0x0001;
                        Mask2 = Mask2 << Axis2.Mask;
                        if ((AxesInGroup & Mask2) != Mask2)
                        {
                            Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis2.Device.ID, i], p_AxisHandle[Axis2.Device.ID, Axis2.Mask]);
                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }
                        }

                        AxesInGroup = AxesInGroup & (Mask1 ^ 0xFFFF);
                        AxesInGroup = AxesInGroup & (Mask2 ^ 0xFFFF);

                        #region Remove redundant axis
                        if (AxesInGroup > 0)
                        {
                            for (int j = 0; j < MAX_AXIS; j++)
                            {
                                uint M = 0x0001;
                                M = M << j;
                                if ((AxesInGroup & (0x0001 << j)) == (0x0001 << j))
                                {
                                    Res = Motion.mAcm_GpRemAxis(p_GroupHandle[Axis2.Device.ID, i], p_AxisHandle[Axis2.Device.ID, (int)Math.Pow(2, i)]);
                                    if (Res != (int)ErrorCode.SUCCESS)
                                    {
                                        throw new Exception(GetErrorMessage(Res));
                                    }
                                }
                            }
                        }
                        #endregion
                        b_AxisInGroup = true;
                        i_GroupIndex = i;
                        break;
                    }
                }

                if (!b_AxisInGroup)
                {
                    //create new group handle
                    for (int i = 0; i < MaxGroup(Axis1.Device); i++)
                    {
                        if (p_GroupHandle[Axis1.Device.ID, i] == (IntPtr)0)
                        {
                            Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis1.Device.ID, i], p_AxisHandle[Axis1.Device.ID, Axis1.Mask]);
                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }

                            Res = Motion.mAcm_GpAddAxis(ref p_GroupHandle[Axis2.Device.ID, i], p_AxisHandle[Axis2.Device.ID, Axis2.Mask]);
                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }

                            i_GroupIndex = i;
                            break;
                        }
                    }
                }

                //add paths
                #region
                double Jerk = 0;//jerk not supported for blending path.
                Res = Motion.mAcm_SetProperty(p_GroupHandle[Axis1.Device.ID, i_GroupIndex], (uint)PropertyID.PAR_GpJerk, ref Jerk, 8);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                uint GpSFEnable = (UInt32)Advantech.Motion.SFEnable.SF_DIS;//disable SF, individual path have different speed.
                Res = Motion.mAcm_SetProperty(p_GroupHandle[Axis1.Device.ID, i_GroupIndex], (uint)PropertyID.CFG_GpSFEnable, ref GpSFEnable, 4);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }

                if (MoveCmd == EMoveCmd.EndPath)
                    Res = Motion.mAcm_GpAddPath(p_GroupHandle[Axis1.Device.ID, i_GroupIndex], (ushort)MoveCmd, (ushort)0/*Blending*/, 0, 0, null, null, ref AxisNum);
                else
                    Res = Motion.mAcm_GpAddPath(p_GroupHandle[Axis1.Device.ID, i_GroupIndex], (ushort)MoveCmd, (ushort)0/*Blending*/, DriveV, StartV, EndPtArr, CenterArr, ref AxisNum);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
                #endregion

                Mutex.ReleaseMutex();
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
        }

        public void PathAddLineAbs(TAxis Axis1, TAxis Axis2, double StartV, double DriveV, double Pt1, double Pt2)
        {
            try
            {
                //if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) Pt1 = -Pt1;
                //if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) Pt2 = -Pt2;

                PathAdd(Axis1, Axis2, EMoveCmd.Abs2DLine, StartV, DriveV, Pt1, Pt2, 0, 0);
            }
            catch { throw; }
        }
        public void PathAddLineRel(TAxis Axis1, TAxis Axis2, double StartV, double DriveV, double Pt1, double Pt2)
        {
            try
            {
                //if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) Pt1 = -Pt1;
                //if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) Pt2 = -Pt2;

                PathAdd(Axis1, Axis2, EMoveCmd.Rel2DLine, StartV, DriveV, Pt1, Pt2, 0, 0);
            }
            catch { throw; }
        }
        public void PathAddCircleAbs(TAxis Axis1, TAxis Axis2, double StartV, double DriveV, ECircDir Dir, double Center1, double Center2, double Pt1, double Pt2)
        {
            try
            {
                //if (InvertPulse[Axis1.Device.ID, Axis1.Mask])
                //{
                //    Center1 = -Center1;
                //    Pt1 = -Pt1;
                //}
                //if (InvertPulse[Axis2.Device.ID, Axis2.Mask])
                //{
                //    Center2 = -Center2;
                //    Pt2 = -Pt2;
                //}

                if (Dir == ECircDir.CW)
                    PathAdd(Axis1, Axis2, EMoveCmd.Abs2DArcCW, StartV, DriveV, Pt1, Pt2, Center1, Center2);
                else
                    PathAdd(Axis1, Axis2, EMoveCmd.Abs2DArcCCW, StartV, DriveV, Pt1, Pt2, Center1, Center2);
            }
            catch { throw; }
        }
        public void PathAddCircleRel(TAxis Axis1, TAxis Axis2, double StartV, double DriveV, ECircDir Dir, double Center1, double Center2, double Pt1, double Pt2)
        {
            try
            {
                //if (InvertPulse[Axis1.Device.ID, Axis1.Mask])
                //{
                //    Center1 = -Center1;
                //    Pt1 = -Pt1;
                //}
                //if (InvertPulse[Axis2.Device.ID, Axis2.Mask])
                //{
                //    Center2 = -Center2;
                //    Pt2 = -Pt2;
                //}

                if (Dir == ECircDir.CW)
                    PathAdd(Axis1, Axis2, EMoveCmd.Rel2DArcCW, StartV, DriveV, Pt1, Pt2, Center1, Center2);
                else
                    PathAdd(Axis1, Axis2, EMoveCmd.Rel2DArcCCW, StartV, DriveV, Pt1, Pt2, Center1, Center2);
            }
            catch { throw; }
        }
        public void PathEnd(TAxis MasterAxis)
        {
            try
            {
                PathAdd(MasterAxis, MasterAxis, EMoveCmd.EndPath, 0, 0, 0, 0, 0, 0);
            }
            catch { throw; }
        }

        public void PathMove(TAxis MasterAxis)
        {
            uint Res = 0;
            try
            {
                Mutex.WaitOne();
                for (int i = 0; i < MaxGroup(MasterAxis.Device) + 1; i++)
                {
                    if (i == MaxGroup(MasterAxis.Device))
                    {
                        throw new Exception("No Path Created.");
                    }

                    if (p_GroupHandle[MasterAxis.Device.ID, i] != (IntPtr)0)
                    {
                        UInt32 AxesInGroup = 0;
                        uint BufLen = 4;
                        Res = Motion.mAcm_GetProperty(p_GroupHandle[MasterAxis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInGroup, ref BufLen);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }

                        uint Mask = 0x0001;
                        Mask = Mask << MasterAxis.Mask;

                        if ((AxesInGroup & Mask) == Mask)
                        {
                            Res = Motion.mAcm_GpMovePath(p_GroupHandle[MasterAxis.Device.ID, i], IntPtr.Zero);
                            if (Res != (int)ErrorCode.SUCCESS)
                            {
                                throw new Exception(GetErrorMessage(Res));
                            }
                            break;
                        }
                    }
                }
                Mutex.ReleaseMutex();
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
        }
        public void PathFree(TAxis MasterAxis)
        {
            uint Res = 0;
            try
            {
                Mutex.WaitOne();
                for (int i = 0; i < MaxGroup(MasterAxis.Device) + 1; i++)
                {
                    if (i == MaxGroup(MasterAxis.Device))
                    {
                        throw new Exception("No avail group.");
                    }

                    if (p_GroupHandle[MasterAxis.Device.ID, i] == (IntPtr)0) continue;

                    UInt32 AxesInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[MasterAxis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint Mask = 0x0001;
                    Mask = Mask << MasterAxis.Mask;

                    if ((AxesInGroup & Mask) == Mask)
                    {
                        Res = Motion.mAcm_GpResetPath(ref p_GroupHandle[MasterAxis.Device.ID, i]);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                        break;
                    }
                }
                Mutex.ReleaseMutex();
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
        }
        public void PathInfo(TAxis MasterAxis, ref uint IndexNo, ref uint CurCmd, ref uint Remain)
        {
            uint Res = 0;
            try
            {
                Mutex.WaitOne();
                for (int i = 0; i < MaxGroup(MasterAxis.Device) + 1; i++)
                {
                    if (i == MaxGroup(MasterAxis.Device))
                    {
                        throw new Exception("No Path Created.");
                    }

                    if (p_GroupHandle[MasterAxis.Device.ID, i] == (IntPtr)0) continue;

                    UInt32 AxesInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[MasterAxis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint Mask = 0x0001;
                    Mask = Mask << MasterAxis.Mask;

                    if ((AxesInGroup & Mask) == Mask)
                    {
                        uint FreeCnt = new uint();
                        Res = Motion.mAcm_GpGetPathStatus(p_GroupHandle[MasterAxis.Device.ID, i], ref IndexNo, ref CurCmd, ref Remain, ref FreeCnt);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                        break;
                    }
                }
                Mutex.ReleaseMutex();
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;

            }
        }

        public void PathBlendTime(TAxis MasterAxis, uint BlendTime)
        {
            uint Res = 0;

            try
            {
                Mutex.WaitOne();
                for (int i = 0; i < MaxGroup(MasterAxis.Device) + 1; i++)
                {
                    if (i == MaxGroup(MasterAxis.Device))
                    {
                        throw new Exception("No Group Created.");
                    }

                    if (p_GroupHandle[MasterAxis.Device.ID, i] == (IntPtr)0) continue;

                    UInt32 AxesInGroup = 0;
                    uint BufLen = 4;
                    Res = Motion.mAcm_GetProperty(p_GroupHandle[MasterAxis.Device.ID, i], (uint)PropertyID.CFG_GpAxesInGroup, ref AxesInGroup, ref BufLen);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        throw new Exception(GetErrorMessage(Res));
                    }

                    uint Mask = 0x0001;
                    Mask = Mask << MasterAxis.Mask;

                    if ((AxesInGroup & Mask) == Mask)
                    {
                        Res = Motion.mAcm_SetProperty(p_GroupHandle[MasterAxis.Device.ID, i], (uint)PropertyID.CFG_GpBldTime, ref BlendTime, 4);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            throw new Exception(GetErrorMessage(Res));
                        }
                        break;
                    }
                }
                Mutex.ReleaseMutex();
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
        }

        #endregion

        #region Position/Counter
        public void GetLCntr(TAxis Axis, ref double Value)
        {
            Mutex.WaitOne();
            try
            {
                uint Res = Motion.mAcm_AxGetCmdPosition(p_AxisHandle[Axis.Device.ID, Axis.Mask], ref Value);
                if (InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                }
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch { throw; }
            Mutex.ReleaseMutex();
        }
        public void SetLCntr(TAxis Axis, double Value)
        {
            Mutex.WaitOne();
            try
            {
                if (InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                }
                uint Res = Motion.mAcm_AxSetCmdPosition(p_AxisHandle[Axis.Device.ID, Axis.Mask], Value);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch { throw; }
            Mutex.ReleaseMutex();
        }
        public void GetRCntr(TAxis Axis, ref double Value)
        {
            Mutex.WaitOne();
            try
            {
                uint Res = Motion.mAcm_AxGetActualPosition(p_AxisHandle[Axis.Device.ID, Axis.Mask], ref Value);
                if (InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                }
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch { throw; }
            Mutex.ReleaseMutex();
        }
        public void SetRCntr(TAxis Axis, double Value)
        {
            Mutex.WaitOne();
            try
            {
                if (InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                }
                uint Res = Motion.mAcm_AxSetActualPosition(p_AxisHandle[Axis.Device.ID, Axis.Mask], Value);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch { throw; }
            Mutex.ReleaseMutex();
        }

        public enum ELatchPosType { Command, Actual }
        public void LatchGetCntr(TAxis Axis, ELatchPosType Pos, ref double Value)
        {
            Mutex.WaitOne();
            try
            {
                uint Res = Motion.mAcm_AxGetLatchData(p_AxisHandle[Axis.Device.ID, Axis.Mask], (uint)Pos, ref Value);
                if (InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                }
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
            Mutex.ReleaseMutex();
        }

        public void LatchTrig(TAxis Axis)
        {
            Mutex.WaitOne();
            try
            {
                uint Res = Motion.mAcm_AxTriggerLatch(p_AxisHandle[Axis.Device.ID, Axis.Mask]);
                if (Res != (int)ErrorCode.SUCCESS) throw new Exception(GetErrorMessage(Res));
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
            Mutex.ReleaseMutex();
        }
        public void LatchReset(TAxis Axis)
        {
            Mutex.WaitOne();
            try
            {
                uint Res = Motion.mAcm_AxResetLatch(p_AxisHandle[Axis.Device.ID, Axis.Mask]);
                if (Res != (int)ErrorCode.SUCCESS) throw new Exception(GetErrorMessage(Res));
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
            Mutex.ReleaseMutex();
        }
        public bool LatchDataAvail(TAxis Axis)
        {
            Mutex.WaitOne();
            try
            {
                byte Flag = 0;
                uint Res = Motion.mAcm_AxGetLatchFlag(p_AxisHandle[Axis.Device.ID, Axis.Mask], ref Flag);
                if (Res != (int)ErrorCode.SUCCESS) throw new Exception(GetErrorMessage(Res));

                return (Flag == 1);
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
            Mutex.ReleaseMutex();
        }

        public void CmpSetData(TAxis Axis, double Value)//default 5us
        {
            Mutex.WaitOne();
            try
            {
                if (InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                }
                uint Res = Motion.mAcm_AxSetCmpData(p_AxisHandle[Axis.Device.ID, Axis.Mask], Value);
                if (Res != (int)ErrorCode.SUCCESS)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(GetErrorMessage(Res));
                }
            }
            catch { throw; }
            Mutex.ReleaseMutex();
        }


        #endregion

        #region IO
        public void UpdateInput(ref TInput Input)
        {
            uint Res = 0;

            if (Input.Device.Type == EDeviceType.P1265 && Input.Axis_Port == 6)
            {
                #region
                byte BitData = 0;
                UInt16 Ch = (UInt16)Input.Mask;
                if (Ch == 0x01) Ch = 0;
                if (Ch == 0x02) Ch = 1;
                if (Ch == 0x04) Ch = 2;
                if (Ch == 0x08) Ch = 3;
                if (Ch == 0x10) Ch = 4;
                if (Ch == 0x20) Ch = 5;
                if (Ch == 0x40) Ch = 6;
                if (Ch == 0x40) Ch = 7;
                Mutex.WaitOne();
                try
                {
                    Res = Motion.mAcm_DaqDiGetBit(p_AxisHandle[Input.Device.ID, Input.Axis_Port], Ch, ref BitData);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                }
                catch
                {
                    Mutex.ReleaseMutex();
                    throw;
                }
                if (BitData == 1) Input.Status = true; else Input.Status = false;

                Mutex.ReleaseMutex();
                return;
                #endregion
            }

            if (Input.Axis_Port >= 0 && Input.Axis_Port <= 7)
            {
                uint Mask = (uint)Input.Mask;

                if (Mask == 0x00000002 || Mask == 0x00000004 || Mask == 0x00000010 || Mask == 0x00000020)
                {
                    #region
                    ushort Ch = 0;
                    byte BitData = 0;
                    switch (Mask)
                    {                                  //P1240 Signal   P1245 Signal 
                        case 0x00000002: Ch = 0; break;//In1            In1/LTC
                        case 0x00000004: Ch = 1; break;//In2            In2/RDY
                        case 0x00000010: Ch = 2; break;//ExP            In4/JOG+
                        case 0x00000020: Ch = 3; break;//ExN            In5/JOG-
                    }

                    Mutex.WaitOne();
                    Res = Motion.mAcm_AxDiGetBit(p_AxisHandle[Input.Device.ID, Input.Axis_Port], Ch, ref BitData);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                    if (BitData == 1) Input.Status = true; else Input.Status = false;
                    Mutex.ReleaseMutex();
                    #endregion
                }
                else
                {
                    #region
                    uint Data = 0;
                    switch (Mask)
                    {                                             //P1240 Signal    P1245 Signal
                        case 0x00000080: Mask = 0x00000002; break;//                1:  Alm
                        case 0x00000400: Mask = 0x00000004; break;//                2:  HLmtP
                        case 0x00000800: Mask = 0x00000008; break;//                3:  HLmtN

                        case 0x00000008: Mask = 0x00000010; break;//                4: In3/ORG
                        case 0x00002000: Mask = 0x00000040; break;//                6: Emg

                        case 0x00000001: Mask = 0x00000200; break;//                9: EZ
                        case 0x00000040: Mask = 0x00002000; break;//                13: Inp

                        case 0x00000100: Mask = 0x00010000; break;//                17: SLmtP
                        case 0x00000200: Mask = 0x00020000; break;//                18: SLmtN
                        default: return;
                    }
                    Mutex.WaitOne();
                    try
                    {
                        Res = Motion.mAcm_AxGetMotionIO(p_AxisHandle[Input.Device.ID, Input.Axis_Port], ref Data);
                        if (Res != (int)ErrorCode.SUCCESS)
                        {
                            Mutex.ReleaseMutex();
                            throw new Exception(GetErrorMessage(Res));
                        }
                    }
                    catch
                    {
                        Mutex.ReleaseMutex();
                        throw;
                    }
                    if ((Data & Mask) == Mask) Input.Status = true; else Input.Status = false;
                    Mutex.ReleaseMutex();

                    #endregion
                }
            }
        }
        private void UpdateOutput(ref TOutput Output, bool Status)
        {
            Mutex.WaitOne();

            uint Res = 0;
            byte BitData;
            if (Status) BitData = 1; else BitData = 0;

            if (Output.Device.Type == EDeviceType.P1265 && Output.Axis_Port == 6)
            {
                #region
                UInt16 Ch = (UInt16)Output.Mask;
                if (Ch == 0x01) Ch = 0;
                if (Ch == 0x02) Ch = 1;
                if (Ch == 0x04) Ch = 2;
                if (Ch == 0x08) Ch = 3;
                if (Ch == 0x10) Ch = 4;
                if (Ch == 0x20) Ch = 5;
                if (Ch == 0x40) Ch = 6;
                if (Ch == 0x40) Ch = 7;
                try
                {
                    Res = Motion.mAcm_DaqDoSetBit(p_AxisHandle[Output.Device.ID, Output.Axis_Port], Ch, BitData);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                }
                catch
                {
                    Mutex.ReleaseMutex();
                    throw;
                }
                #endregion
                Output.Status = Status;
                Mutex.ReleaseMutex();
                return;
            }

            if (Output.Axis_Port >= 0 && Output.Axis_Port <= 7)
            {
                #region
                UInt16 Ch = (UInt16)Output.Mask;
                switch (Ch)
                {
                    case 0x01: Ch = 4; break;
                    case 0x02: Ch = 5; break;
                    case 0x04: Ch = 6; break;
                    case 0x08: Ch = 7; break;
                    default:
                        {
                            Mutex.ReleaseMutex();
                            return;
                        }
                }
                try
                {
                    Res = Motion.mAcm_AxDoSetBit(p_AxisHandle[Output.Device.ID, Output.Axis_Port], Ch, BitData);
                    if (Res != (int)ErrorCode.SUCCESS)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(GetErrorMessage(Res));
                    }
                }
                catch
                {
                    Mutex.ReleaseMutex();
                    throw;
                }
                #endregion
                Output.Status = Status;
                Mutex.ReleaseMutex();
                return;
            }
        }
        public void UpdateOutputHi(ref TOutput Output)
        {
            try
            {
                UpdateOutput(ref Output, true);
            }
            catch { throw; };
        }
        public void UpdateOutputLo(ref TOutput Output)
        {
            try
            {
                UpdateOutput(ref Output, false);
            }
            catch { throw; };
        }
        #endregion

        public void UpdateAxisPara(TAxis Axis)
        {
            try
            {
                SetInvertPulse(Axis, Axis.Para.InvertPulse);
                SetUPP(Axis, Axis.Para.Unit.Resolution);

                SetAccel(Axis, (uint)Axis.Para.Accel);
                SetDecel(Axis, (uint)Axis.Para.Accel);
                SetStartV(Axis, (uint)Axis.Para.StartV);
                SetDriveV(Axis, (uint)Axis.Para.SlowV);

                switch (Axis.Para.SwLimit.LimitType)
                {
                    case EAxisSwLimitType.Disable:
                        SoftwareLimitEnable(Axis, false); break;
                    case EAxisSwLimitType.Logical:
                    case EAxisSwLimitType.Real:
                        SoftwareLimitEnable(Axis, true); break;
                }
                if (Axis.Para.SwLimit.LimitType != EAxisSwLimitType.Disable)
                {
                    SetSLmtN(Axis, Axis.Para.SwLimit.PosN);
                    SetSLmtP(Axis, Axis.Para.SwLimit.PosP);
                }
            }
            catch { throw; }
        }
    }
}
