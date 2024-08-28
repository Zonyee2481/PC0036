using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MotionIODevice.Common;

namespace Machine
{
    public partial class IOPointTitle : UserControl
    {

        public IOPointTitle()
        {
            InitializeComponent();
        }

        public override string Text
        {
            get { return lbl_Display.Text; }
            set
            {
                lbl_Display.Text = value;
            }
        }

        public override Color ForeColor
        {
            //get { return lblDisplay.ForeColor; }
            set
            {
                lbl_Display.ForeColor = value;
            }
        }

        public override Color BackColor
        {
            //get { return lbl_Display.BackColor; }
            set
            {
                lbl_Display.BackColor = value;
            }
        }

        private Size m_IOPointSize = new Size(180, 20);
        public Size IOPointSize
        {
            get { return m_IOPointSize; }
            set
            {
                if (value.Width >= 120 && value.Height >= 20)
                {
                    m_IOPointSize = value;
                    this.MaximumSize = m_IOPointSize;
                    this.MinimumSize = m_IOPointSize;
                    this.Size = m_IOPointSize;
                }
            }
        }

        public TAxis Taxis { get; set; }
        private void IOPoint_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }
    }
}
