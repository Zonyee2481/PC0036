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
        public double _dDuration = 1;
        public string _sDuration_2 = "";
        public int _iCounter = 0;
        int time;
        int S, M, H;
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
            if (Convert.ToDouble(txt_Duration.Text) <= 0)
            {
                frmMsg = new frmMessaging2();
                frmMsg.ShowMsg("Time's Up Duration Not Allow To Less Than Or Equal 0!", frmMessaging2.TMsgBtn.smbOK);
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

                TaskDeviceRecipe.asDeviceID[Files.Count()] = txt_DeviceID.Text;
                TaskDeviceRecipe.aiAssignedNo[Files.Count()] = cmbAssignCode.SelectedIndex;
                TaskDeviceRecipe.aiCounter[Files.Count()] = Convert.ToInt32(txt_Counter.Text);
                TaskDeviceRecipe.adDuration[Files.Count()] = Convert.ToDouble(txt_Duration.Text);
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
                    TaskDeviceRecipe.aiCounter[_iIndex] = Convert.ToInt32(txt_Counter.Text);
                    TaskDeviceRecipe.adDuration[_iIndex] = Convert.ToDouble(txt_Duration.Text);
                    TaskDeviceRecipe.SaveDeviceRecipe(txt_DeviceID.Text, _iIndex);
                }
                else
                {
                    TaskDeviceRecipe.asDeviceID[_iIndex] = txt_DeviceID.Text;
                    TaskDeviceRecipe.aiAssignedNo[_iIndex] = cmbAssignCode.SelectedIndex;
                    TaskDeviceRecipe.aiCounter[_iIndex] = Convert.ToInt32(txt_Counter.Text);
                    TaskDeviceRecipe.adDuration[_iIndex] = Convert.ToDouble(txt_Duration.Text);
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
                txt_Counter.Text = _iCounter.ToString();
                txt_Duration.Text = _dDuration.ToString();
                time = Convert.ToInt32(_dDuration * (60 * 60));
                S = time % 60;
                M = (time / 60) % 60;
                H = (time / (3600)) % 24;
                _sDuration_2 = H + " H " + M + " M " + S + " S ";
                txtDuration_2.Text = _sDuration_2;
                btn_Reset.Enabled = true;
            }
            else
            {
                txt_DeviceID.Text = "";
                cmbAssignCode.SelectedIndex = 0;
                txt_Counter.Text = _iCounter.ToString();
                txt_Duration.Text = _dDuration.ToString();
                time = Convert.ToInt32(_dDuration * (60 * 60));
                S = time % 60;
                M = (time / 60) % 60;
                H = (time / (3600)) % 24;
                _sDuration_2 = H + " H " + M + " M " + S + " S ";
                txtDuration_2.Text = _sDuration_2;
                btn_Reset.Enabled = false;
            }
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

        private void txt_Duration_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref _dDuration, 0, 100);
            txt_Duration.Text = _dDuration.ToString("0.0000");
            time = Convert.ToInt32(_dDuration * (60 * 60));
            S = time % 60;
            M = (time / 60) % 60;
            H = (time / (3600)) % 24;
            _sDuration_2 = H + " H " + M + " M " + S + " S ";
            txtDuration_2.Text = _sDuration_2;
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            frmMsg = new frmMessaging2();
            frmMsg.ShowMsg("Are You Sure Want To Reset Counter To 0?", frmMessaging2.TMsgBtn.smbOK | frmMessaging2.TMsgBtn.smbCancel);
            DialogResult dialogResult = frmMsg.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                _iCounter = 0;
                TaskDeviceRecipe.aiCounter[_iIndex] = 0;
                TaskDeviceRecipe.ResetFileCounter(txt_DeviceID.Text, _iIndex);
                txt_Counter.Text = TaskDeviceRecipe.aiCounter[_iIndex].ToString();
            }
            else if (dialogResult == DialogResult.Cancel)
            {
                return;
            }
        }
    }
}
