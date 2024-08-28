namespace Machine
{
  partial class uctrlAccessConfig
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            this.btn_SaveConfig = new System.Windows.Forms.Button();
            this.ckb_EnabAccessConfig = new System.Windows.Forms.CheckBox();
            this.grb_AccessPage = new System.Windows.Forms.GroupBox();
            this.ckb_EnabDeviceRecipe = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.ckb_EnabHistoryPage = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.ckb_EnabSetupPage = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.ckb_EnabDiagnostic = new System.Windows.Forms.CheckBox();
            this.cmb_LevelAccessPage = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ckb_EnabAutoPage = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btn_Edit = new System.Windows.Forms.Button();
            this.tmr_UpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.btn_CancelAddUser = new System.Windows.Forms.Button();
            this.btn_AddUser = new System.Windows.Forms.Button();
            this.btn_Delete = new System.Windows.Forms.Button();
            this.btn_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_Password = new System.Windows.Forms.TextBox();
            this.bgw_WagoModbusCom = new System.ComponentModel.BackgroundWorker();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_BadgeNum = new System.Windows.Forms.TextBox();
            this.txt_UserName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.cmb_Level = new System.Windows.Forms.ComboBox();
            this.pnl_User = new System.Windows.Forms.Panel();
            this.lv_UserData = new System.Windows.Forms.ListView();
            this.label13 = new System.Windows.Forms.Label();
            this.lbl_IdleTimeAutoLogout = new System.Windows.Forms.Label();
            this.grb_AccessPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnl_User.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_SaveConfig
            // 
            this.btn_SaveConfig.Location = new System.Drawing.Point(15, 276);
            this.btn_SaveConfig.Name = "btn_SaveConfig";
            this.btn_SaveConfig.Size = new System.Drawing.Size(200, 39);
            this.btn_SaveConfig.TabIndex = 31;
            this.btn_SaveConfig.Text = "Save";
            this.btn_SaveConfig.UseVisualStyleBackColor = true;
            this.btn_SaveConfig.Click += new System.EventHandler(this.btn_SaveConfig_Click);
            // 
            // ckb_EnabAccessConfig
            // 
            this.ckb_EnabAccessConfig.AutoSize = true;
            this.ckb_EnabAccessConfig.Location = new System.Drawing.Point(221, 231);
            this.ckb_EnabAccessConfig.Name = "ckb_EnabAccessConfig";
            this.ckb_EnabAccessConfig.Size = new System.Drawing.Size(59, 17);
            this.ckb_EnabAccessConfig.TabIndex = 26;
            this.ckb_EnabAccessConfig.Text = "Enable";
            this.ckb_EnabAccessConfig.UseVisualStyleBackColor = true;
            // 
            // grb_AccessPage
            // 
            this.grb_AccessPage.Controls.Add(this.ckb_EnabDeviceRecipe);
            this.grb_AccessPage.Controls.Add(this.label12);
            this.grb_AccessPage.Controls.Add(this.ckb_EnabHistoryPage);
            this.grb_AccessPage.Controls.Add(this.label16);
            this.grb_AccessPage.Controls.Add(this.ckb_EnabSetupPage);
            this.grb_AccessPage.Controls.Add(this.label10);
            this.grb_AccessPage.Controls.Add(this.btn_SaveConfig);
            this.grb_AccessPage.Controls.Add(this.ckb_EnabAccessConfig);
            this.grb_AccessPage.Controls.Add(this.ckb_EnabDiagnostic);
            this.grb_AccessPage.Controls.Add(this.cmb_LevelAccessPage);
            this.grb_AccessPage.Controls.Add(this.label6);
            this.grb_AccessPage.Controls.Add(this.label7);
            this.grb_AccessPage.Enabled = false;
            this.grb_AccessPage.Location = new System.Drawing.Point(401, 329);
            this.grb_AccessPage.Name = "grb_AccessPage";
            this.grb_AccessPage.Size = new System.Drawing.Size(318, 338);
            this.grb_AccessPage.TabIndex = 41;
            this.grb_AccessPage.TabStop = false;
            this.grb_AccessPage.Text = "Access Page Setup";
            // 
            // ckb_EnabDeviceRecipe
            // 
            this.ckb_EnabDeviceRecipe.AutoSize = true;
            this.ckb_EnabDeviceRecipe.Location = new System.Drawing.Point(221, 96);
            this.ckb_EnabDeviceRecipe.Name = "ckb_EnabDeviceRecipe";
            this.ckb_EnabDeviceRecipe.Size = new System.Drawing.Size(59, 17);
            this.ckb_EnabDeviceRecipe.TabIndex = 45;
            this.ckb_EnabDeviceRecipe.Text = "Enable";
            this.ckb_EnabDeviceRecipe.UseVisualStyleBackColor = true;
            // 
            // label12
            // 
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(15, 89);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(200, 26);
            this.label12.TabIndex = 44;
            this.label12.Text = "Device Recipe Page:";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckb_EnabHistoryPage
            // 
            this.ckb_EnabHistoryPage.AutoSize = true;
            this.ckb_EnabHistoryPage.Location = new System.Drawing.Point(221, 432);
            this.ckb_EnabHistoryPage.Name = "ckb_EnabHistoryPage";
            this.ckb_EnabHistoryPage.Size = new System.Drawing.Size(59, 17);
            this.ckb_EnabHistoryPage.TabIndex = 43;
            this.ckb_EnabHistoryPage.Text = "Enable";
            this.ckb_EnabHistoryPage.UseVisualStyleBackColor = true;
            this.ckb_EnabHistoryPage.Visible = false;
            // 
            // label16
            // 
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(15, 426);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(200, 26);
            this.label16.TabIndex = 42;
            this.label16.Text = "History Page:";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label16.Visible = false;
            // 
            // ckb_EnabSetupPage
            // 
            this.ckb_EnabSetupPage.AutoSize = true;
            this.ckb_EnabSetupPage.Location = new System.Drawing.Point(221, 69);
            this.ckb_EnabSetupPage.Name = "ckb_EnabSetupPage";
            this.ckb_EnabSetupPage.Size = new System.Drawing.Size(59, 17);
            this.ckb_EnabSetupPage.TabIndex = 33;
            this.ckb_EnabSetupPage.Text = "Enable";
            this.ckb_EnabSetupPage.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(15, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(200, 26);
            this.label10.TabIndex = 32;
            this.label10.Text = "Setup Page:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckb_EnabDiagnostic
            // 
            this.ckb_EnabDiagnostic.AutoSize = true;
            this.ckb_EnabDiagnostic.Location = new System.Drawing.Point(221, 177);
            this.ckb_EnabDiagnostic.Name = "ckb_EnabDiagnostic";
            this.ckb_EnabDiagnostic.Size = new System.Drawing.Size(59, 17);
            this.ckb_EnabDiagnostic.TabIndex = 25;
            this.ckb_EnabDiagnostic.Text = "Enable";
            this.ckb_EnabDiagnostic.UseVisualStyleBackColor = true;
            // 
            // cmb_LevelAccessPage
            // 
            this.cmb_LevelAccessPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_LevelAccessPage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_LevelAccessPage.FormattingEnabled = true;
            this.cmb_LevelAccessPage.Items.AddRange(new object[] {
            "Operator",
            "Technician",
            "Engineer",
            "Administrator"});
            this.cmb_LevelAccessPage.Location = new System.Drawing.Point(15, 19);
            this.cmb_LevelAccessPage.Name = "cmb_LevelAccessPage";
            this.cmb_LevelAccessPage.Size = new System.Drawing.Size(200, 24);
            this.cmb_LevelAccessPage.TabIndex = 24;
            this.cmb_LevelAccessPage.SelectedIndexChanged += new System.EventHandler(this.cmb_LevelAccessPage_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(200, 26);
            this.label6.TabIndex = 16;
            this.label6.Text = "Diagnostic Page:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(200, 26);
            this.label7.TabIndex = 17;
            this.label7.Text = "Access Config Page:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ckb_EnabAutoPage
            // 
            this.ckb_EnabAutoPage.AutoSize = true;
            this.ckb_EnabAutoPage.Location = new System.Drawing.Point(622, 693);
            this.ckb_EnabAutoPage.Name = "ckb_EnabAutoPage";
            this.ckb_EnabAutoPage.Size = new System.Drawing.Size(59, 17);
            this.ckb_EnabAutoPage.TabIndex = 41;
            this.ckb_EnabAutoPage.Text = "Enable";
            this.ckb_EnabAutoPage.UseVisualStyleBackColor = true;
            this.ckb_EnabAutoPage.Visible = false;
            // 
            // label15
            // 
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(416, 686);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(200, 26);
            this.label15.TabIndex = 40;
            this.label15.Text = "Auto Page:";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label15.Visible = false;
            // 
            // btn_Edit
            // 
            this.btn_Edit.Enabled = false;
            this.btn_Edit.Location = new System.Drawing.Point(543, 194);
            this.btn_Edit.Name = "btn_Edit";
            this.btn_Edit.Size = new System.Drawing.Size(176, 39);
            this.btn_Edit.TabIndex = 40;
            this.btn_Edit.Text = "Edit";
            this.btn_Edit.UseVisualStyleBackColor = true;
            this.btn_Edit.Click += new System.EventHandler(this.btn_Edit_Click);
            // 
            // tmr_UpdateDisplay
            // 
            this.tmr_UpdateDisplay.Enabled = true;
            this.tmr_UpdateDisplay.Interval = 80;
            // 
            // btn_CancelAddUser
            // 
            this.btn_CancelAddUser.Enabled = false;
            this.btn_CancelAddUser.Location = new System.Drawing.Point(401, 284);
            this.btn_CancelAddUser.Name = "btn_CancelAddUser";
            this.btn_CancelAddUser.Size = new System.Drawing.Size(136, 39);
            this.btn_CancelAddUser.TabIndex = 38;
            this.btn_CancelAddUser.Text = "Cancel Add User";
            this.btn_CancelAddUser.UseVisualStyleBackColor = true;
            this.btn_CancelAddUser.Click += new System.EventHandler(this.btn_CancelAddUser_Click);
            // 
            // btn_AddUser
            // 
            this.btn_AddUser.Location = new System.Drawing.Point(401, 239);
            this.btn_AddUser.Name = "btn_AddUser";
            this.btn_AddUser.Size = new System.Drawing.Size(136, 39);
            this.btn_AddUser.TabIndex = 37;
            this.btn_AddUser.Text = "Add User";
            this.btn_AddUser.UseVisualStyleBackColor = true;
            this.btn_AddUser.Click += new System.EventHandler(this.btn_AddUser_Click);
            // 
            // btn_Delete
            // 
            this.btn_Delete.Enabled = false;
            this.btn_Delete.Location = new System.Drawing.Point(543, 284);
            this.btn_Delete.Name = "btn_Delete";
            this.btn_Delete.Size = new System.Drawing.Size(176, 38);
            this.btn_Delete.TabIndex = 36;
            this.btn_Delete.Text = "Delete";
            this.btn_Delete.UseVisualStyleBackColor = true;
            this.btn_Delete.Click += new System.EventHandler(this.btn_Delete_Click);
            // 
            // btn_Save
            // 
            this.btn_Save.Enabled = false;
            this.btn_Save.Location = new System.Drawing.Point(543, 239);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(176, 39);
            this.btn_Save.TabIndex = 35;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(735, 51);
            this.label1.TabIndex = 0;
            this.label1.Text = "Access Config";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Password
            // 
            this.txt_Password.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Password.Location = new System.Drawing.Point(146, 109);
            this.txt_Password.Name = "txt_Password";
            this.txt_Password.Size = new System.Drawing.Size(176, 22);
            this.txt_Password.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(136, 26);
            this.label2.TabIndex = 16;
            this.label2.Text = "Badge Number:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 26);
            this.label3.TabIndex = 17;
            this.label3.Text = "User Name:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(4, 105);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 26);
            this.label5.TabIndex = 33;
            this.label5.Text = "Login Password";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_BadgeNum
            // 
            this.txt_BadgeNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_BadgeNum.Location = new System.Drawing.Point(146, 19);
            this.txt_BadgeNum.Name = "txt_BadgeNum";
            this.txt_BadgeNum.Size = new System.Drawing.Size(176, 22);
            this.txt_BadgeNum.TabIndex = 20;
            // 
            // txt_UserName
            // 
            this.txt_UserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_UserName.Location = new System.Drawing.Point(146, 49);
            this.txt_UserName.Name = "txt_UserName";
            this.txt_UserName.Size = new System.Drawing.Size(176, 22);
            this.txt_UserName.TabIndex = 21;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(739, 55);
            this.panel1.TabIndex = 31;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(4, 75);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 26);
            this.label4.TabIndex = 18;
            this.label4.Text = "Level:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmb_Level
            // 
            this.cmb_Level.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Level.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_Level.FormattingEnabled = true;
            this.cmb_Level.Items.AddRange(new object[] {
            "Operator",
            "Technician",
            "Engineer",
            "Administrator"});
            this.cmb_Level.Location = new System.Drawing.Point(146, 78);
            this.cmb_Level.Name = "cmb_Level";
            this.cmb_Level.Size = new System.Drawing.Size(176, 24);
            this.cmb_Level.TabIndex = 23;
            // 
            // pnl_User
            // 
            this.pnl_User.Controls.Add(this.label2);
            this.pnl_User.Controls.Add(this.label3);
            this.pnl_User.Controls.Add(this.label4);
            this.pnl_User.Controls.Add(this.txt_BadgeNum);
            this.pnl_User.Controls.Add(this.txt_UserName);
            this.pnl_User.Controls.Add(this.cmb_Level);
            this.pnl_User.Controls.Add(this.txt_Password);
            this.pnl_User.Controls.Add(this.label5);
            this.pnl_User.Enabled = false;
            this.pnl_User.Location = new System.Drawing.Point(397, 45);
            this.pnl_User.Name = "pnl_User";
            this.pnl_User.Size = new System.Drawing.Size(334, 143);
            this.pnl_User.TabIndex = 39;
            // 
            // lv_UserData
            // 
            this.lv_UserData.FullRowSelect = true;
            this.lv_UserData.GridLines = true;
            this.lv_UserData.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lv_UserData.HideSelection = false;
            this.lv_UserData.Location = new System.Drawing.Point(6, 64);
            this.lv_UserData.MultiSelect = false;
            this.lv_UserData.Name = "lv_UserData";
            this.lv_UserData.Size = new System.Drawing.Size(385, 735);
            this.lv_UserData.TabIndex = 42;
            this.lv_UserData.UseCompatibleStateImageBehavior = false;
            this.lv_UserData.View = System.Windows.Forms.View.Details;
            this.lv_UserData.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lv_UserData_MouseClick_1);
            this.lv_UserData.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lv_UserData_MouseUp_1);
            // 
            // label13
            // 
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(401, 191);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(229, 26);
            this.label13.TabIndex = 35;
            this.label13.Text = "Auto Logout After Idle Time (m):";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label13.Visible = false;
            // 
            // lbl_IdleTimeAutoLogout
            // 
            this.lbl_IdleTimeAutoLogout.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_IdleTimeAutoLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_IdleTimeAutoLogout.ForeColor = System.Drawing.Color.Green;
            this.lbl_IdleTimeAutoLogout.Location = new System.Drawing.Point(636, 191);
            this.lbl_IdleTimeAutoLogout.Name = "lbl_IdleTimeAutoLogout";
            this.lbl_IdleTimeAutoLogout.Size = new System.Drawing.Size(83, 26);
            this.lbl_IdleTimeAutoLogout.TabIndex = 43;
            this.lbl_IdleTimeAutoLogout.Text = "0";
            this.lbl_IdleTimeAutoLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_IdleTimeAutoLogout.Visible = false;
            this.lbl_IdleTimeAutoLogout.Click += new System.EventHandler(this.lbl_IdleTimeAutoLogout_Click);
            // 
            // uctrlAccessConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lv_UserData);
            this.Controls.Add(this.grb_AccessPage);
            this.Controls.Add(this.btn_Edit);
            this.Controls.Add(this.btn_CancelAddUser);
            this.Controls.Add(this.btn_AddUser);
            this.Controls.Add(this.btn_Delete);
            this.Controls.Add(this.ckb_EnabAutoPage);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnl_User);
            this.Controls.Add(this.lbl_IdleTimeAutoLogout);
            this.Controls.Add(this.label13);
            this.ForeColor = System.Drawing.Color.DarkBlue;
            this.Name = "uctrlAccessConfig";
            this.Size = new System.Drawing.Size(739, 818);
            this.Load += new System.EventHandler(this.uctrlAccessConfig_Load);
            this.grb_AccessPage.ResumeLayout(false);
            this.grb_AccessPage.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.pnl_User.ResumeLayout(false);
            this.pnl_User.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion
    private System.Windows.Forms.Button btn_SaveConfig;
    private System.Windows.Forms.CheckBox ckb_EnabAccessConfig;
    private System.Windows.Forms.GroupBox grb_AccessPage;
    private System.Windows.Forms.CheckBox ckb_EnabDiagnostic;
    private System.Windows.Forms.ComboBox cmb_LevelAccessPage;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Button btn_Edit;
    private System.Windows.Forms.Timer tmr_UpdateDisplay;
    private System.Windows.Forms.Button btn_CancelAddUser;
    private System.Windows.Forms.Button btn_AddUser;
    private System.Windows.Forms.Button btn_Delete;
    private System.Windows.Forms.Button btn_Save;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox txt_Password;
    private System.ComponentModel.BackgroundWorker bgw_WagoModbusCom;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.TextBox txt_BadgeNum;
    private System.Windows.Forms.TextBox txt_UserName;
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.ComboBox cmb_Level;
    private System.Windows.Forms.Panel pnl_User;
    private System.Windows.Forms.ListView lv_UserData;
    private System.Windows.Forms.CheckBox ckb_EnabSetupPage;
    private System.Windows.Forms.Label label10;
    private System.Windows.Forms.Label label13;
    private System.Windows.Forms.Label lbl_IdleTimeAutoLogout;
    private System.Windows.Forms.CheckBox ckb_EnabAutoPage;
    private System.Windows.Forms.Label label15;
    private System.Windows.Forms.CheckBox ckb_EnabHistoryPage;
    private System.Windows.Forms.Label label16;
    private System.Windows.Forms.CheckBox ckb_EnabDeviceRecipe;
    private System.Windows.Forms.Label label12;
  }
}
