#define OOP
#if OOP
using static MotionIODevice.Common;
using MotionIODevice;
#else
using ES.CMotion;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using Infrastructure;

namespace Machine
{
    public partial class uctrlMotorParam : UserControl
    {
        public ES.UserCtrl.UserCtrlTools UserCtrl = new ES.UserCtrl.UserCtrlTools();
        private const string nulltext = "-";
        private int SelectedTabIndex;
        private int SelectedAxis = -1;
        private TAxis SelectedTAxis = null;
        private frmMessaging2 msgForm;
        int spdSelection = 0;
        int spd = 1;

        public static uctrlMotorParam Page = new uctrlMotorParam();
        public uctrlMotorParam()
        {
            InitializeComponent();
            InitGUI();
            this.Load += uctrlMotorParam_Load;
            spdSelection = 0;
            spd = 1;
        }

        private void uctrlMotorParam_Load(object sender, EventArgs e)
        {
            lbl_Title.Text = "Motor Parameter";
        }

        public void ShowPage(Control parent)
        {
            this.Parent = parent;
            this.Dock = DockStyle.Fill;
            this.Show();

        }

        public void HidePage()
        {
            Visible = false;
        }

        private void InitGUI()
        {
            var Advantech_BoardNo = frmMain.AdvantechModule.GetBoardNo;
            var STF_BoardNo = frmMain.MoonStfModule.GetBoardNo;
            //var Galil_BoardNo = frmMain.GalilModule.GetBoardNo;

            var BoardNo = Advantech_BoardNo + STF_BoardNo /*+ Galil_BoardNo*/;

            for (int i = 0; i < BoardNo; i++)
            {
                TabPage tp = new TabPage();
                tp.Text = "MC " + (i + 1).ToString();
                tc_Axis.TabPages.Add(tp);
            }

            var tabIndex = 0;
            var motionboardtype = frmMain.AdvantechModule.MotionBoardType();
            if (motionboardtype == EDeviceType.P1245)
            {
                for (int i = 0; i < Advantech_BoardNo; i++)
                {
                    var axisList = frmMain.AdvantechModule.GetAxisList(i);
                    for (int j = 0; j < axisList.Count; j++)
                    {
                        DrawIndicator(tc_Axis, axisList[j], j, tabIndex);
                    }
                    tabIndex++;
                }
            }

            motionboardtype = frmMain.MoonStfModule.MotionBoardType();
            if (motionboardtype == EDeviceType.MoonsSTF)
            {
                for (int i = 0; i < STF_BoardNo; i++)
                {
                    var axisList = frmMain.MoonStfModule.GetAxisList(i);
                    for (int j = 0; j < axisList.Count; j++)
                    {
                        DrawIndicator(tc_Axis, axisList[j], j, tabIndex);

                    }
                    tabIndex++;
                }
            }

            //motionboardtype = frmMain.GalilModule.MotionBoardType();
            //if (motionboardtype == EDeviceType.B140)
            //{
            //    for (int i = 0; i < STF_BoardNo; i++)
            //    {
            //        var axisList = frmMain.GalilModule.GetAxisList(i);
            //        for (int j = 0; j < axisList.Count; j++)
            //        {
            //            DrawIndicator(tc_Axis, axisList[j], j, tabIndex);

            //        }
            //        tabIndex++;
            //    }
            //}
            DisableButtons();
        }

        public void DrawIndicator(TabControl tc, TAxis ix, int idx, int tabIdx, int fac = 8)
        {
            if (ix.Name == "Spare")
            {
                return;
            }

            DisplayBtn btn = new DisplayBtn();
            btn.Text = ix.Name;
            btn.Tag = idx;
            btn.SelectedTAxis = ix;

            int maxSide = idx / fac;
            int rowIdx = idx % fac;

            btn.Location = new Point(2 + (maxSide * 250), 3 + rowIdx * 60);
            btn.Click += new EventHandler(Button_Clicked);

            if (tabIdx < tc.TabCount)
            {
                tc.TabPages[tabIdx].Controls.Add(btn);
            }
        }

        public void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                DisableButtons();
                DisplayBtn btn = sender as DisplayBtn;
                ushort bit = Convert.ToUInt16(btn.Tag);

                if (btn != null)
                {
                    btn.BackColor = Color.Lime;

                    SelectedAxis = bit;
                    SelectedTAxis = btn.SelectedTAxis;
                    UpdateDisplay(SelectedTabIndex, SelectedAxis);
                    EnableTxtLabel();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Button_Clicked",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableButtons()
        {
            foreach (DisplayBtn btn in GetSelectedTabIndicator(tc_Axis))
            {
                btn.BackColor = Color.White;
            }
            DisplayNull();
            SelectedAxis = -1;
        }

        DisplayBtn[] GetSelectedTabIndicator(TabControl tc)
        {
            List<DisplayBtn> list = new List<DisplayBtn>();
            foreach (Control ctrl in tc.SelectedTab.Controls)
            {
                DisplayBtn btn = ctrl as DisplayBtn;
                if (btn != null)
                {
                    list.Add(btn);
                }
            }
            return list.ToArray();
        }

        private void DisplayNull()
        {
            lbl_InvertMtrOn.Text = nulltext;
            lbl_InvertDir.Text = nulltext;
            lbl_MtrAlmLogic.Text = nulltext;
            lbl_Encoder.Text = nulltext;
            lbl_DistPerPulse.Text = nulltext;
            lbl_HomeDir.Text = nulltext;
            lbl_HomeSlowV.Text = nulltext;
            lbl_HomeFastV.Text = nulltext;
            lbl_HomeTimeOut.Text = nulltext;
            lbl_Accel.Text = nulltext;
            lbl_StartV.Text = nulltext;
            lbl_SlowV.Text = nulltext;
            lbl_FastV.Text = nulltext;
            lbl_JogSlowV.Text = nulltext;
            lbl_JogMedV.Text = nulltext;
            lbl_JogFastV.Text = nulltext;

            lbl_InvertMtrOn.Enabled = false;
            lbl_InvertDir.Enabled = false;
            lbl_MtrAlmLogic.Enabled = false;
            lbl_Encoder.Enabled = false;
            lbl_DistPerPulse.Enabled = false;
            lbl_HomeDir.Enabled = false;
            lbl_HomeSlowV.Enabled = false;
            lbl_HomeFastV.Enabled = false;
            lbl_HomeTimeOut.Enabled = false;
            lbl_Accel.Enabled = false;
            lbl_StartV.Enabled = false;
            lbl_SlowV.Enabled = false;
            lbl_FastV.Enabled = false;
            lbl_JogSlowV.Enabled = false;
            lbl_JogMedV.Enabled = false;
            lbl_JogFastV.Enabled = false;

            btn_Home.Enabled = false;
            btn_JogN.Enabled = false;
            btn_JogP.Enabled = false;
        }

        private void EnableTxtLabel()
        {
            lbl_InvertMtrOn.Enabled = true;
            lbl_InvertDir.Enabled = true;
            lbl_MtrAlmLogic.Enabled = true;
            lbl_Encoder.Enabled = true;
            lbl_DistPerPulse.Enabled = true;
            lbl_HomeDir.Enabled = true;
            lbl_HomeSlowV.Enabled = true;
            lbl_HomeFastV.Enabled = true;
            lbl_HomeTimeOut.Enabled = true;
            lbl_Accel.Enabled = true;
            lbl_StartV.Enabled = true;
            lbl_SlowV.Enabled = true;
            lbl_FastV.Enabled = true;
            lbl_JogSlowV.Enabled = true;
            lbl_JogMedV.Enabled = true;
            lbl_JogFastV.Enabled = true;

            btn_Home.Enabled = true;
            btn_JogN.Enabled = true;
            btn_JogP.Enabled = true;
        }

        private void UpdateDisplay(int BoardNo, int AxisNo)
        {
            List<TAxis> axisList = new List<TAxis>();
            if (BoardNo <= 1)
                axisList = frmMain.AdvantechModule.GetAxisList(BoardNo);
            else if (BoardNo == 3)
                axisList = frmMain.GalilModule.GetAxisList(0);
            else
                axisList = frmMain.MoonStfModule.GetAxisList(0);

            TAxis PAxis = axisList[AxisNo];

            lbl_InvertMtrOn.Text = PAxis.Para.InvertMtrOn.ToString();
            lbl_InvertDir.Text = PAxis.Para.InvertPulse.ToString();
            lbl_Encoder.Text = PAxis.Para.Encoder.ToString();
            switch (PAxis.Para.MotorAlarmType)
            {
                case EMotorAlarmType.None:
                    lbl_MtrAlmLogic.Text = "None";
                    break;
                case EMotorAlarmType.NC:
                    lbl_MtrAlmLogic.Text = "NC";
                    break;
                case EMotorAlarmType.NO:
                    lbl_MtrAlmLogic.Text = "NO";
                    break;
            }
            lbl_DistPerPulse.Text = PAxis.Para.Unit.Resolution.ToString("F5");
            if (PAxis.Para.Home.Dir == EHomeDir.N)
            {
                lbl_HomeDir.Text = "Negative";
            }
            else
            {
                lbl_HomeDir.Text = "Positive";
            }
            lbl_HomeSlowV.Text = PAxis.Para.Home.SlowV.ToString("F3");
            lbl_HomeFastV.Text = PAxis.Para.Home.FastV.ToString("F3");
            lbl_HomeTimeOut.Text = PAxis.Para.Home.Timeout.ToString();

            lbl_Accel.Text = PAxis.Para.Accel.ToString("F3");
            lbl_StartV.Text = PAxis.Para.StartV.ToString("F3");
            lbl_SlowV.Text = PAxis.Para.SlowV.ToString("F3");
            lbl_MedV.Text= PAxis.Para.MedV.ToString("F3");
            lbl_FastV.Text = PAxis.Para.FastV.ToString("F3");

            lbl_JogSlowV.Text = PAxis.Para.Jog.SlowV.ToString("F3");
            lbl_JogMedV.Text = PAxis.Para.Jog.MedV.ToString("F3");
            lbl_JogFastV.Text = PAxis.Para.Jog.FastV.ToString("F3");
        }

        private void UpdateEncoderValue()
        {
            var TabIndex = SelectedTabIndex;
            var Axis = SelectedAxis;
            List<TAxis> axisList = new List<TAxis>();

            if (frmMain.AdvantechModule.GetBoardNo - 1 >= TabIndex)
            {
                axisList = frmMain.AdvantechModule.GetAxisList(TabIndex);
            }
            else if (TabIndex == 3)
            {
                axisList = frmMain.GalilModule.GetAxisList(0);
            }
            else
            {
                axisList = frmMain.MoonStfModule.GetAxisList(0);
            }

            TAxis PAxis = axisList[Axis];


            if (SelectedAxis != -1)
            {
                if (frmMain.AdvantechModule.GetBoardNo - 1 >= TabIndex)
                {
                    if (PAxis.Para.Encoder)
                    {
                        lbl_Pos.Text = frmMain.AdvantechModule.GetRealPos(TabIndex, Axis).ToString("F3");
                    }
                    else
                    {
                        lbl_Pos.Text = frmMain.AdvantechModule.GetLogicPos(TabIndex, Axis).ToString("F3");
                    }
                }
                else if (TabIndex == 3)
                {
                    if (PAxis.Para.Encoder)
                    {
                        lbl_Pos.Text = frmMain.GalilModule.GetRealPos(0, Axis).ToString("F3");
                    }
                    else
                    {
                        lbl_Pos.Text = frmMain.GalilModule.GetLogicPos(0, Axis).ToString("F3");
                    }
                }
                else
                {
                    lbl_Pos.Text = frmMain.MoonStfModule.GetLogicPos(0, Axis).ToString("F3");
                }
            }
        }

        private IMotionControl MotionCtrlModule(int SelectedTabIndex)
        {
            var axisList = frmMain.AdvantechModule.GetMotionBoard(0);
            switch (SelectedTabIndex)
            {
                case 0:
                case 1:
                    axisList = frmMain.AdvantechModule.GetMotionBoard(SelectedTabIndex);
                    break;
                case 2:
                    axisList = frmMain.MoonStfModule.GetMotionBoard(0);
                    break;
                case 3:
                    axisList = frmMain.GalilModule.GetMotionBoard(0);
                    break;
            }

            return axisList;
        }

        private DialogResult PromptMessageOkCancel(string strmsg)
        {            
            InvokeHelper.Enable(this, false);
            string message = strmsg;
            msgForm = new frmMessaging2();
            msgForm.ShowMsg(strmsg, frmMessaging2.TMsgBtn.smbOK | frmMessaging2.TMsgBtn.smbCancel);
            DialogResult dialogResult = msgForm.ShowDialog();
            InvokeHelper.Enable(this, true);

            return dialogResult;
        }

        private void LogAction(Control ctrl, double Value = 0.0)
        {
            var axisList = SelectedTabIndex > 1 ? frmMain.MoonStfModule.GetMotionBoard(0) : frmMain.AdvantechModule.GetMotionBoard(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];

            string Description = this.Name + " - " + PAxis.Name + " : " + ctrl.Text;
            uctrlAuto.Page.AddToLog(Description);
            //using (var sysLogDB = new SystemLogUnitOfWork(SM.Connection))
            //{
            //    SysLog sysLog = new SysLog();
            //    sysLog.Date = DateTime.Today;
            //    sysLog.Time = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            //    sysLog.EventType = "Action";
            //    sysLog.Description = Description;
            //    sysLog.Code = ctrl.Name;
            //    sysLog.UserID = tpUser.Item3;
            //    sysLogDB.SystemLogRepository.Insert(sysLog);
            //    sysLogDB.Save();
            //}
        }

        private void tmr_UpdateDisplay_Tick(object sender, EventArgs e)
        {
            if (this.Visible)
            {
                if (spdSelection == 0)
                {
                    btn_Spd.Text = "Slow Jog";
                }                
                else if (spdSelection == 1)
                {
                    btn_Spd.Text = "Fast Jog";
                }

                if (SelectedAxis != -1)
                {
                    UpdateEncoderValue();
                    return;
                }
            }

            if (this.Visible)
            {
                if (SelectedAxis != -1)
                {
                    UpdateEncoderValue();
                    return;
                }
            }
        }

        private void tc_Axis_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTabIndex = tc_Axis.SelectedIndex;
            DisableButtons();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DialogResult result = PromptMessageOkCancel("Save Motor Param?" + (char)13 +
            "OK - Continue to Save." + (char)13 +
            "Cancel - Cancel Save.");

            if (result == DialogResult.Cancel)
            {
                return;
            }
            LogAction(sender as Button);

            var axisList = MotionCtrlModule(SelectedTabIndex);

            if (SelectedTabIndex <= 1)
            {
                frmMain.AdvantechModule.SaveParameters(SelectedTabIndex);
            }
            else if (SelectedTabIndex == 3)
            {
                frmMain.GalilModule.SaveParameters(0);
            }
            else
            {
                frmMain.MoonStfModule.SaveParameters(0);
            }
        }

        private void btn_Spd_Click(object sender, EventArgs e)
        {
            spdSelection++;
            if (spdSelection > 1) spdSelection = 0;
        }

        private void btn_Home_Click(object sender, EventArgs e)
        {
            //var axisList = SelectedTabIndex > 1 ? frmMain.MoonStfModule.GetMotionBoard(0) : frmMain.AdvantechModule.GetMotionBoard(SelectedTabIndex);
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];

            LogAction(sender as Button);

            string message = "Do you want to home " + PAxis.Name + " ?";
            string title = "Home Axis";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                return;
            }
            LogAction(sender as Button);

            #region Interlock Check
            if (PAxis.Name == TotalAxis.Rotation.ToString())
            {
                //Stn0 Home Sensor
                if (frmMain.MoonStfModule.SensLmtN(0, 0))
                {
                    PromptMessageOkCancel("Stn0 Z Not At Safe Pos!");
                    return;
                }
                //UnloadingStn Home Sensor
                if (frmMain.MoonStfModule.SensLmtN(0, 1))
                {
                    PromptMessageOkCancel("UnloadingStn Z Not At Safe Pos!");
                    return;
                }
                //Reje
                if (!frmMain.IOModule.ReadBit(1, 17))
                {
                    PromptMessageOkCancel("Reject Lifter Not At Safe Pos!");
                    return;
                }
            }
            #endregion

            HomingType motortype = new HomingType();

            var AxisName = (TotalAxis)Enum.Parse(typeof(TotalAxis), PAxis.Name);

            switch (AxisName)
            {
                case TotalAxis.Rotation:
                    {
                        motortype = HomingType.Without_NP;
                    }
                    break;
                case TotalAxis.UnloadRotary:
                    {
                        motortype = HomingType.Without_NP;
                    }
                    break;
                case TotalAxis.LoadingStnZ:
                    {
                        motortype = HomingType.LimitType;
                    }
                    break;
                case TotalAxis.UnloadStnZ:
                    {
                        motortype = HomingType.LimitType;
                    }
                    break;
            }

            axisList.Home(SelectedAxis, motortype);
        }

        private void btn_JogN_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }

            //var axisList = SelectedTabIndex > 1 ? frmMain.MoonStfModule.GetMotionBoard(0) : frmMain.AdvantechModule.GetMotionBoard(SelectedTabIndex);
            var axisList = MotionCtrlModule(SelectedTabIndex);
            LogAction(sender as Button);
            axisList.Jog(SelectedAxis, false, spdSelection);
            //TaskRun.SetMaintMsg(PAxis.RunModule, (int)eMESSAGE.MAINT_JOG_POS_MTR, PAxis.RunAxis, spd); if (e.Button != MouseButtons.Left) return;
        }

        private void btn_JogN_MouseUp(object sender, MouseEventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            //var axisList = SelectedTabIndex > 1 ? frmMain.MoonStfModule.GetMotionBoard(0) : frmMain.AdvantechModule.GetMotionBoard(SelectedTabIndex);            

            LogAction(sender as Button);
            axisList.ForceStop(SelectedAxis);
        }

        private void btn_JogP_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) { return; }

            //var axisList = SelectedTabIndex > 1 ? frmMain.MoonStfModule.GetMotionBoard(0) : frmMain.AdvantechModule.GetMotionBoard(SelectedTabIndex);
            var axisList = MotionCtrlModule(SelectedTabIndex);

            LogAction(sender as Button);
            axisList.Jog(SelectedAxis, true, spdSelection);
            //TaskRun.SetMaintMsg(PAxis.RunModule, (int)eMESSAGE.MAINT_JOG_POS_MTR, PAxis.RunAxis, spd); if (e.Button != MouseButtons.Left) return;
        }

        private void btn_JogP_MouseUp(object sender, MouseEventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);

            //var axisList = SelectedTabIndex > 1 ? frmMain.MoonStfModule.GetMotionBoard(0) : frmMain.AdvantechModule.GetMotionBoard(SelectedTabIndex);            

            LogAction(sender as Button);
            axisList.ForceStop(SelectedAxis);
        }

        private void lbl_DistPerPulse_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];

            UserCtrl.UserAdjustExecute("Dist Per Pulse (mm)", ref PAxis.Para.Unit.Resolution, 0.00001, 10000);
            LogAction(sender as Control, PAxis.Para.Unit.Resolution);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);
        }

        private void lbl_InvertDir_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            PAxis.Para.InvertPulse = !PAxis.Para.InvertPulse;
            LogAction(sender as Control, PAxis.Para.InvertPulse ? 1.0 : -1.0);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);
        }

        private void lbl_MtrAlmLogic_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];

            if (PAxis.Para.MotorAlarmType == EMotorAlarmType.NO)
            {
                PAxis.Para.MotorAlarmType = EMotorAlarmType.None;
            }
            else
            {
                PAxis.Para.MotorAlarmType++;
            }
            LogAction(sender as Control, (double)PAxis.Para.MotorAlarmType);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);
        }

        private void lbl_InvertMtrOn_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];

            PAxis.Para.InvertMtrOn = !PAxis.Para.InvertMtrOn;
            LogAction(sender as Control, PAxis.Para.InvertMtrOn ? 1.0 : -1.0);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);
        }

        private void lbl_HomeDir_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            if (PAxis.Para.Home.Dir == EHomeDir.N)
            {
                PAxis.Para.Home.Dir = EHomeDir.P;
            }
            else
            {
                PAxis.Para.Home.Dir = EHomeDir.N;
            }
            LogAction(sender as Control, (double)PAxis.Para.Home.Dir);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_Encoder_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            LogAction(sender as Control, PAxis.Para.Encoder ? 1.0 : -1.0);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);
        }

        private void lbl_HomeSlowV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 100;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);
            UserCtrl.UserAdjustExecute("Home Slow Speed (mm/s)", ref PAxis.Para.Home.SlowV, Min, Max);

            LogAction(sender as Control, PAxis.Para.Home.SlowV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_HomeFastV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 10000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);
            UserCtrl.UserAdjustExecute("Home Fast Speed (mm/s)", ref PAxis.Para.Home.FastV, Min, Max / 5);
            LogAction(sender as Control, PAxis.Para.Home.FastV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_HomeTimeOut_Click(object sender, EventArgs e)
        {
            var Board = MotionCtrlModule(SelectedTabIndex);
            var axisList = Board.AxisList;
            TAxis PAxis = axisList[SelectedAxis];

            UserCtrl.UserAdjustExecute("Home Timeout (ms)", ref PAxis.Para.Home.Timeout, 0, 60000);
            LogAction(sender as Control, PAxis.Para.Home.Timeout);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_JogSlowV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 100000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);

            UserCtrl.UserAdjustExecute("Jog Slow Speed (mm/s)", ref PAxis.Para.Jog.SlowV, 0, 60000);
            LogAction(sender as Control, PAxis.Para.Jog.SlowV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);
        }

        private void lbl_JogMedV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 100000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);
            UserCtrl.UserAdjustExecute("Jog Med Speed (mm/s)", ref PAxis.Para.Jog.MedV, Min, Max);

            LogAction(sender as Control, PAxis.Para.Jog.MedV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_JogFastV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 1000000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);
            UserCtrl.UserAdjustExecute("Jog Fast Speed (mm/s)", ref PAxis.Para.Jog.FastV, Min, Max);

            LogAction(sender as Control, PAxis.Para.Jog.FastV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_Accel_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 1000000000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);

            UserCtrl.UserAdjustExecute("Operation Accel (mm/s)", ref PAxis.Para.Accel, Min, Max);
            LogAction(sender as Control, PAxis.Para.Accel);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_StartV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 100000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);

            UserCtrl.UserAdjustExecute("Operation Start Speed (mm/s)", ref PAxis.Para.StartV, Min, Max);
            LogAction(sender as Control, PAxis.Para.StartV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_SlowV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 10000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);

            UserCtrl.UserAdjustExecute("Operation Slow Speed (mm/s)", ref PAxis.Para.SlowV, Min, Max);
            LogAction(sender as Control, PAxis.Para.SlowV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_FastV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 1000000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);
            UserCtrl.UserAdjustExecute("Operation Fast Speed (mm/s)", ref PAxis.Para.FastV, Min, Max);
            LogAction(sender as Control, PAxis.Para.FastV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);

        }

        private void lbl_MedV_Click(object sender, EventArgs e)
        {
            var axisList = MotionCtrlModule(SelectedTabIndex);
            TAxis PAxis = axisList.AxisList[SelectedAxis];
            double Min = 1;
            double Max = 1000;
            //axisList.GetMotorSpeedRange(SelectedAxis, ref Min, ref Max);
            UserCtrl.UserAdjustExecute("Operation Medium Speed (mm/s)", ref PAxis.Para.MedV, Min, Max);
            LogAction(sender as Control, PAxis.Para.MedV);
            UpdateDisplay(SelectedTabIndex, SelectedAxis);
        }
    }
}
