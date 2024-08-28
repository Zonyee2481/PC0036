using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SeqServer;
//using Core.Database;

namespace Machine
{
    public partial class frmMessaging : Form
    {

        public EventHandler AlarmClearEvt;
        public Thread thread;
        public MessageEventArg m_strmsg = new MessageEventArg();
        public frmMessaging(MessageEventArg strmsg = null)
        {
            InitializeComponent(); 
            if (strmsg == null)
            {
                strmsg = new MessageEventArg();
            }
            m_strmsg.StationName = strmsg.StationName;
            m_strmsg.Message = strmsg.Message;

             thread = new Thread(() =>
            {
                Application.Run(this);
            });
            thread.IsBackground = true; // Set the thread as a background thread
            thread.Start();
            
        }
        public static bool MsgShowing = false;
        public static uint CurrentMsgID = 0;
        private uint LastMsgInQueID = 0;
        const uint MaxActiveMsg = 255;
        public enum TMsgBtn { smbNone = 0, smbAlmClr = 1, smbOK = 2, smbRetry = 4, smbStop = 8, smbCancel = 16 }
        public enum TMsgRes { smrNone = 0, smrAlmClr = 1, smrOK = 2, smrRetry = 4, smrStop = 8, smrCancel = 16 }
        public string[] MsgStr = new string[MaxActiveMsg];
        public TMsgBtn[] MsgBtn = new TMsgBtn[MaxActiveMsg];
        public TMsgRes[] MsgRes = new TMsgRes[MaxActiveMsg];

        public uint ShowMsg(string Msg, TMsgBtn Btn)
        {
            uint LastMsgInQueID = 0;
            StartUp();

            btn_AlmClr.Enabled = false;
            btn_OK.Enabled = false;
            btn_Stop.Enabled = false;
            btn_Retry.Enabled = false;
            btn_Cancel.Enabled = false;

            if ((Btn & TMsgBtn.smbAlmClr) == TMsgBtn.smbAlmClr) btn_AlmClr.Enabled = true;
            if ((Btn & TMsgBtn.smbOK) == TMsgBtn.smbOK) btn_OK.Enabled = true;
            if ((Btn & TMsgBtn.smbStop) == TMsgBtn.smbStop) btn_Stop.Enabled = true;
            if ((Btn & TMsgBtn.smbRetry) == TMsgBtn.smbRetry) btn_Retry.Enabled = true;
            if ((Btn & TMsgBtn.smbCancel) == TMsgBtn.smbCancel) btn_Cancel.Enabled = true;

            lbl_Msg.Text = Msg;

            return LastMsgInQueID;
        }

        public bool ShowMsgClear(uint ID)
        {

            int t = Environment.TickCount;
            return false;
        }
        private void StartUp()
        {
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;
            //AutoSizeMode = AutoSizeMode.GrowAndShrink;

            TopMost = true;
            //AutoSize = true;
        }

        public TMsgRes GetMsgRes(uint ID)
        {
            return MsgRes[ID % MaxActiveMsg];
        }

        private void btn_AlmClr_Click(object sender, EventArgs e)
        {
            if (AlarmClearEvt != null)
            {
                AlarmClearEvt(null, null);
            }
        }

        private void btn_Retry_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Retry;
            m_strmsg.dialogResult = DialogResult.Retry;
            frmMain.MainEvent.UITriggerEvent(EV_TYPE.RetryReq, m_strmsg);
            //thread.Abort();
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            m_strmsg.dialogResult = DialogResult.OK;
            frmMain.MainEvent.UITriggerEvent(EV_TYPE.RetryReq, m_strmsg);
            //thread.Abort();
            this.Close();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Abort;
            m_strmsg.dialogResult = DialogResult.Abort;
            frmMain.MainEvent.UITriggerEvent(EV_TYPE.RetryReq, m_strmsg);
            //thread.Abort();
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            m_strmsg.dialogResult = DialogResult.Cancel;
            frmMain.MainEvent.UITriggerEvent(EV_TYPE.RetryReq, m_strmsg);
            //thread.Abort();
            this.Close();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            try
            {
                base.OnFormClosing(e);
                
            }
            catch { }
        }
    }
}
