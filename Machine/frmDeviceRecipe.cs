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
            this.cmbAssignCode.SelectedIndexChanged += cmbAssignCode_SelectedIndexChanged;
            InitGUI();
        }
        public bool _bEdit = false;
        public bool _bNew = false;
        public int _iIndex = 0;
        public string _sDeviceID = "";
        public int _iAssignedNo = 0;
        public int _iDuration = 1;
        public string _sDuration_2 = "";
        public int _iCounter = 0;
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
            if (cmbAssignCode.SelectedIndex < 0)
            {
                frmMsg = new frmMessaging2();
                frmMsg.ShowMsg("Assigned Code Not Allow To Empty!", frmMessaging2.TMsgBtn.smbOK);
                DialogResult dialogResult = frmMsg.ShowDialog();
                if (dialogResult == DialogResult.OK)
                {
                    return false;
                }
            }
            //if (Convert.ToDouble(txt_Duration.Text) <= 0)
            //{
            //    frmMsg = new frmMessaging2();
            //    frmMsg.ShowMsg("Time's Up Duration Not Allow To Less Than Or Equal 0!", frmMessaging2.TMsgBtn.smbOK);
            //    DialogResult dialogResult = frmMsg.ShowDialog();
            //    if (dialogResult == DialogResult.OK)
            //    {
            //        return false;
            //    }
            //}    
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

                TaskDeviceRecipe.asDeviceID[Files.Count()] = txt_DeviceID.Text;
                TaskDeviceRecipe.aiAssignedNo[Files.Count()] = cmbAssignCode.SelectedIndex;
                TimeSpan timeSpan = new TimeSpan(TaskDeviceRecipe.iHour, TaskDeviceRecipe.iMin, TaskDeviceRecipe.iSec);
                TaskDeviceRecipe.adDuration[Files.Count()] = timeSpan.TotalMilliseconds;
                TaskDeviceRecipe.SaveDeviceRecipe(txt_DeviceID.Text, Files.Count());
            }
            else if (_bEdit)
            {
                if (TaskDeviceRecipe.asDeviceID[_iIndex] != txt_DeviceID.Text)
                {
                    string oldFileName = TaskDeviceRecipe.asDeviceID[_iIndex];
                    TaskDeviceRecipe.RenameDeviceRecipe(oldFileName, txt_DeviceID.Text);
                    TaskDeviceRecipe.asDeviceID[_iIndex] = txt_DeviceID.Text;
                    TaskDeviceRecipe.aiAssignedNo[_iIndex] = cmbAssignCode.SelectedIndex;
                    TimeSpan timeSpan = new TimeSpan(TaskDeviceRecipe.iHour, TaskDeviceRecipe.iMin, TaskDeviceRecipe.iSec);
                    TaskDeviceRecipe.adDuration[_iIndex] = timeSpan.TotalMilliseconds;
                    TaskDeviceRecipe.SaveDeviceRecipe(txt_DeviceID.Text, _iIndex);
                }
                else
                {
                    TaskDeviceRecipe.asDeviceID[_iIndex] = txt_DeviceID.Text;
                    TaskDeviceRecipe.aiAssignedNo[_iIndex] = cmbAssignCode.SelectedIndex;
                    TimeSpan timeSpan = new TimeSpan(TaskDeviceRecipe.iHour, TaskDeviceRecipe.iMin, TaskDeviceRecipe.iSec);
                    TaskDeviceRecipe.adDuration[_iIndex] = timeSpan.TotalMilliseconds;
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
                cmbAssignCode.SelectedIndex = _iAssignedNo;
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(_iDuration);
                txtDurationS.Text = timeSpan.Seconds.ToString("00");
                txtDurationM.Text = timeSpan.Minutes.ToString("00");
                txtDurationH.Text = timeSpan.Hours.ToString("00");
            }
            else
            {
                txt_DeviceID.Text = "";
                cmbAssignCode.SelectedIndex = 0;
                txtDurationS.Text = "00";
                txtDurationM.Text = "00";
                txtDurationH.Text = "00";
            }

            //txt_Duration.Text = _dDuration.ToString();
            //time = Convert.ToInt32(_dDuration * (60 * 60));
            //S = time % 60;
            //M = (time / 60) % 60;
            //H = (time / (3600)) % 24;
            //_sDuration_2 = H + " H " + M + " M " + S + " S ";
            //txtDuration_2.Text = _sDuration_2;
        }                

        private void tmr_UpdateDisplay_Tick(object sender, EventArgs e)
        {

        }

        private void UpdateCmbBox()
        {
            for (int i = 0; i < TaskBitCode.lBitCodes.Count; i++)
            {
                cmbAssignCode.Items.Add(TaskBitCode.lBitCodes[i].Index);
            }

            //Enum_OCRRecipe[] ocrArray = (Enum_OCRRecipe[])Enum.GetValues(typeof(Enum_OCRRecipe));
            //for (int i = 0; i < ocrArray.Length; i++)
            //{
            //    Cmb_OCRRec.Items.Add(ocrArray[i]);
            //}
            //Cmb_OCRRec.SelectedItem = Cmb_OCRRec.Items[0];
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

        private void cmbAssignCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            _iAssignedNo = cmbAssignCode.SelectedIndex;
        }

        private void txtDurationS_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iSec, 0, 59);
            txtDurationS.Text = TaskDeviceRecipe.iSec.ToString("00");
        }

        private void txtDurationM_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iMin, 0, 59);
            txtDurationM.Text = TaskDeviceRecipe.iMin.ToString("00");
        }

        private void txtDurationH_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref TaskDeviceRecipe.iHour, 0, 23);
            txtDurationH.Text = TaskDeviceRecipe.iHour.ToString("00");
        }
    }
}
