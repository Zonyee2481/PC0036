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
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txt_DeviceID = new System.Windows.Forms.TextBox();
            this.btn_SaveDeviceRecipe = new System.Windows.Forms.Button();
            this.btn_Close = new System.Windows.Forms.Button();
            this.tmr_UpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.grb_DeviceRecipe = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDuration_2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_Counter = new System.Windows.Forms.TextBox();
            this.cmbAssignCode = new System.Windows.Forms.ComboBox();
            this.txt_Duration = new System.Windows.Forms.Label();
            this.gbBitCodes = new System.Windows.Forms.GroupBox();
            this.pnlBitCode = new System.Windows.Forms.Panel();
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
            // label31
            // 
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(19, 127);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(223, 27);
            this.label31.TabIndex = 174;
            this.label31.Text = "5. Counter:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btn_SaveDeviceRecipe.Location = new System.Drawing.Point(367, 172);
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
            this.btn_Close.Location = new System.Drawing.Point(513, 172);
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
            this.grb_DeviceRecipe.Controls.Add(this.panel1);
            this.grb_DeviceRecipe.Controls.Add(this.txtDuration_2);
            this.grb_DeviceRecipe.Controls.Add(this.label3);
            this.grb_DeviceRecipe.Controls.Add(this.btn_Reset);
            this.grb_DeviceRecipe.Controls.Add(this.label2);
            this.grb_DeviceRecipe.Controls.Add(this.txt_Counter);
            this.grb_DeviceRecipe.Controls.Add(this.cmbAssignCode);
            this.grb_DeviceRecipe.Controls.Add(this.txt_Duration);
            this.grb_DeviceRecipe.Controls.Add(this.label26);
            this.grb_DeviceRecipe.Controls.Add(this.label30);
            this.grb_DeviceRecipe.Controls.Add(this.label31);
            this.grb_DeviceRecipe.Controls.Add(this.label32);
            this.grb_DeviceRecipe.Controls.Add(this.txt_DeviceID);
            this.grb_DeviceRecipe.Controls.Add(this.btn_SaveDeviceRecipe);
            this.grb_DeviceRecipe.Controls.Add(this.btn_Close);
            this.grb_DeviceRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_DeviceRecipe.Location = new System.Drawing.Point(0, 0);
            this.grb_DeviceRecipe.Name = "grb_DeviceRecipe";
            this.grb_DeviceRecipe.Size = new System.Drawing.Size(671, 727);
            this.grb_DeviceRecipe.TabIndex = 291;
            this.grb_DeviceRecipe.TabStop = false;
            this.grb_DeviceRecipe.Text = "Device Recipe";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbBitCodes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 231);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(665, 493);
            this.panel1.TabIndex = 300;
            // 
            // txtDuration_2
            // 
            this.txtDuration_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDuration_2.Location = new System.Drawing.Point(248, 101);
            this.txtDuration_2.Name = "txtDuration_2";
            this.txtDuration_2.ReadOnly = true;
            this.txtDuration_2.Size = new System.Drawing.Size(134, 26);
            this.txtDuration_2.TabIndex = 299;
            this.txtDuration_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 27);
            this.label3.TabIndex = 297;
            this.label3.Text = "4. Duration (HH mm ss):";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btn_Reset
            // 
            this.btn_Reset.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Reset.Location = new System.Drawing.Point(388, 131);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(75, 23);
            this.btn_Reset.TabIndex = 296;
            this.btn_Reset.Text = "Reset";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(388, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 16);
            this.label2.TabIndex = 295;
            this.label2.Text = "hour";
            // 
            // txt_Counter
            // 
            this.txt_Counter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Counter.Location = new System.Drawing.Point(248, 128);
            this.txt_Counter.Name = "txt_Counter";
            this.txt_Counter.ReadOnly = true;
            this.txt_Counter.Size = new System.Drawing.Size(134, 26);
            this.txt_Counter.TabIndex = 294;
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
            // txt_Duration
            // 
            this.txt_Duration.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Duration.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txt_Duration.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txt_Duration.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Duration.ForeColor = System.Drawing.Color.Green;
            this.txt_Duration.Location = new System.Drawing.Point(248, 73);
            this.txt_Duration.Name = "txt_Duration";
            this.txt_Duration.Size = new System.Drawing.Size(134, 27);
            this.txt_Duration.TabIndex = 289;
            this.txt_Duration.Text = "0";
            this.txt_Duration.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txt_Duration.Click += new System.EventHandler(this.txt_Duration_Click);
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
            // frmDeviceRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 727);
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
    private System.Windows.Forms.Label label31;
    private System.Windows.Forms.Label label32;
    private System.Windows.Forms.TextBox txt_DeviceID;
    private System.Windows.Forms.Button btn_SaveDeviceRecipe;
    private System.Windows.Forms.Button btn_Close;
    private System.Windows.Forms.Timer tmr_UpdateDisplay;
    private System.Windows.Forms.GroupBox grb_DeviceRecipe;
    private System.Windows.Forms.Label txt_Duration;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Counter;
        private System.Windows.Forms.ComboBox cmbAssignCode;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.TextBox txtDuration_2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbBitCodes;
        private System.Windows.Forms.Panel pnlBitCode;
    }
}