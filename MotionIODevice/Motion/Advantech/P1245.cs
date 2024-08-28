using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MotionIODevice.Common;

namespace MotionIODevice
{
    public class P1245 : IMotionControl
    {
        const int MAX_AXIS = 4, MAX_INPUT = 7, MAX_OUTPUT = 4;
        private bool Opened = false;
        private Common.TDevice _device;
        private List<Common.TAxis> _axisList = new List<Common.TAxis>();
        private List<List<Common.TInput>> _inputList = new List<List<Common.TInput>>();
        private List<List<Common.TOutput>> _outputList = new List<List<Common.TOutput>>();

        private bool bBenchDebug = false;        

        public enum INPUT
        {
            BREAK_SIGNAL,
            SPARE_IN2,
            //ALARM,
            SENSE_HOME,
            IN_POS,
            ALARM,
            //Spare,
            P_LIMIT,
            N_LIMIT,
        }

        public enum OUTPUT
        {
            SPARE_OP1,
            BREAK_OFF,
            MTR_ON,
            ALARM_CLEAR,
        }

        public enum MOVES
        {
            MOVE_IN_POS,
            SENS_ON,
            SENS_OFF,
            MOVE_FAIL,
            SET_PARAM_FAIL,
            MTR_ALARM,
            ERR,
        }

        public P1245(Common.TDevice device, List<string> AxisNameList, List<TRunModule> RunInfoList)
        {
            this._device = device;
            InitAxis(AxisNameList);
            InitIO();
            InitRunInfo(RunInfoList);
        }

        #region IMotionControl
        public void InitAxis(List<string> AxisNameList)
        {
            _axisList.Add(new Common.TAxis(this._device, 0x00, "", AxisNameList[0], false, CardNumber.None));
            _axisList.Add(new Common.TAxis(this._device, 0x01, "", AxisNameList[1], false, CardNumber.None));
            _axisList.Add(new Common.TAxis(this._device, 0x02, "", AxisNameList[2], false, CardNumber.None));
            _axisList.Add(new Common.TAxis(this._device, 0x03, "", AxisNameList[3], false, CardNumber.None));

        }

        public void InitIO()
        {
            for (int i = 0; i < MAX_AXIS; i++)
            {
                List<TInput> ipList = new List<TInput>();
                ipList.Add(new Common.TInput(this._device, (byte)i, 0x0002, "", ""));
                ipList.Add(new Common.TInput(this._device, (byte)i, 0x0004, "", ""));
                ipList.Add(new Common.TInput(this._device, (byte)i, 0x0008, "", ""));
                ipList.Add(new Common.TInput(this._device, (byte)i, 0x0040, "", ""));
                ipList.Add(new Common.TInput(this._device, (byte)i, 0x0080, "", ""));
                ipList.Add(new Common.TInput(this._device, (byte)i, 0x0400, "", ""));
                ipList.Add(new Common.TInput(this._device, (byte)i, 0x0800, "", ""));

                _inputList.Add(ipList);

                List<TOutput> opList = new List<TOutput>();
                opList.Add(new Common.TOutput(this._device, (byte)i, 0x01, "", ""));
                opList.Add(new Common.TOutput(this._device, (byte)i, 0x02, "", ""));
                opList.Add(new Common.TOutput(this._device, (byte)i, 0x04, "", ""));
                opList.Add(new Common.TOutput(this._device, (byte)i, 0x08, "", ""));
                _outputList.Add(opList);
            }


        }

        public List<Common.TAxis> AxisList { get { return _axisList; } }
        public int GetAxisNum { get { return _axisList.Count; } }

        public int GetInputNum { get { return _inputList.Count; } }
        public int GetOutputNum { get { return _outputList.Count; } }
        public bool OpenBoard()
        {
            OpenAdvBoard();
            LoadMotorPara();
            return true;
        }
        public bool CloseBoard()
        {
            CloseAdvBoard();
            return true;
        }
        public bool ReadBit(int bit)
        {
            if (!Opened) return false;
            int point, axis = Math.DivRem(bit, MAX_INPUT, out point);
            var input = _inputList[axis][point];

            AdvMot.Api.UpdateInput(ref input);


            _inputList[axis][point] = input;

            return input.Status;

        }
        public bool OutBit(int bit, EOutputStatus status)
        {
            if (!Opened && !bBenchDebug) return false;
            int point, axis = Math.DivRem(bit, MAX_OUTPUT, out point);
            var output = _outputList[axis][point];

            switch (status)
            {
                case EOutputStatus.Hi:

                    if (!bBenchDebug)
                    {
                        AdvMot.Api.UpdateOutputHi(ref output);
                    }
                    else
                    {
                        output.Status = true;
                    }

                    break;
                case EOutputStatus.Lo:

                    if (!bBenchDebug)
                    {
                        AdvMot.Api.UpdateOutputLo(ref output);
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
        public bool Servo(int axis, bool On)
        {
            var Axis = _axisList[axis];
            var MtrOn_output = _outputList[axis][(int)OUTPUT.MTR_ON];

            if (Axis.ZEnable)
            {
                var SrvoOn_output = _outputList[axis][(int)OUTPUT.BREAK_OFF];
                var Brake = On;
                SetOutput(ref SrvoOn_output, Brake);
            }

            if (Axis.Para.InvertMtrOn)
            {
               return SetOutput(ref MtrOn_output, !On);
            }
            else
            {
               return SetOutput(ref MtrOn_output, On);
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
        public bool Home(int axis, HomingType motorType)
        {
            bool bMotion = false;
            if (motorType == HomingType.NPHType)
            {
                bMotion = HomeBySensor_NPH(axis);
            }
            else if (motorType == HomingType.Without_NP)
            {
                bMotion = HomeBySensor_H(axis);
            }
           
            return bMotion;
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
                if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.FastV, Axis.Para.Accel)) { return false; }
                MovePtpAbs(Axis, Value);
                bool b_axisWait = false;

                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref b_axisWait);
                    if (!b_axisWait) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
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
                else if (spdselection == 2)
                {
                    speed = Axis.Para.MedV;
                }

                if (!SetMotionParam(Axis, Axis.Para.StartV, speed, Axis.Para.Accel)) { return false; }
                MovePtpAbs(Axis, Value);
                bool b_axisWait = false;

                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref b_axisWait);
                    if (!b_axisWait) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        //public bool MoveSlowAbs(int axisNo, double Value, int Timeout)
        //{
        //    var Axis = _axisList[axisNo];


        //    if (AxisAlarm(axisNo))
        //    {
        //        return false;
        //    }

        //    try
        //    {
        //        if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.SlowV, Axis.Para.Accel)) { return false; }
        //        MovePtpAbs(Axis, Value);
        //        bool b_axisWait = false;

        //        int t = Environment.TickCount + Timeout;
        //        while (true)
        //        {
        //            AxisBusy(Axis, ref b_axisWait);
        //            if (!b_axisWait) break;
        //            if (Environment.TickCount > t) break;
        //            Thread.Sleep(1);
        //        }

        //        ForceStop(Axis);

        //        if (Environment.TickCount > t)
        //        {
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }

        //    return true;
        //}

        //public bool MoveSlowAbs(int axisNo, double Value)
        //{
        //    var Axis = _axisList[axisNo];

        //    if (AxisAlarm(axisNo))
        //    {
        //        return false;
        //    }

        //    try
        //    {
        //        if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.SlowV, Axis.Para.Accel)) { return false; }
        //        MovePtpAbs(Axis, Value);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return false;
        //    }
        //    return true;
        //}

        public bool MoveRel(int axisNo, double Value, int Timeout)
        {
            var Axis = _axisList[axisNo];

            if (AxisAlarm(axisNo))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.FastV, Axis.Para.Accel)) { return false; }
                MovePtpRel(Axis, Value);
                bool b_axisWait = false;

                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref b_axisWait);
                    if (!b_axisWait) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
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
                if (!SetMotionParam(Axis, Axis.Para.StartV, speed, Axis.Para.Accel)) { return false; }
                MovePtpRel(Axis, Value);
                bool b_axisWait = false;

                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis, ref b_axisWait);
                    if (!b_axisWait) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
                return false;
            }

            return true;
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
                if (!SetMotionParam(Axis, Axis.Para.StartV, bSlow ? Axis.Para.SlowV : Axis.Para.FastV, Axis.Para.Accel)) { return false; }

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
                    dSpeed = Axis.Para.Jog.SlowV;
                else if (nSpeed == 1)
                    dSpeed = Axis.Para.Jog.FastV;
                

                if (!SetMotionParam(Axis, Axis.Para.StartV, dSpeed, Axis.Para.Accel)) { return false; }

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

        public bool MoveAbsLine(int axis1No, int axis2No, double Value1, double Value2, int Timeout)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }
                bool b_axisWait1 = false;
                bool b_axisWait2 = false;

                MoveXYAbs(Axis1, Axis2, Value1, Value2);
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis1, ref b_axisWait1);
                    AxisBusy(Axis2, ref b_axisWait2);
                    if (!b_axisWait1 && !b_axisWait2) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool MoveAbsLine(int axis1No, int axis2No, double Value1, double Value2)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }

                MoveXYAbs(Axis1, Axis2, Value1, Value2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool MoveSlowAbsLine(int axis1No, int axis2No, double Value1, double Value2, int Timeout)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.SlowV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.SlowV, Axis2.Para.Accel)) { return false; }
                bool b_axisWait1 = false;
                bool b_axisWait2 = false;

                MoveXYAbs(Axis1, Axis2, Value1, Value2);
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis1, ref b_axisWait1);
                    AxisBusy(Axis2, ref b_axisWait2);
                    if (!b_axisWait1 && !b_axisWait2) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool MoveSlowAbsLine(int axis1No, int axis2No, double Value1, double Value2)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.SlowV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.SlowV, Axis2.Para.Accel)) { return false; }

                MoveXYAbs(Axis1, Axis2, Value1, Value2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool MoveRelLine(int axis1No, int axis2No, double Value1, double Value2, int Timeout)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }
                bool b_axisWait1 = false;
                bool b_axisWait2 = false;

                MoveXYRel(Axis1, Axis2, Value1, Value2);
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis1, ref b_axisWait1);
                    AxisBusy(Axis2, ref b_axisWait2);
                    if (!b_axisWait1 && !b_axisWait2) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool MoveRelLine(int axis1No, int axis2No, double Value1, double Value2)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.FastV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.FastV, Axis2.Para.Accel)) { return false; }

                MoveXYRel(Axis1, Axis2, Value1, Value2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool MoveSlowRelLine(int axis1No, int axis2No, double Value1, double Value2, int Timeout)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.SlowV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.SlowV, Axis2.Para.Accel)) { return false; }
                bool b_axisWait1 = false;
                bool b_axisWait2 = false;

                MoveXYRel(Axis1, Axis2, Value1, Value2);
                int t = Environment.TickCount + Timeout;
                while (true)
                {
                    AxisBusy(Axis1, ref b_axisWait1);
                    AxisBusy(Axis2, ref b_axisWait2);
                    if (!b_axisWait1 && !b_axisWait2) break;
                    if (Environment.TickCount > t) break;
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
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool MoveSlowRelLine(int axis1No, int axis2No, double Value1, double Value2)
        {
            var Axis1 = _axisList[axis1No];
            var Axis2 = _axisList[axis2No];

            if (AxisAlarm(axis1No) || AxisAlarm(axis2No))
            {
                return false;
            }

            try
            {
                if (!SetMotionParam(Axis1, Axis1.Para.StartV, Axis1.Para.SlowV, Axis1.Para.Accel)) { return false; }
                if (!SetMotionParam(Axis2, Axis2.Para.StartV, Axis2.Para.SlowV, Axis2.Para.Accel)) { return false; }

                MoveXYRel(Axis1, Axis2, Value1, Value2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        public bool ForceStop(int axisNo)
        {
            var Axis = _axisList[axisNo];
            ForceStop(Axis);
            AxisWait(axisNo, false);
            return true;
        }

        public bool ReadInput(int axisNo, int Input)
        {
            var ip = _inputList[axisNo][(int)Input];
            AdvMot.Api.UpdateInput(ref ip);
            return ip.Status;
        }

        public bool SetLogicPos(int axisNo, double Pos)
        {
            var Axis = _axisList[axisNo];
            SetLogicPos(Axis, Pos);
            return true;
        }

        public bool SetRealPos(int axisNo, double Pos)
        {
            var Axis = _axisList[axisNo];
            SetRealPos(Axis, Pos);
            return true;
        }

        public void AxisBusy(int axisNo, ref bool bAxisWait)
        {
            bool bBusy = false;
            var Axis = _axisList[axisNo];
            AxisBusy(Axis, ref bBusy);
            bAxisWait = bBusy;
        }

        public void GetMotorSpeedRange(int axisNo, ref double Min, ref double Max)
        {
            var Axis = _axisList[axisNo];
            AdvMot.Api.GetSpeedMinMax(Axis, ref Min, ref Max);
        }

        public void GetMotorAccelRange(int axisNo, ref double Min, ref double Max)
        {
            var Axis = _axisList[axisNo];
            AdvMot.Api.GetAccelMinMax(Axis, ref Min, ref Max);
        }
        #endregion

        public bool Home(int axisNo, ref Common.TInput _SensHome, ref Common.TInput _SensLmtP, ref Common.TInput _SensLmtN, ref Common.TInput _Alm, ref Common.TOutput _MtrOn, ref Common.TOutput _AlmClr)
        {
            var Axis = _axisList[axisNo];
            bool PositiveDir = true;
            //bool _bEnabSwLmt = VerifyEnableSoftwareLmt(Axis);
            string EMsg = Axis.Name + " Home Positive";


            if (Axis.Para.Home.Dir == Common.EHomeDir.N)
            {
                PositiveDir = false;
                EMsg = Axis.Name + " Home Negative";
            }
            try
            {
                AdvMot.Api.HardwareLimitEnable(Axis, false);
                //if (_bEnabSwLmt) AdvMot.Api.SoftwareLimitEnable(Axis, false);
            }
            catch { }
            Delay(50);

            //if (!MtrOn(axisNo, false)) goto _End;
            #region Alarm Clear
            if (Axis.Para.MotorAlarmType != Common.EMotorAlarmType.None)
            {
                if (AxisAlarm(axisNo))
                {
                    if (!Servo(axisNo, false)) goto _End;
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
            //MtrBreak(axisNo, EOutputStatus.Hi); //Servo On/Off will handle
            if (!Servo(axisNo, true)) goto _End;
            Delay(50);
            if (AxisAlarm(axisNo)) goto _End;

            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
        _Rescan:
            ClearAxisError(Axis);
            Delay(100);
            if (SensLmtP(axisNo))
            #region Clear Home
            {
                Delay(50);
                if (!SetMotionParam(Axis, 10, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;
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
                    if (!ForceStop(Axis)) goto _End;
                    goto _End;
                }
                while (true)
                {
                    if (SensHome(axisNo)) break;
                    Thread.Sleep(1);
                    //GDefine.AppProMsg();
                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (AxisAlarm(axisNo)) goto _End;
                }
                if (!ForceStop(Axis)) goto _End;
                //if (!DecelStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
            }
            #endregion
            Delay(50);
            if (!SensHome(axisNo))
            #region Search Home
            {
                if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;

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
                    if (SensLmtP(axisNo))
                    {
                        if (!DecelStop(Axis)) goto _End;
                        goto _Rescan;
                    }
                    if (HomeTimeout(axisNo, t)) goto _End;
                    if (AxisAlarm(axisNo)) goto _End;
                    Thread.Sleep(1);
                    //Application.DoEvents();
                }
                //if (!ForceStop(Axis)) goto _End;
                if (!DecelStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
            }
            #endregion
            Delay(50);
            if (SensHome(axisNo))
            #region Clear Home
            {
                if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
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
            }
            #endregion
            Delay(50);
            if (!SensHome(axisNo))
            #region Touch Home
            {
                //if (!SetMotionParam(Axis, 1, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
                if (!SetMotionParam(Axis, 0.2, 0.2, 1)) goto _End;
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
                        break;
                    }
                    Thread.Sleep(1);
                    //GDefine.AppProMsg();
                    if (HomeTimeout(axisNo, t))
                    {
                        goto _End;
                    }
                    if (AxisAlarm(axisNo)) goto _End;
                }
                if (!ForceStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
            }
            if (!Delay(200)) goto _End;
            #endregion
            Delay(50);
            if (AxisAlarm(axisNo)) goto _End;
            Delay(200);
            #region Set Param
            Delay(200);
            SetLogicPos(Axis, 0);
            SetRealPos(Axis, 0);
            UpdateAxis(Axis);
            #endregion

            //AdvMot.Api.HardwareLimitEnable(Axis, true);
            //if (_bEnabSwLmt) AdvMot.Api.SoftwareLimitEnable(Axis, true);
            return true;
        _End:
            //AdvMot.Api.HardwareLimitEnable(Axis, true);
            //if (_bEnabSwLmt) AdvMot.Api.SoftwareLimitEnable(Axis, true);
            //UpdateAxis(Axis);
            return false;
        }

        //public void UpdateAxisName(List<string> AxisNameList)
        //{
        //    int i = 0;
        //    foreach (var axis in _axisList)
        //    {
        //        var tempAxis = axis;
        //        tempAxis.Name = AxisNameList[i];
        //        _axisList[i] = tempAxis;

        //        i++;
        //    }
        //}           

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

        public void SaveRecipePositon(string RecipeName)
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

        //public void InitIO()
        //{
        //    for (int i = 0; i < MAX_AXIS; i++)
        //    {
        //        List<TInput> ipList = new List<TInput>();
        //        ipList.Add(new Common.TInput(_device, (byte)i, 0x0002, "", ""));
        //        ipList.Add(new Common.TInput(_device, (byte)i, 0x0004, "", ""));
        //        ipList.Add(new Common.TInput(_device, (byte)i, 0x0008, "", ""));
        //        ipList.Add(new Common.TInput(_device, (byte)i, 0x0040, "", ""));
        //        ipList.Add(new Common.TInput(_device, (byte)i, 0x0080, "", ""));
        //        ipList.Add(new Common.TInput(_device, (byte)i, 0x0400, "", ""));
        //        ipList.Add(new Common.TInput(_device, (byte)i, 0x0800, "", ""));

        //        _inputList.Add(ipList);

        //        List<TOutput> opList = new List<TOutput>();
        //        opList.Add(new Common.TOutput(_device, (byte)i, 0x01, "", ""));
        //        opList.Add(new Common.TOutput(_device, (byte)i, 0x02, "", ""));
        //        opList.Add(new Common.TOutput(_device, (byte)i, 0x04, "", ""));
        //        opList.Add(new Common.TOutput(_device, (byte)i, 0x08, "", ""));
        //        _outputList.Add(opList);
        //    }


        //}

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
                UpdateAxis(axis);
            }
        }

        public static void UpdateAxis(Common.TAxis Axis)
        {
            string ExMsg = "UpdateAxis";

            try
            {
                ExMsg = "P1245 " + ExMsg;
                AdvMot.Api.UpdateAxisPara(Axis);
                //AdvMot.Api.HardwareLimitEnable(Axis, true);
                //AdvMot.Api.HardwareLimitLogicActHigh(Axis, false);
                //AdvMot.Api.MotorAlarmEnable(Axis, true);

                if(Axis.Name == "Rotation" ||
                    Axis.Name == "UnloadRotary")
                {
                    AdvMot.Api.MotorAlarmLogicActHigh(Axis, true);
                }
                
                if (Axis.Para.SwLimit.LimitType != Common.EAxisSwLimitType.Disable)
                    AdvMot.Api.SoftwareLimitEnable(Axis, true);
                else
                    AdvMot.Api.SoftwareLimitEnable(Axis, false);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                //throw new Exception(ExMsg);
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

        public void OpenAdvBoard()
        {
            try
            {
                Opened = false;
                //AdvMot.Api.OpenDevice(_device);                
                if (_device.EType)
                {
                    AdvMot.Api.OpenDevice(_device);
                }
                else
                {
                    AdvMot.Api.OpenDeviceLtype(_device);
                }
                AdvMot.Api.Reset(_device.ID);
                AdvMot.Api.EmgLogicActHigh(_device, true);

                Thread.Sleep(500);

                foreach (var axis in _axisList)
                {
                    AdvMot.Api.HardwareLimitEnable(axis, false);
                }

                Opened = true;

                //foreach (var axis in _axisList)
                //{
                //    UpdateAxis(axis);
                //}
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };
        }
        public void CloseAdvBoard()
        {
            try
            {
                AdvMot.Api.CloseDevice(_device);
                Opened = false;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); };
        }
        public double GetLogicPos(Common.TAxis Axis)
        {
            double Pos = 0;

            if (!Opened) return 0;

            try
            {
                AdvMot.Api.GetLCntr(Axis, ref Pos);
            }
            catch { }
            return Math.Round(Pos, 3);
        }

        public bool SetLogicPos(Common.TAxis Axis, double Pos)
        {
            string ErrMsg = " SetLogicPos";
            try
            {
                AdvMot.Api.SetLCntr(Axis, Pos);
            }
            catch (Exception Ex)
            {
                ErrMsg = ErrMsg + (char)13 + Ex.Message.ToString();
                return false;
            }

            return true;
        }
        public double GetRealPos(Common.TAxis Axis)
        {
            double Value = 0;
            if (!Opened) { return Value; }
            try
            {
                AdvMot.Api.GetRCntr(Axis, ref Value);
            }
            catch { }
            return Value;
        }

        public bool SetRealPos(Common.TAxis Axis, double Value)
        {
            string ErrMsg = "SetEncoderPos";
            try
            {
                AdvMot.Api.SetRCntr(Axis, Value);
            }
            catch (Exception Ex)
            {
                ErrMsg = ErrMsg + (char)13 + Ex.Message.ToString();
                return false;
            }
            return true;
        }

        public bool SetMotionParam(Common.TAxis Axis, double StartV, double DriveV, double Accel)
        {
            if (!Opened) { return false; }
            string ExMsg = "SetMotionParam";

            double d_StartV = StartV;
            double d_DriveV = DriveV;

            if (d_StartV > d_DriveV) d_StartV = d_DriveV;

            try
            {
                AdvMot.Api.SetStartV(Axis, d_StartV);
                AdvMot.Api.SetDriveV(Axis, d_DriveV);
                AdvMot.Api.SetAccel(Axis, Accel);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }

            return true;
        }
        public void SetMotionParam(Common.TAxis Axis, double StartV, double DriveV, double Accel, double Decel)
        {
            if (!Opened) { return; }
            string ExMsg = "SetMotionParam";

            double d_StartV = StartV;
            double d_DriveV = DriveV;

            if (d_StartV > d_DriveV) d_StartV = d_DriveV;

            try
            {
                AdvMot.Api.SetStartV(Axis, d_StartV);
                AdvMot.Api.SetDriveV(Axis, d_DriveV);
                AdvMot.Api.SetAccel(Axis, Accel);
                AdvMot.Api.SetDecel(Axis, Decel);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void RefreshDI(ref Common.TInput Input)
        {
            if (!Opened) { return; }
            string ExMsg = "RefreshDI";
            try
            {
                AdvMot.Api.UpdateInput(ref Input);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
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
                    AdvMot.Api.UpdateOutputHi(ref Output);
                }
                if (Status == Common.EOutputStatus.Lo)
                {
                    AdvMot.Api.UpdateOutputLo(ref Output);
                }
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void MovePtpRel(Common.TAxis Axis, double Value)
        {
            string ExMsg = Axis.Name + " MovePtpRel";
            try
            {
                AdvMot.Api.MovePtpRel(Axis, Value);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void MovePtpAbs(Common.TAxis Axis, double Value)
        {
            string ExMsg = Axis.Name + " MovePtpAbs";
            try
            {
                AdvMot.Api.MovePtpAbs(Axis, Value);

            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void MoveXYAbs(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2)
        {
            string ExMsg = Axis1.Name + Axis2.Name + " MoveXYAbs";
            try
            {
                AdvMot.Api.MoveLineAbs(Axis1, Axis2, Value1, Value2);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void MoveLineAbs2(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2)
        {
            string ExMsg = Axis1.Name + Axis2.Name + " MoveLineAbs2";
            try
            {
                AdvMot.Api.MoveLineAbs(Axis1, Axis2, Value1, Value2);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void MoveXYRel(Common.TAxis Axis1, Common.TAxis Axis2, double Value1, double Value2)
        {
            string ExMsg = Axis1.Name + Axis2.Name + " MoveXYAbs";
            try
            {
                AdvMot.Api.MoveLineRel(Axis1, Axis2, Value1, Value2);

            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void AxisBusy(Common.TAxis Axis, ref bool Busy)
        {
            string ExMsg = "AxisBusy";
            try
            {
                Busy = AdvMot.Api.AxisBusy(Axis);
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

            //if (MotorAlarm(Axis)) return false;
            //if (AxisError(Axis)) return false;

            try
            {
                bool Busy = false;
                AxisBusy(Axis, ref Busy);
                return Busy;
            }
            catch (Exception Ex)
            {
                return false;
            }
        }
        internal bool SLmtP(Common.TAxis Axis)
        {
            Common.TInput SLmt = new Common.TInput();
            SLmt.Device.ID = Axis.Device.ID;
            SLmt.Device.Type = Axis.Device.Type;
            SLmt.Axis_Port = Axis.Mask;
            SLmt.Mask = 0x0100;

            if (Axis.Para.InvertPulse)
            {
                SLmt.Mask = 0x0200;
            }

            return SLmt.Status;
        }
        internal bool SLmtN(Common.TAxis Axis)
        {
            Common.TInput SLmt = new Common.TInput();
            SLmt.Device.ID = Axis.Device.ID;
            SLmt.Device.Type = Axis.Device.Type;
            SLmt.Axis_Port = Axis.Mask;
            SLmt.Mask = 0x0200;

            if (Axis.Para.InvertPulse)
            {
                SLmt.Mask = 0x0100;
            }
            return SLmt.Status;
        }
        public void JogP(Common.TAxis Axis)
        {
            string ExMsg = Axis.Name + " JogP";
            try
            {
                if (AxisBusy(Axis)) return;
                ClearAxisError(Axis);
                AdvMot.Api.JogP(Axis);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void JogN(Common.TAxis Axis)
        {
            string ExMsg = Axis.Name + " JogN";
            try
            {
                if (AxisBusy(Axis)) return;
                AdvMot.Api.ClearAxisError(Axis);
                AdvMot.Api.JogN(Axis);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public bool ForceStop(Common.TAxis Axis)
        {
            if (!Opened) return false;
            string ExMsg = Axis + " ForceStop";
            try
            {
                AdvMot.Api.ForceStop(Axis);
            }
            catch (Exception Ex)
            {
                //ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                //throw new Exception(ExMsg);
                return false;
            }

            return true;
        }
        public bool DecelStop(Common.TAxis Axis)
        {
            string ExMsg = "DecelStop";
            try
            {
                AdvMot.Api.DecelStop(Axis);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
                return false;
            }

            return true;
        }
        public void ClearAxisError(Common.TAxis Axis)
        {
            string ExMsg = "ClearAxisError";
            try
            {
                AdvMot.Api.ClearAxisError(Axis);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                //throw new Exception(ExMsg);
                MessageBox.Show(ExMsg);
            }
        }

        public void UpdateAxisHome(Common.TAxis Axis)
        {
            string ExMsg = "UpdateAxis";
            if (!Opened) return;
            try
            {
                ExMsg = "P1245 " + ExMsg;
                AdvMot.Api.UpdateAxisPara(Axis);
                AdvMot.Api.HardwareLimitLogicActHigh(Axis, false);
                AdvMot.Api.MotorAlarmEnable(Axis, true);
                AdvMot.Api.MotorAlarmLogicActHigh(Axis, true);
                System.Threading.Thread.Sleep(100);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void GetMotorSpeedRange(Common.TAxis Axis, ref double Min, ref double Max)
        {
            string ExMsg = Axis.Name + " GetMotorSpeedRange";

            if (!Opened) return;
            try
            {
                AdvMot.Api.GetSpeedMinMax(Axis, ref Min, ref Max);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public void GetMotorAccelRange(Common.TAxis Axis, ref double Min, ref double Max)
        {
            string ExMsg = Axis.Name + " GetMotorAccelRange";

            if (!Opened) return;
            try
            {
                AdvMot.Api.GetAccelMinMax(Axis, ref Min, ref Max);
            }
            catch (Exception Ex)
            {
                ExMsg = ExMsg + (char)13 + Ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }
        public bool VerifyEnableSoftwareLmt(Common.TAxis Axis)
        {
            if (Axis.Para.SwLimit.LimitType == Common.EAxisSwLimitType.Disable)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        internal bool AxisAlarm(int nAxis)
        {
            var axis = _axisList[nAxis];
            var ipalarm = _inputList[nAxis][(int)INPUT.ALARM];

            AdvMot.Api.UpdateInput(ref ipalarm);
            return AxisAlarm(axis, ref ipalarm);
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
                    AdvMot.Api.UpdateInput(ref Alm);

                    if (!Alm.Status)
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
            return true;
        }

        internal void AlarmClear(ref Common.TOutput AlmClr, bool On)
        {
            SetOutput(ref AlmClr, On);
        }

        public void MotorAlmReset(int nAxis)
        {
            if (!Opened) { return; }
            var almClr = _outputList[nAxis][(int)OUTPUT.ALARM_CLEAR];

            AdvMot.Api.UpdateOutputHi(ref almClr);
            Delay(500);
            AdvMot.Api.UpdateOutputLo(ref almClr);
            //return almClr.Status;
        }

        public bool SensLmtP(int nAxis)
        {
            if (!Opened) return false;
            var ipLmtP = _inputList[nAxis][(int)INPUT.P_LIMIT];
            AdvMot.Api.UpdateInput(ref ipLmtP);
            return ipLmtP.Status;

        }

        public bool SensLmtN(int nAxis)
        {
            if (!Opened) return false;
            var ipLmtN = _inputList[nAxis][(int)INPUT.N_LIMIT];
            AdvMot.Api.UpdateInput(ref ipLmtN);
            return ipLmtN.Status;
        }

        public bool SensHome(int nAxis)
        {
            if (!Opened) return false;
            var ipHome = _inputList[nAxis][(int)INPUT.SENSE_HOME];
            AdvMot.Api.UpdateInput(ref ipHome);
            return ipHome.Status;

        }

        public bool SensInPos(int nAxis)
        {
            if (!Opened) return false;
            var ipHome = _inputList[nAxis][(int)INPUT.IN_POS];
            AdvMot.Api.UpdateInput(ref ipHome);
            return ipHome.Status;
        }

        public bool SensAlarm(int nAxis)
        {
            if (!Opened) return false;
            var ipHome = _inputList[nAxis][(int)INPUT.ALARM];
            AdvMot.Api.UpdateInput(ref ipHome);
            return ipHome.Status;
        }

        public bool MtrBreak(int nAxis, EOutputStatus Status) //XOut4
        {
            if (!Opened) { return false; }
            var opBreak = _outputList[nAxis][(int)OUTPUT.BREAK_OFF];

            switch (Status)
            {
                case Common.EOutputStatus.Hi:
                    AdvMot.Api.UpdateOutputHi(ref opBreak);
                    break;
                case Common.EOutputStatus.Lo:
                    AdvMot.Api.UpdateOutputLo(ref opBreak);
                    break;
            }

            return opBreak.Status;
        }
        internal bool AxisWait(int axisNo, bool Busy) 
        {
            var Axis = _axisList[axisNo];
            string EMsg = Axis.Name + " AxisWait";

            if (AxisAlarm(axisNo)) return false;

            try
            {
                if (Busy)
                {
                    Busy = false;
                    AxisBusy(Axis, ref Busy);
                    while (Busy)
                    {
                        AxisBusy(Axis, ref Busy);
                        Thread.Sleep(0);
                    }
                }

            }
            catch (Exception Ex)
            {
                //GDefine.Status = EStatus.ErrorInit;

                //EMsg = EMsg + (char)13 + Ex.Message.ToString();
                //Msg MsgBox = new Msg();
                //MsgBox.Show(ErrCode.GANTRY_MOTION_EX_ERR, EMsg);
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

        public double GetCurrentPos(int AxisNo)
        {
            var Axis = _axisList[AxisNo];
            double dPos = 0.0;
            if (Axis.Para.Encoder)
            {
                dPos = GetRealPos(Axis);
            }
            else
            {
                dPos = GetLogicPos(Axis);
            }

            return dPos;
        }

        public double GetRealPos(int axisNo)
        {
            var Axis = _axisList[axisNo];
            return GetRealPos(Axis);
        }

        public double GetLogicPos(int axisNo)
        {
            var Axis = _axisList[axisNo];
            return GetLogicPos(Axis);
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
                AdvMot.Api.HardwareLimitEnable(Axis, false);
            }
            catch { }
            Delay(50);
            #endregion

            #region Alarm Clear
            if (Axis.Para.MotorAlarmType != Common.EMotorAlarmType.None)
            {
                if (AxisAlarm(axisNo))
                {
                    if (!Servo(axisNo, false)) goto _End;
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
            if (!Servo(axisNo, true)) goto _End;
            Delay(100);
            if (AxisAlarm(axisNo)) goto _End;

            #endregion

            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
            ClearAxisError(Axis);
            Delay(500);

        _Forward:
            if (!SensHome(axisNo))
            #region Search Home
            {
                if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;

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
                if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;

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
                if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
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
                if (!SetMotionParam(Axis, 0.2, 0.2, 1)) goto _End;
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
            var _AlmClr = _outputList[axisNo][(int)OUTPUT.ALARM_CLEAR];

            #region Init
            bool PositiveDir = true;
            string EMsg = Axis.Name + " Home Positive";
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
                if (AxisAlarm(axisNo))
                {
                    if (!Servo(axisNo, false)) goto _End;
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
            if (!Servo(axisNo, true)) goto _End;
            Delay(50);
            if (AxisAlarm(axisNo)) goto _End;

            #endregion

            int t = GetTickCount() + (int)Axis.Para.Home.Timeout;
            ClearAxisError(Axis);
            Delay(500);

            #region Search Home

            if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.FastV, Axis.Para.Accel)) goto _End;

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
                if (HomeTimeout(axisNo, t)) goto _End;
                if (AxisAlarm(axisNo)) goto _End;
                Thread.Sleep(1);
            }
            if (!ForceStop(Axis)) goto _End;
            if (!AxisWait(axisNo, false)) goto _End;

            #endregion

            #region Clear Home
            Delay(500);
            if (!SetMotionParam(Axis, Axis.Para.StartV, Axis.Para.Home.SlowV, Axis.Para.Accel)) goto _End;
            if (PositiveDir)
                JogN(Axis);
            else
                JogP(Axis);

            if (AxisAlarm(axisNo))
            {
                if (!ForceStop(Axis)) goto _End;
                goto _End;
            }

            while (SensHome(axisNo))
            {
                Thread.Sleep(1);
                if (HomeTimeout(axisNo, t)) goto _End;
                if (AxisAlarm(axisNo)) goto _End;
            }
            if (!ForceStop(Axis)) goto _End;
            if (!AxisWait(axisNo, false)) goto _End;

            #endregion

            Delay(500);
            if (!SensHome(axisNo))
            #region Touch Home
            {
                if (!SetMotionParam(Axis, 0.2, 0.2, 1)) goto _End;
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
                    if (HomeTimeout(axisNo, t))
                    {
                        Thread.Sleep(1);
                        goto _End;
                    }
                    if (AxisAlarm(axisNo)) goto _End;
                }
                if (!ForceStop(Axis)) goto _End;
                if (!AxisWait(axisNo, false)) goto _End;
            }
            #endregion

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

        

        public void SaveMotorParameters()
        {
            SaveMotorParam();
        }        
       
    }
}
