#define OOP
#if OOP
using static MotionIODevice.Common;
using MotionIODevice;
#else
using ES.CMotion;
#endif
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionIODevice.IO;

namespace Machine
{
  public partial class uctrlIODiag : UserControl
    {
        private volatile int InputSelectedIndex, OutputSelectedIndex, MotionSelectedIndex;
        private List<IMotionMain> CompileMotionboards = new List<IMotionMain>();

        private System.Windows.Forms.Timer tmr_Display = new System.Windows.Forms.Timer();

        private Tuple<bool, string, string> tpUser;        
        
            private void OnUserLogin(Tuple<bool, string, string> tpUser)
        {
            this.tpUser = tpUser;
        }

        public uctrlIODiag()
        {
            InitializeComponent();
            InitGUI();
            
        }
        public static uctrlIODiag Page = new uctrlIODiag();
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
            RTUpdateIO();
            //RTUpdateMIO();
            tmr_Display.Start();
        }

        private void RTUpdateIO()
        {
            foreach (IOPoint io in GetSelectedTabIndicator(tc_In))
            {
                int BoardNo = InputSelectedIndex;
                ushort bit = Convert.ToUInt16(io.Tag);
                io.BackColor = frmMain.IOModule.ReadBit(BoardNo, bit) ? Color.Lime : Color.Transparent;
            }

            foreach (IOPoint io in GetSelectedTabIndicator(tc_Out))
            {
                int BoardNo = OutputSelectedIndex;
                ushort bit = Convert.ToUInt16(io.Tag);

                bool state = frmMain.IOModule.OutBit(BoardNo, bit, TOutputStatus.St);
                if (state)
                {
                    io.BackColor = Color.Red; io.ForeColor = Color.Black;
                }
                else
                {
                    io.BackColor = Color.Transparent; io.ForeColor = Color.Red;
                }
            }
        }

        private void RTUpdateMIO()
        {
            foreach (IOPoint io in GetSelectedTabIndicator(tm_In))
            {
                int BoardNo = MotionSelectedIndex;
                ushort bit = Convert.ToUInt16(io.Tag);

                //Temporary hardcoded
                if (MotionSelectedIndex <= 1)
                    io.BackColor = frmMain.AdvantechModule.ReadBit(BoardNo, bit) ? Color.Lime : Color.Transparent;
                else
                    io.BackColor = frmMain.MoonStfModule.ReadBit(0, bit) ? Color.Lime : Color.Transparent;
            }

            foreach (IOPoint io in GetSelectedTabIndicator(tm_Out))
            {
                int BoardNo = MotionSelectedIndex;
                ushort bit = Convert.ToUInt16(io.Tag);
                bool state = false;

                //Temporary hardcoded
                if (MotionSelectedIndex <= 1)
                    state = frmMain.AdvantechModule.OutBit(BoardNo, bit, Common.EOutputStatus.St);
                else
                    state = frmMain.MoonStfModule.OutBit(0, bit, Common.EOutputStatus.St);

                if (state)
                {
                    io.BackColor = Color.Red; io.ForeColor = Color.Black;
                }
                else
                {
                    io.BackColor = Color.Transparent; io.ForeColor = Color.Red;
                }
            }
        }

        private void InitGUI()
        {
            InitIOGUI();
            //InitMotionGUI();
            //ResetTabOne();
            StartTimer();
        }

        private void InitIOGUI()
        {
            var TotalIOBoards = frmMain.IOModule.GetBoardNo;
            for (int i = 1; i <= TotalIOBoards; i++)
            {
                TabPage tp = new TabPage();
                tp.Text = "Input_" + i.ToString();
                tc_In.TabPages.Add(tp);
            }
            for (int i = 1; i <= TotalIOBoards; i++)
            {
                TabPage tp = new TabPage();
                tp.Text = "Output_" + i.ToString();
                tc_Out.TabPages.Add(tp);
            }

            for (int i = 0; i < TotalIOBoards; i++)
            {
                var ipList = frmMain.IOModule.GetBoardInputList(i);
                var opList = frmMain.IOModule.GetBoardOutputList(i);

                var tabIndex = i;


                for (int j = 0; j < ipList.Count; j++)
                {
                    DrawIndicator(tc_In, ipList[j], j, tabIndex);
                }

                for (int j = 0; j < opList.Count; j++)
                {
                    DrawIndicator(tc_Out, opList[j], j, tabIndex);
                }

            }

        }

        private void InitMotionGUI()
        {
            var TotalMotionBoard = 0;
            CompileMotionboards.Add(frmMain.AdvantechModule);
            CompileMotionboards.Add(frmMain.MoonStfModule);

            for (int i = 0; i < CompileMotionboards.Count; i++)
            {
                TotalMotionBoard += CompileMotionboards[i].GetBoardNo;
            }

            for (int i = 1; i <= TotalMotionBoard; i++)
            {
                TabPage tp = new TabPage();
                tp.Text = "Motion Input_" + i.ToString();
                tm_In.TabPages.Add(tp);
            }

            for (int i = 1; i <= TotalMotionBoard; i++)
            {
                TabPage tp = new TabPage();
                tp.Text = "Motion Output_" + i.ToString();
                tm_Out.TabPages.Add(tp);
            }
            var tabIndex = 0;
            var motionboardtype = frmMain.AdvantechModule.MotionBoardType();

            if (motionboardtype  == EDeviceType.P1245)
            {                
                for (int i = 0; i < frmMain.AdvantechModule.GetBoardNo ; i ++)
                {
                    var ipList = frmMain.AdvantechModule.GetInputList(i);
                    var opList = frmMain.AdvantechModule.GetOutputList(i);
                    
                    var axisList = frmMain.AdvantechModule.GetAxisList(i);
                    for (int j = 0; j < axisList.Count; j++)
                    {
                        DrawIndicator(tm_In, axisList[j], j, tabIndex);
                    }

                    for (int j = 0; j < ipList.Count; j++)
                    {
                        DrawIndicator(tm_In, ipList[j], j, tabIndex, 7);
                    }

                    for (int j = 0; j < opList.Count; j++)
                    {
                        DrawIndicator(tm_Out, opList[j], j, tabIndex, 4);
                    }
                    tabIndex++;
                }
                
            }
            motionboardtype = frmMain.MoonStfModule.MotionBoardType();
            if (motionboardtype == EDeviceType.MoonsSTF)
            {
                for (int i = 0; i < frmMain.MoonStfModule.GetBoardNo; i++)
                {
                    var ipList = frmMain.MoonStfModule.GetInputList(i);
                    var opList = frmMain.MoonStfModule.GetOutputList(i);

                    var axisList = frmMain.MoonStfModule.GetAxisList(i);
                    for (int j = 0; j < axisList.Count; j++)
                    {
                        DrawIndicator(tm_In, axisList[j], j, tabIndex);
                    }

                    for (int j = 0; j < ipList.Count; j++)
                    {
                        DrawIndicator(tm_In, ipList[j], j, tabIndex, 8);
                    }

                    for (int j = 0; j < opList.Count; j++)
                    {
                        DrawIndicator(tm_Out, opList[j], j, tabIndex, 4);
                    }
                    tabIndex++;
                }

            }

            //var MoonsStfMotionBoard = frmMain.MoonStfModule.GetBoardNo;
            //for (int i = 0; i < MoonsStfMotionBoard; i++)
            //{
            //    var ipList = frmMain.MoonStfModule.GetInputList(i);
            //    var opList = frmMain.MoonStfModule.GetOutputList(i);

            //    var axisList = frmMain.MoonStfModule.GetAxisList(i);
            //    for (int j = 0; j < axisList.Count; j++)
            //    {
            //        DrawIndicator(tm_In, axisList[j], j, tabIndex);
            //    }

            //    for (int j = 0; j < ipList.Count; j++)
            //    {
            //        DrawIndicator(tm_In, ipList[j], j, tabIndex, 7);
            //    }

            //    for (int j = 0; j < opList.Count; j++)
            //    {
            //        DrawIndicator(tm_Out, opList[j], j, tabIndex, 4);
            //    }
            //    tabIndex++;
            //}
        }

        private void ResetTabOne()
        {
            tc_In.SelectTab(0);
            tc_Out.SelectTab(0);
            tm_In.SelectTab(0);
            tm_Out.SelectTab(0);

            InputSelectedIndex = OutputSelectedIndex = MotionSelectedIndex = 0;
        }

        public void DrawIndicator(TabControl tc, MotionIODevice.IO.TInput ip, int idx, int tabIdx, int fac = 8)
        {
            IOPoint iop = new IOPoint();
            iop.Label = String.IsNullOrEmpty(ip.Label) ? ("X " + (idx + 1).ToString("000")) : ip.Label;
            iop.Text = String.IsNullOrEmpty(ip.DisName) ? ip.Name : ip.DisName;
            iop.Tag = idx;

            iop.ForeColor = tc.Tag.Equals("Input") ? Color.Blue : Color.Red;
            int maxSide = idx / fac;
            int rowIdx = idx % fac;

            iop.Location = new Point(2 + (maxSide * 240), 3 + rowIdx * 28);

            if (!tc.Tag.Equals("Input"))
            {
                iop.Click += new EventHandler(Button_Clicked);
            }
            if (tabIdx < tc.TabCount)
            {
                tc.TabPages[tabIdx].Controls.Add(iop);
            }
        }

        public void DrawIndicator(TabControl tc, MotionIODevice.IO.TOutput op, int idx, int tabIdx, int fac = 8)
        {
            IOPoint iop = new IOPoint();
            iop.Label = String.IsNullOrEmpty(op.Label) ? ("Y " + (idx + 1).ToString("000")) : op.Label;
            iop.Text = String.IsNullOrEmpty(op.DisName) ? op.Name : op.DisName;
            iop.Tag = idx;
            iop.ForeColor = tc.Tag.Equals("Input") ? Color.Blue : Color.Red;
            int maxSide = idx / fac;
            int rowIdx = idx % fac;

            iop.Location = new Point(2 + (maxSide * 240), 3 + rowIdx * 28);

            if (!tc.Tag.Equals("Input"))
            {
                // Button Click Event
                iop.Click += new EventHandler(Button_Clicked);
            }
            if (tabIdx < tc.TabCount)
            {
                // Added IO Point to Tab Page
                tc.TabPages[tabIdx].Controls.Add(iop);
            }
        }

        public void DrawIndicator(TabControl tc, Common.TInput ip, int idx, int tabIdx, int fac = 8)
        {
            IOPoint iop = new IOPoint();
            iop.Label = String.IsNullOrEmpty(ip.Label) ? ("MX " + (idx + 1).ToString("000")) : ip.Label;
            iop.Text = String.IsNullOrEmpty(ip.Name) ? ("Input " + (idx + 1).ToString()) : ip.Name;
            iop.Tag = idx;

            iop.ForeColor = tc.Tag.Equals("Input") ? Color.Blue : Color.Red;
            int maxSide = idx / fac;
            int rowIdx = idx % fac;

            iop.Location = new Point(2 + (maxSide * 247), 35 + rowIdx * 28);

            if (!tc.Tag.Equals("Input"))
            {
                iop.Click += new EventHandler(MotionButton_Clicked);
            }
            if (tabIdx < tc.TabCount)
            {
                tc.TabPages[tabIdx].Controls.Add(iop);
            }
        }

        public void DrawIndicator(TabControl tc, Common.TOutput op, int idx, int tabIdx, int fac = 8)
        {
            IOPoint iop = new IOPoint();
            iop.Label = String.IsNullOrEmpty(op.Label) ? ("MY " + (idx + 1).ToString("000")) : op.Label;
            iop.Text = String.IsNullOrEmpty(op.Name) ? ("Output " + (idx + 1).ToString()) : op.Name;
            iop.Tag = idx;
            iop.ForeColor = tc.Tag.Equals("Input") ? Color.Blue : Color.Red;
            int maxSide = idx / fac;
            int rowIdx = idx % fac;

            iop.Location = new Point(2 + (maxSide * 247), 3 + rowIdx * 28);

            if (!tc.Tag.Equals("Input"))
            {
                // Button Click Event
                iop.Click += new EventHandler(MotionButton_Clicked);
            }
            if (tabIdx < tc.TabCount)
            {
                // Added IO Point to Tab Page
                tc.TabPages[tabIdx].Controls.Add(iop);
            }
        }
        public void DrawIndicator(TabControl tc, TAxis axis, int idx, int tabIdx, int fac = 1)
        {
            IOPointTitle iop = new IOPointTitle();
            iop.Text = axis.Name;
            iop.Tag = idx;
            iop.Taxis = axis;
            iop.BackColor = Color.Black;
            iop.ForeColor = Color.White;
            int maxSide = idx / fac;
            int rowIdx = idx % fac;

            iop.Location = new Point(2 + (maxSide * 247), 3 + rowIdx * 28);
            if (tabIdx < tc.TabCount)
            {
                // Added IO Point to Tab Page
                tc.TabPages[tabIdx].Controls.Add(iop);
            }
        }
        public void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                IOPoint io = sender as IOPoint;
                ushort bit = Convert.ToUInt16(io.Tag);
                var boardNo = OutputSelectedIndex;

                if (io != null)
                {

                    if (frmMain.IOModule.OutBit(boardNo, bit, TOutputStatus.St))
                    {
                        //LogAction(io, false);
                        frmMain.IOModule.OutBit(boardNo, bit, TOutputStatus.Lo);
                        ////if (bit == 5)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 6, TOutputStatus.Hi);
                        ////}
                        ////else if (bit == 6)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 5, TOutputStatus.Hi);
                        ////}
                        ////else if (bit == 7)
                        //if (bit == 7)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 8, TOutputStatus.Hi);
                        //}
                        //else if (bit == 8)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 7, TOutputStatus.Hi);
                        //}
                        //else if (bit == 12)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 13, TOutputStatus.Hi);
                        //}
                        //else if (bit == 13)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 12, TOutputStatus.Hi);
                        //}
                        ////else if (bit == 14)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 15, TOutputStatus.Hi);
                        ////}
                        ////else if (bit == 15)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 14, TOutputStatus.Hi);
                        ////}
                        //else if (bit == 18)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 19, TOutputStatus.Hi);
                        //}
                        //else if (bit == 19)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 18, TOutputStatus.Hi);
                        //}
                        //else if (bit == 22)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 23, TOutputStatus.Hi);
                        //}
                        //else if (bit == 23)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 22, TOutputStatus.Hi);
                        //}
                        ////else if (bit == 30)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 31, TOutputStatus.Hi);
                        ////}
                        ////else if (bit == 31)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 30, TOutputStatus.Hi);
                        ////}
                    }
                    else
                    {
                        //LogAction(io, true);
                        frmMain.IOModule.OutBit(boardNo, bit, TOutputStatus.Hi);
                        //if (bit == 5)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 6, TOutputStatus.Lo);
                        //}
                        //else if (bit == 6)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 5, TOutputStatus.Lo);
                        //}
                        //else if (bit == 7)
                        //if (bit == 7)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 8, TOutputStatus.Lo);
                        //}
                        //else if (bit == 8)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 7, TOutputStatus.Lo);
                        //}
                        //else if (bit == 12)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 13, TOutputStatus.Lo);
                        //}
                        //else if (bit == 13)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 12, TOutputStatus.Lo);
                        //}
                        ////else if (bit == 14)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 15, TOutputStatus.Lo);
                        ////}
                        ////else if (bit == 15)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 14, TOutputStatus.Lo);
                        ////}
                        //else if (bit == 18)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 19, TOutputStatus.Lo);
                        //}
                        //else if (bit == 19)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 18, TOutputStatus.Lo);
                        //}
                        //else if (bit == 22)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 23, TOutputStatus.Lo);
                        //}
                        //else if (bit == 23)
                        //{
                        //    frmMain.IOModule.OutBit(boardNo, 22, TOutputStatus.Lo);
                        //}
                        ////else if (bit == 30)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 31, TOutputStatus.Lo);
                        ////}
                        ////else if (bit == 31)
                        ////{
                        ////    frmMain.IOModule.OutBit(boardNo, 30, TOutputStatus.Lo);
                        ////}
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Button_Clicked",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void MotionButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                IOPoint io = sender as IOPoint;
                ushort bit = Convert.ToUInt16(io.Tag);
                var boardNo = MotionSelectedIndex;


                if (io != null)
                {
                    if (MotionSelectedIndex <= 1)
                    {
                        if (frmMain.AdvantechModule.OutBit(boardNo, bit, Common.EOutputStatus.St))
                        {
                            frmMain.AdvantechModule.OutBit(boardNo, bit, Common.EOutputStatus.Lo);
                        }
                        else
                        {
                            //LogAction(io, true);
                            frmMain.AdvantechModule.OutBit(boardNo, bit, Common.EOutputStatus.Hi);
                        }
                    }
                    else
                    {
                        if (frmMain.MoonStfModule.OutBit(0, bit, Common.EOutputStatus.St))
                        {
                            frmMain.MoonStfModule.OutBit(0, bit, Common.EOutputStatus.Lo);
                        }
                        else
                        {
                            //LogAction(io, true);
                            frmMain.MoonStfModule.OutBit(0, bit, Common.EOutputStatus.Hi);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Button_Clicked",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void tc_In_SelectedIndexChanged(object sender, EventArgs e)
        {
            InputSelectedIndex = tc_In.SelectedIndex;
        }

        public void HidePage()
        {
            this.Visible = false;
        }

        private void tm_In_SelectedIndexChanged(object sender, EventArgs e)
        {
            MotionSelectedIndex = tm_In.SelectedIndex;
            tm_Out.SelectTab(MotionSelectedIndex);
        }

        private void tc_Out_SelectedIndexChanged(object sender, EventArgs e)
        {
            OutputSelectedIndex = tc_Out.SelectedIndex;
        }

        IOPoint[] GetSelectedTabIndicator(TabControl tc)
        {
            List<IOPoint> list = new List<IOPoint>();
            foreach (Control ctrl in tc.SelectedTab.Controls)
            {
                IOPoint io = ctrl as IOPoint;
                if (io != null)
                {
                    list.Add(io);
                }
            }
            return list.ToArray();
        }
       
    }
}
