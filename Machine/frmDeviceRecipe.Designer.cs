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
            this.cbMasterRecipe = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbRunHz_2nd = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDurationS_2nd = new System.Windows.Forms.Label();
            this.txtDurationM_2nd = new System.Windows.Forms.Label();
            this.txtDurationH_2nd = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDurationS_1st = new System.Windows.Forms.Label();
            this.txtDurationM_1st = new System.Windows.Forms.Label();
            this.txtDurationH_1st = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbBitCodes = new System.Windows.Forms.GroupBox();
            this.pnlBitCode = new System.Windows.Forms.Panel();
            this.cmbRunHz_1st = new System.Windows.Forms.ComboBox();
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
            this.label26.Text = "1. Device ID / Product No:";
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
            this.label30.Text = "2. 1st Running Hz:";
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
            this.label32.Text = "3. 1st Running Duration:";
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
            this.btn_SaveDeviceRecipe.Location = new System.Drawing.Point(367, 194);
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
            this.btn_Close.Location = new System.Drawing.Point(513, 194);
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
            this.grb_DeviceRecipe.Controls.Add(this.cbMasterRecipe);
            this.grb_DeviceRecipe.Controls.Add(this.label12);
            this.grb_DeviceRecipe.Controls.Add(this.cmbRunHz_2nd);
            this.grb_DeviceRecipe.Controls.Add(this.label11);
            this.grb_DeviceRecipe.Controls.Add(this.label1);
            this.grb_DeviceRecipe.Controls.Add(this.label2);
            this.grb_DeviceRecipe.Controls.Add(this.label3);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationS_2nd);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationM_2nd);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationH_2nd);
            this.grb_DeviceRecipe.Controls.Add(this.label10);
            this.grb_DeviceRecipe.Controls.Add(this.label8);
            this.grb_DeviceRecipe.Controls.Add(this.label7);
            this.grb_DeviceRecipe.Controls.Add(this.label6);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationS_1st);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationM_1st);
            this.grb_DeviceRecipe.Controls.Add(this.txtDurationH_1st);
            this.grb_DeviceRecipe.Controls.Add(this.panel1);
            this.grb_DeviceRecipe.Controls.Add(this.cmbRunHz_1st);
            this.grb_DeviceRecipe.Controls.Add(this.label26);
            this.grb_DeviceRecipe.Controls.Add(this.label30);
            this.grb_DeviceRecipe.Controls.Add(this.label32);
            this.grb_DeviceRecipe.Controls.Add(this.txt_DeviceID);
            this.grb_DeviceRecipe.Controls.Add(this.btn_SaveDeviceRecipe);
            this.grb_DeviceRecipe.Controls.Add(this.btn_Close);
            this.grb_DeviceRecipe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grb_DeviceRecipe.Location = new System.Drawing.Point(0, 0);
            this.grb_DeviceRecipe.Name = "grb_DeviceRecipe";
            this.grb_DeviceRecipe.Size = new System.Drawing.Size(671, 749);
            this.grb_DeviceRecipe.TabIndex = 291;
            this.grb_DeviceRecipe.TabStop = false;
            this.grb_DeviceRecipe.Text = "Device Recipe";
            // 
            // cbMasterRecipe
            // 
            this.cbMasterRecipe.AutoSize = true;
            this.cbMasterRecipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbMasterRecipe.Location = new System.Drawing.Point(272, 163);
            this.cbMasterRecipe.Name = "cbMasterRecipe";
            this.cbMasterRecipe.Size = new System.Drawing.Size(15, 14);
            this.cbMasterRecipe.TabIndex = 317;
            this.cbMasterRecipe.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(19, 157);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(223, 27);
            this.label12.TabIndex = 316;
            this.label12.Text = "6. Master Recipe:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbRunHz_2nd
            // 
            this.cmbRunHz_2nd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRunHz_2nd.FormattingEnabled = true;
            this.cmbRunHz_2nd.Location = new System.Drawing.Point(248, 105);
            this.cmbRunHz_2nd.Name = "cmbRunHz_2nd";
            this.cmbRunHz_2nd.Size = new System.Drawing.Size(134, 21);
            this.cmbRunHz_2nd.TabIndex = 315;
            this.cmbRunHz_2nd.SelectedIndexChanged += new System.EventHandler(this.cmbRunHz_2nd_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(19, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(223, 27);
            this.label11.TabIndex = 314;
            this.label11.Text = "4. 2nd Running Hz:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(528, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 16);
            this.label1.TabIndex = 313;
            this.label1.Text = "S";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.DarkBlue;
            this.label2.Location = new System.Drawing.Point(426, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 16);
            this.label2.TabIndex = 312;
            this.label2.Text = "M";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkBlue;
            this.label3.Location = new System.Drawing.Point(325, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(18, 16);
            this.label3.TabIndex = 311;
            this.label3.Text = "H";
            // 
            // txtDurationS_2nd
            // 
            this.txtDurationS_2nd.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationS_2nd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationS_2nd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationS_2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationS_2nd.ForeColor = System.Drawing.Color.Green;
            this.txtDurationS_2nd.Location = new System.Drawing.Point(451, 129);
            this.txtDurationS_2nd.Name = "txtDurationS_2nd";
            this.txtDurationS_2nd.Size = new System.Drawing.Size(71, 27);
            this.txtDurationS_2nd.TabIndex = 310;
            this.txtDurationS_2nd.Text = "00";
            this.txtDurationS_2nd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationS_2nd.Click += new System.EventHandler(this.txtDurationS_2nd_Click);
            // 
            // txtDurationM_2nd
            // 
            this.txtDurationM_2nd.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationM_2nd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationM_2nd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationM_2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationM_2nd.ForeColor = System.Drawing.Color.Green;
            this.txtDurationM_2nd.Location = new System.Drawing.Point(349, 129);
            this.txtDurationM_2nd.Name = "txtDurationM_2nd";
            this.txtDurationM_2nd.Size = new System.Drawing.Size(71, 27);
            this.txtDurationM_2nd.TabIndex = 309;
            this.txtDurationM_2nd.Text = "00";
            this.txtDurationM_2nd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationM_2nd.Click += new System.EventHandler(this.txtDurationM_2nd_Click);
            // 
            // txtDurationH_2nd
            // 
            this.txtDurationH_2nd.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationH_2nd.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationH_2nd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationH_2nd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationH_2nd.ForeColor = System.Drawing.Color.Green;
            this.txtDurationH_2nd.Location = new System.Drawing.Point(248, 129);
            this.txtDurationH_2nd.Name = "txtDurationH_2nd";
            this.txtDurationH_2nd.Size = new System.Drawing.Size(71, 27);
            this.txtDurationH_2nd.TabIndex = 308;
            this.txtDurationH_2nd.Text = "00";
            this.txtDurationH_2nd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationH_2nd.Click += new System.EventHandler(this.txtDurationH_2nd_Click);
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(19, 129);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(223, 27);
            this.label10.TabIndex = 307;
            this.label10.Text = "5. 2nd Running Duration:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            // txtDurationS_1st
            // 
            this.txtDurationS_1st.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationS_1st.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationS_1st.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationS_1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationS_1st.ForeColor = System.Drawing.Color.Green;
            this.txtDurationS_1st.Location = new System.Drawing.Point(451, 73);
            this.txtDurationS_1st.Name = "txtDurationS_1st";
            this.txtDurationS_1st.Size = new System.Drawing.Size(71, 27);
            this.txtDurationS_1st.TabIndex = 303;
            this.txtDurationS_1st.Text = "00";
            this.txtDurationS_1st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationS_1st.Click += new System.EventHandler(this.txtDurationS_1st_Click);
            // 
            // txtDurationM_1st
            // 
            this.txtDurationM_1st.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationM_1st.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationM_1st.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationM_1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationM_1st.ForeColor = System.Drawing.Color.Green;
            this.txtDurationM_1st.Location = new System.Drawing.Point(349, 73);
            this.txtDurationM_1st.Name = "txtDurationM_1st";
            this.txtDurationM_1st.Size = new System.Drawing.Size(71, 27);
            this.txtDurationM_1st.TabIndex = 302;
            this.txtDurationM_1st.Text = "00";
            this.txtDurationM_1st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationM_1st.Click += new System.EventHandler(this.txtDurationM_1st_Click);
            // 
            // txtDurationH_1st
            // 
            this.txtDurationH_1st.BackColor = System.Drawing.SystemColors.Window;
            this.txtDurationH_1st.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.txtDurationH_1st.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.txtDurationH_1st.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDurationH_1st.ForeColor = System.Drawing.Color.Green;
            this.txtDurationH_1st.Location = new System.Drawing.Point(248, 73);
            this.txtDurationH_1st.Name = "txtDurationH_1st";
            this.txtDurationH_1st.Size = new System.Drawing.Size(71, 27);
            this.txtDurationH_1st.TabIndex = 301;
            this.txtDurationH_1st.Text = "00";
            this.txtDurationH_1st.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.txtDurationH_1st.Click += new System.EventHandler(this.txtDurationH_1st_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbBitCodes);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 253);
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
            this.gbBitCodes.Text = "Hz Code Assignments";
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
            // cmbRunHz_1st
            // 
            this.cmbRunHz_1st.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRunHz_1st.FormattingEnabled = true;
            this.cmbRunHz_1st.Location = new System.Drawing.Point(248, 49);
            this.cmbRunHz_1st.Name = "cmbRunHz_1st";
            this.cmbRunHz_1st.Size = new System.Drawing.Size(134, 21);
            this.cmbRunHz_1st.TabIndex = 293;
            this.cmbRunHz_1st.SelectedIndexChanged += new System.EventHandler(this.cmbRunHz_1st_SelectedIndexChanged);
            // 
            // frmDeviceRecipe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 749);
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
        private System.Windows.Forms.ComboBox cmbRunHz_1st;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbBitCodes;
        private System.Windows.Forms.Panel pnlBitCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label txtDurationS_1st;
        private System.Windows.Forms.Label txtDurationM_1st;
        private System.Windows.Forms.Label txtDurationH_1st;
        private System.Windows.Forms.CheckBox cbMasterRecipe;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbRunHz_2nd;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtDurationS_2nd;
        private System.Windows.Forms.Label txtDurationM_2nd;
        private System.Windows.Forms.Label txtDurationH_2nd;
        private System.Windows.Forms.Label label10;
    }
}