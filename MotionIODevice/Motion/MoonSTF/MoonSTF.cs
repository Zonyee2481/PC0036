using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;
using Infrastructure;
using Microsoft.Office.Interop.Excel;

namespace MotionIODevice
{
    public class MoonSTF : Common , IMotionControl
    {
        const int MAX_AXIS = 32, MAX_INPUT = 8, MAX_OUTPUT = 4;
        eSCLLibHelper m_eSCLLibHelper = null;
        private bool[] Axis_Opened = Enumerable.Repeat(true, MAX_AXIS).ToArray();
        private List<Common.TAxis> _axisList = new List<Common.TAxis>();
        private List<List<Common.TInput>> _inputList = new List<List<Common.TInput>>();
        private List<List<Common.TOutput>> _outputList = new List<List<Common.TOutput>>();

        public enum INPUT
        {
            IN1,
            IN2,
            P_LIMIT,
            N_LIMIT,
            IN3,
            IN4,
            SENSE_HOME,
            IN5
        }

        public enum OUTPUT
        {
            SPARE_OP1,
            BREAK_OFF,
            MTR_ON,
            SPARE_OP2,
        }

        private bool bBenchDebug = false;
        Mutex Mutex = new Mutex();

        uint DevCount = 0;

        double[] UPP = new double[MAX_AXIS];// { { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 }, { 1, 1, 1, 1, 1, 1, 1, 1 } };
        bool[] InvertPulse = new bool[MAX_AXIS];// { { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false }, { false, false, false, false, false, false } };

        double[] StartV = new double[MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[] DriveV = new double[MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[] Accel = new double[MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[] Decel = new double[MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };
        double[] Jerk = new double[MAX_AXIS];// { { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 } };       

        public int GetAxisNum { get { return _axisList.Count; } }

        public int GetInputNum { get { return _inputList.Count; } }

        public int GetOutputNum { get { return _outputList.Count; } }

        public List<TAxis> AxisList { get { return _axisList; } }

        public MoonSTF(List<TAxis> _Axis, List<TRunModule> RunInfoList)
        {
            m_eSCLLibHelper = new eSCLLibHelper();
            _axisList = _Axis.ToList();

            for (int i = 0; i < _axisList.Count() ; i++)
            {
                List<TInput> ipList = new List<TInput>();
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 0, "", ""));
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 1, "", ""));
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 2, "", ""));
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 3, "", ""));
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 4, "", ""));
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 5, "", ""));
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 6, "", ""));
                ipList.Add(new Common.TInput(_Axis[i].Device, (byte)i, 7, "", ""));
                _inputList.Add(ipList);

                List<TOutput> opList = new List<TOutput>();
                opList.Add(new Common.TOutput(_Axis[i].Device, (byte)i, 1, "", ""));
                opList.Add(new Common.TOutput(_Axis[i].Device, (byte)i, 2, "", ""));
                opList.Add(new Common.TOutput(_Axis[i].Device, (byte)i, 3, "", ""));
                opList.Add(new Common.TOutput(_Axis[i].Device, (byte)i, 4, "", ""));
                _outputList.Add(opList);

                var axis = _axisList[i];
                axis.RunModule = RunInfoList[i].RunModule;
                axis.RunAxis = RunInfoList[i].MtrAxis;
                axis.AxisPosName = RunInfoList[i].AxisPosName;
                axis.AxisPos = RunInfoList[i].AxisPos;
                axis.ZEnable = RunInfoList[i].ZEnable;
                axis.CardNo = RunInfoList[i].CardNo;
                _axisList[i] = axis;
            }

            //_inputList = inputs.ToList();
            //_outputList = outputs.ToList();

            for (int j = 1; j < MAX_AXIS; j++)
            {
                UPP[j] = 1;
                InvertPulse[j] = false;
                StartV[j] = 0;
                DriveV[j] = 0;
                Accel[j] = 0;
                Decel[j] = 0;
            }
        }   
        
        #region IMotionControls
        bool IMotionControl.OpenBoard()
        {
            OpenMoonsBoard();
            LoadMotorPara();
            return true;
        }
        
        private void OpenMoonsBoard()
        {
            try
            {
                
                m_eSCLLibHelper.ClearAllAxes();
                for (int i = 0; i < _axisList.Count; i++)
                {
                    AddAxis(_axisList[i].Device.IPAddress);
                }
                OpenTCPDevice();
                

            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };            
        }

        internal void LoadMotorPara()
        {
            string FileName = FilePath.iniFilePath + FilePath.axisconfigIni;
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

        bool IMotionControl.CloseBoard()
        {
            CloseDevice();
            return true;
        }

        bool IMotionControl.ReadBit(int bit)
        {            
            int point, axis = Math.DivRem(bit, MAX_INPUT, out point);
            var input = _inputList[axis][point];
            UpdateInput(ref input);

            if (!Axis_Opened[input.Axis_Port]/* && !bBenchDebug*/) return false;
            _inputList[axis][point] = input;

            return input.Status;
        }

        bool IMotionControl.OutBit(int bit, EOutputStatus status)
        {
            int point, axis = Math.DivRem(bit, MAX_OUTPUT, out point);
            var output = _outputList[axis][point];

            var Axis = _axisList[axis];

            if (!Axis_Opened[output.Axis_Port]/* && !bBenchDebug*/) return false;
            switch (status)
            {
                case EOutputStatus.Hi:

                    if (!bBenchDebug || true)
                    {
                        WriteOutput(ref Axis, output, true);
                    }
                    else
                    {
                        output.Status = true;
                    }

                    break;
                case EOutputStatus.Lo:

                    if (!bBenchDebug || true)
                    {
                        WriteOutput(ref Axis, output, false);
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
            
        bool IMotionControl.Home(int Axis, HomingType motorType)
        {
            bool bMotion = false;
            if (motorType == HomingType.NPHType)
            {
                bMotion = HomeBySensor_NPH(Axis);
            }
            else if (motorType == HomingType.LimitType)
            {
                bMotion = HomeBySensor_L(Axis);
            }
            return bMotion;
        }

        private bool HomeBySensor_NPH(int axisNo)
        {
            var Axis = _axisList[axisNo];

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
            
            Delay(50);
            #endregion

            #region Alarm Clear
            if (Axis.Para.MotorAlarmType != Common.EMotorAlarmType.None)
            {
                if (CheckMtrIsInAlarm(Axis))
                {
                    Servo(axisNo, false);
                    Delay(250);
                    MotorAlmReset(axisNo);
                }
            }

            Delay(50);
            //MotorBrake(Axis, true);
            if (!Servo(axisNo, true)) goto _End;
            Delay(50);
            if (CheckMtrIsInAlarm(Axis)) goto _End;

            #endregion
            JogSpeed(Axis, Axis.Para.Jog.SlowV, Axis.Para.Psnt.Accel, Axis.Para.Psnt.Accel);
            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
            MotorAlmReset(axisNo);
            Delay(200);

        _Forward:
            if (!SensHome(axisNo))
            #region Search Home
            {
                //if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;

                if (PositiveDir)
                    JogP(Axis);
                else
                    JogN(Axis);

                if (CheckMtrIsInAlarm(Axis))
                {
                    ForceStop(Axis);
                    goto _End;
                }

                while (!SensHome(axisNo))
                {
                    if (PositiveDir && !fnPositive(axisNo) || !PositiveDir && !fnNegative(axisNo))
                    {
                        ForceStop(Axis);
                        Thread.Sleep(1);
                        goto _Reverse;
                    }

                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (CheckMtrIsInAlarm(Axis)) goto _End;
                    Thread.Sleep(1);
                    //Application.DoEvents();
                }
                //if (!ForceStop(Axis)) goto _End;
                ForceStop(Axis);
                AxisInPos(Axis);//Wait busy is off
                goto _LeaveStep;
            }
        #endregion

        _Reverse:
            if (!SensHome(axisNo))
            #region Search Home Reverse
            {                
                if (PositiveDir)
                    JogN(Axis);
                else
                    JogP(Axis);

                if (CheckMtrIsInAlarm(Axis))
                {
                    ForceStop(Axis);
                    goto _End;
                }

                while (!SensHome(axisNo))
                {
                    if (PositiveDir && !fnNegative(axisNo) || !PositiveDir && !fnPositive(axisNo))
                    {
                        ForceStop(Axis);
                        Thread.Sleep(1);
                        goto _Forward;
                    }
                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (CheckMtrIsInAlarm(Axis)) goto _End;
                    Thread.Sleep(1);
                    //Application.DoEvents();
                }
                //if (!ForceStop(Axis)) goto _End;
                ForceStop(Axis);
                AxisInPos(Axis);//Wait busy is off
                goto _LeaveStep;
            }
            #endregion
            Delay(200);

        _LeaveStep:
            if (SensHome(axisNo))
            #region Clear Home
            {                
                if (PositiveDir)
                    JogN(Axis);
                else
                    JogP(Axis);

                if (CheckMtrIsInAlarm(Axis))
                {
                    ForceStop(Axis);
                    goto _End;
                }
                while (true)
                {
                    if (!SensHome(axisNo)) break;
                    Thread.Sleep(1);
                    //GDefine.AppProMsg();
                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (CheckMtrIsInAlarm(Axis)) goto _End;
                }
                ForceStop(Axis);
                AxisInPos(Axis);//Wait busy is off
                goto _TouchHome;
            }
            Delay(50);
            #endregion

        _TouchHome:
            Delay(500);
            if (!SensHome(axisNo))
            #region Touch Home
            {
                //if (!SetMotionParam(Axis, 1, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
                //LoadSlowPara(Axis);
                JogSpeed(Axis, 0.2, 0.2, 1);

                if (PositiveDir)
                    JogP(Axis);
                else
                    JogN(Axis);
                if (CheckMtrIsInAlarm(Axis))
                {
                    ForceStop(Axis);
                    goto _End;
                }

                while (true)
                {
                    if (SensHome(axisNo))
                    {
                        ForceStop(Axis);
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
                    if (CheckMtrIsInAlarm(Axis))
                    {
                        ForceStop(Axis);
                        goto _End;
                    }
                }
                ForceStop(Axis);
                AxisInPos(Axis);//Wait busy is off
                goto _Done;
            }

            #endregion
        _Done:
            if (!Delay(200)) goto _End;
            Delay(50);
            if (CheckMtrIsInAlarm(Axis)) goto _End;
            #region Set Param
            Delay(200);
            SetLCntr(Axis, 0);
            //SetRealPos(Axis, 0);
            //UpdateAxisPara(Axis);
            #endregion

            return true;
        _End:
            ForceStop(Axis);
            return false;
        }

        public bool HomeBySensor_L(int axisNo)
        {
            var Axis = _axisList[axisNo];

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

            Delay(50);
            #endregion

            #region Alarm Clear
            if (Axis.Para.MotorAlarmType != Common.EMotorAlarmType.None)
            {
                if (CheckMtrIsInAlarm(Axis))
                {
                    Servo(axisNo, false);
                    Delay(250);
                    MotorAlmReset(axisNo);
                }
            }

            Delay(50);
            //MotorBrake(Axis, true);
            if (!Servo(axisNo, true)) goto _End;
            Delay(50);
            if (CheckMtrIsInAlarm(Axis)) goto _End;

            #endregion
            JogSpeed(Axis, Axis.Para.Jog.SlowV, Axis.Para.Psnt.Accel, Axis.Para.Psnt.Accel);
            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
            MotorAlmReset(axisNo);
            Delay(200);

            #region Search Limit                      
            if (PositiveDir)
                JogP(Axis);
            else
                JogN(Axis);

            if (CheckMtrIsInAlarm(Axis))
            {
                ForceStop(Axis);
                goto _End;
            }

            while (true)
            {
                if (PositiveDir && !fnPositive(axisNo) || !PositiveDir && !fnNegative(axisNo))
                {

                    ForceStop(Axis);
                    AxisInPos(Axis);
                    goto _LeaveStep;
                }
                if (HomeTimeout(axisNo, t)) goto _End;
                if (CheckMtrIsInAlarm(Axis)) goto _End;
                Thread.Sleep(1);
            }


        #endregion

        #region Leave Step

        _LeaveStep:
            Delay(500);
            if (PositiveDir)
                JogN(Axis);
            else
                JogP(Axis);

            if (CheckMtrIsInAlarm(Axis))
            {
                ForceStop(Axis);
                goto _End;
            }
            while (true)
            {
                if (PositiveDir && fnPositive(axisNo) || !PositiveDir && fnNegative(axisNo)) break;
                Thread.Sleep(1);
                //GDefine.AppProMsg();
                if (HomeTimeout(axisNo, t)) goto _End;
                if (CheckMtrIsInAlarm(Axis)) goto _End;
            }
            ForceStop(Axis);
            AxisInPos(Axis);//Wait busy is off
            goto _TouchLimit;

        #endregion

            #region Touch Limit
        _TouchLimit:
            Delay(500);

            //if (!SetMotionParam(Axis, 1, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
            //LoadSlowPara(Axis);
            JogSpeed(Axis, 0.2, 0.2, 1);

            if (PositiveDir)
                JogP(Axis);
            else
                JogN(Axis);
            if (CheckMtrIsInAlarm(Axis))
            {
                ForceStop(Axis);
                goto _End;
            }

            while (true)
            {
                if (PositiveDir && !fnPositive(axisNo) || !PositiveDir && !fnNegative(axisNo))
                {
                    ForceStop(Axis);
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
                if (CheckMtrIsInAlarm(Axis))
                {
                    ForceStop(Axis);
                    goto _End;
                }
            }
            ForceStop(Axis);
            AxisInPos(Axis);//Wait busy is off
            goto _Done;


        #endregion
        _Done:
            if (!Delay(200)) goto _End;
            Delay(50);
            if (CheckMtrIsInAlarm(Axis)) goto _End;
            #region Set Param
            Delay(200);
            SetLCntr(Axis, 0);
            return true;
        //SetRealPos(Axis, 0);
        //UpdateAxisPara(Axis);
        #endregion


        #endregion

        _End:
            ForceStop(Axis);
            return false;
        }

        bool IMotionControl.MoveAbs(int axis, double Value, int Timeout)
        {
            var Axis = _axisList[axis];


            if (CheckMtrIsInAlarm(Axis))
            {
                return false;
            }

            try
            {                
                MovePtpAbs(Axis, Value, Axis.Para.FastV, Axis.Para.Accel, Axis.Para.Accel);
                if(!MoveDone(Axis, Timeout)) return false ;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        bool IMotionControl.MoveAbsSpd(int axis, double Value, int spdselection, int Timeout)
        {
            var Axis = _axisList[axis];


            if (CheckMtrIsInAlarm(Axis))
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
                else if (spdselection == 2)
                {
                    speed = Axis.Para.MedV;
                }

                MovePtpAbs(Axis, Value, speed, Axis.Para.Accel, Axis.Para.Accel);
                if (!MoveDone(Axis, Timeout)) return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        bool IMotionControl.MoveRel(int axis, double Value, int Timeout)
        {
            var Axis = _axisList[axis];
            
            if (CheckMtrIsInAlarm(Axis))
            {
                return false;
            }

            try
            {
                
                MovePtpRel(Axis, Value, Axis.Para.FastV, Axis.Para.Accel, Axis.Para.Accel);

                if (!MoveDone(Axis, Timeout)) return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }

        bool IMotionControl.MoveRelSpd(int axis, double Value, int spdselection, int Timeout)
        {
            var Axis = _axisList[axis];

            if (CheckMtrIsInAlarm(Axis))
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
                MovePtpRel(Axis, Value, speed, Axis.Para.Accel, Axis.Para.Accel);

                if (!MoveDone(Axis, Timeout)) return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
        }
        
        bool IMotionControl.Jog(int axis, bool bPositive, bool bSlow = false)
        {
            var Axis = _axisList[axis];

            if (CheckMtrIsInAlarm(Axis))
            {
                return false;
            }            

            try
            {
                if (bSlow == false)
                {
                    JogSpeed(Axis, Axis.Para.MedV, Accel[axis], Decel[axis]);
                }
                else
                {
                    JogSpeed(Axis, Axis.Para.SlowV, Accel[axis], Decel[axis]);
                }

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

        bool IMotionControl.Jog(int axis, bool bPositive, int speed = 0)
        {
            var Axis = _axisList[axis];

            if (CheckMtrIsInAlarm(Axis))
            {
                return false;
            }

            try
            {
                if (speed == 0)
                {
                    JogSpeed(Axis, Axis.Para.Jog.SlowV, Axis.Para.Accel, Axis.Para.Accel);
                }
                else if (speed == 1)
                {
                    JogSpeed(Axis, Axis.Para.Jog.FastV, Axis.Para.Accel, Axis.Para.Accel);
                }                

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
        
        bool IMotionControl.MoveAbsLine(int Axis1, int Axis2, double Value1, double Value2, int Timeout)
        {
            throw new NotImplementedException();
        }

        bool IMotionControl.MoveRelLine(int Axis1, int Axis2, double Value1, double Value2, int Timeout)
        {
            throw new NotImplementedException();
        }

        bool IMotionControl.ForceStop(int axis)
        {
            try
            {
                var Axis = _axisList[axis];
                ForceStop(Axis);
            }
            catch (Exception Ex)
            {
                //ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                //throw new Exception(ExMsg);
                return false;
            }

            return true;
        }

        bool IMotionControl.ReadInput(int axis, int input)
        {
            var Input = _inputList[axis][input];
            UpdateInput(ref Input);

            return Input.Status;
        }

        bool IMotionControl.SetLogicPos(int axis, double Pos)
        {
            var Axis = _axisList[axis];
            try
            {
                SetLCntr(Axis, Pos);
            }
            catch(Exception ex)
            {                
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
           
        }       

        double IMotionControl.GetRealPos(int Axis)
        {
            throw new NotImplementedException();
        }

        double IMotionControl.GetLogicPos(int axis)
        {
            double Pos = 0;
            var Axis = _axisList[axis];
            try
            {
                //if (!Opened) return 0;
                GetLCntr(Axis, ref Pos);
            }
            catch { }
            return Math.Round(Pos, 3);
        }

        public bool SetRealPos(int Axis, double Pos)
        {
            throw new NotImplementedException();
        } // ZY

        double GetRealPos(int Axis)
        {
            throw new NotImplementedException();
        } // ZY

        void IMotionControl.GetMotorSpeedRange(int axisNo, ref double Min, ref double Max)
        {
            var Axis = _axisList[axisNo];
            GetSpeedMinMax(Axis, ref Min, ref Max);
        }

        void IMotionControl.GetMotorAccelRange(int axisNo, ref double Min, ref double Max)
        {
            var Axis = _axisList[axisNo];
            GetAccelMinMax(Axis, ref Min, ref Max);
        }

        bool IMotionControl.SensInPos(int nAxis)
        {
            var Axis = _axisList[nAxis];
            return AxisInPos(Axis);
        }

        bool IMotionControl.SensAlarm(int nAxis)
        {
            var Axis = _axisList[nAxis];
            return CheckMtrIsInAlarm(Axis);
        }

        void IMotionControl.SaveRecipePositon(string RecipeName)
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

        void IMotionControl.LoadRecipePosition(string RecipeName)
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

        void IMotionControl.SaveMotorParameters()
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
                UpdateAxisPara(axis);
            }
        }

        bool IMotionControl.MoveAbsLine(int axisNo1, int axisNo2, double dValue1, double dValue2)
        {
            throw new NotImplementedException();
        }

        bool IMotionControl.MoveRelLine(int axisNo1, int axisNo2, double dValue1, double dValue2)
        {
            throw new NotImplementedException();
        }

        void IMotionControl.AxisBusy(int axisNo, ref bool bAxisWait)
        {
            var Axis = _axisList[axisNo];
            bAxisWait = !AxisInPos(Axis);
        }        

        #region Common
        private string CheckErrorMessage()
        {
            eSCLLibHelper.ErrorInfo errorInfo = new eSCLLibHelper.ErrorInfo();
            m_eSCLLibHelper.GetLastErrorInfo(ref errorInfo);
            return (string.Format("Error code: {0}. Description:\r\n{1}", errorInfo.ErrorCode, errorInfo.ErrorMessage));
        }

        public bool MoveDone(TAxis Axis, int Timeout)
        {
            int t = Environment.TickCount + Timeout;
            while (true)
            {
                if (AxisInPos(Axis)) break; ;
                if (Environment.TickCount > t) break;
                Thread.Sleep(1);
            }

            ForceStop(Axis);
            if (Environment.TickCount > t)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Device

        public bool AddAxis(string _sIP)
        {
            try
            {
                m_eSCLLibHelper.AddAxis(_sIP);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool ClearAllAxis()
        {
            try
            {
                m_eSCLLibHelper.ClearAllAxes();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool OpenTCPDevice()
        {
            try
            {
                string msg;
                if (m_eSCLLibHelper.IsOpen())
                {
                    m_eSCLLibHelper.Close();
                }     
                

                if(!m_eSCLLibHelper.Open(true)) goto _Fail;

                Axis_Opened = Enumerable.Repeat(true, MAX_AXIS).ToArray();
                return true;

            _Fail:
                msg = CheckErrorMessage();
                for (int i = 0; i < _axisList.Count; i++)
                {
                    Axis_Opened[i] = !msg.Contains($"{_axisList[i].Device.IPAddress}");
                }
                MessageBox.Show(msg);

                return false;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Open SerialPort Failed:\r\n{0}", ex.Message));
                return false;
            }
        }
        public bool CloseDevice()
        {
            try
            {
                bool ret = m_eSCLLibHelper.Close();
                if (ret == false)
                {
                    MessageBox.Show("Fail to close communication.");
                }
                if (ret == false)
                {
                    CheckErrorMessage();

                    return false;
                }
                Axis_Opened = Enumerable.Repeat(false, MAX_AXIS).ToArray();
                return true;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(string.Format("Open SerialPort Failed:\r\n{0}", ex.Message));
                return false;
            }
        }
        #endregion

        #region Para
        public void SetUPP(TAxis Axis, double Value)
        {
            try
            {
                UPP[Axis.NodeID] = Value;
            }
            catch { throw; }
        }
        public void SetInvertPulse(TAxis Axis, bool Invert)
        {
            InvertPulse[Axis.NodeID] = Invert;
        }

        public void SetAcc(TAxis Axis, double Value)
        {
            try
            {
                Accel[Axis.NodeID] = Value;
                Axis.Para.Psnt.Accel = Value;
            }
            catch { throw; }
        }
        public void SetDcc(TAxis Axis, double Value)
        {
            try
            {
                Decel[Axis.NodeID] = Value;
                Axis.Para.Psnt.Decel = Value;
            }
            catch { throw; }
        }
        public void SetVel(TAxis Axis, double Value)
        {
            try
            {
                DriveV[Axis.NodeID] = Value;
                Axis.Para.Psnt.DriveV = Value;
            }
            catch { throw; }
        }
        #endregion
        #region Motion        

        public bool AxisInPos(TAxis Axis)
        {
            try
            {
                
                Thread.Sleep(1);
                return m_eSCLLibHelper.IsInPosition(Axis.NodeID);
            }
            catch { throw; };
        }
        public void MovePtpRel(TAxis Axis, double Value, double Vel, double Acc, double Dcc)
        {
            try
            {
                bool Res;
                if (InvertPulse[Axis.NodeID]) Value = -Value;
                int Distance = (int)(Value / UPP[Axis.NodeID]);
                Res = this.m_eSCLLibHelper.RelMove(Axis.NodeID, Distance, Vel, Acc, Dcc);
                if (!Res)
                {
                    throw new Exception(CheckErrorMessage());
                }
            }
            catch { throw; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Axis"></param>
        /// <param name="Value"> 1. Value must be in mm
        /// <param name="Vel"></param>
        /// <param name="Acc"></param>
        /// <param name="Dcc"></param>
        public void MovePtpAbs(TAxis Axis, double Value, double Vel, double Acc, double Dcc)
        {
            try
            {
                bool Res;
                if (InvertPulse[Axis.NodeID]) Value = -Value;

                //Input mm and convert mm to pulse
                //Etc:
                //Value= 5mm
                //UPP= Resolution= 0.001 mm/pulse
                //Pulse= 5/0.001= 5000 pulses
                //Hence, The motor is drive with pulses.
                int Pulses = (int)(Value / UPP[Axis.NodeID]);
                Res = this.m_eSCLLibHelper.AbsMove(Axis.NodeID, Pulses, Vel, Acc, Dcc);
                if (!Res)
                {
                    throw new Exception(CheckErrorMessage());
                }
            }
            catch { throw; }
        }

        private void Jop(TAxis Axis, bool Positive)
        {
            try
            {
                bool Res;
                int Dir = 1;

                if (Positive & !InvertPulse[Axis.NodeID])
                {
                    Dir = 1;
                }
                if (!Positive & !InvertPulse[Axis.NodeID])
                {
                    Dir = -1;
                }
                if (Positive & InvertPulse[Axis.NodeID])
                {
                    Dir = -1;
                }
                if (!Positive & InvertPulse[Axis.NodeID])
                {
                    Dir = 1;
                }

                Mutex.WaitOne();
                Res = m_eSCLLibHelper.WriteDistanceOrPosition(Axis.NodeID, Math.Abs(10000) * Dir);
                Res = this.m_eSCLLibHelper.WriteCommenceJogging(Axis.NodeID);
                if (!Res)
                {
                    throw new Exception(CheckErrorMessage());
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
        public void JogSpeed(TAxis Axis, double JogSpeed, double Acc, double Dcc)
        {
            try
            {
                bool Res;
                Res = m_eSCLLibHelper.WriteJogSpeed(Axis.NodeID, JogSpeed);
                Res = m_eSCLLibHelper.WriteJogAcceleration(Axis.NodeID, Acc);
                Res = m_eSCLLibHelper.WriteJogDeceleration(Axis.NodeID, Dcc);
                if (!Res)
                {
                    throw new Exception(CheckErrorMessage());
                }
            }
            catch { throw; }
        }
        public void StopJogging(TAxis Axis)
        {
            try
            {
                bool Res;
                Res = this.m_eSCLLibHelper.WriteStopJogging(Axis.NodeID);
                if (!Res)
                {
                    throw new Exception(CheckErrorMessage());
                }
            }
            catch
            {
                throw;
            }
        }
        private void Stop(TAxis Axis, bool Emg)
        {
            try
            {
                bool Res;
                Res = this.m_eSCLLibHelper.WriteStopAndKill(Axis.NodeID, Emg);
                if (!Res)
                {
                    throw new Exception(CheckErrorMessage());
                }
            }
            catch
            {
                throw;
            }
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

        public bool Servo(int axis, bool status)
        {
            try
            {
                var Axis = _axisList[axis];         
                if (!Axis_Opened[axis]) return false;

                if (Axis.ZEnable)
                {
                    var output = _outputList[Axis.Mask][(int)OUTPUT.BREAK_OFF];
                    if (!WriteOutput(ref Axis, output, status)) goto _Error;
                }

                if (status)
                {
                    if (!m_eSCLLibHelper.WriteMotorEnable(Axis.NodeID)) goto _Error;
                }
                else
                {
                    if (!m_eSCLLibHelper.WriteMotorDisable(Axis.NodeID)) goto _Error;
                }                
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); return false; };
            return true;

        _Error:
            {                
                MessageBox.Show($"{CheckErrorMessage()}");
                return false;
            }
        }

        public void MotorAlmReset(int axis)
        {
            try
            {
                var Axis = _axisList[axis];
                if (!Axis_Opened[axis]) return;
                if (!m_eSCLLibHelper.WriteAlarmReset(Axis.NodeID, true)) goto _Error;
                return;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };

        _Error:
            {
                MessageBox.Show($"{CheckErrorMessage()}");
            }
        }

        public void GetSpeedMinMax(TAxis Axis, ref double Min, ref double Max)
        {
            try
            {
                uint BufLen = 8;
                double MaxSpeed = 0;
                m_eSCLLibHelper.ReadMaximumVelocity(Axis.NodeID, ref Max);                

                Min = 1 * UPP[Axis.NodeID];
                Max = MaxSpeed * UPP[Axis.NodeID];
            }
            catch { throw; }
        }
        public void GetAccelMinMax(TAxis Axis, ref double Min, ref double Max)
        {
            try
            {
                uint BufLen = 8;
                double MaxSpeed = 0;
                m_eSCLLibHelper.ReadMaxAcceleration(Axis.NodeID, ref Max);

                Min = 1 * UPP[Axis.NodeID];
                Max = MaxSpeed * UPP[Axis.NodeID];
            }
            catch { throw; }
        }
        #endregion

        #region Position/Counter
        public void GetLCntr(TAxis Axis, ref double Value)
        {
            Mutex.WaitOne();
            try
            {
                bool Res;
                int immediatePosition = 0;
                Res = this.m_eSCLLibHelper.ReadImmediatePosition(Axis.NodeID, ref immediatePosition);
                Value = immediatePosition * UPP[Axis.NodeID];
                if (InvertPulse[Axis.NodeID])
                {
                    Value = -immediatePosition * UPP[Axis.NodeID];
                }
                if (!Res)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(CheckErrorMessage());
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
                if (InvertPulse[Axis.NodeID])
                {
                    Value = -Value;
                }
                Value = Value * UPP[Axis.NodeID];
                bool Res;
                Res = this.m_eSCLLibHelper.WriteSetPosition(Axis.NodeID, (int)Value);
                if (!Res)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(CheckErrorMessage());
                }
            }
            catch { throw; }
            Mutex.ReleaseMutex();
        }


        //
        #endregion
        #region IO
        //mask X1 = 0, X2 = 1, X3 = 2, X4 = 3, X5 = 4, X6 = 5, X7 = 6, X8 = 7
        public bool UpdateInput(ref TInput Input)
        {
            bool Res;

            try
            {
                if (Input.Axis_Port >= 0 && Input.Axis_Port <= 7)
                {
                    uint Mask = (uint)Input.Mask;

                    #region
                    int _iStatus = 0;
                    Mutex.WaitOne();
                    Res = this.m_eSCLLibHelper.ReadInputStatus(Input.Device.ID, ref _iStatus, false);
                    if (!Res)
                    {
                        Mutex.ReleaseMutex();
                        throw new Exception(CheckErrorMessage());
                    }
                    if ((_iStatus & (1 << (Input.Mask))) == 0) Input.Status = true; else Input.Status = false;
                    Mutex.ReleaseMutex();
                    #endregion
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool WriteOutput(ref TAxis Axis, TOutput output, bool Status)
        {
            bool Res;
            char St;          

            try
            {
                #region
                Mutex.WaitOne();
                if (Status)
                {
                    St = 'L';
                }
                else
                {
                    St = 'H';
                }
               Res = this.m_eSCLLibHelper.WriteSetOutput(Axis.NodeID, (int)output.Mask , St, false);
                if (!Res)
                {
                    Mutex.ReleaseMutex();
                    throw new Exception(CheckErrorMessage());
                }
                output.Status = Status;
                Mutex.ReleaseMutex();
                #endregion

                return true;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        public void UpdateAxisPara(TAxis Axis)
        {
            try
            {
                SetInvertPulse(Axis, Axis.Para.InvertPulse);
                SetUPP(Axis, Axis.Para.Unit.Resolution);

                SetAcc(Axis, (uint)Axis.Para.Accel);
                SetDcc(Axis, (uint)Axis.Para.Accel);
                SetVel(Axis, (uint)Axis.Para.SlowV);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };
        }

        public bool CheckMtrIsInAlarm(TAxis Axis)
        {
            try
            {
                if (this.m_eSCLLibHelper.IsInAlarm(Axis.NodeID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        public bool CheckMtrIsEnabled(TAxis Axis)
        {
            try
            {
                if (this.m_eSCLLibHelper.IsMotorEnabled(Axis.NodeID))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }


        #region Sensor
        public bool SensLmtP(int nAxis)
        {
            var ipLmtP = _inputList[nAxis][(int)INPUT.P_LIMIT];
            if (!Axis_Opened[ipLmtP.Axis_Port]/* && !bBenchDebug*/) return false;
            UpdateInput(ref ipLmtP);
            return ipLmtP.Status;

        }

        public bool SensLmtN(int nAxis)
        {            
            var ipLmtN = _inputList[nAxis][(int)INPUT.N_LIMIT];
            if (!Axis_Opened[ipLmtN.Axis_Port]/* && !bBenchDebug*/) return false;
            UpdateInput(ref ipLmtN);
            return ipLmtN.Status;
        }

        public bool SensHome(int nAxis)
        {
            var ipHOME = _inputList[nAxis][(int)INPUT.SENSE_HOME];
            if (!Axis_Opened[ipHOME.Axis_Port]/* && !bBenchDebug*/) return false;
            UpdateInput(ref ipHOME);
            return ipHOME.Status;
        }

        #endregion
        public bool HomeTimeout(int axisNo, int Timeout)
        {
            var Axis = _axisList[axisNo];
            if (GetTickCount() >= Timeout)
            {
                ForceStop(Axis);
                return true;

            }

            return false;
        }

        
    }
}
