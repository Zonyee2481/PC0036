using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Machine
{
    public partial class uctrlAccessConfig : UserControl
    {
        public uctrlAccessConfig()
        {
            InitializeComponent();
        }
        public static uctrlAccessConfig Page = new uctrlAccessConfig();
        private frmMessaging2 frmMsg;
        public void ShowPage(Control parent)
        {
            Page.Parent = parent;
            Page.Dock = DockStyle.Fill;
            //Page.UpdateDisplay();
            Page.Show();
            ClearListView();
            UpdateListView();
            if ((int)AccessConfig.CurrentAccessLvl > 2)
            {
                grb_AccessPage.Enabled = true;
                cmb_LevelAccessPage.SelectedIndex = 0;
            }
            else
            {
                grb_AccessPage.Enabled = false;
            }
            UpdateCheckBoxTick();
            lbl_IdleTimeAutoLogout.Text = AccessConfig._iIdleTimeLogOut.ToString();
        }
        public void HidePage()
        {
            Visible = false;
        }
        private void ClearListView()
        {

            foreach (ListViewItem item in lv_UserData.Items)
            {
                item.Remove();
            }
        }
        private void UpdateListView()
        {
            for (int i = 0; i < AccessConfig._slBadgeNumber.Count; i++)
            {
                string[] arr = new string[3];
                ListViewItem itm;

                arr[0] = AccessConfig._slBadgeNumber[i];
                arr[1] = AccessConfig._slUserName[i].ToString();
                arr[2] = AccessConfig._aAccessLevel[AccessConfig._slLevel[i]].ToString();

                itm = new ListViewItem(arr);
                lv_UserData.Items.Add(itm);
            }
        }
        private void UpdateCheckBoxTick()
        {
            if (cmb_LevelAccessPage.SelectedIndex == 0)
            {
                ckb_EnabSetupPage.Checked = AccessConfig._OperatorAccess[0];
                ckb_EnabDeviceRecipe.Checked = AccessConfig._OperatorAccess[1];
                ckb_EnabDiagnostic.Checked = AccessConfig._OperatorAccess[2];
                ckb_EnabAccessConfig.Checked = AccessConfig._OperatorAccess[3];
            }
            else if (cmb_LevelAccessPage.SelectedIndex == 1)
            {
                ckb_EnabSetupPage.Checked = AccessConfig._TechnicianAccess[0];
                ckb_EnabDeviceRecipe.Checked = AccessConfig._TechnicianAccess[1];
                ckb_EnabDiagnostic.Checked = AccessConfig._TechnicianAccess[2];
                ckb_EnabAccessConfig.Checked = AccessConfig._TechnicianAccess[3];
            }
            else if (cmb_LevelAccessPage.SelectedIndex == 2)
            {
                ckb_EnabSetupPage.Checked = AccessConfig._EngineerAccess[0];
                ckb_EnabDeviceRecipe.Checked = AccessConfig._EngineerAccess[1];
                ckb_EnabDiagnostic.Checked = AccessConfig._EngineerAccess[2];
                ckb_EnabAccessConfig.Checked = AccessConfig._EngineerAccess[3];
            }
            else if (cmb_LevelAccessPage.SelectedIndex == 3)
            {
                ckb_EnabSetupPage.Checked = AccessConfig._AdministratorAccess[0];
                ckb_EnabDeviceRecipe.Checked = AccessConfig._AdministratorAccess[1];
                ckb_EnabDiagnostic.Checked = AccessConfig._AdministratorAccess[2];
                ckb_EnabAccessConfig.Checked = AccessConfig._AdministratorAccess[3];
            }
        }

        private void uctrlAccessConfig_Load(object sender, EventArgs e)
        {
            lv_UserData.Columns.Add("Badge Number", 125);
            lv_UserData.Columns.Add("User Name", 125);
            lv_UserData.Columns.Add("Level", 125);
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            lv_UserData.Enabled = false;
            btn_Save.Enabled = true;
            btn_AddUser.Enabled = false;
            btn_CancelAddUser.Enabled = false;
            pnl_User.Enabled = true;
            btn_Edit.Enabled = false;
            btn_Delete.Enabled = false;
            GDefine._bPanelUnlock = false;
        }

        public static bool _AddNewUser = false;
        private void btn_AddUser_Click(object sender, EventArgs e)
        {
            lv_UserData.Enabled = false;
            btn_AddUser.Enabled = false;
            btn_CancelAddUser.Enabled = true;
            btn_Edit.Enabled = false;
            btn_Delete.Enabled = false;
            pnl_User.Enabled = true;
            _AddNewUser = true;
            GDefine._bPanelUnlock = false;
            btn_Save.Enabled = true;
            for (int i = 0; i < lv_UserData.Items.Count; i++)
            {
                if (lv_UserData.Items[i].Selected)
                {
                    lv_UserData.Items[i].Selected = false;
                }
            }

            txt_BadgeNum.Text = "";
            txt_UserName.Text = "New User";
            cmb_Level.SelectedIndex = 0;
        }

        private void btn_CancelAddUser_Click(object sender, EventArgs e)
        {
            txt_BadgeNum.Text = string.Empty;
            txt_UserName.Text = string.Empty;
            cmb_Level.SelectedIndex = 0;
            lv_UserData.Enabled = true;
            btn_AddUser.Enabled = true;
            btn_CancelAddUser.Enabled = false;
            pnl_User.Enabled = false;
            btn_Save.Enabled = false;
            _AddNewUser = false;
            GDefine._bPanelUnlock = true;
        }
        void ClearText()
        {
            txt_BadgeNum.Text = string.Empty;
            txt_UserName.Text = string.Empty;
            cmb_Level.SelectedIndex = 0;
            txt_Password.Text = string.Empty;
        }
        private void btn_Save_Click(object sender, EventArgs e)
        {
            if (txt_BadgeNum.Text == string.Empty || txt_BadgeNum.Text.Trim() == "")
            {
                frmMsg = new frmMessaging2();
                frmMsg.ShowMsg("Empty Badge Number Detected! Please insert Badge Number.", frmMessaging2.TMsgBtn.smbOK);
                DialogResult dialogResult = frmMsg.ShowDialog();
                txt_BadgeNum.Text = string.Empty;
                return;
            }
            if (_AddNewUser)
            {
                DialogResult dialogResult;
                for (int j = 0; j < AccessConfig._slBadgeNumber.Count; j++)
                {
                    if (txt_BadgeNum.Text == AccessConfig._slBadgeNumber[j])
                    {
                        frmMsg = new frmMessaging2();
                        frmMsg.ShowMsg("Duplicate Badge Number " + AccessConfig._slBadgeNumber[j] + (char)13 + "Please Insert Corrected Badge Number!", frmMessaging2.TMsgBtn.smbOK);
                        dialogResult = frmMsg.ShowDialog();
                        if (dialogResult == DialogResult.OK)
                        {
                            return;
                        }
                    }
                }
                frmMsg = new frmMessaging2();
                frmMsg.ShowMsg("Are You Sure to Save New User?", frmMessaging2.TMsgBtn.smbOK | frmMessaging2.TMsgBtn.smbCancel);
                dialogResult = frmMsg.ShowDialog();
                if (dialogResult == DialogResult.Cancel)
                {
                    return;
                }

                string[] arr = new string[4];
                ListViewItem itm;
                AccessConfig._slBadgeNumber.Add(txt_BadgeNum.Text);
                AccessConfig._slUserName.Add(txt_UserName.Text);
                AccessConfig._slLevel.Add(cmb_Level.SelectedIndex);
                AccessConfig._slPassword.Add(txt_Password.Text);
                arr[0] = AccessConfig._slBadgeNumber[AccessConfig._slBadgeNumber.Count - 1].ToString();
                arr[1] = AccessConfig._slUserName[AccessConfig._slBadgeNumber.Count - 1].ToString();
                arr[2] = AccessConfig._aAccessLevel[AccessConfig._slLevel[AccessConfig._slBadgeNumber.Count - 1]].ToString();
                arr[3] = AccessConfig._slPassword[AccessConfig._slPassword.Count - 1].ToString();
                itm = new ListViewItem(arr);
                lv_UserData.Items.Add(itm);
                //lv_UserData.Items[AccessConfig._slBadgeNumber.Count - 1].Selected = true;

                _AddNewUser = false;
                btn_AddUser.Enabled = true;
                btn_Save.Enabled = false;
                btn_CancelAddUser.Enabled = false;
                lv_UserData.Enabled = true;
                pnl_User.Enabled = false;
                btn_Edit.Enabled = false;
                btn_Delete.Enabled = false;
                ClearText();
                AccessConfig.SaveAccessConfig();
            }
            else
            {
                DialogResult dialogResult;
                for (int i = 0; i < lv_UserData.Items.Count; i++)
                {
                    if (lv_UserData.Items[i].Selected)
                    {
                        for (int j = 0; j < AccessConfig._slBadgeNumber.Count; j++)
                        {
                            if (txt_BadgeNum.Text == AccessConfig._slBadgeNumber[j])
                            {
                                if (i == j) continue;
                                frmMsg = new frmMessaging2();
                                frmMsg.ShowMsg("Duplicate Badge Number " + AccessConfig._slBadgeNumber[j] + (char)13 +
                                "Please Insert Corrected Badge Number!", frmMessaging2.TMsgBtn.smbOK);
                                dialogResult = frmMsg.ShowDialog();
                                if (dialogResult == DialogResult.OK)
                                {
                                    txt_BadgeNum.Text = string.Empty;
                                    return;
                                }
                            }
                        }

                        frmMsg = new frmMessaging2();
                        frmMsg.ShowMsg("Are You Sure to Save Changes?", frmMessaging2.TMsgBtn.smbOK | frmMessaging2.TMsgBtn.smbCancel);
                        dialogResult = frmMsg.ShowDialog();
                        if (dialogResult == DialogResult.Cancel)
                        {
                            return;
                        }

                        AccessConfig._slBadgeNumber[i] = txt_BadgeNum.Text.ToString();
                        AccessConfig._slUserName[i] = txt_UserName.Text.ToString();
                        AccessConfig._slLevel[i] = cmb_Level.SelectedIndex;
                        AccessConfig._slPassword[i] = txt_Password.Text.ToString();
                        lv_UserData.Items[i].Text = AccessConfig._slBadgeNumber[i];
                        lv_UserData.Items[i].SubItems[1].Text = AccessConfig._slUserName[i].ToString();
                        lv_UserData.Items[i].SubItems[2].Text = AccessConfig._aAccessLevel[AccessConfig._slLevel[i]].ToString();


                        _AddNewUser = false;
                        btn_AddUser.Enabled = true;
                        btn_Save.Enabled = false;
                        btn_CancelAddUser.Enabled = false;
                        lv_UserData.Enabled = true;
                        pnl_User.Enabled = false;
                        btn_Edit.Enabled = false;
                        btn_Delete.Enabled = false;
                        ClearText();
                        AccessConfig.SaveAccessConfig();
                    }
                }
            }
            GDefine._bPanelUnlock = true;
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lv_UserData.Items.Count; i++)
            {
                if (lv_UserData.Items[i].Selected)
                {
                    frmMsg = new frmMessaging2();
                    frmMsg.ShowMsg("Are you sure to delete User " + AccessConfig._slUserName[i] + (char)13 +
                       "OK - Delete.", frmMessaging2.TMsgBtn.smbOK | frmMessaging2.TMsgBtn.smbCancel);
                    DialogResult dialogResult = frmMsg.ShowDialog();
                    if (dialogResult == DialogResult.OK)
                    {
                        lv_UserData.Items[i].Remove();
                        AccessConfig._slBadgeNumber.RemoveAt(i);
                        AccessConfig._slUserName.RemoveAt(i);
                        AccessConfig._slPassword.RemoveAt(i);
                        AccessConfig._slLevel.RemoveAt(i);
                        AccessConfig.SaveAccessConfig();
                    }
                }
            }
        }

        private void lv_UserData_MouseClick(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lv_UserData.Items.Count; i++)
            {
                if (lv_UserData.Items[i].Selected)
                {
                    txt_BadgeNum.Text = AccessConfig._slBadgeNumber[i];
                    txt_UserName.Text = AccessConfig._slUserName[i];
                    cmb_Level.SelectedIndex = AccessConfig._slLevel[i];
                    txt_Password.Text = AccessConfig._slPassword[i];
                }
            }
        }

        private void lv_UserData_MouseUp(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lv_UserData.Items.Count; i++)
            {
                if (lv_UserData.Items[i].Selected)
                {
                    txt_BadgeNum.Text = AccessConfig._slBadgeNumber[i];
                    txt_UserName.Text = AccessConfig._slUserName[i];
                    cmb_Level.SelectedIndex = AccessConfig._slLevel[i];
                    txt_Password.Text = AccessConfig._slPassword[i];
                    btn_Edit.Enabled = true;
                    btn_Delete.Enabled = true;
                    return;
                }
                else
                {
                    btn_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                    ClearText();
                }
            }
        }

        private void cmb_LevelAccessPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCheckBoxTick();
        }
        private void UpdateVariable()
        {
            if (cmb_LevelAccessPage.SelectedIndex == 0)
            {
                AccessConfig._OperatorAccess[0] = ckb_EnabSetupPage.Checked;
                AccessConfig._OperatorAccess[1] = ckb_EnabDeviceRecipe.Checked;
                AccessConfig._OperatorAccess[2] = ckb_EnabDiagnostic.Checked;
                AccessConfig._OperatorAccess[3] = ckb_EnabAccessConfig.Checked;
            }
            else if (cmb_LevelAccessPage.SelectedIndex == 1)
            {
                AccessConfig._TechnicianAccess[0] = ckb_EnabSetupPage.Checked;
                AccessConfig._TechnicianAccess[1] = ckb_EnabDeviceRecipe.Checked;
                AccessConfig._TechnicianAccess[2] = ckb_EnabDiagnostic.Checked;
                AccessConfig._TechnicianAccess[3] = ckb_EnabAccessConfig.Checked;

            }
            else if (cmb_LevelAccessPage.SelectedIndex == 2)
            {
                AccessConfig._EngineerAccess[0] = ckb_EnabSetupPage.Checked;
                AccessConfig._EngineerAccess[1] = ckb_EnabDeviceRecipe.Checked;
                AccessConfig._EngineerAccess[2] = ckb_EnabDiagnostic.Checked;
                AccessConfig._EngineerAccess[3] = ckb_EnabAccessConfig.Checked;
            }
            else if (cmb_LevelAccessPage.SelectedIndex == 3)
            {
                AccessConfig._AdministratorAccess[0] = ckb_EnabSetupPage.Checked;
                AccessConfig._AdministratorAccess[1] = ckb_EnabDeviceRecipe.Checked;
                AccessConfig._AdministratorAccess[2] = ckb_EnabDiagnostic.Checked;
                AccessConfig._AdministratorAccess[3] = ckb_EnabAccessConfig.Checked;
            }

            AccessConfig.SaveAccessPageConfig();
        }
        private void btn_SaveConfig_Click(object sender, EventArgs e)
        {
            frmMsg = new frmMessaging2();
            frmMsg.ShowMsg("Are You Sure to Save Access Page Configuration?", frmMessaging2.TMsgBtn.smbOK | frmMessaging2.TMsgBtn.smbCancel);
            DialogResult dialogResult = frmMsg.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                UpdateVariable(); return;
            }
        }

        private void lv_UserData_MouseClick_1(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lv_UserData.Items.Count; i++)
            {
                if (lv_UserData.Items[i].Selected)
                {
                    txt_BadgeNum.Text = AccessConfig._slBadgeNumber[i];
                    txt_UserName.Text = AccessConfig._slUserName[i];
                    cmb_Level.SelectedIndex = AccessConfig._slLevel[i];
                    txt_Password.Text = AccessConfig._slPassword[i];
                }
            }
        }

        private void lv_UserData_MouseUp_1(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < lv_UserData.Items.Count; i++)
            {
                if (lv_UserData.Items[i].Selected)
                {
                    txt_BadgeNum.Text = AccessConfig._slBadgeNumber[i];
                    txt_UserName.Text = AccessConfig._slUserName[i];
                    cmb_Level.SelectedIndex = AccessConfig._slLevel[i];
                    txt_Password.Text = AccessConfig._slPassword[i];
                    btn_Edit.Enabled = true;
                    btn_Delete.Enabled = true;
                    return;
                }
                else
                {
                    btn_Edit.Enabled = false;
                    btn_Delete.Enabled = false;
                    ClearText();
                }
            }
        }

        private void lbl_IdleTimeAutoLogout_Click(object sender, EventArgs e)
        {
            int value = AccessConfig._iIdleTimeLogOut;
            GDefine.UserCtrl.UserAdjustExecute(ref value, 5, 180);
            AccessConfig._iIdleTimeLogOut = value;
            lbl_IdleTimeAutoLogout.Text = AccessConfig._iIdleTimeLogOut.ToString();
        }
    }
}
