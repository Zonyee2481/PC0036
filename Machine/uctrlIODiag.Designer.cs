
namespace Machine
{
    partial class uctrlIODiag
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_Title = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tc_Out = new System.Windows.Forms.TabControl();
            this.tc_In = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tm_In = new System.Windows.Forms.TabControl();
            this.tm_Out = new System.Windows.Forms.TabControl();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_Title.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.lbl_Title.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbl_Title.Location = new System.Drawing.Point(0, 0);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(1183, 45);
            this.lbl_Title.TabIndex = 4;
            this.lbl_Title.Text = "IO Diagnostic";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 45);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1183, 427);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.tc_Out);
            this.tabPage1.Controls.Add(this.tc_In);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(1175, 401);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "I/O";
            // 
            // tc_Out
            // 
            this.tc_Out.Location = new System.Drawing.Point(4, 5);
            this.tc_Out.Margin = new System.Windows.Forms.Padding(2);
            this.tc_Out.Name = "tc_Out";
            this.tc_Out.SelectedIndex = 0;
            this.tc_Out.Size = new System.Drawing.Size(1002, 260);
            this.tc_Out.TabIndex = 0;
            this.tc_Out.Tag = "Output";
            this.tc_Out.SelectedIndexChanged += new System.EventHandler(this.tc_Out_SelectedIndexChanged);
            // 
            // tc_In
            // 
            this.tc_In.Location = new System.Drawing.Point(4, 270);
            this.tc_In.Margin = new System.Windows.Forms.Padding(2);
            this.tc_In.Name = "tc_In";
            this.tc_In.SelectedIndex = 0;
            this.tc_In.Size = new System.Drawing.Size(1002, 260);
            this.tc_In.TabIndex = 1;
            this.tc_In.Tag = "Input";
            this.tc_In.SelectedIndexChanged += new System.EventHandler(this.tc_In_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.tm_In);
            this.tabPage2.Controls.Add(this.tm_Out);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(1175, 401);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Motion";
            // 
            // tm_In
            // 
            this.tm_In.Location = new System.Drawing.Point(4, 5);
            this.tm_In.Margin = new System.Windows.Forms.Padding(2);
            this.tm_In.Name = "tm_In";
            this.tm_In.SelectedIndex = 0;
            this.tm_In.Size = new System.Drawing.Size(1002, 310);
            this.tm_In.TabIndex = 2;
            this.tm_In.Tag = "Input";
            this.tm_In.SelectedIndexChanged += new System.EventHandler(this.tm_In_SelectedIndexChanged);
            // 
            // tm_Out
            // 
            this.tm_Out.Location = new System.Drawing.Point(4, 291);
            this.tm_Out.Margin = new System.Windows.Forms.Padding(2);
            this.tm_Out.Name = "tm_Out";
            this.tm_Out.SelectedIndex = 0;
            this.tm_Out.Size = new System.Drawing.Size(1002, 212);
            this.tm_Out.TabIndex = 3;
            this.tm_Out.Tag = "Output";
            // 
            // uctrlIODiag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lbl_Title);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "uctrlIODiag";
            this.Size = new System.Drawing.Size(1183, 472);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tc_Out;
        private System.Windows.Forms.TabControl tc_In;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tm_In;
        private System.Windows.Forms.TabControl tm_Out;
    }
}
