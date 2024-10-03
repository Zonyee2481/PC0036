using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Management;
using System.Security.Cryptography;
using Microsoft.Win32;
using System.IO;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;
using System.Threading.Tasks;
using SeqServer;
using MotionIODevice;
using MotionIODevice.IO;
using Infrastructure;

namespace Machine
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            PW = pnl_Control.Width;
            this.Size = new Size(1920, 1040);
            this.Text = "TTOT UI " + Application.ProductVersion.ToString();
            PanelControl = pnl_Form;
            //GDefine.LoadComParam();
            IOModule = new IOMain();
            //AdvantechModule = new MotionMain_Advantech();
            //MoonStfModule = new MotionMain_MoonsSTF();
            //GalilModule = new MotionMain_Galil();

            SequenceRun = new MainSeq();
            MainEvent = new MainEvent();
            //MainConnection = new MainConnection();

            IOModule.InitIO();
            //AdvantechModule.InitMotion();
            //MoonStfModule.InitMotion();
            //GalilModule.InitMotion();

            List<IMotionControl> m_boardlist = new List<IMotionControl>();
            //m_boardlist.AddRange(AdvantechModule.GetBoardList().ToList());
            //m_boardlist.AddRange(MoonStfModule.GetBoardList().ToList());
            //m_boardlist.AddRange(GalilModule.GetBoardList().ToList());
            
            List<IMotionMain> m_motionmainlist = new List<IMotionMain>();
            //m_motionmainlist.Add(AdvantechModule);
            //m_motionmainlist.Add(MoonStfModule);
            //m_motionmainlist.Add(GalilModule);

            var m_ioboardlist = IOModule.GetBoardList();

            //Parse Motor//IO Data to Sequence
            SequenceRun.SetMotionBoardList = m_boardlist;
            SequenceRun.SetIOBoardInputList = m_ioboardlist;
            TaskIO.SetIOMain = m_ioboardlist;
            SequenceRun.BuildData();
            SequenceRun.BuildSequence();            
            //Parse sequence from MainSeq to MainEvent(UI, Sequence can trigger to each other now)
            MainEvent.SetBaseSequence = SequenceRun.GetSeqModule;
            MainEvent.EventPubAndSub_UI2Seq();
            SequenceRun.SetEventPool_UI2Seq = MainEvent.GetEventPool_UI2Seq;
            //MainEvent.GetEvenPoolSeq2UI(SequenceRun.ParseEventPoolSeq2UI());
            //MainEvent.EventPubAndSub_SeqtoSeq();
            //Parse Event to Seq (Init, Begin, Stop)
            //MainConnection.BuildAllConnectionAsync(GDefine._IpPort);
            //SequenceRun.SetMainConnection = MainConnection;
            GDefine.LoadDefaultFile();
            //MainEvent.UITriggerEvent(EV_TYPE.BeginSeq);
            //GDefine.ParseToSequence();
            //GDefine.ResetCounterFile();
        }
        public static frmLog frmLog = new frmLog();
        //public static frmMessaging frmMsg = new frmMessaging();
        public static tagSeqFlag mytagSeqFlag = new tagSeqFlag();
        //Panel move form Declaration wtih dll Import
        public static IIOMain IOModule = null;
        public static IMotionMain AdvantechModule = null;
        public static IMotionMain MoonStfModule = null;
        public static IMainSeq SequenceRun = null;
        public static IEvent MainEvent = null;
        public static IMainConnection MainConnection = null;
        public static IMotionMain GalilModule = null;
        public static Panel PanelControl = null;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void frmMain_Load(object sender, EventArgs e)
        {
            lbl_Version.Text = "Version: " + Application.ProductVersion.ToString();
            uctrlAuto.Page.AddToLog("Software Start up!");
            //TaskDeviceRecipe.ReadLaserCode();

            GDefine.LoadDefaultFile();


            //Parse data to UI 
            TaskIO.SetBitCodes((int)Output.BITCODE_1,
                (int)Output.BITCODE_2,
                (int)Output.BITCODE_4,
                (int)Output.BITCODE_8);

            TaskIO.SetTimesUp((int)Output.TIMESUP);

            TaskIO.SetStartControlRelay((int)Output.START_CONTROL_RELAY);

            TaskIO.SetStartButtonLED((int)Output.START_BUTTON_LED);

            AccessConfig.LoadAccessConfig();
            AccessConfig.LoadAccessPageConfig();
            uctrlLogin.Page.ShowPage(pnl_Form);
            TaskDeviceRecipe.LoadDeviceRecipe();
            TaskDeviceRecipe.LoadDeviceTimeLimit(GDefine.DevicePath, SM.RecipeName + GDefine.DeviceRecipeExt);
            TaskBitCode.InitBitCode();
            TaskBitCode.LoadBitCodeRecipe();
            TaskLotInfo.DeleteLotRecordData(); // Delete past 2 lot record data
            //LoadMachineRecipe();
        }

        private void LoadMachineRecipe()
        {
            //IniFile runtimeIni = new IniFile(FilePath.runtimeIni);
            //TaskLotInfo.InitLot();
            //lbl_Recipe.Text = "Recipe: " + SM.RecipeName;
        }


        /////////////////////////////////////////////////////////////////////

        //Slider function
        int PW;
        bool _bHide = false;

        private void tmr_slider_Tick(object sender, EventArgs e)
        {
            if (_bHide)
            {
                pnl_Control.Width = pnl_Control.Width + 20;
                if (pnl_Control.Width >= PW)
                {
                    tmr_slider.Stop();
                    _bHide = false;
                    this.Refresh();
                    btn_SlideIn.Visible = true;
                    btn_SlideOut.Visible = false;
                }
            }
            else
            {
                pnl_Control.Width = pnl_Control.Width - 20;
                if (pnl_Control.Width <= 70)
                {
                    tmr_slider.Stop();
                    _bHide = true;
                    btn_SlideIn.Visible = false;
                    btn_SlideOut.Visible = true;
                    this.Refresh();
                }
            }
        }

        private void btn_SlideIn_Click_1(object sender, EventArgs e)
        {
            tmr_slider.Start();
        }

        private void btn_SlideOut_Click(object sender, EventArgs e)
        {
            tmr_slider.Start();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btn_CloseAll_Click(object sender, EventArgs e)
        {
            string message = "Do you want to close application";
            string title = "Message Info";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);

            if (result == DialogResult.No)
            {
                return;
            }
            this.Close();
        }

        private void btn_frmIODiag_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            btn_frmIODiag.BackColor = Color.RoyalBlue;
            uctrlIODiag.Page.ShowPage(pnl_Form);
            uctrlAuto.Page.AddToLog("Access Diagnostic Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
            //GDefine.dbMain.LogAction(AccessConfig._sCurrentLoginUserName, btn_frmIODiag.Name, "Access Diagnostic Page! ");
        }

        public void UserControlClose()
        {
            uctrlAccessConfig.Page.HidePage();
            uctrlLogin.Page.HidePage();
            uctrlIODiag.Page.HidePage();
            uctrlDeviceRecipe.Page.HidePage();
            uctrlSetup.Page.HidePage();
            uctrlAuto.Page.HidePage();
        }

        public void TurnBtnColor()
        {
            btn_Main.BackColor = Color.Transparent;
            btn_Auto.BackColor = Color.Transparent;
            btn_Setup.BackColor = Color.Transparent;
            btn_DeviceRecipe.BackColor = Color.Transparent;
            btn_frmIODiag.BackColor = Color.Transparent;
            btn_AccessConfig.BackColor = Color.Transparent;
        }

        private void btn_Main_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            btn_Main.BackColor = Color.RoyalBlue;
            uctrlLogin.Page.ShowPage(pnl_Form);
            uctrlAuto.Page.AddToLog("Access Main Login Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
            //GDefine.dbMain.LogAction(AccessConfig._sCurrentLoginUserName, btn_Main.Name, "Access Main Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
        }

        private void btn_Auto_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            btn_Auto.BackColor = Color.RoyalBlue;
            uctrlAuto.Page.ShowPage(pnl_Form);
            uctrlAuto.Page.AddToLog("Access Auto Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
        }

        public static bool QuickBestGuessAboutAccessibilityOfNetworkPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            string pathRoot = Path.GetPathRoot(path);
            if (string.IsNullOrEmpty(pathRoot)) return false;
            ProcessStartInfo pinfo = new ProcessStartInfo("net", "use");
            pinfo.CreateNoWindow = true;
            pinfo.RedirectStandardOutput = true;
            pinfo.UseShellExecute = false;
            string output;
            using (Process p = Process.Start(pinfo))
            {
                output = p.StandardOutput.ReadToEnd();
            }
            foreach (string line in output.Split('\n'))
            {
                if (line.Contains(pathRoot) && line.Contains("OK"))
                {
                    return true; // shareIsProbablyConnected
                }
            }
            return false;
        }

        private void btn_Setup_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            btn_Setup.BackColor = Color.RoyalBlue;
            uctrlSetup.Page.ShowPage(pnl_Form);
            uctrlAuto.Page.AddToLog("Access Setup Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
        }

        private void btn_Recipe_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            //GDefine._bSaveRecipe = true;
            //uctrlSaveLoadRecipe.Page.ShowPage(pnl_Form);
        }

        private void btn_LoadRecipe_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            //GDefine._bSaveRecipe = false;
            //uctrlSaveLoadRecipe.Page.ShowPage(pnl_Form);

        }
        [DllImport("user32.dll")]
        public static extern Boolean GetLastInputInfo(ref tagLASTINPUTINFO plii);
        public struct tagLASTINPUTINFO
        {
            public uint cbSize;
            public Int32 dwTime;
        }
        private void tmr_Main_Tick(object sender, EventArgs e)
        {
            //if (!frmMain.frmMsg.Visible)
            //{
            //    return;
            //}
            lbl_Recipe.Text = "Recipe: " + SM.RecipeName;
            tmr_Main.Enabled = false;
            //pnl_Control.Enabled = GDefine._bPanelUnlock;

            lbl_Date.Text = "Date: " + DateTime.Now.ToString("dd-MM-yyyy");
            lbl_Time.Text = "Time: " + DateTime.Now.ToString("hh:mm:ss");
            //lbl_Recipe.Text = "Recipe: " + GDefine.Recipe.ToString();
            CheckAccessPage();

            //DateTime dt = DateTime.Now;
            //if (Convert.ToDateTime(GDefine._sCurrentDate) < dt.Date)
            //{
            //    GDefine._sCurrentDate = dt.ToString("dd-MM-yyyy");
            //    GDefine.SaveCurrentDate();
            //    //for (int i = 0; i < TaskDeviceRecipe.aiCounter.Count(); i++)
            //    //{
            //    //    TaskDeviceRecipe.aiCounter[i] = 0;
            //    //}
            //    TaskDeviceRecipe.SaveDeviceRecipe(); ;
            //}

            try
            {
                tagLASTINPUTINFO LastInput = new tagLASTINPUTINFO();
                Int32 IdleTime;
                LastInput.cbSize = (uint)Marshal.SizeOf(LastInput);
                LastInput.dwTime = 0;

                if (GetLastInputInfo(ref LastInput))
                {
                    IdleTime = System.Environment.TickCount - LastInput.dwTime;

                }

                //if (!TaskEMO.bgw_EMO.IsBusy) TaskEMO.bgw_EMO.RunWorkerAsync();
            }
            catch { }
            tmr_Main.Enabled = true;
        }

        private void btn_AccessConfig_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            btn_AccessConfig.BackColor = Color.RoyalBlue;
            uctrlAccessConfig.Page.ShowPage(pnl_Form);
            uctrlAuto.Page.AddToLog("Access Config Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
            //GDefine.dbMain.LogAction(AccessConfig._sCurrentLoginUserName, btn_AccessConfig.Name, "Access Access Config Page! ");
        }


        void CheckAccessPage()
        {
            //return;
            if (AccessConfig._sCurrentLoginLevel == "")
            {
                //btn_Auto.Visible = false;
                btn_Setup.Enabled = false;
                btn_frmIODiag.Enabled = false;
                btn_AccessConfig.Enabled = false;
                btn_DeviceRecipe.Enabled = false;
                return;
            }
            if ((int)AccessConfig.CurrentAccessLvl == 0)
            {
                btn_Setup.Enabled = AccessConfig._OperatorAccess[0];
                btn_DeviceRecipe.Enabled = AccessConfig._OperatorAccess[1];
                btn_frmIODiag.Enabled = AccessConfig._OperatorAccess[2];
                btn_AccessConfig.Enabled = AccessConfig._OperatorAccess[3];
            }
            if ((int)AccessConfig.CurrentAccessLvl == 1)
            {
                btn_Setup.Enabled = AccessConfig._OperatorAccess[0];
                btn_DeviceRecipe.Enabled = AccessConfig._OperatorAccess[1];
                btn_frmIODiag.Enabled = AccessConfig._OperatorAccess[2];
                btn_AccessConfig.Enabled = AccessConfig._OperatorAccess[3];
            }
            if ((int)AccessConfig.CurrentAccessLvl == 2)
            {
                btn_Setup.Enabled = AccessConfig._OperatorAccess[0];
                btn_DeviceRecipe.Enabled = AccessConfig._OperatorAccess[1];
                btn_frmIODiag.Enabled = AccessConfig._OperatorAccess[2];
                btn_AccessConfig.Enabled = AccessConfig._OperatorAccess[3];
            }
            if ((int)AccessConfig.CurrentAccessLvl == 3)
            {
                btn_Setup.Enabled = true;
                btn_frmIODiag.Enabled = true;
                btn_AccessConfig.Enabled = true;
                btn_DeviceRecipe.Enabled = true;
            }
        }

        private void btn_History_Click(object sender, EventArgs e)
        {
            try
            {
                //frmMain.frmLog.Visible = !frmMain.frmLog.Visible;
            }
            catch
            {
                //frmLog frm = new frmLog();
                //frm.Visible = true;
            }
            //frmMain.frmLog.Height = 145;
            //frmMain.frmLog.Width = 1050;
            //frmMain.frmLog.Left = 0;
            //frmMain.frmLog.Top = 1080 - frmMain.frmLog.Height;
            //frmMain.frmLog.Left = 190;
            //frmMain.frmLog.TopMost = true;
            //UserControlClose();
            TurnBtnColor();
            //btn_History.BackColor = Color.RoyalBlue;
            //uctrlHistory.Page.ShowPage(pnl_Form);
            //uctrlAuto.Page.AddToLog("Access History Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
        }

        //void HardwareConnection()
        //{
        //    try
        //    {

        //        TaskLaserInput.ConnectToHost(ref GDefine.LaserInIP, ref GDefine.LaserInPort);
        //    }
        //    catch { }
        //    try
        //    {
        //        TaskLaserOutput.ConnectToHost(ref GDefine.LaserOutIP, ref GDefine.LaserOutPort);
        //    }
        //    catch { }
        //    try
        //    {
        //        TaskCognex.ConnectToHost(ref GDefine.OCRIP, ref GDefine.OCRPort);
        //    }
        //    catch { }
        //    try
        //    {
        //        TaskKeyenceInput.ConnectToHost(ref GDefine.KeyenceInIP, ref GDefine.KeyenceInPort);
        //    }
        //    catch { }
        //    try
        //    {
        //        TaskKeyenceOutput.ConnectToHost(ref GDefine.KeyenceOutIP, ref GDefine.KeyenceOutPort);
        //    }
        //    catch { }
        //}

        //void HardwareConnection()
        //{
        //    StringBuilder TaskLaserConnection = new StringBuilder("Start");
        //    StringBuilder TaskCognexConnection = new StringBuilder("Start");
        //    StringBuilder TaskKeyenceConnection = new StringBuilder("Start");

        //    Station TotalStation = new Station { stations = new string[] { "Laser", "Cognex", "Keyence" } };

        //    TaskLaserInput Laser = new TaskLaserInput();
        //    TaskLaserInput Cognex = new TaskLaserInput();
        //    TaskLaserInput Keyence = new TaskLaserInput();

        //    frmDisplay ConnectionDisplay = new frmDisplay(TotalStation, TaskLaserConnection, TaskCognexConnection, TaskKeyenceConnection);

        //    ConnectionDisplay.Show();
        //    Task WaitConnectionDone = Task.Run(() => ConnectionDisplay.WaitDone(TaskLaserConnection, TaskCognexConnection, TaskKeyenceConnection));


        //    Task a = Task.Run(()=> Laser.ConnectToHost(GDefine.LaserInIP, GDefine.LaserInPort, TaskLaserConnection));
        //    Task b = Task.Run(() => Cognex.ConnectToHost(GDefine.OCRIP, GDefine.OCRPort, TaskCognexConnection));
        //    Task c = Task.Run(() => Keyence.ConnectToHost(GDefine.KeyenceInIP, GDefine.KeyenceInPort, TaskKeyenceConnection));

        //    Task.WaitAll(a, b, c, WaitConnectionDone);

        //}

        void HardwareDisconnect()
        {
            //TaskLaserZ.DisconnectToHost();
            //TaskVisionZ.DisconnectToHost();
            //TaskTCPClient.DisconnectToHost();
            //sqlConnection.CloseSqlCon();
        }

        private void btn_ManualMode_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            //btn_ManualMode.BackColor = Color.RoyalBlue;
            //uctrlManual.Page.ShowPage(pnl_Form);
            //uctrlAuto.Page.AddToLog("Access Manual Mode Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
        }
        private void btn_DeviceRecipe_Click(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            btn_DeviceRecipe.BackColor = Color.RoyalBlue;
            uctrlDeviceRecipe.Page.ShowPage(pnl_Form);
            uctrlAuto.Page.AddToLog("Access Device Recipe Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
            //GDefine.dbMain.LogAction(AccessConfig._sCurrentLoginUserName, btn_SysLog.Name, "Access Device Recipe Page! ");
        }

        private void btn_ManualMode_Click_1(object sender, EventArgs e)
        {
            UserControlClose();
            TurnBtnColor();
            //btn_ManualMode.BackColor = Color.RoyalBlue;
            //uctrlManualMark.Page.ShowPage(pnl_Form);
            //uctrlAuto.Page.AddToLog("Access Manual Mode Page! " + "[" + AccessConfig._sCurrentLoginUserName + "]");
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            IOModule.CloseBoard();
        }
    }
}
