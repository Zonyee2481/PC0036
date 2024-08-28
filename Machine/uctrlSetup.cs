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
    public partial class uctrlSetup : UserControl
    {
        private System.Windows.Forms.Timer tmr_Display = new System.Windows.Forms.Timer();
        public uctrlSetup()
        {
            InitializeComponent();
            InitGUI();
        }
        public static uctrlSetup Page = new uctrlSetup();

        public void ShowPage(Control parent)
        {
            try
            {
                this.Parent = parent;
                this.Dock = DockStyle.Fill;

                this.Show();
            }
            catch (Exception ex)
            {
                string ExMsg = (char)13 + ex.Message.ToString();
                throw new Exception(ExMsg);
            }
        }

        public void HidePage()
        {
            Visible = false;
        }

        private void StartTimer()
        {
            this.tmr_Display.Enabled = true;
            this.tmr_Display.Interval = 10;
            this.tmr_Display.Tick += new System.EventHandler(this.tmr_Display_Tick);
        }

        private void tmr_Display_Tick(object sender, EventArgs e)
        {
            if (!Visible) return;
            tmr_Display.Stop();

            tmr_Display.Start();
        }

        private void InitGUI()
        {
            lbl_MaxCount.Text = GDefine._iMaxCounter.ToString();
            InitBitCodeGUI();
            StartTimer();
        }

        private void InitBitCodeGUI()
        {
            pnlBitCodesPoint.Controls.Clear();
            for (int i = 0; i < TaskBitCode.lBitCodes.Count; i++)
            {
                DrawIndicator(pnlBitCodesPoint, i);
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
            bc.Click += new EventHandler(TextBox_Clicked);
            pnl.Controls.Add(bc);
        }

        public void TextBox_Clicked(object sender, EventArgs e)
        {
            try
            {
                BitCodePoint bc = sender as BitCodePoint;
                frmBitCodeInfo frm = new frmBitCodeInfo(bc);
                frm.ShowDialog();
                TaskBitCode.LoadBitCodeRecipe();
                InitGUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "TextBox_Clicked",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lbl_MaxCount_Click(object sender, EventArgs e)
        {
            GDefine.UserCtrl.UserAdjustExecute(ref GDefine._iMaxCounter, 1, 100);
            lbl_MaxCount.Text = GDefine._iMaxCounter.ToString();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            GDefine.SaveDefaultFile();
        }
    }
}
