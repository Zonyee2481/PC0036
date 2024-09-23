using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Machine
{
    public partial class uctrlDeviceRecipe : UserControl
    {
        private frmMessaging2 frmMsg;
        public uctrlDeviceRecipe()
        {
            InitializeComponent();
            lv_DeviceRecipeList.Columns.Add("No", 30);
            lv_DeviceRecipeList.Columns.Add("Device ID", 200);
            lv_DeviceRecipeList.Columns.Add("Assigned Code", 100);            
            lv_DeviceRecipeList.Columns.Add("Duration 1", 120);
            lv_DeviceRecipeList.Columns.Add("Duration 2", 120);
            //lv_DeviceRecipeList.Columns.Add("Counter", 50);
        }
        public static uctrlDeviceRecipe Page = new uctrlDeviceRecipe();
        public void ShowPage(Control parent)
        {
            Page.Parent = parent;
            Page.Dock = DockStyle.Fill;
            Page.Show();
            ClearListView();
            UpdateListView();
        }

        public void HidePage()
        {
            Visible = false;
        }

        private void UpdateListView()
        {
            int counter = 0;
            for (int i = 0; i < TaskDeviceRecipe.asDeviceID.Count(); i++)
            {
                string[] arr = new string[6];
                ListViewItem itm;
                try
                {
                    if (TaskDeviceRecipe.asDeviceID[i] == null) break;
                    counter++;
                    int time = Convert.ToInt32(TaskDeviceRecipe.adDuration[i] * (60 * 60));
                    arr[0] = counter.ToString();
                    int S, M, H;
                    S = time % 60;
                    M = (time / 60) % 60;
                    H = (time / (3600)) % 24;
                    //arr[3] = H + "H" + M + "M" + S + "S";
                    arr[1] = TaskDeviceRecipe.asDeviceID[i].ToString();
                    arr[2] = TaskDeviceRecipe.aiAssignedNo[i].ToString();
                    arr[3] = TaskDeviceRecipe.adDuration[i].ToString("0.0000");
                    arr[4] = H + " H " + M + " M " + S + " S ";
                    //arr[5] = TaskDeviceRecipe.aiCounter[i].ToString();
                    itm = new ListViewItem(arr);
                    lv_DeviceRecipeList.Items.Add(itm);
                }
                catch { }
            }
        }
        private void ClearListView()
        {
            foreach (ListViewItem item in lv_DeviceRecipeList.Items)
            {
                item.Remove();
            }
        }

        static frmDeviceRecipe form = new frmDeviceRecipe();
        private void btn_AddDeviceRecipe_Click(object sender, EventArgs e)
        {
            form._bEdit = false;
            form._bNew = true;
            form._sDeviceID = "";
            form._iAssignedNo = 0;
            //form._iCounter = 0;
            form._dDuration = 0;

            form.ShowDialog();
            ClearListView();
            UpdateListView();
        }

        private void btn_EditDeviceRecipe_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lv_DeviceRecipeList.Items.Count; i++)
            {
                if (lv_DeviceRecipeList.Items[i].Selected)
                {
                    form._iIndex = i;
                    form._bEdit = true;
                    form._bNew = false;
                    form._sDeviceID = lv_DeviceRecipeList.Items[i].SubItems[1].Text;
                    form._iAssignedNo = Convert.ToInt32(lv_DeviceRecipeList.Items[i].SubItems[2].Text);                    
                    form._dDuration = Convert.ToDouble(lv_DeviceRecipeList.Items[i].SubItems[3].Text);
                    form._sDuration_2 = lv_DeviceRecipeList.Items[i].SubItems[4].Text;
                    //form._iCounter = Convert.ToInt32(lv_DeviceRecipeList.Items[i].SubItems[5].Text);
                    form.ShowDialog();
                    ClearListView();
                    UpdateListView();
                }
            }
        }

        private void btn_DeleteDeviceRecipe_Click(object sender, EventArgs e)
        {
            string fileName = "";
            int count = 0;
            for (int i = 0; i < lv_DeviceRecipeList.Items.Count; i++)
            {
                if (lv_DeviceRecipeList.Items[i].Selected)
                {
                    count = i;
                    fileName = lv_DeviceRecipeList.Items[i].SubItems[1].Text;
                    goto _Continue;
                }
            }
            return;
        //Array.Clear(_asDeviceName[count], count, );
        _Continue:
            frmMsg = new frmMessaging2();
            frmMsg.ShowMsg("Are you sure to delete the selected Device Recipe?" + (char)13 +
                       "OK - Delete.", frmMessaging2.TMsgBtn.smbOK | frmMessaging2.TMsgBtn.smbCancel);
            DialogResult dialogResult = frmMsg.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                File.Delete(GDefine.DevicePath + @"\" + fileName + ".ini");
                ClearListView();
                TaskDeviceRecipe.LoadDeviceRecipe();
                UpdateListView();
            }
        }
    }
}
