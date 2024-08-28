using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MotionIODevice.Common;

namespace Machine
{
    public partial class DisplayBtn : UserControl
    {
        public DisplayBtn()
        {
            InitializeComponent();
        }

        public override Color BackColor
        {
            set
            {
                btn_AxisName.BackColor = value;
            }
        }

        public int BoradNo { get; set; }
        public int AxisNo { get; set; }

        public TAxis SelectedTAxis { get; set; }

        public override string Text
        {
            get { return btn_AxisName.Text; }
            set
            {
                btn_AxisName.Text = value;
            }
        }

        public override Color ForeColor
        {
            set
            {
                btn_AxisName.ForeColor = value;
            }
        }

        private void btn_AxisName_Click(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }
    }
}
