using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Ports;
using Infrastructure;
using SeqServer;

namespace Machine
{
    public partial class uctrlCommunication : UserControl
    {
        public uctrlCommunication()
        {
            InitializeComponent();
            UpdateCmbBox();
            this.cmb_NestID.SelectedIndexChanged += cmb_NestID_SelectedIndexChanged;
            this.radioButton_Bypass.Click += radioButton_Bypass_OnClick;
        }
        public static uctrlCommunication Page = new uctrlCommunication();
        public static bool AutoPageShow = false;

        private void RefreshOption(bool st, System.Windows.Forms.Label lbl)
        {
            if (st)
            {
                lbl.Text = "Enable";
                lbl.ForeColor = Color.Green;
            }
            else
            {
                lbl.Text = "Disable";
                lbl.ForeColor = Color.Red;
            }
        }        

        private string RefreshOption(bool st)
        {
            if (st)
            {
                return "Enable";
            }
            else
            {
                return "Disable";
            }
        }

        public void ShowPage(Control parent)
        {
            Page.Parent = parent;
            Page.Dock = DockStyle.Fill;
            Page.Show();
            UpdateVar();
        }
        public void HidePage()
        {
            Visible = false;
        }

        void UpdateVar()
        {
            txt_TopLaserIPAddress.Text = GDefine.LaserInIP;
            txt_TopLaserPort.Text = GDefine.LaserInPort;
            txt_BtmLaserIPAddress.Text = GDefine.LaserOutIP;
            txt_BottomLaserPort.Text = GDefine.LaserOutPort;
            txt_VisionOCRIPAddress.Text = GDefine.OCRIP;
            txt_VisionOCRPort.Text = GDefine.OCRPort;
            txt_CognexMainIP.Text = GDefine.CognexMainIP;
            txt_CognexMainPort.Text = GDefine.CognexMainPort;
            txt_Top2DIDIPAddress.Text = GDefine.KeyenceInIP;
            txt_Top2DIDPort.Text = GDefine.KeyenceInPort;
            txt_Btm2DIDIPAddress.Text = GDefine.KeyenceOutIP;
            txt_Btm2DIDPort.Text = GDefine.KeyenceOutPort;
            numericUpDown_VacOn.Value = (decimal)GDefine._bVacOnvalue;
            numericUpDown_VacOff.Value = (decimal)GDefine._bVacOffvalue;
            numericUpDown_CylTimeOut.Value = (decimal)GDefine._bCylTimeOut;
            numericUpDown1_CylBuf.Value = (decimal)GDefine._bCylBuffer;
            numericUpDown1_WaitInterval.Value = (int)GDefine._bWaitInterval;

            numericUpDown_InProdCnt.Value = GDefine._bInProdCnt;
            numericUpDown_OutProdCnt.Value = GDefine._bOutProdCnt;
        }

        private void tmrUpdateDisplay_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;
            tmrUpdateDisplay.Enabled = false;

            //lbl__TopLaserFeedback.Text = TaskLaserInput.RXData;
            btn_TopLaserDisconnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserTop) ? true : false;
            btn_TopLaserConnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserTop) ? false : true;
            lbl_TopLaserconnectStatus.Text = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserTop) ? "Connected..." : "Disconnected";
            lbl_TopLaserconnectStatus.BackColor = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserTop) ? Color.LimeGreen : Color.Red;
            //lbl__TopLaserFeedback.Text = frmMain.MainConnection.ModuleFeedBack(TcpModuleID.LaserTop);

            //lbl__BottomLaserFeedback.Text = TaskLaserOutput.RXData;
            btn_BottomLaserDisconnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserBtm) ? true : false;
            btn_BottomLaserConnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserBtm) ? false : true;
            lbl_BottomLaserconnectStatus.Text = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserBtm) ? "Connected..." : "Disconnected";
            lbl_BottomLaserconnectStatus.BackColor = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserBtm) ? Color.LimeGreen : Color.Red;
            //lbl__BottomLaserFeedback.Text = frmMain.MainConnection.ModuleFeedBack(TcpModuleID.LaserBtm);


            btn_VisionOCRDisconnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexOCR) ? true : false;
            btn_VisionOCRConnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexOCR) ? false : true;
            lbl_VisionOCRconnectStatus.Text = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexOCR) ? "Connected..." : "Disconnected";
            lbl_VisionOCRconnectStatus.BackColor = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexOCR) ? Color.LimeGreen : Color.Red;
            //lbl_VisionFeedback.Text = frmMain.MainConnection.ModuleFeedBack(TcpModuleID.CognexOCR);           

            btn_Top2DIDDisconnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceTop) ? true : false;
            btn_Top2DIDConnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceTop) ? false : true;            
            lbl_Top2DIDconnectStatus.Text = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceTop) ? "Connected..." : "Disconnected";
            lbl_Top2DIDconnectStatus.BackColor = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceTop) ? Color.LimeGreen : Color.Red;
            //lbl_Top2DIDFeedback.Text = frmMain.MainConnection.ModuleFeedBack(TcpModuleID.KeyenceTop);

            btn_Btm2DIDDisconnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceBtm) ? true : false;
            btn_Btm2DIDConnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceBtm) ? false : true;
            lbl_Btm2DIDconnectStatus.Text = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceBtm) ? "Connected..." : "Disconnected";
            lbl_Btm2DIDconnectStatus.BackColor = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceBtm) ? Color.LimeGreen : Color.Red;
            //lbl_Btm2DIDFeedback.Text = frmMain.MainConnection.ModuleFeedBack(TcpModuleID.KeyenceBtm);

            btn_CognexMainDisconnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? true : false;
            btn_CognexMainConnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? false : true;
            lbl_CognexMainConnectStatus.Text = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? "Connected..." : "Disconnected";
            lbl_CognexMainConnectStatus.BackColor = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? Color.LimeGreen : Color.Red;
            //lbl_CognexMainFeedback.Text = frmMain.MainConnection.ModuleFeedBack(TcpModuleID.CognexServer);


            RefreshOption(GDefine._bEnabInLaser, lbl_EnableInputLaser);
            RefreshOption(GDefine._bEnabOutLaser, lbl_EnableOutputLaser);
            RefreshOption(GDefine._bEnabCognexOCR, lbl_EnableCognexOCR);
            RefreshOption(GDefine._bEnabInKeyence, lbl_EnableInputKeyence2DID);
            RefreshOption(GDefine._bEnabOutKeyence, lbl_EnableOutputKeyence2DID);
            RefreshOption(GDefine._bEnabDoorCheck, lbl_EnableDoorCheck);
            RefreshOption(GDefine._bEnabLaserMotor, lbl_EnableLaserMotor);
            RefreshOption(GDefine._bEnabInKeyenceStopWhenReadFail, lbl_StopWhenReadFailIn2DID);
            RefreshOption(GDefine._bEnabOutKeyenceStopWhenReadFail, lbl_StopWhenReadFailOut2DID);
            RefreshOption(GDefine._bFailUnitUnloadToPass, lbl_FailUnitUnloadToPass);
            RefreshOption(GDefine._bEnableUnloadRtr, lbl_EnableUnloadRtr);
            RefreshOption(GDefine._bEnableLeftPnp, lbl_EnablePnpLeft);
            RefreshOption(GDefine._bEnableRightPnp, lbl_EnablePnpRight);


            tmrUpdateDisplay.Enabled = true;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            DialogResult result = uctrlAuto.Page.PromptMessageOkCancel("Do you want to save the Configuration?");
            if (result == DialogResult.Cancel)
            {
                return;
            }

            if (tabControl1.SelectedTab == tabPage4)
            {
                GDefine.SaveComParam();
                frmMain.mytagSeqFlag.InProdCount = GDefine._bInProdCnt = (int)numericUpDown_InProdCnt.Value;
                frmMain.mytagSeqFlag.OutProdCount = GDefine._bOutProdCnt = (int)numericUpDown_OutProdCnt.Value;
                frmMain.mytagSeqFlag.VacOnInterval = GDefine._bVacOnvalue =(double)numericUpDown_VacOn.Value;
                frmMain.mytagSeqFlag.VacOffInterval = GDefine._bVacOffvalue = (double)numericUpDown_VacOff.Value;
                frmMain.mytagSeqFlag.SettlingiTime = GDefine._bWaitInterval = (int)numericUpDown1_WaitInterval.Value;
                frmMain.mytagSeqFlag.CylTimeOut = GDefine._bCylTimeOut = (double)numericUpDown_CylTimeOut.Value;
                frmMain.mytagSeqFlag.CylBuffer = GDefine._bCylBuffer = (int)numericUpDown1_CylBuf.Value;
                GDefine.SaveDefaultFile();
            }

            if (tabControl1.SelectedTab == tab_Nestcfg)
            {
                if (cmb_NestID.SelectedIndex != -1)
                {
                    if (!TextVerify(
                        textBox_TM1.Text,
                        textBox_TM2.Text,
                        textBox_TM3.Text,
                        textBox_TM4.Text,
                        textBox_TM5.Text,
                        textBox_TM6.Text))
                    {
                        MessageBox.Show("Top Marking Offset Text Wrong format");
                        return;
                    }

                    if (!TextVerify(
                        textBox_BM1.Text,
                        textBox_BM2.Text,
                        textBox_BM3.Text,
                        textBox_BM4.Text,
                        textBox_BM5.Text,
                        textBox_BM6.Text))
                    {
                        MessageBox.Show("Btm Marking Offset Text Wrong format");
                        return;
                    }

                    var _TM = ConcText(
                        textBox_TM1.Text,
                        textBox_TM2.Text,
                        textBox_TM3.Text,
                        textBox_TM4.Text,
                        textBox_TM5.Text,
                        textBox_TM6.Text);

                    var _BM = ConcText(
                        textBox_BM1.Text,
                        textBox_BM2.Text,
                        textBox_BM3.Text,
                        textBox_BM4.Text,
                        textBox_BM5.Text,
                        textBox_BM6.Text);

                    _TM = _TM.Replace(" ", "");
                    _BM = _BM.Replace(" ", "");

                    double[] nestheighoffset = new double[(int)TotalLaser.TotalLaser];
                    string[] nestmarkingoffset = new string[(int)TotalLaser.TotalLaser];

                    nestheighoffset[(int)TotalLaser.TopLaser] = (double)numericUpDown_TH.Value;
                    nestmarkingoffset[(int)TotalLaser.TopLaser] = _TM;

                    nestheighoffset[(int)TotalLaser.BtmLaser] = (double)numericUpDown_BH.Value;
                    nestmarkingoffset[(int)TotalLaser.BtmLaser] = _BM;

                    var Unit = new Unit
                    {
                        IsPsnt = false,
                        UnitStatus = UnitStatus.Empty,
                        GeneratedBarcode = string.Empty,
                        LotCounter = 0,
                        NestHeightOffset = nestheighoffset,
                        MarkingOffset = nestmarkingoffset
                    };

                    if (frmMain.SequenceRun.FixZoneData.Any(x => x.Nest_ID == cmb_NestID.SelectedIndex))
                    {
                        var item = frmMain.SequenceRun.FixZoneData.FirstOrDefault(x => x.Nest_ID == cmb_NestID.SelectedIndex);
                        int index = frmMain.SequenceRun.FixZoneData.IndexOf(item);

                        frmMain.SequenceRun.FixZoneData[index].IsBypass = radioButton_Bypass.Checked;
                        frmMain.SequenceRun.FixZoneData[index].Unit = Unit;

                        frmMain.SequenceRun.SaveFixZoneData();
                    }
                }
            }
        }
        public bool isValidIP(string ip)
        {
            string pattern = @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$";

            Regex check = new Regex(pattern);
            bool valid = false;

            if (ip == "")
            {

                valid = false;
                return false;
            }
            else
            {
                valid = check.IsMatch(ip, 0);
            }

            if (!valid)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Invalid IP Address." + (char)13 + "Please Insert Again.",
                                               frmMessaging.TMsgBtn.smbOK);
            }

            return valid;
        }
        private void btn_TopLaserConnect_Click(object sender, EventArgs e)
        {
            if (txt_TopLaserIPAddress.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Input Top Laser IPAddress.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (txt_TopLaserPort.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Input Top Laser Port Number.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (isValidIP(txt_TopLaserIPAddress.Text))
            {
                if (txt_TopLaserPort.Text.Length < 4) return;
                GDefine.LaserInIP = txt_TopLaserIPAddress.Text;
                GDefine.LaserInPort = txt_TopLaserPort.Text;
                try
                {

                    //TaskLaserInput.ConnectToHost(ref GDefine.LaserInIP, ref GDefine.LaserInPort);
                    frmMain.MainConnection.BuildSingleConnection(TcpModuleID.LaserTop, GDefine.LaserInIP, GDefine.LaserInPort);
                    if (frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserTop))
                    {
                        btn_TopLaserDisconnect.Enabled = GDefine.LaserInConn.Connected;
                        btn_TopLaserConnect.Enabled = false;
                        lbl_TopLaserconnectStatus.Text = "Connected...";
                        lbl_TopLaserconnectStatus.BackColor = Color.LimeGreen;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn__TopLaserDisconnect_Click(object sender, EventArgs e)
        {
            frmMain.MainConnection.DcSingleConnection(TcpModuleID.LaserTop);
            btn_TopLaserConnect.Enabled = true;
            btn_TopLaserDisconnect.Enabled = GDefine.LaserInConn.Connected;
            lbl_TopLaserconnectStatus.Text = "Disconnected...";
            lbl_TopLaserconnectStatus.BackColor = Color.Red;
        }

        private void btn_BottomLaserConnect_Click(object sender, EventArgs e)
        {
            if (txt_BtmLaserIPAddress.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Output Bottom Laser IPAddress.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (txt_BottomLaserPort.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Output Bottom Laser Port Number.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (isValidIP(txt_BtmLaserIPAddress.Text))
            {
                if (txt_BottomLaserPort.Text.Length < 4) return;
                GDefine.LaserOutIP = txt_BtmLaserIPAddress.Text;
                GDefine.LaserOutPort = txt_BottomLaserPort.Text;
                try
                {

                    //TaskLaserOutput.ConnectToHost(ref GDefine.LaserOutIP, ref GDefine.LaserOutPort);
                    frmMain.MainConnection.BuildSingleConnection(TcpModuleID.LaserBtm, GDefine.LaserOutIP, GDefine.LaserOutPort);
                    if (frmMain.MainConnection.ModuleIsConnect(TcpModuleID.LaserBtm))
                    {
                        btn_BottomLaserDisconnect.Enabled = GDefine.LaserOutConn.Connected;
                        btn_BottomLaserConnect.Enabled = false;
                        lbl_BottomLaserconnectStatus.Text = "Connected...";
                        lbl_BottomLaserconnectStatus.BackColor = Color.LimeGreen;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_BottomLaserDisconnect_Click(object sender, EventArgs e)
        {
            frmMain.MainConnection.DcSingleConnection(TcpModuleID.LaserBtm);
            btn_BottomLaserConnect.Enabled = true;
            btn_BottomLaserDisconnect.Enabled = GDefine.LaserInConn.Connected;
            lbl_BottomLaserconnectStatus.Text = "Disconnected...";
            lbl_BottomLaserconnectStatus.BackColor = Color.Red;
        }

        private void btn_VisionOCRConnect_Click(object sender, EventArgs e)
        {
            if (txt_VisionOCRIPAddress.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Cognex OCR IPAddress.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (txt_VisionOCRPort.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Output Cognex OCR Port Number.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (isValidIP(txt_VisionOCRIPAddress.Text))
            {
                if (txt_VisionOCRPort.Text.Length < 4) return;
                GDefine.OCRIP = txt_VisionOCRIPAddress.Text;
                GDefine.OCRPort = txt_VisionOCRPort.Text;
                try
                {

                    frmMain.MainConnection.BuildSingleConnection(TcpModuleID.CognexOCR, GDefine.OCRIP, GDefine.OCRPort);
                    if (frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexOCR))
                    {
                        btn_VisionOCRDisconnect.Enabled = GDefine.OCRConn.Connected;
                        btn_VisionOCRConnect.Enabled = false;
                        lbl_VisionOCRconnectStatus.Text = "Connected...";
                        lbl_VisionOCRconnectStatus.BackColor = Color.LimeGreen;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_VisionOCRDisconnect_Click(object sender, EventArgs e)
        {
            frmMain.MainConnection.DcSingleConnection(TcpModuleID.CognexOCR);
            btn_VisionOCRConnect.Enabled = true;
            btn_VisionOCRDisconnect.Enabled = GDefine.OCRConn.Connected;
            lbl_VisionOCRconnectStatus.Text = "Disconnected...";
            lbl_VisionOCRconnectStatus.BackColor = Color.Red;
        }

        private void btn_Top2DIDConnect_Click(object sender, EventArgs e)
        {
            if (txt_Top2DIDIPAddress.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Input Top 2DID IPAddress.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (txt_Top2DIDPort.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Output Input Top 2DID Port Number.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (isValidIP(txt_Top2DIDIPAddress.Text))
            {
                if (txt_Top2DIDPort.Text.Length < 4) return;
                GDefine.KeyenceInIP = txt_Top2DIDIPAddress.Text;
                GDefine.KeyenceInPort = txt_Top2DIDPort.Text;
                try
                {

                    //TaskKeyenceInput.ConnectToHost(ref GDefine.KeyenceInIP, ref GDefine.KeyenceInPort);
                    frmMain.MainConnection.BuildSingleConnection(TcpModuleID.KeyenceTop, GDefine.KeyenceInIP, GDefine.KeyenceInPort);
                    if (frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceTop))
                    {
                        btn_Top2DIDDisconnect.Enabled = GDefine.KeyenceInConn.Connected;
                        btn_Top2DIDConnect.Enabled = false;
                        lbl_Top2DIDconnectStatus.Text = "Connected...";
                        lbl_Top2DIDconnectStatus.BackColor = Color.LimeGreen;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_Top2DIDDisconnect_Click(object sender, EventArgs e)
        {
            frmMain.MainConnection.DcSingleConnection(TcpModuleID.KeyenceTop);
            btn_Top2DIDConnect.Enabled = true;
            btn_Top2DIDDisconnect.Enabled = GDefine.OCRConn.Connected;
            lbl_Top2DIDconnectStatus.Text = "Disconnected...";
            lbl_Top2DIDconnectStatus.BackColor = Color.Red;
        }

        private void btn_Btm2DIDConnect_Click(object sender, EventArgs e)
        {
            if (txt_Btm2DIDIPAddress.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Output Bottom 2DID IPAddress.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (txt_Btm2DIDPort.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Output Output Bottom 2DID Port Number.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (isValidIP(txt_Btm2DIDIPAddress.Text))
            {
                if (txt_Btm2DIDPort.Text.Length < 4) return;
                GDefine.KeyenceOutIP = txt_Btm2DIDIPAddress.Text;
                GDefine.KeyenceOutPort = txt_Btm2DIDPort.Text;
                try
                {

                    //TaskKeyenceOutput.ConnectToHost(ref GDefine.KeyenceOutIP, ref GDefine.KeyenceOutPort);
                    frmMain.MainConnection.BuildSingleConnection(TcpModuleID.KeyenceBtm, GDefine.KeyenceOutIP, GDefine.KeyenceOutPort);
                    if (frmMain.MainConnection.ModuleIsConnect(TcpModuleID.KeyenceBtm))
                    {
                        btn_Btm2DIDDisconnect.Enabled = GDefine.KeyenceOutConn.Connected;
                        btn_Btm2DIDConnect.Enabled = false;
                        lbl_Btm2DIDconnectStatus.Text = "Connected...";
                        lbl_Btm2DIDconnectStatus.BackColor = Color.LimeGreen;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void btn_Btm2DIDDisconnect_Click(object sender, EventArgs e)
        {
            frmMain.MainConnection.DcSingleConnection(TcpModuleID.KeyenceBtm);
            btn_Btm2DIDConnect.Enabled = true;
            btn_Btm2DIDDisconnect.Enabled = GDefine.OCRConn.Connected;
            lbl_Btm2DIDconnectStatus.Text = "Disconnected...";
            lbl_Btm2DIDconnectStatus.BackColor = Color.Red;
        }

        private enum CommandSeq
        {
            EOS,
            SendCommand,
            WaitReceive,
            ReceiveError,
            CheckReceiveMsg,
        }

        private enum SETOCRRecipe
        {
            EOS,
            UserLogin,
            SetPassword,
            SetOffline,
            LoadFile,
            SetONline,
        }

        private void SendCommand_WOfeedback(TcpModuleID module, Command enumcommand, Label feedbacklbl)
        {
            CommandSeq sendcommand = CommandSeq.SendCommand;            ;
            int timeout = Environment.TickCount + 10000;
            int Starttime = Environment.TickCount;
            
            while (true)
            {
                switch (sendcommand)
                {
                    case CommandSeq.SendCommand:
                        {
                            frmMain.MainConnection.SendModuleCommand(module, enumcommand);
                            uctrlAuto.Page.AddToLog($"Manual: Module = {module}, Command = {enumcommand}");
                            sendcommand = CommandSeq.WaitReceive;
                        }
                        break;
                    case CommandSeq.WaitReceive:
                        {
                            if (frmMain.MainConnection.ModuleIsReceived(module))
                            {
                                sendcommand = CommandSeq.CheckReceiveMsg;
                            }
                            else if (Environment.TickCount > timeout)
                            {
                                sendcommand = CommandSeq.ReceiveError;

                            }
                        }
                        break;
                    case CommandSeq.ReceiveError:
                        {
                            var result = uctrlAuto.Page.PromptMessageOkCancel("Manual: Receive Message Time Out");
                            if (result == DialogResult.Retry)
                            {
                                sendcommand = CommandSeq.SendCommand;
                            }
                            else
                            {
                                return;
                            }
                        }
                        break;
                    case CommandSeq.CheckReceiveMsg:
                        {
                            string rawresult = frmMain.MainConnection.ModuleFeedBack(module);
                            feedbacklbl.Text += rawresult;
                            string result = rawresult.Replace("\t", "").Replace("\r", "").Replace("\n", "");
                            string[] splitresult = rawresult.Split(',');

                            //if (!GDefine._bEnabInKeyenceStopWhenReadFail)
                            //{
                            //    sendcommand = CommandSeq.EOS ;
                            //    return;
                            //}
                            if (result.Contains("NG") || result == "ER" || result.Length < 4)
                            {
                                //Laser Reply Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return;
                            }
                            else if (result.Contains("?") || result.Contains("Err"))
                            {
                                //Cognex OCR Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return;
                            }
                            else if (result.Contains("OK") || splitresult[0].Contains("K"))
                            {
                                //Pass
                                sendcommand = CommandSeq.EOS;
                                return;
                            }

                            else
                            {
                                sendcommand = CommandSeq.EOS;
                            }
                            return;
                        }
                        break;
                }
                
            }
        }

        private bool SendCommand_WfeedBack(TcpModuleID module, Command enumcommand, Label feedbacklbl, Command enum_command)
        {
            CommandSeq sendcommand = CommandSeq.SendCommand; 
            string enum_Description = CommonFunc.GetEnumDescription(enum_command);

            int timeout = Environment.TickCount + 10000;
            int Starttime = Environment.TickCount;

            while (true)
            {
                switch (sendcommand)
                {
                    case CommandSeq.SendCommand:
                        {
                            frmMain.MainConnection.SendModuleCommand(module, enumcommand);
                            uctrlAuto.Page.AddToLog($"Manual: Module = {module}, Command = {enumcommand}");
                            sendcommand = CommandSeq.WaitReceive;
                        }
                        break;
                    case CommandSeq.WaitReceive:
                        {
                            if (frmMain.MainConnection.ModuleIsReceived(module))
                            {
                                sendcommand = CommandSeq.CheckReceiveMsg;
                            }
                            else if (Environment.TickCount > timeout)
                            {
                                sendcommand = CommandSeq.ReceiveError;

                            }
                        }
                        break;
                    case CommandSeq.ReceiveError:
                        {
                            var result = uctrlAuto.Page.PromptMessageOkCancel("Manual: Receive Message Time Out");
                            if (result == DialogResult.Retry)
                            {
                                sendcommand = CommandSeq.SendCommand;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case CommandSeq.CheckReceiveMsg:
                        {
                            string rawresult = frmMain.MainConnection.ModuleFeedBack(module);
                            feedbacklbl.Text += rawresult;

                            string result = rawresult.Replace("\t", "").Replace("\r", "").Replace("\n", "");
                            string[] splitresult = rawresult.Split(',');

                            //if (!GDefine._bEnabInKeyenceStopWhenReadFail)
                            //{
                            //    sendcommand = CommandSeq.EOS ;
                            //    return;
                            //}
                            if (result.Contains("NG") || result == "ER" || result.Length < 4)
                            {
                                //Laser Reply Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains("?") || result.Contains("Err"))
                            {
                                //Cognex OCR Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains(enum_Description))
                            {
                                //Pass
                                feedbacklbl.Text += enum_Description;
                                sendcommand = CommandSeq.EOS;
                                return true;
                            }
                           
                            return false;
                        }
                        break;

                }
            }
        }
        

        private void SetOCRRecipe_Wlabel(Command loadocr)
        {
            SETOCRRecipe sendcommand = SETOCRRecipe.UserLogin;

            while (true)
            {
                switch (sendcommand)
                {
                    case SETOCRRecipe.UserLogin:
                        {
                            if(SendCommand_WfeedBack(TcpModuleID.CognexServer, Command.Login, lbl_CognexMainFeedback, Command.LoggedIn))
                            sendcommand = SETOCRRecipe.SetOffline;

                            else
                                return;
                        }
                        break;
                    case SETOCRRecipe.SetOffline:
                        {
                            if (SendCommand_WfeedBack(TcpModuleID.CognexServer, Command.SetOffLine, lbl_CognexMainFeedback, Command.CognexServerSetDone))
                                sendcommand = SETOCRRecipe.LoadFile;
                            else return ;
                        }
                        break;
                    case SETOCRRecipe.LoadFile:
                        {
                            if (SendCommand_WfeedBack(TcpModuleID.CognexServer, loadocr, lbl_CognexMainFeedback, Command.CognexServerSetDone))
                                sendcommand = SETOCRRecipe.SetONline;
                            else return;
                        }
                        break;
                    case SETOCRRecipe.SetONline:
                        {
                            if(SendCommand_WfeedBack(TcpModuleID.CognexServer, Command.SetOnline, lbl_CognexMainFeedback, Command.CognexServerSetDone))
                            sendcommand = SETOCRRecipe.EOS;
                            else return;
                        }
                        break;
                }
            }
        }

        public static bool SendCommand(TcpModuleID module, Command enumcommand, Command enum_command)
        {
            CommandSeq sendcommand = CommandSeq.SendCommand;
            string enum_Description = CommonFunc.GetEnumDescription(enum_command);

            int timeout = Environment.TickCount + 10000;

            while (true)
            {
                switch (sendcommand)
                {
                    case CommandSeq.SendCommand:
                        {
                            frmMain.MainConnection.SendModuleCommand(module, enumcommand);
                            uctrlAuto.Page.AddToLog($"Manual: Module = {module}, Command = {enumcommand}");
                            sendcommand = CommandSeq.WaitReceive;
                        }
                        break;
                    case CommandSeq.WaitReceive:
                        {
                            if (frmMain.MainConnection.ModuleIsReceived(module))
                            {
                                sendcommand = CommandSeq.CheckReceiveMsg;
                            }
                            else if (Environment.TickCount > timeout)
                            {
                                sendcommand = CommandSeq.ReceiveError;

                            }
                        }
                        break;
                    case CommandSeq.ReceiveError:
                        {
                            var result = uctrlAuto.Page.PromptMessageOkCancel("Manual: Receive Message Time Out");
                            if (result == DialogResult.Retry)
                            {
                                sendcommand = CommandSeq.SendCommand;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case CommandSeq.CheckReceiveMsg:
                        {
                            string rawresult = frmMain.MainConnection.ModuleFeedBack(module);

                            string result = rawresult.Replace("\t", "").Replace("\r", "").Replace("\n", "");
                            string[] splitresult = rawresult.Split(',');

                            //if (!GDefine._bEnabInKeyenceStopWhenReadFail)
                            //{
                            //    sendcommand = CommandSeq.EOS ;
                            //    return;
                            //}
                            if (result.Contains("NG") || result == "ER")
                            {
                                //Laser Reply Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains("?") || result.Contains("Err"))
                            {
                                //Cognex OCR Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains(enum_Description))
                            {
                                //Pass
                                sendcommand = CommandSeq.EOS;
                                return true;
                            }

                            return false;
                        }
                        break;

                }
            }
        }

        public static bool SendDualCommand(TcpModuleID module, Command Command_1, Command Command_2, Command Receive_Enum)
        {
            CommandSeq sendcommand = CommandSeq.SendCommand;
            string ReceiveEnum_Description = CommonFunc.GetEnumDescription(Receive_Enum);

            int timeout = Environment.TickCount + 10000;

            while (true)
            {
                switch (sendcommand)
                {
                    case CommandSeq.SendCommand:
                        {
                            frmMain.MainConnection.SendModuleCommand(module, Command_1);
                            frmMain.MainConnection.SendModuleCommand(module, Command_2);
                            uctrlAuto.Page.AddToLog($"Manual: Module = {module}, Command = {Command_1}");
                            uctrlAuto.Page.AddToLog($"Manual: Module = {module}, Command = {Command_2}");
                            sendcommand = CommandSeq.WaitReceive;
                        }
                        break;
                    case CommandSeq.WaitReceive:
                        {
                            if (frmMain.MainConnection.ModuleIsReceived(module))
                            {
                                sendcommand = CommandSeq.CheckReceiveMsg;
                            }
                            else if (Environment.TickCount > timeout)
                            {
                                sendcommand = CommandSeq.ReceiveError;

                            }
                        }
                        break;
                    case CommandSeq.ReceiveError:
                        {
                            var result = uctrlAuto.Page.PromptMessageOkCancel("Manual: Receive Message Time Out");
                            if (result == DialogResult.Retry)
                            {
                                sendcommand = CommandSeq.SendCommand;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case CommandSeq.CheckReceiveMsg:
                        {
                            string rawresult = frmMain.MainConnection.ModuleFeedBack(module);

                            string result = rawresult.Replace("\t", "").Replace("\r", "").Replace("\n", "");
                            string[] splitresult = rawresult.Split(',');

                            //if (!GDefine._bEnabInKeyenceStopWhenReadFail)
                            //{
                            //    sendcommand = CommandSeq.EOS ;
                            //    return;
                            //}
                            if (result.Contains("NG") || result == "ER")
                            {
                                //Laser Reply Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains("?") || result.Contains("Err"))
                            {
                                //Cognex OCR Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains(ReceiveEnum_Description))
                            {
                                //Pass
                                sendcommand = CommandSeq.EOS;
                                return true;
                            }

                            return false;
                        }
                        break;

                }
            }
        }
        public static bool SendCustomCommand(TcpModuleID module, string enumcommand, Command enum_command)
        {
            CommandSeq sendcommand = CommandSeq.SendCommand;
            string enum_Description = CommonFunc.GetEnumDescription(enum_command);

            int timeout = Environment.TickCount + 10000;

            while (true)
            {
                switch (sendcommand)
                {
                    case CommandSeq.SendCommand:
                        {
                            frmMain.MainConnection.SendCustomCommand(module, enumcommand);
                            uctrlAuto.Page.AddToLog($"Manual: Module = {module}, Command = {enumcommand}");
                            sendcommand = CommandSeq.WaitReceive;
                        }
                        break;
                    case CommandSeq.WaitReceive:
                        {
                            if (frmMain.MainConnection.ModuleIsReceived(module))
                            {
                                sendcommand = CommandSeq.CheckReceiveMsg;
                            }
                            else if (Environment.TickCount > timeout)
                            {
                                sendcommand = CommandSeq.ReceiveError;

                            }
                        }
                        break;
                    case CommandSeq.ReceiveError:
                        {
                            var result = uctrlAuto.Page.PromptMessageOkCancel("Manual: Receive Message Time Out");
                            if (result == DialogResult.Retry)
                            {
                                sendcommand = CommandSeq.SendCommand;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        break;
                    case CommandSeq.CheckReceiveMsg:
                        {
                            string rawresult = frmMain.MainConnection.ModuleFeedBack(module);

                            string result = rawresult.Replace("\t", "").Replace("\r", "").Replace("\n", "");
                            string[] splitresult = rawresult.Split(',');

                            //if (!GDefine._bEnabInKeyenceStopWhenReadFail)
                            //{
                            //    sendcommand = CommandSeq.EOS ;
                            //    return;
                            //}
                            if (result.Contains("NG") || result == "ER")
                            {
                                //Laser Reply Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains("?") || result.Contains("Err"))
                            {
                                //Cognex OCR Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (result.Contains(enum_Description))
                            {
                                //Pass
                                sendcommand = CommandSeq.EOS;
                                return true;
                            }

                            return false;
                        }
                        break;

                }
            }
        }

        public static bool ChangeLaserModel(string Drive, TcpModuleID module, Command enumcommand, Command PassReplyCommand)
        {
            CommandSeq sendcommand = CommandSeq.SendCommand;
            string Enum_Description = CommonFunc.GetEnumDescription(enumcommand);
            string Pass_Description = CommonFunc.GetEnumDescription(PassReplyCommand);

            int timeout = Environment.TickCount + 5000;
            int Starttime = Environment.TickCount;

            while (true)
            {
                switch (sendcommand)
                {
                    case CommandSeq.SendCommand:
                        {
                            string Model = Enum_Description + Drive + ".lf";
                            frmMain.MainConnection.SendCustomCommand(module, Model);
                            uctrlAuto.Page.AddToLog($"Manual: Module = {module}, Command = {enumcommand}");
                            timeout = Environment.TickCount + 5000; 
                            sendcommand = CommandSeq.WaitReceive;
                        }
                        break;
                    case CommandSeq.WaitReceive:
                        {
                            if (frmMain.MainConnection.ModuleIsReceived(module))
                            {
                                timeout = Environment.TickCount + 5000;
                                sendcommand = CommandSeq.CheckReceiveMsg;
                            }
                            else if (Environment.TickCount > timeout)
                            {
                                uctrlAuto.Page.PromptMessageOkCancel($"{module}: Laser Wait Tcp Respond too long");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                        }
                        break;
                   
                    case CommandSeq.CheckReceiveMsg:
                        {
                            string rawresult = frmMain.MainConnection.ModuleFeedBack(module);

                            string result = rawresult.Replace("\t", "").Replace("\r", "").Replace("\n", "");

                            if (result.Contains(Pass_Description))
                            {
                                sendcommand = CommandSeq.EOS;
                                return true;
                            }
                            else if (result.Contains("NG"))
                            {
                                //Laser Reply Fail
                                var option = uctrlAuto.Page.PromptMessageOkCancel($"Manual: Module - {module}, Result :{result}");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            else if (Environment.TickCount > timeout)
                            {
                                uctrlAuto.Page.PromptMessageOkCancel($"{module}: Laser Change Model Fail");
                                sendcommand = CommandSeq.EOS;
                                return false;
                            }
                            //else
                            //{
                            //    return false;
                            //}

                        }
                        break;

                }
            }
        }


        public static bool SetOCRRecipe(Enum_OCRRecipe loadocr)
        {
            SETOCRRecipe sendcommand = SETOCRRecipe.UserLogin;

            while (true)
            {
                switch (sendcommand)
                {
                    case SETOCRRecipe.UserLogin:
                        {
                            if (SendCommand(TcpModuleID.CognexServer, Command.Login, Command.Received_Password))
                                sendcommand = SETOCRRecipe.SetPassword;

                            else
                            {
                                uctrlAuto.Page.PromptMessageOkCancel($"OCR Server:{SETOCRRecipe.UserLogin}, Fail");
                                return false;
                            }
                        }
                        break;
                    case SETOCRRecipe.SetPassword:
                        {
                            if (SendCommand(TcpModuleID.CognexServer, Command.Password, Command.LoggedIn))
                                sendcommand = SETOCRRecipe.SetOffline;

                            else
                            {
                                uctrlAuto.Page.PromptMessageOkCancel($"OCR Server:{SETOCRRecipe.SetPassword}, Fail");
                                return false;
                            }
                        }
                        break;
                    case SETOCRRecipe.SetOffline:
                        {
                            if (SendCommand(TcpModuleID.CognexServer, Command.SetOffLine, Command.CognexServerSetDone))
                                sendcommand = SETOCRRecipe.LoadFile;
                            else
                            {
                                uctrlAuto.Page.PromptMessageOkCancel($"OCR Server:{SETOCRRecipe.SetOffline}, Fail");
                                return false;
                            }
                        }
                        break;
                    case SETOCRRecipe.LoadFile:
                        {
                            string command = "lf" + loadocr.ToString() + "\r\n";
                            if (SendCustomCommand(TcpModuleID.CognexServer, command, Command.CognexServerSetDone))
                                sendcommand = SETOCRRecipe.SetONline;
                            else
                            {
                                uctrlAuto.Page.PromptMessageOkCancel($"OCR Server:{SETOCRRecipe.LoadFile}, Fail");
                                return false;
                            }
                        }
                        break;
                    case SETOCRRecipe.SetONline:
                        {
                            if (SendCommand(TcpModuleID.CognexServer, Command.SetOnline, Command.CognexServerSetDone))
                            {
                                sendcommand = SETOCRRecipe.EOS;
                                return true;
                            }
                            else
                            {
                                uctrlAuto.Page.PromptMessageOkCancel($"OCR Server:{SETOCRRecipe.SetONline}, Fail");
                                return false;
                            }
                        }
                        break;
                }
            }
        }
        private void btn_TopLaserStatus_Click(object sender, EventArgs e)
        {
            SendCommand_WfeedBack(TcpModuleID.LaserTop, Command.CheckStatus, lbl__TopLaserFeedback, Command.CheckStatus_OK);
          //TaskLaserInput.GetLaserStatus();
    }

        private void btn_TopChangeModel_Click(object sender, EventArgs e)
        {
            //SendCommand_WfeedBack(TcpModuleID.LaserTop, Command.ChangeModel, lbl__TopLaserFeedback, Command.ChangeModel_OK);
            //TaskLaserInput.ChangeModel(TaskDeviceRecipe._sTopMarkFile);
            ChangeLaserModel("D:\\MarkingFile\\484859D-3", TcpModuleID.LaserTop, Command.ChangeModel, Command.ChangeModel_OK);
        }

        private void btn_TopChangeFile_Click(object sender, EventArgs e)
        {
            //SendCommand_WfeedBack(TcpModuleID.LaserTop, Command.ChangeFile_DDrive, lbl__TopLaserFeedback, Command.ChangeFile_OK);
            //TaskLaserInput.ChangeFile();
            ChangeLaserModel("E:\\MarkingFile\\484859D-3", TcpModuleID.LaserTop, Command.ChangeModel, Command.ChangeModel_OK);
        }

        private void btn_TopMark_Click(object sender, EventArgs e)
        {
            SendCommand_WfeedBack(TcpModuleID.LaserTop, Command.Mark, lbl__TopLaserFeedback, Command.Mark_OK);
            //TaskLaserInput.LaserMark();
        }

        private void btn_BottomLaserStatus_Click(object sender, EventArgs e)
        {
            SendCommand_WfeedBack(TcpModuleID.LaserBtm, Command.CheckStatus, lbl__BottomLaserFeedback, Command.CheckStatus_OK);
            //TaskLaserOutput.GetLaserStatus();
        }

        private void btn_BottomChangeModel_Click(object sender, EventArgs e)
        {
            SendCommand_WfeedBack(TcpModuleID.LaserBtm, Command.ChangeModel, lbl__BottomLaserFeedback, Command.ChangeModel_OK);
            //TaskLaserOutput.ChangeModel(TaskDeviceRecipe._sBottomMarkFile);
        }

        private void btn_BottomChangeFile_Click(object sender, EventArgs e)
        {
            SendCommand_WfeedBack(TcpModuleID.LaserBtm, Command.ChangeFile_DDrive, lbl__BottomLaserFeedback, Command.ChangeFile_OK);
            //TaskLaserOutput.ChangeFile();
        }

        private void btn_BottomMark_Click(object sender, EventArgs e)
        {
            SendCommand_WfeedBack(TcpModuleID.LaserBtm, Command.Mark, lbl__BottomLaserFeedback, Command.Mark_OK);
            //TaskLaserOutput.LaserMark();
        }

        private void btnOrientationOCR_Click(object sender, EventArgs e)
        {
            SendCommand_WOfeedback(TcpModuleID.CognexOCR, Command.TriggerAutomode, lbl_CognexMainFeedback);
        }

        private void btn_Top2DID_Click(object sender, EventArgs e)
        {
            SendCommand_WOfeedback(TcpModuleID.KeyenceTop, Command.TriggerSnap, lbl_Top2DIDFeedback);
        }

        private void btn_Rear2DID_Click(object sender, EventArgs e)
        {
            SendCommand_WOfeedback(TcpModuleID.KeyenceBtm, Command.TriggerSnap, lbl_Btm2DIDFeedback);
        }

        private void btn_CognexConnect_Click(object sender, EventArgs e)
        {
            if (txt_CognexMainIP.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Cognex Vision Main IPAddress.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (txt_CognexMainPort.Text == string.Empty)
            {
                uint MsgID = frmMain.frmMsg.ShowMsg("Please Insert Cognex Vision Main Port Number.",
                                               frmMessaging.TMsgBtn.smbOK);
                return;
            }

            if (isValidIP(txt_CognexMainIP.Text))
            {
                if (txt_CognexMainPort.Text.Length < 4) return;
                GDefine.CognexMainIP = txt_CognexMainIP.Text;
                GDefine.CognexMainPort = txt_CognexMainPort.Text;
                try
                {

                    frmMain.MainConnection.BuildSingleConnection(TcpModuleID.CognexServer, GDefine.CognexMainIP, GDefine.CognexMainPort);
                    if (frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer))
                    {
                        btn_CognexMainDisconnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? true : false;
                        btn_CognexMainConnect.Enabled = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? false : true;
                        lbl_CognexMainConnectStatus.Text = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? "Connected..." : "Disconnected";
                        lbl_CognexMainConnectStatus.BackColor = frmMain.MainConnection.ModuleIsConnect(TcpModuleID.CognexServer) ? Color.LimeGreen : Color.Red;

                        //btn_VisionOCRDisconnect.Enabled = GDefine.OCRConn.Connected;
                        //btn_VisionOCRConnect.Enabled = false;
                        //lbl_VisionOCRconnectStatus.Text = "Connected...";
                        //lbl_VisionOCRconnectStatus.BackColor = Color.LimeGreen;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_CognexServerDisconnect_Click(object sender, EventArgs e)
        {
            frmMain.MainConnection.DcSingleConnection(TcpModuleID.CognexServer);
            btn_CognexMainDisconnect.Enabled = true;
            btn_CognexMainConnect.Enabled = GDefine.OCRMainConn.Connected;
            lbl_CognexMainConnectStatus.Text = "Disconnected...";
            lbl_CognexMainConnectStatus.BackColor = Color.Red;            
        }

        private void btn_ChangeBigOCR_Click(object sender, EventArgs e)
        {
            Enabled = false;
            SetOCRRecipe_Wlabel(Command.LoadBigCR);
            Enabled = true;
        }

        private void btn_ChangeSmallOCR_Click(object sender, EventArgs e)
        {
            Enabled = false;
            SetOCRRecipe_Wlabel(Command.LoadSmallCR);
            Enabled = true;
        }

        private void lbl_EnableInputLaser_Click(object sender, EventArgs e)
        {
            GDefine._bEnabInLaser = !GDefine._bEnabInLaser;
        }

        private void lbl_EnableOutputLaser_Click(object sender, EventArgs e)
        {
            GDefine._bEnabOutLaser = !GDefine._bEnabOutLaser;
        }

        private void lbl_EnableCognexOCR_Click(object sender, EventArgs e)
        {
            GDefine._bEnabCognexOCR = !GDefine._bEnabCognexOCR;
        }

        private void lbl_EnableInputKeyence2DID_Click(object sender, EventArgs e)
        {
            GDefine._bEnabInKeyence = !GDefine._bEnabInKeyence;
        }

        private void lbl_EnableOutputKeyence2DID_Click(object sender, EventArgs e)
        {
            GDefine._bEnabOutKeyence = !GDefine._bEnabOutKeyence;
        }

        private void lbl_EnableDoorCheck_Click(object sender, EventArgs e)
        {
            GDefine._bEnabDoorCheck = !GDefine._bEnabDoorCheck;
        }

        private void lbl_StopWhenReadFailIn2DID_Click(object sender, EventArgs e)
        {
            GDefine._bEnabInKeyenceStopWhenReadFail = !GDefine._bEnabInKeyenceStopWhenReadFail;
        }

        private void lbl_StopWhenReadFailOut2DID_Click(object sender, EventArgs e)
        {
            GDefine._bEnabOutKeyenceStopWhenReadFail = !GDefine._bEnabOutKeyenceStopWhenReadFail;
        }

        private void lbl_EnableLaserMotor_Click(object sender, EventArgs e)
        {
            GDefine._bEnabLaserMotor = !GDefine._bEnabLaserMotor;
        }

        private void lbl_FailUnitUnloadToPass_Click(object sender, EventArgs e)
        {
            GDefine._bFailUnitUnloadToPass = !GDefine._bFailUnitUnloadToPass;
        }

        private void lbl_EnableRobot_Click(object sender, EventArgs e)
        {
            GDefine._bEnableUnloadRtr = !GDefine._bEnableUnloadRtr;
        }

        private void cmb_NestID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] _TMValue;
            var item = frmMain.SequenceRun.FixZoneData.FirstOrDefault(x => x.Nest_ID == cmb_NestID.SelectedIndex);
            numericUpDown_TH.Value = Convert.ToDecimal(item.Unit.NestHeightOffset[(int)TotalLaser.TopLaser]);
            _TMValue = item.Unit.MarkingOffset[(int)TotalLaser.TopLaser].Split(';');
            textBox_TM1.Text = _TMValue[0];
            textBox_TM2.Text = _TMValue[1];
            textBox_TM3.Text = _TMValue[2];
            textBox_TM4.Text = _TMValue[3];
            textBox_TM5.Text = _TMValue[4];
            textBox_TM6.Text = _TMValue[5];

            string[] _BMValue;
            numericUpDown_BH.Value = Convert.ToDecimal(item.Unit.NestHeightOffset[(int)TotalLaser.BtmLaser]);
            _BMValue = item.Unit.MarkingOffset[(int)TotalLaser.BtmLaser].Split(';');
            textBox_BM1.Text = _BMValue[0];
            textBox_BM2.Text = _BMValue[1];
            textBox_BM3.Text = _BMValue[2];
            textBox_BM4.Text = _BMValue[3];
            textBox_BM5.Text = _BMValue[4];
            textBox_BM6.Text = _BMValue[5];

            radioButton_Bypass.Checked = item.IsBypass;
        }

        private void UpdateCmbBox()
        {
            var fixzonedata = frmMain.SequenceRun.FixZoneData;

            for (int i = 0; i < fixzonedata.Count; i++)
            {
                cmb_NestID.Items.Add(fixzonedata[i].Nest_ID);
            }
        }

        private string ConcText(params string[] subtext)
        {
            string Text = string.Empty;
            for (int i = 0; i < subtext.Count(); i++)
            {
                Text = string.Join(";", subtext);
            }
            return Text;            
        }
        
        private bool TextVerify(params string[] text)
        {
            for (int i = 0; i < text.Count() ; i++)
            {
                string[] stringtext = text[i].Split(',');
                if (stringtext.Count() < 3)
                {
                    MessageBox.Show($"Exceed Param Max count at Column {i+1}");
                    return false;
                }

                for (int j = 0; j < stringtext.Count() ; j++)
                {
                    if (!double.TryParse(stringtext[j], out _))
                    {
                        MessageBox.Show($"Wrong Format at column {i+1}");
                        return false;
                    }
                }
                
            }
            
            return true;
        }
        
        private void radioButton_Bypass_OnClick(object sender, EventArgs args)
        {
            radioButton_Bypass.Checked = !radioButton_Bypass.Checked;
        }

        private void lbl_EnablePnpLeft_Click(object sender, EventArgs e)
        {
            GDefine._bEnableLeftPnp = !GDefine._bEnableLeftPnp;
        }

        private void lbl_EnablePnpRight_Click(object sender, EventArgs e)
        {
            GDefine._bEnableRightPnp = !GDefine._bEnableRightPnp;
        }
    }
}
