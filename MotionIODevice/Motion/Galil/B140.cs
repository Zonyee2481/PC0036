using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using static MotionIODevice.Common;

namespace MotionIODevice
{
    public class B140 : IMotionControl
    {
        const int MAX_DEVICE = 10, MAX_AXIS = 4, MAX_INPUT = 7, MAX_OUTPUT = 4;
        private bool Opened = false;
        private Common.TDevice _device;
        private List<Common.TAxis> _axisList = new List<Common.TAxis>();
        private List<List<Common.TInput>> _inputList = new List<List<Common.TInput>>();
        private List<List<Common.TOutput>> _outputList = new List<List<Common.TOutput>>();
        Mutex Mutex = new Mutex(false, "Galil_Mutex");
        private Galil.Galil g = new Galil.Galil();
        private bool bBenchDebug = false;

        double[,] UPP = new double[MAX_DEVICE, MAX_AXIS];// { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } };
        bool[,] InvertPulse = new bool[MAX_DEVICE, MAX_AXIS];// { { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false } };
        //double[,] StartV = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[,] DriveV = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[,] Accel = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[,] Decel = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        //double[,] Jerk = new double[MAX_DEVICE, MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };

        public class CN
        {
            public short LimitSwitch;  //m
            public short HomeSwitch;   //n
            public short LatchInput;   //o
            public short GenInputs;    //p
            public short AbortInput;   //q

            public CN()
            {
                LimitSwitch = -2;
                HomeSwitch = -2;
                LatchInput = -2;
                GenInputs = -2;
                AbortInput = -2;
            }
        }

        public enum INPUT
        {
            SENSE_HOME,
            IN_POS,
            ALARM,
            P_LIMIT,
            N_LIMIT,            
        }

        public enum OUTPUT
        {
            MTR_ON,
            ALARM_CLEAR,
        }

        public enum AXIS_NO
        {
            //None = 0,
            A,
            B,
            C,
            D,
        };

        private enum TS
        {
            /*
             * Bit 7 - Axis in motion if high
             * Bit 6 - Axis error exceeds error limit if high
             * Bit 5 - A motor off if high
             * Bit 4 - Undefined
             * Bit 3 - Forward limit switch status inactive if high
             * Bit 2 - Reverse limit switch status inactive if high
             * Bit 1 - Home a switch status
             * Bit 0 - Latched
            */
            LATCHED = 0,
            HOME,
            N_LIMIT,
            P_LIMIT,
            UNDEFINED,
            MTR_OFF,
            MTR_ERR,
            MTR_BUSY,
        }

        private char[] chAxis = new char[4] { 'A', 'B', 'C', 'D' };

        public B140(Common.TDevice device, List<string> AxisNameList, List<TRunModule> RunInfoList)
        {
            this._device = device;
            InitAxis(AxisNameList);
            InitIO();
            InitRunInfo(RunInfoList);
        }

        public void InitRunInfo(List<TRunModule> RunInfoList)
        {
            for (int j = 0; j < MAX_AXIS; j++)
            {
                var axis = _axisList[j];
                axis.RunModule = RunInfoList[j].RunModule;
                axis.RunAxis = RunInfoList[j].MtrAxis;
                axis.AxisPosName = RunInfoList[j].AxisPosName;
                axis.AxisPos = RunInfoList[j].AxisPos;
                axis.ZEnable = RunInfoList[j].ZEnable;
                axis.CardNo = RunInfoList[j].CardNo;
                _axisList[j] = axis;
            }
        }

        #region Interface

        #endregion

        #region IMotionControl
        public void InitAxis(List<string> AxisNameList)
        {
            _axisList.Add(new Common.TAxis(this._device, 1, 0x00, "", AxisNameList[0]));
            _axisList.Add(new Common.TAxis(this._device, 2, 0x01, "", AxisNameList[1]));
            _axisList.Add(new Common.TAxis(this._device, 3, 0x02, "", AxisNameList[2]));
            _axisList.Add(new Common.TAxis(this._device, 4, 0x03, "", AxisNameList[3]));
        }

        public void InitIO()
        {
            for (int i = 0; i < MAX_AXIS; i++)
            {
                List<TInput> ipList = new List<TInput>();
                ipList.Add(new Common.TInput(this._device, 1, (byte)i, 0x0002, "", ""));
                ipList.Add(new Common.TInput(this._device, 2, (byte)i, 0x0004, "", ""));
                ipList.Add(new Common.TInput(this._device, 3, (byte)i, 0x0008, "", ""));
                ipList.Add(new Common.TInput(this._device, 4, (byte)i, 0x0040, "", ""));
                ipList.Add(new Common.TInput(this._device, 5, (byte)i, 0x0080, "", ""));
                _inputList.Add(ipList);

                List<TOutput> opList = new List<TOutput>();
                opList.Add(new Common.TOutput(this._device, 1, (byte)i, 0x01, "", ""));
                opList.Add(new Common.TOutput(this._device, 2, (byte)i, 0x02, "", ""));
                //opList.Add(new Common.TOutput(this._device, 3, (byte)i, 0x03, "", ""));
                _outputList.Add(opList);
            }
        }

        public List<Common.TAxis> AxisList { get { return _axisList; } }
        public int GetAxisNum { get { return _axisList.Count; } }

        public int GetInputNum { get { return _inputList.Count; } }

        public int GetOutputNum { get { return _outputList.Count; } }

        public bool OpenBoard()
        {
            OpenGalilBoard();
            LoadMotorPara();
            //HardwareLimitLogicActHigh(false);
            Servo((int)AXIS_NO.A, true);
            return true;
        }

        public bool CloseBoard()
        {
            return true;
        }

        public bool ReadBit(int bit)
        {
            if (!Opened) { return false; }
            int point, axis = Math.DivRem(bit, MAX_INPUT, out point);
            var input = _inputList[axis][point];

            UpdateInput(ref input);

            _inputList[axis][point] = input;

            return input.Status;
        }

        public bool OutBit(int bit, Common.EOutputStatus status)
        {
            if (!Opened && !bBenchDebug) { return false; }
            int point, axis = Math.DivRem(bit, MAX_OUTPUT, out point);
            var output = _outputList[axis][point];

            switch (status)
            {
                case EOutputStatus.Hi:

                    if (!bBenchDebug)
                    {
                        SetOutput(ref output, true);
                    }
                    else
                    {
                        output.Status = true;
                    }

                    break;
                case EOutputStatus.Lo:

                    if (!bBenchDebug)
                    {
                        SetOutput(ref output, false);
                    }
                    else
                    {
                        output.Status = false;
                    }

                    break;
            }

            _outputList[axis][point] = output;

            return output.Status;
        }

        public bool ServoOn(Common.TAxis Axis)
        {
            bool res = false;
            if (Axis.Para.InvertMtrOn)
            {
                res = SendCommand(string.Format("MO{0}", (AXIS_NO)Axis.Mask));
                
            }
            else
            {
                res = SendCommand(string.Format("SH{0}", (AXIS_NO)Axis.Mask));
            }

            if (!res)
            {
                throw new Exception();
            }

            return true;
        }

        public bool ServoOff(Common.TAxis Axis)
        {
            bool res = false;
            if (Axis.Para.InvertMtrOn)
            {
                res = SendCommand(string.Format("SH{0}", (AXIS_NO)Axis.Mask));
            }
            else
            {
                res = SendCommand(string.Format("MO{0}", (AXIS_NO)Axis.Mask));                
            }

            if (!res)
            {
                throw new Exception();
            }

            return true;
        }

        public bool AxisAlarm(Common.TAxis Axis, ref Common.TInput Alm)
        {
            try
            {
                if (Axis.Para.MotorAlarmType == Common.EMotorAlarmType.None)
                {
                    return false;
                }
                if ((Axis.Para.MotorAlarmType == Common.EMotorAlarmType.NC && Alm.Status) ||
                    (Axis.Para.MotorAlarmType == Common.EMotorAlarmType.NO) && !Alm.Status)
                {
                    UpdateInput(ref Alm);
                    if (!Alm.Status)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return true;
        }

        internal void AlarmClear(ref Common.TOutput AlmClr, bool On)
        {
            SetOutput(ref AlmClr, On);
        }

        public void ClearAxisError(Common.TAxis Axis)
        {
            string ExMsg = "ClearAxisError";
            try
            {
                int axisNo = Axis.Mask;
                if (AxisAlarm(axisNo))
                {
                    var opAlarm = _outputList[axisNo][(int)OUTPUT.ALARM_CLEAR];
                    AlarmClear(ref opAlarm, true);
                    Thread.Sleep(50);
                    AlarmClear(ref opAlarm, false);
                    Thread.Sleep(50);
                    AlarmClear(ref opAlarm, true);
                    Thread.Sleep(50);
                    AlarmClear(ref opAlarm, false);
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                MessageBox.Show(ExMsg);
            }
        }

        internal bool SetOutput(ref Common.TOutput Output, bool On)
        {
            try
            {
                if (On)
                    SetDO(ref Output, Common.EOutputStatus.Hi);
                else
                    SetDO(ref Output, Common.EOutputStatus.Lo);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void SetDO(ref Common.TOutput Output, Common.EOutputStatus Status)
        {
            if (!Opened) { return; }
            string ExMsg = "SetDO";
            try
            {
                if (Status == Common.EOutputStatus.Hi)
                {
                    SetDO_Hi(ref Output, Status);
                }
                if (Status == Common.EOutputStatus.Lo)
                {
                    SetDO_Lo(ref Output, Status);
                }
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        private void SetDO_Hi(ref Common.TOutput Output, Common.EOutputStatus Status)
        {
            switch ((OUTPUT)Output.NodeID)
            {
                case OUTPUT.ALARM_CLEAR:
                    {
                        SendCommand(string.Format("SB {0}", Output.NodeID));
                    }
                    break;
                case OUTPUT.MTR_ON:
                    {
                        Servo(Output.Axis_Port, true);
                    }
                    break;
            }
        }

        private void SetDO_Lo(ref Common.TOutput Output, Common.EOutputStatus Status)
        {
            switch ((OUTPUT)Output.NodeID)
            {
                case OUTPUT.ALARM_CLEAR:
                    {
                        SendCommand(string.Format("CB {0}", Output.NodeID));
                    }
                    break;
                case OUTPUT.MTR_ON:
                    {
                        Servo(Output.Axis_Port, false);
                    }
                    break;
            }
        }

        public bool SetMotionParam(Common.TAxis Axis, double DriveV, double Accel)
        {
            if (!Opened) { return false; }
            string ExMsg = "SetMotionParam";

            double _dDriveV = DriveV;

            try
            {
                SendCommand(string.Format("SP{0}={1}", chAxis[Axis.Mask], Convert.ToInt32(DriveV)));
                SendCommand(string.Format("AC{0}={1}", chAxis[Axis.Mask], Convert.ToInt32(Accel)));
                SendCommand(string.Format("DC{0}={1}", chAxis[Axis.Mask], Convert.ToInt32(Accel)));
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
            return true;
        }

        public void SetMotionParam(Common.TAxis Axis, double DriveV, double Accel, double Decel)
        {
            if (!Opened) { return; }
            string ExMsg = "SetMotionParam";

            double _dDriveV = DriveV;

            try
            {
                SendCommand(string.Format("SP{0}={1}", chAxis[Axis.Mask], Convert.ToInt32(DriveV)));
                SendCommand(string.Format("AC{0}={1}", chAxis[Axis.Mask], Convert.ToInt32(Accel)));
                SendCommand(string.Format("DC{0}={1}", chAxis[Axis.Mask], Convert.ToInt32(Decel)));
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }        

        public void HardwareLimitLogicActHigh(bool LogicHigh)
        {
            string ExMsg = "HardwareLimitLogicActHigh " + LogicHigh;
            try
            {
                bool Res = false;
                int iSetting = LogicHigh ? 1 : -1; 
                Res = SendCommand(string.Format("CN {0}", iSetting));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void SetUPP(Common.TAxis Axis, double Value)
        {
            try
            {
                UPP[Axis.Device.ID, (int)Axis.Mask] = Value;
            }
            catch { throw; }
        }

        public void SetInvertPulse(Common.TAxis Axis, bool Invert)
        {
            InvertPulse[Axis.Device.ID, (int)Axis.Mask] = Invert;
        }

        public void SetAcc(Common.TAxis Axis, double Value)
        {
            try
            {
                int PValue = (int)(Value / UPP[Axis.Device.ID, Axis.Mask]);
                Accel[Axis.Device.ID, (int)Axis.Mask] = PValue;
                Axis.Para.Psnt.Accel = PValue;
                //Accel[Axis.Device.ID, (int)Axis.Mask] = Value;
                //Axis.Para.Psnt.Accel = Value;
            }
            catch { throw; }
        }

        public void SetDcc(TAxis Axis, double Value)
        {
            try
            {
                int PValue = (int)(Value / UPP[Axis.Device.ID, Axis.Mask]);
                Decel[Axis.Device.ID, (int)Axis.Mask] = PValue;
                Axis.Para.Psnt.Decel = PValue;
                //Decel[Axis.Device.ID, (int)Axis.Mask] = Value;
                //Axis.Para.Psnt.Decel = Value;
            }
            catch { throw; }
        }
        public void SetVel(TAxis Axis, double Value)
        {
            try
            {
                int PValue = (int)(Value / UPP[Axis.Device.ID, Axis.Mask]);
                DriveV[Axis.Device.ID, (int)Axis.Mask] = PValue;
                Axis.Para.Psnt.DriveV = PValue;
                //DriveV[Axis.Device.ID, (int)Axis.Mask] = Value;
                //Axis.Para.Psnt.DriveV = PValue;
            }
            catch { throw; }
        }

        public double GetLogicPos(Common.TAxis Axis)
        {
            double Pos = 0;

            if (!Opened) { return 0.000; }
            try
            {
                bool Res = false;
                string strRes = "";
                Res = SendCommandGetResponse(string.Format("MG_RP{0}", (AXIS_NO)Axis.Mask), out strRes);
                if (!Res)
                {
                    throw new Exception();
                }
                else
                {
                    var dStatus = double.Parse(strRes);
                    Pos = (double)dStatus * UPP[Axis.Device.ID, (int)Axis.Mask];
                    if (InvertPulse[Axis.Device.ID, (int)Axis.Mask])
                    {
                        Pos = -Pos;
                    }
                }
            }
            catch { }
            return Math.Round(Pos, 3);
        }

        public double GetRealPos(Common.TAxis Axis)
        {
            double Pos = 0;

            if (!Opened) { return 0.000; }
            try
            {
                bool Res = false;
                string strRes = "";
                Res = SendCommandGetResponse(string.Format("TP{0}", (AXIS_NO)Axis.NodeID), out strRes);

                if (!Res)
                {
                    throw new Exception();
                }
                else
                {
                    var dStatus = double.Parse(strRes);
                    Pos = (double)dStatus * UPP[Axis.Device.ID, (int)Axis.Mask];
                    if (InvertPulse[Axis.Device.ID, (int)Axis.Mask])
                    {
                        Pos = -Pos;
                    }
                }
            }
            catch { }
            return Math.Round(Pos, 3);
        }

        public void Jog(Common.TAxis Axis, bool Positive, double DriveV)
        {
            if (AxisBusy(Axis)) { new Exception("Axis Busy."); }

            try
            {
                bool Res = false;
                int Dir = 1;

                if (Positive & !InvertPulse[Axis.Device.ID, (int)Axis.Mask])
                {
                    Dir = 1;
                }
                if (!Positive & !InvertPulse[Axis.Device.ID, (int)Axis.Mask])
                {
                    Dir = -1;
                }
                if (Positive & InvertPulse[Axis.Device.ID, (int)Axis.Mask])
                {
                    Dir = -1;
                }
                if (!Positive & InvertPulse[Axis.Device.ID, (int)Axis.Mask])
                {
                    Dir = 1;
                }                
                Mutex.WaitOne();
                Res = SendCommand(string.Format("JG{0}={1}", (AXIS_NO)Axis.Mask, (Math.Abs(DriveV) * Dir)));
                Res = SendCommand(string.Format("BG{0}", (AXIS_NO)Axis.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch
            {
                Mutex.ReleaseMutex();
                throw;
            }
            Mutex.ReleaseMutex();
        }

        public void JogP(Common.TAxis Axis)
        {
            string ExMsg = Axis.Name + " JogP";
            try
            {
                bool Res = false;
                string strRes = "";
                double dDriveV = 0.000;
                if (AxisBusy(Axis)) { return; }
                ClearAxisError(Axis);
                Res = SendCommandGetResponse(string.Format("SP{0} = ?", (AXIS_NO)Axis.Mask), out strRes);
                if (!Res)
                {
                    throw new Exception();
                }
                var dResult = double.Parse(strRes);
                dDriveV = (double)dResult;
                Jog(Axis, true, dDriveV);
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void JogN(Common.TAxis Axis)
        {
            string ExMsg = Axis.Name + " JogN";
            try
            {
                bool Res = false;
                string strRes = "";
                double dDriveV = 0.000;
                if (AxisBusy(Axis)) { return; }
                ClearAxisError(Axis);
                Res = SendCommandGetResponse(string.Format("SP{0} = ?", (AXIS_NO)Axis.Mask), out strRes);
                if (!Res)
                {
                    throw new Exception();
                }
                var dResult = double.Parse(strRes);
                dDriveV = (double)dResult;
                Jog(Axis, false, dDriveV);
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void MovePtpAbs(Common.TAxis Axis, double Value)
        {
            string ExMsg = Axis.Name + " MovePtpAbs";
            try
            {
                bool Res = false;
                if (InvertPulse[Axis.Device.ID, Axis.Mask]) { Value = -Value; }
                //int iValue = (int)(Value / UPP[Axis.Device.ID, Axis.Mask]);
                double dValue = Value / UPP[Axis.Device.ID, Axis.Mask];
                Res = SendCommand(string.Format("PA{0} = {1}", (AXIS_NO)Axis.Mask, dValue));
                Res = SendCommand(string.Format("BG{0}", (AXIS_NO)Axis.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void MovePtpRel(Common.TAxis Axis, double Value)
        {
            string ExMsg = Axis.Name + " MovePtpAbs";
            try
            {
                bool Res = false;
                if (InvertPulse[Axis.Device.ID, Axis.Mask]) { Value = -Value; }
                //int iValue = (int)(Value / UPP[Axis.Device.ID, Axis.Mask]);
                double dValue = Value / UPP[Axis.Device.ID, Axis.Mask];
                Res = SendCommand(string.Format("PR{0} = {1}", (AXIS_NO)Axis.Mask, dValue));
                Res = SendCommand(string.Format("BG{0}", (AXIS_NO)Axis.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void MoveXYAbs(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2)
        {
            string ExMsg = Axis1.Name + "_" + Axis2.Name + " MoveXYAbs";
            try
            {
                bool Res = false;
                string A = string.Empty, B = string.Empty, C = string.Empty, D = string.Empty;
                if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) { Value1 = -Value1; }
                //int iValue1 = (int)(Value1 / UPP[Axis1.Device.ID, Axis1.Mask]);
                double dValue1 = Value1 / UPP[Axis1.Device.ID, Axis1.Mask];
                if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) { Value2 = -Value2; }
                //int iValue2 = (int)(Value2 / UPP[Axis2.Device.ID, Axis2.Mask]);
                double dValue2 = Value2 / UPP[Axis2.Device.ID, Axis2.Mask];
                AxisMoveLinePos(Axis1, Axis2, dValue1, dValue2, out A, out B, out C, out D);
                Res = SendCommand(string.Format("PA {0},{1},{2},{3}", A, B, C, D));
                Res = SendCommand(string.Format("BG {0}{1}", (AXIS_NO)Axis1.Mask, (AXIS_NO)Axis2.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void MoveLineAbs(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2)
        {
            string ExMsg = Axis1.Name + "_" + Axis2.Name + " MoveLineAbs";
            try
            {
                bool Res = false;
                string A = string.Empty, B = string.Empty, C = string.Empty, D = string.Empty;
                if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) { Value1 = -Value1; }
                //int iValue1 = (int)(Value1 / UPP[Axis1.Device.ID, Axis1.Mask]);
                double dValue1 = Value1 / UPP[Axis1.Device.ID, Axis1.Mask];
                if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) { Value2 = -Value2; }
                //int iValue2 = (int)(Value2 / UPP[Axis2.Device.ID, Axis2.Mask]);
                double dValue2 = Value2 / UPP[Axis2.Device.ID, Axis2.Mask];
                AxisMoveLinePos(Axis1, Axis2, dValue1, dValue2, out A, out B, out C, out D);
                Res = SendCommand(string.Format("PA {0},{1},{2},{3}", A, B, C, D));
                Res = SendCommand(string.Format("BG {0}{1}", (AXIS_NO)Axis1.Mask, (AXIS_NO)Axis2.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void MoveXYRel(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2)
        {
            string ExMsg = Axis1.Name + "_" + Axis2.Name + " MoveXYRel";
            try
            {
                bool Res = false;
                string A = string.Empty, B = string.Empty, C = string.Empty, D = string.Empty;
                if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) { Value1 = -Value1; }
                //int iValue1 = (int)(Value1 / UPP[Axis1.Device.ID, Axis1.Mask]);
                double dValue1 = Value1 / UPP[Axis1.Device.ID, Axis1.Mask];
                if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) { Value2 = -Value2; }
                //int iValue2 = (int)(Value2 / UPP[Axis2.Device.ID, Axis2.Mask]);
                double dValue2 = Value2 / UPP[Axis2.Device.ID, Axis2.Mask];
                AxisMoveLinePos(Axis1, Axis2, dValue1, dValue2, out A, out B, out C, out D);
                Res = SendCommand(string.Format("PR {0},{1},{2},{3}", A, B, C, D));
                Res = SendCommand(string.Format("BG {0}{1}", (AXIS_NO)Axis1.Mask, (AXIS_NO)Axis2.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void MoveLineRel(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2)
        {
            string ExMsg = Axis1.Name + "_" + Axis2.Name + " MoveLineRel";
            try
            {
                bool Res = false;
                string A = string.Empty, B = string.Empty, C = string.Empty, D = string.Empty;
                if (InvertPulse[Axis1.Device.ID, Axis1.Mask]) { Value1 = -Value1; }
                //int iValue1 = (int)(Value1 / UPP[Axis1.Device.ID, Axis1.Mask]);
                double dValue1 = Value1 / UPP[Axis1.Device.ID, Axis1.Mask];
                if (InvertPulse[Axis2.Device.ID, Axis2.Mask]) { Value2 = -Value2; }
                //int iValue2 = (int)(Value2 / UPP[Axis2.Device.ID, Axis2.Mask]);
                double dValue2 = Value2 / UPP[Axis2.Device.ID, Axis2.Mask];
                AxisMoveLinePos(Axis1, Axis2, dValue1, dValue2, out A, out B, out C, out D);
                Res = SendCommand(string.Format("PR {0},{1},{2},{3}", A, B, C, D));
                Res = SendCommand(string.Format("BG {0}{1}", (AXIS_NO)Axis1.Mask, (AXIS_NO)Axis2.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        private void AxisMoveLinePos(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2, out string A, out string B, out string C, out string D)
        {
            A = string.Empty; 
            B = string.Empty; 
            C = string.Empty; 
            D = string.Empty;

            switch ((AXIS_NO)Axis1.Mask)
            {
                case AXIS_NO.A:
                    {
                        A = Value1.ToString();
                    }
                    break;
                case AXIS_NO.B:
                    {
                        B = Value1.ToString();
                    }
                    break;
                case AXIS_NO.C:
                    {
                        C = Value1.ToString();
                    }
                    break;
                case AXIS_NO.D:
                    {
                        D = Value1.ToString();
                    }
                    break;
            }

            switch ((AXIS_NO)Axis2.Mask)
            {
                case AXIS_NO.A:
                    {
                        A = Value2.ToString();
                    }
                    break;
                case AXIS_NO.B:
                    {
                        B = Value2.ToString();
                    }
                    break;
                case AXIS_NO.C:
                    {
                        C = Value2.ToString();
                    }
                    break;
                case AXIS_NO.D:
                    {
                        D = Value2.ToString();
                    }
                    break;
            }
        }

        public void AxisBusy(Common.TAxis Axis, ref bool Busy)
        {
            string ExMsg = "AxisBusy";
            try
            {
                Busy = AxisBusy(Axis);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        internal bool AxisBusy(Common.TAxis Axis)
        {
            string EMsg = Axis.Name + " AxisBusy";
            double dValue = 0.0;
            try
            {
                string str = "";
                SendCommandGetResponse(string.Format("MG_BG{0}", (AXIS_NO)Axis.Mask), out str);
                dValue = Convert.ToDouble(str);
                if (dValue > 0.0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                throw;
            };
        }

        public bool ForceStop(Common.TAxis Axis)
        {
            if (!Opened) { return false; }
            string ExMsg = Axis + " ForceStop";
            bool Res = false;
            try
            {
                Res = SendCommand(string.Format("ST{0}", (AXIS_NO)Axis.Mask));
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool SetLogicPos(Common.TAxis Axis, double Pos)
        {
            bool Res = false;;
            Res = SendCommand(string.Format("DP{0} = {1}", (AXIS_NO)Axis.Mask, Pos));

            if (!Res)
            {
                throw new Exception();
            }
            return true;
        }

        public bool SetRealPos(Common.TAxis Axis, double Pos)
        {
            bool Res = false;
            Res = SendCommand(string.Format("DE{0} = {1}", (AXIS_NO)Axis.Mask, Pos));

            if (!Res)
            {
                throw new Exception();
            }
            return true;
        }

        public void GetMotorSpeedRange(Common.TAxis Axis, ref double Min, ref double Max)
        {
            string ExMsg = Axis.Name + " GetMotorSpeedRange";
            if (!Opened) { return; }
            try
            {
                Min = 1 * UPP[Axis.Device.ID, (int)Axis.Mask];
                Max = 3000000 * UPP[Axis.Device.ID, (int)Axis.Mask];
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void GetMotorAccelMinMax(Common.TAxis Axis, ref double Min, ref double Max)
        {
            string ExMsg = Axis.Name + " GetAccelMinMax";
            if (!Opened) { return; }
            try
            {
                Min = 1 * UPP[Axis.Device.ID, (int)Axis.Mask];
                Max = 1073740800 * UPP[Axis.Device.ID, (int)Axis.Mask];
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void GetDecelMinMax(Common.TAxis Axis, ref double Min, ref double Max)
        {
            string ExMsg = Axis.Name + " GetDecelMinMax";
            if (!Opened) { return; }
            try
            {
                Min = 1 * UPP[Axis.Device.ID, (int)Axis.Mask];
                Max = 1073740800 * UPP[Axis.Device.ID, (int)Axis.Mask];
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void UpdateInput(ref Common.TInput Input)
        {
            try
            {
                if (Input.Axis_Port >= 0 && Input.Axis_Port <= 7)
                {
                    #region
                    switch ((INPUT)Input.NodeID - 1)
                    {
                        case INPUT.P_LIMIT:
                            {
                                Input.Status = SensLmtP(Input.Axis_Port, SwitchType.TellSwitch);
                            }
                            break;
                        case INPUT.N_LIMIT:
                            {
                                Input.Status = SensLmtN(Input.Axis_Port, SwitchType.TellSwitch);
                            }
                            break;
                        case INPUT.SENSE_HOME:
                            {
                                Input.Status = SensHome(Input.Axis_Port, SwitchType.TellSwitch);
                            }
                            break;
                        case INPUT.ALARM:
                            {
                                // IN[1] = Axis 1 alarm signal; IN[2] = Axis 2 alarm signal;
                                // IN[3] = Axis 3 alarm signal; IN[4] = Axis 4 alarm signal;
                                Input.Status = CardInput((int)Input.Axis_Port + 1);
                            }
                            break;
                    }
                    #endregion
                }
            }
            catch
            {
                Input.Status = false;
            }

        }

        private bool CardInput(int iInputPort)
        {
            bool Res = false;
            int _iStatus = 0;
            string strRes = "";
            Res = SendCommandGetResponse(string.Format("MG @IN[{0}]", iInputPort), out strRes);

            if (!Res)
            {
                throw new Exception();
            }
            else
            {
                var dStatus = double.Parse(strRes);
                _iStatus = (int)dStatus;
            }

            if (_iStatus == 1)
            {
                Res = true;
            }
            else
            {
                Res = false;
            }

            return Res;
        }

        public void UpdateAxis(Common.TAxis Axis)
        {
            string ExMsg = "UpdateAxis";

            try
            {
                ExMsg = "B140 " + ExMsg;
                UpdateAxisPara(Axis);
                HardwareLimitEnable(Axis, false); // Suppose set to enable
                if (Axis.Para.SwLimit.LimitType != Common.EAxisSwLimitType.Disable)
                {
                    SoftwareLimitEnable(Axis, true);
                }
                else
                {
                    SoftwareLimitEnable(Axis, false);
                }
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                //throw new Exception(ExMsg);
            }
        }       

        public void UpdateAxisHome(Common.TAxis Axis)
        {
            string ExMsg = "UpdateAxisHome";
            if (!Opened) return;
            try
            {
                ExMsg = "B140 " + ExMsg;
                UpdateAxisPara(Axis);
                HardwareLimitEnable(Axis, false);
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void UpdateAxisPara(Common.TAxis Axis)
        {
            try
            {
                SetInvertPulse(Axis, Axis.Para.InvertPulse);
                SetUPP(Axis, Axis.Para.Unit.Resolution);

                SetAcc(Axis, (uint)Axis.Para.Accel);
                SetDcc(Axis, (uint)Axis.Para.Accel);
                SetVel(Axis, (uint)Axis.Para.SlowV);

                switch (Axis.Para.SwLimit.LimitType)
                {
                    case EAxisSwLimitType.Disable:
                        {
                            SoftwareLimitEnable(Axis, false);
                        }
                        break;
                    case EAxisSwLimitType.Logical:
                    case EAxisSwLimitType.Real:
                        {
                            SoftwareLimitEnable(Axis, true);
                            SetSLmtP(Axis, Axis.Para.SwLimit.PosP);
                            SetSLmtN(Axis, Axis.Para.SwLimit.PosN);
                        }
                        break;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };
        }

        // HWLmt logic act high
        public bool HardwareLimitEnable(Common.TAxis Axis, bool Enable)
        {
            bool Res = false;
            if (!Opened) { return false; }
            int iLogicHigh = Enable ? 1 : 3; // 0 = Enabled; 3 = Disabled both limit sensor

            Res = SendCommand(string.Format("LD{0} = {1}", (AXIS_NO)Axis.NodeID, iLogicHigh));

            if (!Res)
            {
                throw new Exception();
            }

            return true;
        }

        public void SoftwareLimitEnable(Common.TAxis Axis, bool Enable)
        {
            string ExMsg = "SoftwareLimitEnable";
            bool Res = false;
            if (!Opened) { return; }
            try
            {
                if (!Enable)
                {
                    Res = SendCommand(string.Format("FL{0} = {1}", (AXIS_NO)Axis.Mask, 2147483647));
                    Res = SendCommand(string.Format("BL{0} = {1}", (AXIS_NO)Axis.Mask, -2147483648));

                }
                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                ExMsg = ExMsg + (char)13 + ex.Message.ToString();
                //throw new Exception(ExMsg);
            }
        }

        private void SetSLmt(Common.TAxis Axis, bool Positive, double Value)
        {
            try
            {
                int PValue = (int)(Value / UPP[Axis.Device.ID, Axis.Mask]);
                bool Res = false;
                if (!Opened) { return; }
                if (Positive && !InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Res = SendCommand(string.Format("FL{0} = {1}", (AXIS_NO)Axis.Mask, Value));
                }
                if (Positive && InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                    Res = SendCommand(string.Format("BL{0} = {1}", (AXIS_NO)Axis.Mask, Value));
                }
                if (!Positive && !InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Res = SendCommand(string.Format("BL{0} = {1}", (AXIS_NO)Axis.Mask, Value));
                }
                if (!Positive && InvertPulse[Axis.Device.ID, Axis.Mask])
                {
                    Value = -Value;
                    Res = SendCommand(string.Format("FL{0} = {1}", (AXIS_NO)Axis.Mask, Value));
                }

                if (!Res)
                {
                    throw new Exception();
                }
            }
            catch { throw; }
        }

        public void SetSLmtP(Common.TAxis Axis, double Value)
        {
            string ExMsg = "SetSLmtP";
            try
            {
                SetSLmt(Axis, true, Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void SetSLmtN(Common.TAxis Axis, double Value)
        {
            string ExMsg = "SetSLmtN";
            try
            {
                SetSLmt(Axis, false, Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Home(int axisNo, HomingType motorType)
        {
            bool bMotion = false;
            if (motorType == HomingType.NPHType)
            {
                bMotion = HomeBySensor_NPH(axisNo);
            }
            else if (motorType == HomingType.Without_NP)
            {
                bMotion = HomeBySensor_H(axisNo);
            }

            return bMotion;
        }

        public bool Jog(int axisNo, bool bPositive, bool bSlow = false)
        {
            var Axis = _axisList[axisNo];

            if (AxisAlarm(axisNo))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis, bSlow ? Axis.Para.SlowV : Axis.Para.FastV, Axis.Para.Accel)) { return false; }
                if (bPositive)
                {
                    JogP(Axis);
                }
                else
                {
                    JogN(Axis);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool Jog(int axisNo, bool bPositive, int nSpeed = 0)
        {
            var Axis = _axisList[axisNo];

            if (AxisAlarm(axisNo))
            {
                return false;
            }
            try
            {
                double dSpeed = 0;
                if (nSpeed == 0)
                {
                    dSpeed = Axis.Para.Jog.SlowV;
                }
                else if (nSpeed == 1)
                { 
                    dSpeed = Axis.Para.Jog.FastV; 
                }

                if (!SetMotionParam(Axis, dSpeed, Axis.Para.Accel)) { return false; }
                if (bPositive)
                {
                    JogP(Axis);
                }
                else
                {
                    JogN(Axis);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool MoveAbs(int axisNo, double Value, int Timeout)
        {
            var Axis = _axisList[axisNo];

            if (AxisAlarm(axisNo))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis, Axis.Para.FastV, Axis.Para.Accel)) { return false; }
                MovePtpAbs(Axis, Value);
                bool bAxisWait = false;
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref bAxisWait);
                    if (!bAxisWait) { break; }
                    if (Environment.TickCount > t) { break; }
                    Thread.Sleep(1);
                }

                ForceStop(Axis);

                if (Environment.TickCount > t)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool MoveAbsLine(int axisNo1, int axisNo2, double Value1, double Value2, int Timeout)
        {
            var Axis1 = _axisList[axisNo1];
            var Axis2 = _axisList[axisNo2];

            if (AxisAlarm(axisNo1) || AxisAlarm(axisNo2))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }

                MoveLineAbs(Axis1, Axis2, Value1, Value2);
                bool bAxis1Wait = false, bAxis2Wait = false;
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis1, ref bAxis1Wait);
                    AxisBusy(Axis2, ref bAxis2Wait);
                    if (bAxis1Wait && bAxis2Wait) { break; }
                    if (Environment.TickCount > t) { break; }
                    Thread.Sleep(1);
                }

                ForceStop(Axis1);
                ForceStop(Axis2);

                if (Environment.TickCount > t)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool MoveAbsLine(int axisNo1, int axisNo2, double dValue1, double dValue2)
        {
            var Axis1 = _axisList[axisNo1];
            var Axis2 = _axisList[axisNo2];

            if (AxisAlarm(axisNo1) || AxisAlarm(axisNo2))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }

                MoveLineAbs(Axis1, Axis2, dValue1, dValue1);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool MoveAbsSpd(int axisNo, double Value, int spdselection, int Timeout)
        {
            var Axis = _axisList[axisNo];

            if (AxisAlarm(axisNo))
            {
                return false;
            }

            try
            {
                double speed = 0;
                if (spdselection == 0)
                {
                    speed = Axis.Para.SlowV;
                }
                else if (spdselection == 1)
                {
                    speed = Axis.Para.FastV;
                }
                if (!SetMotionParam(Axis, speed, Axis.Para.Accel)) { return false; }
                MovePtpAbs(Axis, Value);
                bool bAxisWait = false;
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref bAxisWait);
                    if (!bAxisWait) { break; }
                    if (Environment.TickCount > t) { break; }
                    Thread.Sleep(1);
                }

                ForceStop(Axis);

                if (Environment.TickCount > t)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool MoveRel(int axisNo, double Value, int Timeout)
        {
            var Axis = _axisList[axisNo];

            if (AxisAlarm(axisNo))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis, Axis.Para.FastV, Axis.Para.Accel)) { return false; }
                MovePtpRel(Axis, Value);
                bool bAxisWait = false;
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref bAxisWait);
                    if (!bAxisWait) { break; }
                    if (Environment.TickCount > t) { break; }
                    Thread.Sleep(1);
                }

                ForceStop(Axis);

                if (Environment.TickCount > t)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool MoveRelLine(int axisNo1, int axisNo2, double Value1, double Value2, int Timeout)
        {
            var Axis1 = _axisList[axisNo1];
            var Axis2 = _axisList[axisNo2];

            if (AxisAlarm(axisNo1) || AxisAlarm(axisNo2))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }

                MoveLineRel(Axis1, Axis2, Value1, Value2);
                bool bAxis1Wait = false, bAxis2Wait = false;
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis1, ref bAxis1Wait);
                    AxisBusy(Axis2, ref bAxis2Wait);
                    if (bAxis1Wait && bAxis2Wait) { break; }
                    if (Environment.TickCount > t) { break; }
                    Thread.Sleep(1);
                }

                ForceStop(Axis1);
                ForceStop(Axis2);

                if (Environment.TickCount > t)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool MoveRelLine(int axisNo1, int axisNo2, double dValue1, double dValue2)
        {
            var Axis1 = _axisList[axisNo1];
            var Axis2 = _axisList[axisNo2];

            if (AxisAlarm(axisNo1) || AxisAlarm(axisNo2))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }

                MoveLineRel(Axis1, Axis2, dValue1, dValue1);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool MoveRelSpd(int axisNo, double Value, int spdselection, int Timeout)
        {
            var Axis = _axisList[axisNo];

            if (AxisAlarm(axisNo))
            {
                return false;
            }

            try
            {
                double speed = 0;
                if (spdselection == 0)
                {
                    speed = Axis.Para.SlowV;
                }
                else if (spdselection == 1)
                {
                    speed = Axis.Para.FastV;
                }
                if (!SetMotionParam(Axis, speed, Axis.Para.Accel)) { return false; }
                MovePtpRel(Axis, Value);
                bool bAxisWait = false;
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref bAxisWait);
                    if (!bAxisWait) { break; }
                    if (Environment.TickCount > t) { break; }
                    Thread.Sleep(1);
                }

                ForceStop(Axis);

                if (Environment.TickCount > t)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool ForceStop(int axisNo)
        {
            var axis = _axisList[axisNo];
            ForceStop(axis);
            AxisWait(axisNo, false);
            return true;
        }

        public bool ReadInput(int axisNo, int Input)
        {
            var ip = _inputList[axisNo][(int)Input];
            UpdateInput(ref ip);
            return ip.Status;
        }

        public bool SetLogicPos(int axisNo, double Pos)
        {
            if (!Opened) { return false; }
            var axis = _axisList[axisNo];
            return SetLogicPos(axis, Pos);
        }

        public bool SetRealPos(int axisNo, double Pos)
        {
            if (!Opened) { return false; }
            var axis = _axisList[axisNo];
            return SetRealPos(axis, Pos);
        }

        public void AxisBusy(int axisNo, ref bool bAxisWait)
        {
            bool bBusy = false;
            var Axis = _axisList[axisNo];
            AxisBusy(Axis, ref bBusy);
            bAxisWait = bBusy;
        }

        internal bool AxisWait(int axisNo, bool Busy)
        {
            var axis = _axisList[axisNo];
            string EMsg = axis.Name + " AxisWait";

            if (AxisAlarm(axisNo)) { return false; }

            try
            {
                if (Busy)
                {
                    AxisBusy(axis, ref Busy);
                    while (Busy)
                    {
                        AxisBusy(axis, ref Busy);
                        Thread.Sleep(0);
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool HomeTimeout(int axisNo, int Timeout)
        {
            var Axis = _axisList[axisNo];
            if (GetTickCount() >= Timeout)
            {
                if (!ForceStop(Axis)) return false;

                return true;

            }

            return false;
        }

        public double GetLogicPos(int axisNo)
        {
            var axis = _axisList[axisNo];
            return GetLogicPos(axis);
        }

        public double GetRealPos(int axisNo)
        {
            var axis = _axisList[axisNo];
            return GetRealPos(axis);
        }

        public bool HomeBySensor_NPH(int axisNo)
        {
            var Axis = _axisList[axisNo];
            var _AlmClr = _outputList[axisNo][(int)OUTPUT.ALARM_CLEAR];

            #region Init
            bool PositiveDir = true;
            string EMsg = Axis.Name + " Home Positive";


            Func<int, bool> fnPositive, fnNegative = null;
            if (Axis.Para.Home.InvertHomeLmt)
            {
                fnPositive = SensLmtN;
                fnNegative = SensLmtP;
            }
            else
            {
                fnPositive = SensLmtP;
                fnNegative = SensLmtN;
            }

            if (Axis.Para.Home.Dir == Common.EHomeDir.N)
            {
                PositiveDir = false;
                EMsg = Axis.Name + " Home Negative";
            }
            try
            {
                HardwareLimitEnable(Axis, false);
            }
            catch { }
            Delay(50);
            #endregion

            #region Alarm Clear
            if (Axis.Para.MotorAlarmType != Common.EMotorAlarmType.None)
            {
                if (AxisAlarm(axisNo))
                {
                    if (!Servo(axisNo, false)) { goto _End; }
                    Delay(250);
                    MotorAlmReset(axisNo);
                }
            }

            Delay(50);
            MotorAlmReset(axisNo);
            Delay(50);
            ClearAxisError(Axis);
            Delay(50);
            //MtrBreak(axisNo, EOutputStatus.Hi); //Servo On/Off will handle
            if (!Servo(axisNo, true)) { goto _End; }
            Delay(100);
            if (AxisAlarm(axisNo)) { goto _End; }
            #endregion

            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
            ClearAxisError(Axis);
            Delay(500);

        _Forward:
            if (!SensHome(axisNo))
            #region Search Home
            {
                if (!SetMotionParam(Axis, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;

                if (PositiveDir)
                    JogP(Axis);
                else
                    JogN(Axis);

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) goto _End;
                    goto _End;
                }

                while (!SensHome(axisNo))
                {
                    if (PositiveDir && !fnPositive(axisNo) || !PositiveDir && !fnNegative(axisNo))
                    {
                        if (!ForceStop(Axis))
                            goto _End;
                        Thread.Sleep(1);
                        goto _Reverse;
                    }

                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (AxisAlarm(axisNo)) goto _End;
                    Thread.Sleep(1);
                    //Application.DoEvents();
                }
                //if (!ForceStop(Axis)) goto _End;
                if (!ForceStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
                goto _LeaveStep;
            }
        #endregion

        _Reverse:
            if (!SensHome(axisNo))
            #region Search Home Reverse
            {
                if (!SetMotionParam(Axis, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;

                if (PositiveDir)
                    JogN(Axis);
                else
                    JogP(Axis);

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) goto _End;
                    goto _End;
                }

                while (!SensHome(axisNo))
                {
                    if (PositiveDir && !fnNegative(axisNo) || !PositiveDir && !fnPositive(axisNo))
                    {
                        if (!ForceStop(Axis)) goto _End;
                        Thread.Sleep(1);
                        goto _Forward;
                    }
                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (AxisAlarm(axisNo)) goto _End;
                    Thread.Sleep(1);
                    //Application.DoEvents();
                }
                //if (!ForceStop(Axis)) goto _End;
                if (!ForceStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
                goto _LeaveStep;
            }

        #endregion

        _LeaveStep:
            Delay(500);
            if (SensHome(axisNo))
            #region Clear Home
            {
                if (!SetMotionParam(Axis, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
                if (PositiveDir)
                    JogN(Axis);
                else
                    JogP(Axis);

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) goto _End;
                    goto _End;
                }
                while (true)
                {
                    if (!SensHome(axisNo)) break;
                    Thread.Sleep(1);
                    //GDefine.AppProMsg();
                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (AxisAlarm(axisNo)) goto _End;
                }
                if (!ForceStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
                goto _TouchHome;
            }
        #endregion


        _TouchHome:
            Delay(500);
            if (!SensHome(axisNo))
            #region Touch Home
            {
                //if (!SetMotionParam(Axis, 1, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
                if (!SetMotionParam(Axis, 0.2, 1)) goto _End;
                if (PositiveDir)
                    JogP(Axis);
                else
                    JogN(Axis);
                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) goto _End;
                    goto _End;
                }

                while (true)
                {
                    if (SensHome(axisNo))
                    {
                        if (!ForceStop(Axis)) goto _End;
                        Thread.Sleep(1);
                        break;
                    }
                    Thread.Sleep(1);
                    //GDefine.AppProMsg();
                    if (HomeTimeout(axisNo, t))
                    {
                        Thread.Sleep(1);
                        goto _End;
                    }
                    if (AxisAlarm(axisNo)) goto _End;
                }
                if (!ForceStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
                goto _Done;
            }

        _Done:
            if (!Delay(200)) goto _End;
            #endregion
            Delay(50);
            if (AxisAlarm(axisNo)) goto _End;
            #region Set Param
            Delay(200);
            SetLogicPos(Axis, 0);
            SetRealPos(Axis, 0);
            UpdateAxis(Axis);
            #endregion

            return true;
        _End:
            //UpdateAxis(Axis);
            return false;
        }

        public bool HomeBySensor_H(int axisNo)
        {
            var Axis = _axisList[axisNo];
            bool PositiveDir = true;
            string EMsg = Axis.Name + " Home Positive";

            if (Axis.Para.Home.Dir == Common.EHomeDir.N)
            {
                PositiveDir = false;
                EMsg = Axis.Name + " Home Negative";
            }
            try
            {
                HardwareLimitEnable(Axis, false);
            }
            catch { }
            Delay(50);

            #region Alarm Clear
            if (Axis.Para.MotorAlarmType != Common.EMotorAlarmType.None)
            {
                if (AxisAlarm(axisNo))
                {
                    if (!Servo(axisNo, false)) { goto _End; }
                    Delay(250);
                    MotorAlmReset(axisNo);
                }
            }
            #endregion
            Delay(50);
            MotorAlmReset(axisNo);
            Delay(50);
            ClearAxisError(Axis);
            Delay(50);
            if (!Servo(axisNo, true)) { goto _End; }
            Delay(50);
            if (AxisAlarm(axisNo)) { goto _End; }

            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
        _Rescan:
            ClearAxisError(Axis);
            Delay(100);
            if (SensLmtP(axisNo))
            #region Clear Home
            {

                Delay(50);
                if (!SetMotionParam(Axis, Axis.Para.Home.FastV, Axis.Para.Accel)) { goto _End; }
                if (PositiveDir)
                {
                    JogN(Axis);
                }
                else
                {
                    JogP(Axis);
                }

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (true)
                {
                    if (SensHome(axisNo)) break;
                    Delay(1);
                    if (HomeTimeout(axisNo, t)) { goto _End; }
                    if (AxisAlarm(axisNo)) { goto _End; }
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }

            }
            #endregion
            Delay(50);
            if (!SensHome(axisNo))
            #region Search Home
            {
                if (!SetMotionParam(Axis, Axis.Para.Home.FastV, Axis.Para.Accel)) { goto _End; }

                if (PositiveDir)
                {
                    JogP(Axis);
                }
                else
                {
                    JogN(Axis);
                }

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (!SensHome(axisNo))
                {
                    if (SensLmtP(axisNo))
                    {
                        if (!ForceStop(Axis)) { goto _End; }
                        goto _Rescan;
                    }
                    if (HomeTimeout(axisNo, t)) { goto _End; }
                    if (AxisAlarm(axisNo)) { goto _End; }
                    Thread.Sleep(1);
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }
            }
            #endregion
            Delay(50);
            if (SensHome(axisNo))
            #region Clear Home
            {
                if (!SetMotionParam(Axis, Axis.Para.Home.SlowV, Axis.Para.Accel)) { goto _End; }
                if (PositiveDir)
                {
                    JogN(Axis);
                }
                else
                {
                    JogP(Axis);
                }

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (true)
                {
                    if (!SensHome(axisNo)) { break; }
                    Thread.Sleep(1);
                    if (HomeTimeout(axisNo, t)) { goto _End; }
                    if (AxisAlarm(axisNo)) { goto _End; }
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }
            }
            #endregion
            Delay(50);
            if (!SensHome(axisNo))
            #region Touch Home
            {
                if (!SetMotionParam(Axis, 0.2, 1)) { goto _End; }
                if (PositiveDir)
                {
                    JogP(Axis);
                }
                else
                {
                    JogN(Axis);
                }
                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (true)
                {
                    if (SensHome(axisNo))
                    {
                        if (!ForceStop(Axis)) { goto _End; }
                        break;
                    }
                    Thread.Sleep(1);
                    if (HomeTimeout(axisNo, t))
                    {
                        goto _End;
                    }
                    if (AxisAlarm(axisNo)) { goto _End; }
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }
            }
            if (!Delay(200)) { goto _End; }
            #endregion
            Delay(50);
            if (AxisAlarm(axisNo)) { goto _End; }
            Delay(200);
            #region Set Param
            Delay(200);
            SetLogicPos(Axis, 0);
            SetRealPos(Axis, 0);
            UpdateAxis(Axis);
            #endregion
            return true;
        _End:
            return false;
        }

        public void GetMotorAccelRange(int axisNo, ref double Min, ref double Max)
        {
            var axis = _axisList[axisNo];
            GetMotorAccelMinMax(axis, ref Min, ref Max);
        }

        public void GetMotorSpeedRange(int axisNo, ref double Min, ref double Max)
        {
            var axis = _axisList[axisNo];
            GetMotorSpeedRange(axis, ref Min, ref Max);
        }

        public bool Home(int axisNo, ref Common.TInput _SensHome, ref Common.TInput _SensLmtP, ref Common.TInput _SensLmtN, Common.TInput _Alm, ref Common.TOutput _MtrOn, ref Common.TOutput _AlmClr)
        {
            var Axis = _axisList[axisNo];
            bool PositiveDir = true;
            string EMsg = Axis.Name + " Home Positive";

            if (Axis.Para.Home.Dir == Common.EHomeDir.N)
            {
                PositiveDir = false;
                EMsg = Axis.Name + " Home Negative";
            }
            try
            {
                HardwareLimitEnable(Axis, false);                
            }
            catch { }
            Delay(50);

            #region Alarm Clear
            if (Axis.Para.MotorAlarmType != Common.EMotorAlarmType.None)
            {
                if (AxisAlarm(axisNo))
                {
                    if (!Servo(axisNo, false)) { goto _End; }
                    Delay(250);
                    MotorAlmReset(axisNo);
                }
            }
            #endregion
            Delay(50);
            MotorAlmReset(axisNo);
            Delay(50);
            ClearAxisError(Axis);
            Delay(50);
            if (!Servo(axisNo, true)) { goto _End; }
            Delay(50);
            if (AxisAlarm(axisNo)) { goto _End; }

            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
            _Rescan:
            ClearAxisError(Axis);
            Delay(100);
            if (SensLmtP(axisNo))
            #region Clear Home
            {

                Delay(50);
                if (!SetMotionParam(Axis, Axis.Para.Home.FastV, Axis.Para.Accel)) { goto _End; }
                if (PositiveDir)
                {
                    JogN(Axis);
                }
                else
                {
                    JogP(Axis);
                }

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (true)
                {
                    if (SensHome(axisNo)) break;
                    Delay(1);
                    if (HomeTimeout(axisNo, t)) { goto _End; }
                    if (AxisAlarm(axisNo)) { goto _End; }
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }
                
            }
            #endregion
            Delay(50);
            if (!SensHome(axisNo))
            #region Search Home
            {
                if (!SetMotionParam(Axis, Axis.Para.Home.FastV, Axis.Para.Accel)) { goto _End; }

                if (PositiveDir)
                {
                    JogP(Axis);
                }
                else
                {
                    JogN(Axis);
                }

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (!SensHome(axisNo))
                {
                    if (SensLmtP(axisNo))
                    {
                        if (!ForceStop(Axis)) { goto _End; }
                        goto _Rescan;
                    }
                    if (HomeTimeout(axisNo, t)) { goto _End; }
                    if (AxisAlarm(axisNo)) { goto _End; }
                    Thread.Sleep(1);
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }
            }
            #endregion
            Delay(50);
            if (SensHome(axisNo))
            #region Clear Home
            {
                if (!SetMotionParam(Axis, Axis.Para.Home.SlowV, Axis.Para.Accel)) { goto _End; }
                if (PositiveDir)
                {
                    JogN(Axis);
                }                    
                else
                {
                    JogP(Axis);
                }

                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (true)
                {
                    if (!SensHome(axisNo)) { break; }
                    Thread.Sleep(1);
                    if (HomeTimeout(axisNo, t)) { goto _End; }
                    if (AxisAlarm(axisNo)) { goto _End; }
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }
            }
            #endregion
            Delay(50);
            if (!SensHome(axisNo))
            #region Touch Home
            {
                if (!SetMotionParam(Axis, 0.2, 1)) { goto _End; }
                if (PositiveDir)
                {
                    JogP(Axis);
                }
                else
                {
                    JogN(Axis);
                }
                if (AxisAlarm(axisNo))
                {
                    if (!ForceStop(Axis)) { goto _End; }
                    goto _End;
                }
                while (true)
                {
                    if (SensHome(axisNo))
                    {
                        if (!ForceStop(Axis)) { goto _End; }
                        break;
                    }
                    Thread.Sleep(1);
                    if (HomeTimeout(axisNo, t))
                    {
                        goto _End;
                    }
                    if (AxisAlarm(axisNo)) { goto _End; }
                }
                if (!ForceStop(Axis)) { goto _End; }
                if (!AxisWait(axisNo, false)) { goto _End; }
            }
            if (!Delay(200)) { goto _End; }
            #endregion
            Delay(50);
            if (AxisAlarm(axisNo)) { goto _End; }
            Delay(200);
            #region Set Param
            Delay(200);
            SetLogicPos(Axis, 0);
            SetRealPos(Axis, 0);
            UpdateAxis(Axis);
            #endregion
            return true;
        _End:
            return false;
        }

        public void SaveRecipePosition(string RecipeName)
        {
            string FileName = FilePath.teachPath + RecipeName + ".ini";
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(FileName);

            foreach (var axis in _axisList)
            {
                for (int i = 0; i < axis.AxisPosName.Count; i++)
                {
                    if (!String.IsNullOrEmpty(axis.AxisPosName[i]))
                    {
                        IniFile.WriteDouble(axis.Name, axis.AxisPosName[i], axis.AxisPos[i]);
                    }
                }
            }
        }

        public void LoadRecipePosition(string RecipeName)
        {
            string FileName = FilePath.teachPath + RecipeName + ".ini";
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(FileName);

            foreach (var axis in _axisList)
            {
                for (int i = 0; i < axis.AxisPosName.Count; i++)
                {
                    if (!String.IsNullOrEmpty(axis.AxisPosName[i]))
                    {
                        axis.AxisPos[i] = IniFile.ReadDouble(axis.Name, axis.AxisPosName[i], axis.AxisPos[i]);
                    }
                }
            }
        }

        public void MotorAlmReset(int axisNo)
        {
            if (!Opened) { return; }
            var almClr = _outputList[axisNo][(int)OUTPUT.ALARM_CLEAR];
            SetDO(ref almClr, EOutputStatus.Hi);
            Thread.Sleep(500);
            SetDO(ref almClr, EOutputStatus.Lo);
        }

        public void OpenGalilBoard()
        {
            try
            {
                OpenConnection();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }        

        public void SaveMotorParameters()
        {
            SaveMotorParam();
        }

        public void SaveRecipePositon(string RecipeName)
        {
            throw new NotImplementedException();
        }

        public bool SensAlarm(int axisNo)
        {
            if (!Opened) { return false; }
            var axis = _axisList[axisNo];
            var ipAlarm = _inputList[axisNo][(int)INPUT.ALARM];
            UpdateInput(ref ipAlarm);
            return ipAlarm.Status;
        }

        public bool SensHome(int axisNo)
        {
            if (!Opened) { return false; }
            var axis = _axisList[axisNo];
            var ipHome = _inputList[axisNo][(int)INPUT.SENSE_HOME];
            UpdateInput(ref ipHome);
            return ipHome.Status;
        }

        public bool SensHome(int axisNo, SwitchType type)
        {
            if (!Opened) { return false; }
            bool Res = false;
            string strRes = "";
            switch (type)
            {
                case SwitchType.TellSwitch:
                    {
                        Res = SendCommandGetResponse(string.Format("MG_TS{0}", (AXIS_NO)axisNo), out strRes);
                        if (!Res)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            var dStatus = double.Parse(strRes);
                            int iStatus = (int)dStatus;
                            Decimal2Binary(iStatus, ref strRes);
                        }
                        Res = SwitchResult(TS.HOME, strRes);
                    }
                    break;
                case SwitchType.DirectCommand:
                    {
                        int _iStatus = '0';
                        Res = SendCommandGetResponse(string.Format("MG_HM{0}", (AXIS_NO)axisNo), out strRes);
                        if (!Res)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            var dStatus = double.Parse(strRes);
                            _iStatus = (int)dStatus;
                        }
                        if (_iStatus == 1)
                        {
                            Res = true;
                        }
                        else
                        {
                            Res = false;
                        }
                    }
                    break;
            }
            return Res;
        }

        public bool SensInPos(int axisNo)
        {
            return false;
        }

        internal bool AxisAlarm(int axisNo)
        {
            var axis = _axisList[axisNo];
            var ipalarm = _inputList[axisNo][(int)INPUT.ALARM];

            UpdateInput(ref ipalarm);
            return AxisAlarm(axis, ref ipalarm);
        }

        public bool SensLmtN(int axisNo)
        {
            if (!Opened) { return false; }
            var axis = _axisList[axisNo];
            var ipLmtN = _inputList[axisNo][(int)INPUT.N_LIMIT];
            UpdateInput(ref ipLmtN);
            return ipLmtN.Status;
        }

        public bool SensLmtN(int axisNo, SwitchType type)
        {
            if (!Opened) { return false; }
            bool Res = false;
            string strRes = "";
            switch (type)
            {
                case SwitchType.TellSwitch:
                    {
                        Res = SendCommandGetResponse(string.Format("MG_TS{0}", (AXIS_NO)axisNo), out strRes);
                        if (!Res)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            var dStatus = double.Parse(strRes);
                            int iStatus = (int)dStatus;
                            Decimal2Binary(iStatus, ref strRes);
                        }
                        Res = SwitchResult(TS.N_LIMIT, strRes);
                    }
                    break;
                case SwitchType.DirectCommand:
                    {
                        int _iStatus = '0';

                        Res = SendCommandGetResponse(string.Format("MG_LR{0}", (AXIS_NO)axisNo), out strRes);
                        if (!Res)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            var dStatus = double.Parse(strRes);
                            _iStatus = (int)dStatus;
                        }
                        if (_iStatus == 1)
                        {

                            Res = true;
                        }
                        else
                        {
                            Res = false;
                        }
                    }
                    break;
            }
            return Res;
        }

        public bool SensLmtP(int axisNo)
        {
            if (!Opened) { return false; }
            var axis = _axisList[axisNo];
            var ipLmtP = _inputList[axisNo][(int)INPUT.P_LIMIT];
            UpdateInput(ref ipLmtP);
            return ipLmtP.Status;
        }

        public bool SensLmtP(int axisNo, SwitchType type)
        {
            if (!Opened) { return false; }
            bool Res = false;
            string strRes = "";
            switch (type)
            {
                case SwitchType.TellSwitch:
                    {
                        Res = SendCommandGetResponse(string.Format("MG_TS{0}", (AXIS_NO)axisNo), out strRes);
                        if (!Res)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            var dStatus = double.Parse(strRes);
                            int iStatus = (int)dStatus;
                            Decimal2Binary(iStatus, ref strRes);
                        }
                        Res = SwitchResult(TS.P_LIMIT, strRes);
                    }
                    break;
                case SwitchType.DirectCommand:
                    {
                        int _iStatus = '0';

                        Res = SendCommandGetResponse(string.Format("MG_LF{0}", (AXIS_NO)axisNo), out strRes);
                        if (!Res)
                        {
                            throw new Exception();
                        }
                        else
                        {
                            var dStatus = double.Parse(strRes);
                            _iStatus = (int)dStatus;
                        }
                        if (_iStatus == 1)
                        {

                            Res = true;
                        }
                        else
                        {
                            Res = false;
                        }
                    }
                    break;
            }
            return Res;
        }

        public bool Servo(int axisNo, bool On)
        {
            var Axis = _axisList[axisNo];          
            
            bool res = false;
            if (!Opened) { return false; }
            if (On)
            {
                res = ServoOn(Axis);
            }
            else
            {
                res = ServoOff(Axis);
            }

            return res;
        }

        #endregion

        public delegate void ProcessMessage(string strMessage);

        public ProcessMessage fnMessageCallBack = null;

        public bool IsConnected()
        {
            return Opened;
        }

        public bool OpenConnection()
        {
            try
            {
                g.address = _device.IPAddress;
                g.connection();
            }
            catch
            {
                MessageBox.Show("Galil Failed To Established Connection!");
                return false;
            }

            Opened = true;
            return true;
        }

        public void CloseConnection()
        {

        }

        public bool DownloadProgram(string file)
        {
            try
            {
                g.programDownloadFile(file);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public double GetValue(string command)
        {
            double dValue = 0.0;
            try
            {
                dValue = g.commandValue(command);
            }
            catch
            {
                return -1.0;
            }

            return dValue;
        }

        public bool SendCommand(string command)
        {
            string res = "";
            Mutex.WaitOne();
            try
            {
                res = g.command(command, "\r", ":", true);
            }
            catch
            {
                return false;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }

            return res == "" || res == ":";
        }

        public bool SendCommandGetResponse(string command, out string res)
        {
            res = "";
            Mutex.WaitOne();
            try
            {
                res = g.command(command, "\r", ":", true);
            }
            catch
            {
                return false;
            }
            finally
            {
                Mutex.ReleaseMutex();
            }

            return true;
        }

        public bool ExecuteCommand(string command, int thread = 0)
        {
            return SendCommand(string.Format("XQ#{0},{1}", command, thread));
        }

        public bool CheckMotionCompleted(char command, int timeout, int debounce = 1)
        {
            double dValue = 0.0;
            var t = Environment.TickCount + timeout;
            do
            {
                string str = "";
                SendCommandGetResponse(string.Format("MG_BG{0}", command), out str);
                dValue = Convert.ToDouble(str);

                if (Environment.TickCount > t)
                {
                    return false;
                }

                Delay(debounce);
            }
            while (dValue > 0.0);


            return true;
        }

        public bool AxisBusy(int axisNo)
        {
            double dValue = 0.0;
            try
            {
                string str = "";
                SendCommandGetResponse(string.Format("MG_BG{0}", (AXIS_NO)axisNo), out str);
                dValue = Convert.ToDouble(str);
                if (dValue > 0.0)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            };
        }

        public bool Configure(CN cn)
        {
            var m = cn.LimitSwitch > -2 ? cn.LimitSwitch.ToString() : "";
            var n = cn.HomeSwitch > -2 ? cn.HomeSwitch.ToString() : "";
            var o = cn.LatchInput > -2 ? cn.LatchInput.ToString() : "";
            var p = cn.GenInputs > -2 ? cn.GenInputs.ToString() : "";
            var q = cn.AbortInput > -2 ? cn.AbortInput.ToString() : "";

            return SendCommand(string.Format("CN {0},{1},{2},{3},{4}", m, n, o, p, q));
        }

        private void LoadMotorPara()
        {
            string FileName = FilePath.iniFilePath + "\\" + FilePath.axisconfigIni;
            ES.Net.IniFile IniFile = new ES.Net.IniFile();

            foreach (var axis in _axisList)
            {
                axis.Para.ReadInifile(FileName, axis.Name);
            }

            if (!File.Exists(FileName))
            {
                foreach (var axis in _axisList)
                {
                    axis.Para.Encoder = true;
                    axis.Para.Multiplier = 100;
                    axis.Para.Unit.Resolution = 0.001;
                    axis.Para.SwLimit.PosP = 1000;
                    axis.Para.SwLimit.PosN = -1000;
                    axis.Para.Jog.SlowV = 1;
                    axis.Para.Jog.MedV = 20;
                    axis.Para.Jog.FastV = 50;
                    axis.Para.Home.SlowV = 1;
                    axis.Para.Home.FastV = 25;
                    axis.Para.Home.Timeout = 15000;
                    axis.Para.Accel = 500;
                    axis.Para.StartV = 10;
                    axis.Para.SlowV = 50;
                    axis.Para.SlowV = 100;
                    axis.Para.FastV = 300;
                }
            }

            foreach (var axis in _axisList)
            {
                UpdateAxisPara(axis);
            }
        }       

        private void SaveMotorParam()
        {

            string FileName = FilePath.iniFilePath + FilePath.axisconfigIni;
            ES.Net.IniFile IniFile = new ES.Net.IniFile();
            IniFile.Create(FileName);

            if (!File.Exists(FileName))
            {
                LoadMotorPara();
            }
            foreach (var axis in _axisList)
            {
                axis.Para.WriteInifile(FileName, axis.Name);
            }
            foreach (var axis in _axisList)
            {
                UpdateAxis(axis);
            }

        }

        private void Decimal2Binary(int deci, ref string strTx)
        {
            strTx = Convert.ToString(deci, 2);
        }

        private bool ConvertBit2Bool(char bit)
        {
            if (bit == '0')
            {
                return false;
            }
            return true;
        }

        private bool SwitchResult(TS tellSwitch, string strRes)
        {
            char[] arrRes = strRes.ToCharArray();
            bool res = false;
            res = ConvertBit2Bool(arrRes[(int)tellSwitch]);
            return res;
        }
    }
}
