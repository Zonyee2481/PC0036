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
    public partial class frmBitCodeInfo : Form
    {
        BitCodePoint bc;
        public frmBitCodeInfo(BitCodePoint bc)
        {
            InitializeComponent();
            this.bc = bc;
            txtNum.Text = bc.Index.ToString();
            cbBitCode_1.Checked = bc.CheckedBitCode_1;
            cbBitCode_2.Checked = bc.CheckedBitCode_2;
            cbBitCode_4.Checked = bc.CheckedBitCode_4;
            cbBitCode_8.Checked = bc.CheckedBitCode_8;
            txtDescription.Text = bc.DescriptionText;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TaskBitCode.lBitCodes[bc.Index].Description = txtDescription.Text;
            TaskBitCode.SaveBitCodeRecipe();
            Close();
        }

        private void cbBitCode_1_Click(object sender, EventArgs e)
        {
            cbBitCode_1.Checked = bc.CheckedBitCode_1;
        }

        private void cbBitCode_2_CheckedChanged(object sender, EventArgs e)
        {
            cbBitCode_2.Checked = bc.CheckedBitCode_2;
        }

        private void cbBitCode_4_CheckedChanged(object sender, EventArgs e)
        {
            cbBitCode_4.Checked = bc.CheckedBitCode_4;
        }

        private void cbBitCode_8_CheckedChanged(object sender, EventArgs e)
        {
            cbBitCode_8.Checked = bc.CheckedBitCode_8;
        }
    }
}
