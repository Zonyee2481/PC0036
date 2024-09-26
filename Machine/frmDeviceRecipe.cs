using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.IO;
using Infrastructure;

namespace Machine
{
    public partial class frmDeviceRecipe : Form
    {
        public frmDeviceRecipe()
        {
            InitializeComponent();
            UpdateCmbBox();            
            this.cmbRunHz_1st.SelectedIndexChanged += cmbRunHz_1st_SelectedIndexChanged;
            InitGUI();
        }
        public bool _bEdit = false;
        public bool _bNew = false;
        public int _iIndex = 0;
        public string _sDeviceID = "";
        public int _iRunHz_1st = 0;
        public int _iRunHz_2nd = 0;
        public int _iTimeLimit_1st = 1;
        public int _iTimeLimit_2nd = 1;
        public bool _bMasterProduct = false;
        private frmMessaging2 frmMsg;
        private void btn_Close_Click(object sender, EventArgs e)
        {
            Close();
        }
        bool CheckTextBox()
        {
            if (txt_DeviceID.Text == "" || txt_DeviceID.Text == string.Empty)
            {
                frmMsg = new frmMessaging2();
                frmMsg.ShowMsg("Device ID Not Allow To Empty!", frmMessaging2.TMsgBtn.smbOK);
                DialogResult dialogResult = frmMsg.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    return false;
                }
            }
            if (cmbRunHz_1st.SelectedIndex < 0)
            {
                frmMsg = new frmMessaging2();
                frmMsg.ShowMsg("1st Running Hz Not Allow To Empty!", frmMessaging2.TMsgBtn.smbOK);
                DialogResult dialogResult = frmMsg.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    return false;
                }
            }
            if (cmbRunHz_2nd.SelectedIndex < 0)
            {
                frmMsg = new frmMessaging2();
                frmMsg.ShowMsg("2nd Running Hz Not Allow To Empty!", frmMessaging2.TMsgBtn.smbOK);
                DialogResult dialogResult = frmMsg.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    return false;
                }
            }
            return true;
        }

        private void btn_SaveDeviceRecipe_Click(object sender, EventArgs e)
        {
            if (!CheckTextBox()) return;
            if (_bNew)
            {               
                DirectoryInfo d = new DirectoryInfo(GDefine.DevicePath);

                FileInfo[] Files = d.GetFiles("*.ini");

                for (int i = 0; i < Files.Count(); i++)
                {
                    if (Files[i].Name == txt_DeviceID.Text + ".ini")
                    {
                        frmMsg = new frmMessaging2();
                        frmMsg.ShowMsg("Duplicated Device ID! " + txt_DeviceID.Text + " Already Exist In the System.", frmMessaging2.TMsgBtn.smbOK);
                        DialogResult dialogResult = frmMsg.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            return;
                        }
                    }
                }

                TimeSpan timeSpan = new TimeSpan();
                TaskDeviceRecipe.asDeviceID[Files.Count()] = txt_DeviceID.Text;

                TaskDeviceRecipe.aiRunHz_1st[Files.Count()] = cmbRunHz_1st.SelectedIndex;
                timeSpan = new TimeSpan(TaskDeviceRecipe.iHour_1st, TaskDeviceRecipe.iMin_1st, TaskDeviceRecipe.iSec_1st);
                TaskDeviceRecipe.adTimeLimit_1st[Files.Count()] = timeSpan.TotalMilliseconds;

                TaskDeviceRecipe.aiRunHz_2nd[Files.Count()] = cmbRunHz_2nd.SelectedIndex;
                timeSpan = new TimeSpan(TaskDeviceRecipe.iHour_2nd, TaskDeviceRecipe.iMin_2nd, TaskDeviceRecipe.iSec_2nd);
                TaskDeviceRecipe.adTimeLimit_2nd[Files.Count()] = timeSpan.TotalMilliseconds;

                TaskDeviceRecipe.abMasterProduct[Files.Count()] = cbMasterRecipe.Checked;

                TaskDeviceRecipe.SaveDeviceRecipe(txt_DeviceID.Text, Files.Count());
            }
            else if (_bEdit)
            {
                if (TaskDeviceRecipe.asDeviceID[_iIndex] != txt_DeviceID.Text)
                {
                    TimeSpan timeSpan = new TimeSpan();
                    string oldFileName = TaskDeviceRecipe.asDeviceID[_iIndex];
                    TaskDeviceRecipe.RenameDeviceRecipe(oldFileName, txt_DeviceID.Text);
                    TaskDeviceRecipe.asDeviceID[_iIndex] = txt_DeviceID.Text;

                    TaskDeviceRecipe.aiRunHz_1st[_iIndex] = cmbRunHz_1st.SelectedIndex;
                    timeSpan = new TimeSpan(TaskDeviceRecipe.iHour_1st, TaskDeviceRecipe.iMin_1st, TaskDeviceRecipe.iSec_1st);
                    TaskDeviceRecipe.adTimeLimit_1st[_iIndex] = timeSpan.TotalMilliseconds;

                    TaskDeviceRecipe.aiRunHz_2nd[_iIndex] = cmbRunHz_2nd.SelectedIndex;
                    timeSpan = new TimeSpan(TaskDeviceRecipe.iHour_2nd, TaskDeviceRecipe.iMin_2nd, TaskDeviceRecipe.iSec_2nd);
                    TaskDeviceRecipe.adTimeLimit_2nd[_iIndex] = timeSpan.TotalMilliseconds;
                    
                    TaskDeviceRecipe.abMasterProduct[_iIndex] = cbMasterRecipe.Checked;

                    TaskDeviceRecipe.SaveDeviceRecipe(txt_DeviceID.Text, _iIndex);
                }
                else
                {
                    TimeSpan timeSpan = new TimeSpan();
                    TaskDeviceRecipe.asDeviceID[_iIndex] = txt_DeviceID.Text;

                    TaskDeviceRecipe.aiRunHz_1st[_iIndex] = cmbRunHz_1st.SelectedIndex;
                    timeSpan = new TimeSpan(TaskDeviceRecipe.iHour_1st, TaskDeviceRecipe.iMin_1st, TaskDeviceRecipe.iSec_1st);
                    TaskDeviceRecipe.adTimeLimit_1st[_iIndex] = timeSpan.TotalMilliseconds;
                    TaskDeviceRecipe.aiRunHz_1st[_iIndex] = cmbRunHz_1st.SelectedIndex;

                    TaskDeviceRecipe.aiRunHz_2nd[_iIndex] = cmbRunHz_2nd.SelectedIndex;
                    timeSpan = new TimeSpan(TaskDeviceRecipe.iHour_2nd, TaskDeviceRecipe.iMin_2nd, TaskDeviceRecipe.iSec_2nd);
                    TaskDeviceRecipe.adTimeLimit_2nd[_iIndex] = timeSpan.TotalMilliseconds;
                    TaskDeviceRecipe.aiRunHz_2nd[_iIndex] = cmbRunHz_2nd.SelectedIndex;

                    TaskDeviceRecipe.abMasterProduct[_iIndex] = cbMasterRecipe.Checked;

                    TaskDeviceRecipe.SaveDeviceRecipe(txt_DeviceID.Text, _iIndex);
                }
            }
            Close();
        }

        private void frmDeviceRecipe_Load(object sender, EventArgs e)
        {
            if (_bEdit)
            {
                txt_DeviceID.Text = _sDeviceID;
                cmbRunHz_1st.SelectedIndex = _iRunHz_1st;
                cmbRunHz_2nd.SelectedIndex = _iRunHz_2nd;
                TimeSpan timeSpan = new TimeSpan();
                timeSpan = TimeSpan.FromMilliseconds(_iTimeLimit_1st);
                txtDurationS_1st.Text = timeSpan.Seconds.ToString("00");
                txtDurationM_1st.Text = timeSpan.Minutes.ToString("00");
                txtDurationH_1st.Text = timeSpan.Hours.ToString("00");
                timeSpan = TimeSpan.FromMilliseconds(_iTimeLimit_2nd);
                txtDurationS_2nd.Text = timeSpan.Seconds.ToString("00");
                txtDurationM_2nd.Text = timeSpan.Minutes.ToString("00");
                txtDurationH_2nd.Text = timeSpan.Hours.ToString("00");
                cbMasterRecipe.Checked = _bMasterProduct;
            }
            else
            {
                txt_DeviceID.Text = "";
                cmbRunHz_1st.SelectedIndex = 0;
                txtDurationS_1st.Text = "00";
                txtDurationM_1st.Text = "00";
                txtDurationH_1st.Text = "00";
                cmbRunHz_2nd.SelectedIndex = 0;
                txtDurationS_2nd.Text = "00";
                txtDurationM_2nd.Text = "00";
                txtDurationH_2nd.Text = "00";
            }
        }                

        private void tmr_UpdateDisplay_Tick(object sender, EventArgs e)
        {

        }

        private void UpdateCmbBox()
        {
            for (int i = 0; i < TaskBitCode.lBitCodes.Count; i++)
            {
                cmbRunHz_1st.Items.Add(TaskBitCode.lBitCodes[i].Index);
                cmbRunHz_2nd.Items.Add(TaskBitCode.lBitCodes[i].Index);
            }
        }

        private void InitGUI()
        {
            InitBitCodeGUI();
        }

        private void InitBitCodeGUI()
        {
            pnlBitCode.Controls.Clear();
            for (int i = 0; i < TaskBitCode.lBitCodes.Count; i++)
            {
                DrawIndicator(pnlBitCode, i);
            }
        }

        public void DrawIndicator(Panel pnl, int idx)
        {
            BitCodePoint bc = new BitCodePoint();
            bc.Index = idx;
            //bc.Label = String.IsNullOrEmpty(TaskBitCode.lBitCodes[idx].RecipeName) ? "" : TaskBitCode.lBitCodes[idx].RecipeName;
            bc.IndexText = TaskBitCode.lBitCodes[idx].Index.ToString() + ".";
            bc.DescriptionText = TaskBitCode.lBitCodes[idx].Description.ToString();
            bc.CheckedBitCode_1 = TaskBitCode.lBitCodes[idx].BitCode_1;
            bc.CheckedBitCode_2 = TaskBitCode.lBitCodes[idx].BitCode_2;
            bc.CheckedBitCode_4 = TaskBitCode.lBitCodes[idx].BitCode_4;
            bc.CheckedBitCode_8 = TaskBitCode.lBitCodes[idx].BitCode_8;
            bc.Location = new Point(2, 3 + (bc.Height * idx));
            pnl.Controls.Add(bc);
        }

        private void cmbRunHz_1st_SelectedIndexChanged(object sender, EventArgs e)
        {
            _iRunHz_1st = cmbRunHz_1st.SelectedIndex;
        }

        private void txtDurationH_1st_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iHour_1st, 0, 23);
            txtDurationH_1st.Text = TaskDeviceRecipe.iHour_1st.ToString("00");
        }

        private void txtDurationM_1st_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iMin_1st, 0, 59);
            txtDurationM_1st.Text = TaskDeviceRecipe.iMin_1st.ToString("00");
        }

        private void txtDurationS_1st_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iSec_1st, 0, 59);
            txtDurationS_1st.Text = TaskDeviceRecipe.iSec_1st.ToString("00");
        }

        private void txtDurationH_2nd_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iHour_2nd, 0, 23);
            txtDurationH_2nd.Text = TaskDeviceRecipe.iHour_2nd.ToString("00");
        }

        private void txtDurationM_2nd_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iMin_2nd, 0, 59);
            txtDurationM_2nd.Text = TaskDeviceRecipe.iMin_2nd.ToString("00");
        }

        private void txtDurationS_2nd_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iSec_2nd, 0, 59);
            txtDurationS_2nd.Text = TaskDeviceRecipe.iSec_2nd.ToString("00");
        }

        private void cmbRunHz_2nd_SelectedIndexChanged(object sender, EventArgs e)
        {
            _iRunHz_2nd = cmbRunHz_2nd.SelectedIndex;
        }
    }
}
