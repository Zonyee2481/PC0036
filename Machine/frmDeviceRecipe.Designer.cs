namespace Machine
{
  partial class frmDeviceRecipe
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
            this.components = new System.ComponentModel.Container();
            this.label26 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_DeviceID = new System.Windows.Forms.TextBox();
            this.btn_SaveDeviceRecipe = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tmr_UpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.grb_DeviceRecipe = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDurationS = new System.Windows.Forms.Label();
            this.txtDurationM = new System.Windows.Forms.Label();
            this.txtDurationH = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbBitCodes = new System.Windows.Forms.GroupBox();
            this.pnlBitCode = new System.Windows.Forms.Panel();
            this.cmbAssignCode = new System.Windows.Forms.ComboBox();
            this.grb_DeviceRecipe.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbBitCodes.SuspendLayout();
            this.SuspendLayout();
            // 
            // label26
            // 
            this.label26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label26.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(19, 16);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(223, 27);
            this.label26.TabIndex = 172;
            this.label26.Text = "1. Device ID / Product Num:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label30
            // 
            this.label30.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.Location = new System.Drawing.Point(19, 45);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(223, 27);
            this.label30.TabIndex = 173;
            this.label30.Text = "2. Assigned Code:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label32
            // 
            this.label32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label32.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(19, 73);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(223, 27);
            this.label32.TabIndex = 175;
            this.label32.Text = "3. Duration:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_DeviceID
            // 
            this.txt_DeviceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_DeviceID.Location = new System.Drawing.Point(248, 17);
            this.txt_DeviceID.Name = "txt_DeviceID";
            this.txt_DeviceID.Size = new System.Drawing.Size(405, 26);
            this.txt_DeviceID.TabIndex = 176;
            // 
            // btn_SaveDeviceRecipe
            // 
            this.btn_SaveDeviceRecipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SaveDeviceRecipe.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_SaveDeviceRecipe.Location = new System.Drawing.Point(367, 126);
            this.btn_SaveDeviceRecipe.Name = "btn_SaveDeviceRecipe";
            this.btn_SaveDeviceRecipe.Size = new System.Drawing.Size(140, 53);
            this.btn_SaveDeviceRecipe.TabIndex = 180;
            this.btn_SaveDeviceRecipe.Text = "Save";
            this.btn_SaveDeviceRecipe.UseVisualStyleBackColor = true;
            this.btn_SaveDeviceRecipe.Click += new System.EventHandler(this.btn_SaveDeviceRecipe_Click);
            // 
            // btn_Close
            // 
            this.btn_Close.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Close.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Close.Location = new System.Drawing.Point(513, 126);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(140, 53);
            this.btn_Close.TabIndex = 195;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // tmr_UpdateDisplay
            // 
            this.tmr_UpdateDisplay.Tick += new System.EventHandler(this.tmr_UpdateDisplay_Tick);
            // 
            // grb_DeviceRecipe
            // 
            this.grb_DeviceRecipe.Controls.Add(this.label8);
            this.grb_DeviceRecipe.Controls.Add(this.label7);
            this.grb_DeviceRecipe.Controls.Add(this.label6);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationS);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationM);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationH);
            this.grb_DeviceRecipe.Controls.Add(this.panel1);
            this.grb_DeviceRecipe.Controls.Add(this.cmbAssignCode);
            this.grb_DeviceRecipe.Controls.Add(this.label26);
            this.grb_DeviceRecipe.Controls.Add(this.label30);
            this.grb_DeviceRecipe.Controls.Add(this.label32);
            this.grb_DeviceRecipe.Controls.Add(this.txt_DeviceID);
            this.grb_DeviceRecipe.Controls.Add(this.btn_SaveDeviceRecipe);
            this.grb_DeviceRecipe.Controls.Add(this.btn_Close);
            this.grb_DeviceRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_DeviceRecipe.Location = new System.Drawing.Point(0, 0);
            this.grb_DeviceRecipe.Name = "grb_DeviceRecipe";
            this.grb_DeviceRecipe.Size = new System.Drawing.Size(671, 688);
            this.grb_DeviceRecipe.TabIndex = 291;
            this.grb_DeviceRecipe.TabStop = false;
            this.grb_DeviceRecipe.Text = "Device Recipe";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.DarkBlue;
            this.label8.Location = new System.Drawing.Point(528, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 16);
            this.label8.TabIndex = 306;
            this.label8.Text = "S";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.DarkBlue;
            this.label7.Location = new System.Drawing.Point(426, 78);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 16);
            this.label7.TabIndex = 305;
            this.label7.Text = "M";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.DarkBlue;
            this.label6.Location = new System.Drawing.Point(325, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 16);
            this.label6.TabIndex = 304;
            this.label6.Text = "H";
            // 
            // txtDurationS
            // 
            this.txtDurationS.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationS.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationS.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationS.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationS.ForeColor = System.Drawing.Color.Green;
            this.txtDurationS.Location = new System.Drawing.Point(451, 73);
            this.txtDurationS.Name = "txtDurationS";
            this.txtDurationS.Size = new System.Drawing.Size(71, 27);
            this.txtDurationS.TabIndex = 303;
            this.txtDurationS.Text = "00";
            this.txtDurationS.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationS.Click += new System.EventHandler(this.txtDurationS_Click);
            // 
            // txtDurationM
            // 
            this.txtDurationM.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationM.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationM.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationM.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationM.ForeColor = System.Drawing.Color.Green;
            this.txtDurationM.Location = new System.Drawing.Point(349, 73);
            this.txtDurationM.Name = "txtDurationM";
            this.txtDurationM.Size = new System.Drawing.Size(71, 27);
            this.txtDurationM.TabIndex = 302;
            this.txtDurationM.Text = "00";
            this.txtDurationM.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationM.Click += new System.EventHandler(this.txtDurationM_Click);
            // 
            // txtDurationH
            // 
            this.txtDurationH.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationH.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationH.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationH.ForeColor = System.Drawing.Color.Green;
            this.txtDurationH.Location = new System.Drawing.Point(248, 73);
            this.txtDurationH.Name = "txtDurationH";
            this.txtDurationH.Size = new System.Drawing.Size(71, 27);
            this.txtDurationH.TabIndex = 301;
            this.txtDurationH.Text = "00";
            this.txtDurationH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationH.Click += new System.EventHandler(this.txtDurationH_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbBitCodes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 192);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 493);
            this.panel1.TabIndex = 300;
            // 
            // gbBitCodes
            // 
            this.gbBitCodes.Controls.Add(this.pnlBitCode);
            this.gbBitCodes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbBitCodes.Location = new System.Drawing.Point(0, 0);
            this.gbBitCodes.Name = "gbBitCodes";
            this.gbBitCodes.Size = new System.Drawing.Size(665, 493);
            this.gbBitCodes.TabIndex = 0;
            this.gbBitCodes.TabStop = false;
            this.gbBitCodes.Text = "Bit Code";
            // 
            // pnlBitCode
            // 
            this.pnlBitCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBitCode.Enabled = false;
            this.pnlBitCode.Location = new System.Drawing.Point(3, 16);
            this.pnlBitCode.Name = "pnlBitCode";
            this.pnlBitCode.Size = new System.Drawing.Size(659, 474);
            this.pnlBitCode.TabIndex = 0;
            // 
            // cmbAssignCode
            // 
            this.cmbAssignCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAssignCode.FormattingEnabled = true;
            this.cmbAssignCode.Location = new System.Drawing.Point(248, 49);
            this.cmbAssignCode.Name = "cmbAssignCode";
            this.cmbAssignCode.Size = new System.Drawing.Size(134, 21);
            this.cmbAssignCode.TabIndex = 293;
            this.cmbAssignCode.SelectedIndexChanged += new System.EventHandler(this.cmbAssignCode_SelectedIndexChanged);
            // 
            // frmDeviceRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 688);
            this.Controls.Add(this.grb_DeviceRecipe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "frmDeviceRecipe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDeviceRecipe";
            this.Load += new System.EventHandler(this.frmDeviceRecipe_Load);
            this.grb_DeviceRecipe.ResumeLayout(false);
            this.grb_DeviceRecipe.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbBitCodes.ResumeLayout(false);
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label label26;
    private System.Windows.Forms.Label label30;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.TextBox txt_DeviceID;
    private System.Windows.Forms.Button btn_SaveDeviceRecipe;
    private System.Windows.Forms.Button btn_Close;
    private System.Windows.Forms.Timer tmr_UpdateDisplay;
    private System.Windows.Forms.GroupBox grb_DeviceRecipe;
        private System.Windows.Forms.ComboBox cmbAssignCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbBitCodes;
        private System.Windows.Forms.Panel pnlBitCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label txtDurationS;
        private System.Windows.Forms.Label txtDurationM;
        private System.Windows.Forms.Label txtDurationH;
    }
}