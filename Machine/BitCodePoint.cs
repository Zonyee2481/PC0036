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
    public partial class BitCodePoint : UserControl
    {
        public BitCodePoint()
        {
            InitializeComponent();
        }

        public TextBox txt
        {
            get
            {
                return txtDescription;
            }
        }

        public string IndexText
        {
            get { return lblIndex.Text; }
            set
            {
                lblIndex.Text = value;
            }
        }

        public string DescriptionText
        {
            get { return txtDescription.Text; }
            set
            {
                txtDescription.Text = value;
            }
        }

        public override string Text 
        {
            get { return lblIndex.Text; }
            set
            {
                lblIndex.Text = value;
            }
        }

        public bool CheckedBitCode_1
        {
            get { return cbBitCode_1.Checked; }
            set
            {
                cbBitCode_1.Checked = value;
            }
        }

        public bool CheckedBitCode_2
        {
            get { return cbBitCode_2.Checked; }
            set
            {
                cbBitCode_2.Checked = value;
            }
        }

        public bool CheckedBitCode_4
        {
            get { return cbBitCode_4.Checked; }
            set
            {
                cbBitCode_4.Checked = value;
            }
        }

        public bool CheckedBitCode_8
        {
            get { return cbBitCode_8.Checked; }
            set
            {
                cbBitCode_8.Checked = value;
            }
        }

        public int Index
        {
            get; set;
        }

        private void cbBitCode_1_Click(object sender, EventArgs e)
        {
            cbBitCode_1.Checked = !CheckedBitCode_1;
        }

        private void cbBitCode_2_Click(object sender, EventArgs e)
        {
            cbBitCode_2.Checked = !CheckedBitCode_2;
        }

        private void cbBitCode_4_Click(object sender, EventArgs e)
        {
            cbBitCode_4.Checked = !CheckedBitCode_4;
        }

        private void cbBitCode_8_Click(object sender, EventArgs e)
        {
            cbBitCode_8.Checked = !CheckedBitCode_8;
        }

        private void txtDescription_DoubleClick(object sender, EventArgs e)
        {
            this.InvokeOnClick(this, e);
        }
    }
}
