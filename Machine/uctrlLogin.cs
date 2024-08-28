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
    public partial class uctrlLogin : UserControl
    {
        public uctrlLogin()
        {
            InitializeComponent();
        }
        public static uctrlLogin Page = new uctrlLogin();
        public void ShowPage(Control parent)
        {
            Page.Parent = parent;
            Page.Dock = DockStyle.Fill;
            Page.UpdateDisplay();
            Page.Show();
            txt_ID.Focus();
        }
        public void HidePage()
        {
            Visible = false;
        }

        private void UpdateDisplay()
        {

        }

        string[] AccessStr = { "Operator", "Technician", "Engineer", "Administrator" };
        private void UpdateList()
        {
            cbox_Name.Items.Clear();
            for (int i = 0; i < 4; i++)
            {
                cbox_Name.Items.Add(AccessStr[i]);
            }
            //cbox_Name.SelectedIndex = AccessConfig.AccessUser.LogonIdx;
        }

        private void uctrlLogin_Load(object sender, EventArgs e)
        {
            tbox_Pswd.Text = "";
            UpdateList();
            cbox_Name.SelectedIndex = 0;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (!AccessConfig.LoginCheck(txt_ID.Text, tbox_Pswd.Text))
            {
                txt_ID.Text = "";
                tbox_Pswd.Text = "";
                return;
            }
            txt_ID.Text = "";
            tbox_Pswd.Text = "";
            pnl_Login.Visible = false;
            lbl_Welcome.Text = "Welcome " + AccessConfig._sCurrentLoginUserName + " !";
            llb_Login.Text = "Logout";
            UpdateDisplay();
            //AccessConfig._bUserLogin = true;
            uctrlAuto.Page.AddToLog("Login Success! Welcome  " + AccessConfig._sCurrentLoginUserName);
            //GDefine.dbMain.LogAction(AccessConfig._sCurrentLoginUserName, btn_OK.Name, "Login Success! Welcome " + AccessConfig._sCurrentLoginUserName);
        }

        private void btn_AccessSetup_Click(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            pnl_Login.Visible = false;
            txt_ID.Text = "";
            tbox_Pswd.Text = "";
            txt_ID.Focus();
            //btn_AccessSetup.Visible = true;
        }

        private void llb_Login_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (llb_Login.Text == "Logout")
            {
                //GDefine.dbMain.LogAction("", llb_Login.Name, AccessConfig._sCurrentLoginUserName + " Is Logging out! ");
                AccessConfig._sCurrentLoginUserName = "";
                AccessConfig._sCurrentLoginBatchNum = "";
                AccessConfig._sCurrentLoginLevel = "";
                AccessConfig.CurrentAccessLvl = 0;
                //AccessConfig._bUserLogin = false;
                pnl_Login.Visible = true;
                llb_Login.Text = "Login";
                lbl_Welcome.Text = "Welcome Operator !";
                txt_ID.Focus();
                uctrlAuto.Page.AddToLog(AccessConfig._sCurrentLoginUserName + " Is Logging out! ");

                return;
            }
            pnl_Login.Visible = true;
            tbox_Pswd.Text = string.Empty;
            txt_ID.Focus();
        }

        private void uctrlLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!AccessConfig.LoginCheck(txt_ID.Text, tbox_Pswd.Text))
                {
                    txt_ID.Text = "";
                    tbox_Pswd.Text = "";
                    return;
                }
                txt_ID.Text = "";
                tbox_Pswd.Text = "";
                pnl_Login.Visible = false;
                lbl_Welcome.Text = "Welcome " + AccessConfig._sCurrentLoginUserName + " !";
                llb_Login.Text = "Logout";
                UpdateDisplay();
            }
        }

        private void tbox_Pswd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!AccessConfig.LoginCheck(txt_ID.Text, tbox_Pswd.Text))
                {
                    txt_ID.Text = "";
                    tbox_Pswd.Text = "";
                    return;
                }
                txt_ID.Text = "";
                tbox_Pswd.Text = "";
                pnl_Login.Visible = false;
                lbl_Welcome.Text = "Welcome " + AccessConfig._sCurrentLoginUserName + " !";
                llb_Login.Text = "Logout";
                //AccessConfig._bUserLogin = true;
                //UpdateDisplay();
                //frmMain.GetPageAccessLvl();
                uctrlAuto.Page.AddToLog("Login Success! Welcome  " + AccessConfig._sCurrentLoginUserName);
                //GDefine.dbMain.LogAction(AccessConfig._sCurrentLoginUserName, btn_OK.Name, "Login Success! Welcome " + AccessConfig._sCurrentLoginUserName);
            }
        }

        private void txt_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!AccessConfig.LoginCheck(txt_ID.Text, tbox_Pswd.Text))
                {
                    txt_ID.Text = "";
                    tbox_Pswd.Text = "";
                    return;
                }
                txt_ID.Text = "";
                tbox_Pswd.Text = "";
                pnl_Login.Visible = false;
                lbl_Welcome.Text = "Welcome " + AccessConfig._sCurrentLoginUserName + " !";
                llb_Login.Text = "Logout";
                //UpdateDisplay();
                //AccessConfig._bUserLogin = true;
                //frmMain.GetPageAccessLvl();
                uctrlAuto.Page.AddToLog("Login Success! Welcome  " + AccessConfig._sCurrentLoginUserName);
                //GDefine.dbMain.LogAction(AccessConfig._sCurrentLoginUserName, btn_OK.Name, "Login Success! Welcome " + AccessConfig._sCurrentLoginUserName);
            }
        }
    }
}
