
namespace Machine
{
    partial class frmBitCodeInfo
    {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbBitCodeInfo = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBitCode_8 = new System.Windows.Forms.CheckBox();
            this.cbBitCode_4 = new System.Windows.Forms.CheckBox();
            this.cbBitCode_2 = new System.Windows.Forms.CheckBox();
            this.cbBitCode_1 = new System.Windows.Forms.CheckBox();
            this.txtNum = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.gbBitCodeInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbBitCodeInfo);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 224);
            this.panel1.TabIndex = 0;
            // 
            // gbBitCodeInfo
            // 
            this.gbBitCodeInfo.Controls.Add(this.btnClose);
            this.gbBitCodeInfo.Controls.Add(this.btnSave);
            this.gbBitCodeInfo.Controls.Add(this.txtDescription);
            this.gbBitCodeInfo.Controls.Add(this.label1);
            this.gbBitCodeInfo.Controls.Add(this.cbBitCode_8);
            this.gbBitCodeInfo.Controls.Add(this.cbBitCode_4);
            this.gbBitCodeInfo.Controls.Add(this.cbBitCode_2);
            this.gbBitCodeInfo.Controls.Add(this.cbBitCode_1);
            this.gbBitCodeInfo.Controls.Add(this.txtNum);
            this.gbBitCodeInfo.Controls.Add(this.label26);
            this.gbBitCodeInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBitCodeInfo.Location = new System.Drawing.Point(0, 0);
            this.gbBitCodeInfo.Name = "gbBitCodeInfo";
            this.gbBitCodeInfo.Size = new System.Drawing.Size(466, 224);
            this.gbBitCodeInfo.TabIndex = 0;
            this.gbBitCodeInfo.TabStop = false;
            this.gbBitCodeInfo.Text = "Bit Code Info";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnClose.Location = new System.Drawing.Point(314, 155);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(140, 53);
            this.btnClose.TabIndex = 196;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.ForeColor = System.Drawing.Color.DarkBlue;
            this.btnSave.Location = new System.Drawing.Point(12, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 53);
            this.btnSave.TabIndex = 185;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtDescription
            // 
            this.txtDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescription.Location = new System.Drawing.Point(214, 105);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(240, 26);
            this.txtDescription.TabIndex = 184;
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 27);
            this.label1.TabIndex = 183;
            this.label1.Text = "Description:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbBitCode_8
            // 
            this.cbBitCode_8.AutoSize = true;
            this.cbBitCode_8.Location = new System.Drawing.Point(343, 72);
            this.cbBitCode_8.Name = "cbBitCode_8";
            this.cbBitCode_8.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_8.TabIndex = 182;
            this.cbBitCode_8.Text = "Bit Code 8";
            this.cbBitCode_8.UseVisualStyleBackColor = true;
            this.cbBitCode_8.CheckedChanged += new System.EventHandler(this.cbBitCode_8_CheckedChanged);
            this.cbBitCode_8.Click += new System.EventHandler(this.cbBitCode_1_Click);
            // 
            // cbBitCode_4
            // 
            this.cbBitCode_4.AutoSize = true;
            this.cbBitCode_4.Location = new System.Drawing.Point(227, 72);
            this.cbBitCode_4.Name = "cbBitCode_4";
            this.cbBitCode_4.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_4.TabIndex = 181;
            this.cbBitCode_4.Text = "Bit Code 4";
            this.cbBitCode_4.UseVisualStyleBackColor = true;
            this.cbBitCode_4.CheckedChanged += new System.EventHandler(this.cbBitCode_4_CheckedChanged);
            this.cbBitCode_4.Click += new System.EventHandler(this.cbBitCode_1_Click);
            // 
            // cbBitCode_2
            // 
            this.cbBitCode_2.AutoSize = true;
            this.cbBitCode_2.Location = new System.Drawing.Point(118, 72);
            this.cbBitCode_2.Name = "cbBitCode_2";
            this.cbBitCode_2.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_2.TabIndex = 180;
            this.cbBitCode_2.Text = "Bit Code 2";
            this.cbBitCode_2.UseVisualStyleBackColor = true;
            this.cbBitCode_2.CheckedChanged += new System.EventHandler(this.cbBitCode_2_CheckedChanged);
            this.cbBitCode_2.Click += new System.EventHandler(this.cbBitCode_1_Click);
            // 
            // cbBitCode_1
            // 
            this.cbBitCode_1.AutoSize = true;
            this.cbBitCode_1.Location = new System.Drawing.Point(12, 72);
            this.cbBitCode_1.Name = "cbBitCode_1";
            this.cbBitCode_1.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_1.TabIndex = 179;
            this.cbBitCode_1.Text = "Bit Code 1";
            this.cbBitCode_1.UseVisualStyleBackColor = true;
            this.cbBitCode_1.Click += new System.EventHandler(this.cbBitCode_1_Click);
            // 
            // txtNum
            // 
            this.txtNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNum.Location = new System.Drawing.Point(214, 25);
            this.txtNum.Name = "txtNum";
            this.txtNum.ReadOnly = true;
            this.txtNum.Size = new System.Drawing.Size(240, 26);
            this.txtNum.TabIndex = 177;
            // 
            // label26
            // 
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(12, 26);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(181, 27);
            this.label26.TabIndex = 173;
            this.label26.Text = "Bit Code Num:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmBitCodeInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 224);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmBitCodeInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmBitCodeInfo";
            this.panel1.ResumeLayout(false);
            this.gbBitCodeInfo.ResumeLayout(false);
            this.gbBitCodeInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbBitCodeInfo;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox txtNum;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbBitCode_8;
        private System.Windows.Forms.CheckBox cbBitCode_4;
        private System.Windows.Forms.CheckBox cbBitCode_2;
        private System.Windows.Forms.CheckBox cbBitCode_1;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
    }
}