using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class frmLog : Form
    {
        public frmLog()
        {
            InitializeComponent();
        }

        private void frmLog_Load(object sender, EventArgs e)
        {
            this.Text = "Log";
        }

        object m_log = new object();
        public void AddToLog(string S)
        {
            lock (m_log)
            {
                if (lbox_Log.Items.Count >= 100)
                {
                    try
                    {
                        if (lbox_Log.InvokeRequired)
                        {
                            lbox_Log.Invoke(new Action(() => lbox_Log.Items.RemoveAt(0)));
                        }
                        else
                        {
                            lbox_Log.Items.RemoveAt(0);
                        }
                    }
                    catch { }
                }

                string Date = DateTime.Now.Date.ToString("yyyyMMdd");
                string Time = DateTime.Now.ToString("HH:mm:ss");
                string MM = DateTime.Now.Month.ToString();
                if (MM.Length == 1) { MM = "0" + MM; }
                string YYYY = DateTime.Now.Year.ToString();
                string DD = DateTime.Now.Day.ToString();
                if (DD.Length == 1) { DD = "0" + DD; }

                //S = S;// Time + (char)9 + S;
                try
                {
                    if (lbox_Log.InvokeRequired)
                    {
                        // If we are not in the UI's thread we use BeginInvoke to run this method in the UI's thred.

                        lbox_Log.Invoke(new Action(() => lbox_Log.Items.Add(S)));
                        //lbox_Log.Invoke(new Action(() => lbox_Log.TopIndex = lbox_Log.Items.IndexOf(S)));
                        lbox_Log.Invoke(new Action(() => lbox_Log.TopIndex = lbox_Log.Items.Count - 1));
                    }
                    else
                    {
                        lbox_Log.Items.Add(S);
                        //lbox_Log.SetSelected(lbox_Log.Items.Count - 1, true);
                        lbox_Log.TopIndex = lbox_Log.Items.Count - 1;
                        //lbox_Log.Update();
                        //lbox_Log.Refresh(); 
                    }
                }
                catch
                { }
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            lbox_Log.Items.Clear();
        }

        private void btn_Hide_Click(object sender, EventArgs e)
        {
            Visible = false;
        }
    }

}
