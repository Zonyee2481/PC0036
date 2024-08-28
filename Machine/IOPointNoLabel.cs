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
    public partial class IOPointNoLabel : UserControl
    {

        public IOPointNoLabel()
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

        private Size m_IOPointSize = new Size(150, 20);
        public Size IOPointSize
        {
            get { return m_IOPointSize; }
            set
            {
                if (value.Width >= 100 && value.Height >= 20)
                {
                    m_IOPointSize = value;
                    this.MaximumSize = m_IOPointSize;
                    this.MinimumSize = m_IOPointSize;
                    this.Size = m_IOPointSize;
                }
            }
        }

        private void IOPoint_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }
    }
}
