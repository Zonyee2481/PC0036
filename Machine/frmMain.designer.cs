namespace Machine
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnl_Form = new System.Windows.Forms.Panel();
            this.pnl_Control = new System.Windows.Forms.Panel();
            this.btn_DeviceRecipe = new System.Windows.Forms.Button();
            this.btn_AccessConfig = new System.Windows.Forms.Button();
            this.lbl_Version = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_CloseAll = new System.Windows.Forms.Button();
            this.btn_Setup = new System.Windows.Forms.Button();
            this.btn_frmIODiag = new System.Windows.Forms.Button();
            this.btn_Auto = new System.Windows.Forms.Button();
            this.btn_Main = new System.Windows.Forms.Button();
            this.btn_SlideIn = new System.Windows.Forms.Button();
            this.btn_SlideOut = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Welcome = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lbl_Recipe = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.tmr_slider = new System.Windows.Forms.Timer(this.components);
            this.tmr_Main = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.pnl_Control.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gainsboro;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel2.Controls.Add(this.pnl_Form);
            this.panel2.Controls.Add(this.pnl_Control);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1272, 920);
            this.panel2.TabIndex = 1;
            // 
            // pnl_Form
            // 
            this.pnl_Form.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_Form.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnl_Form.ForeColor = System.Drawing.Color.DarkBlue;
            this.pnl_Form.Location = new System.Drawing.Point(187, 98);
            this.pnl_Form.Name = "pnl_Form";
            this.pnl_Form.Size = new System.Drawing.Size(1085, 822);
            this.pnl_Form.TabIndex = 3;
            // 
            // pnl_Control
            // 
            this.pnl_Control.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.pnl_Control.Controls.Add(this.btn_DeviceRecipe);
            this.pnl_Control.Controls.Add(this.btn_AccessConfig);
            this.pnl_Control.Controls.Add(this.lbl_Version);
            this.pnl_Control.Controls.Add(this.panel6);
            this.pnl_Control.Controls.Add(this.btn_Setup);
            this.pnl_Control.Controls.Add(this.btn_frmIODiag);
            this.pnl_Control.Controls.Add(this.btn_Auto);
            this.pnl_Control.Controls.Add(this.btn_Main);
            this.pnl_Control.Controls.Add(this.btn_SlideIn);
            this.pnl_Control.Controls.Add(this.btn_SlideOut);
            this.pnl_Control.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnl_Control.Location = new System.Drawing.Point(0, 98);
            this.pnl_Control.Name = "pnl_Control";
            this.pnl_Control.Size = new System.Drawing.Size(187, 822);
            this.pnl_Control.TabIndex = 1;
            this.pnl_Control.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // btn_DeviceRecipe
            // 
            this.btn_DeviceRecipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_DeviceRecipe.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_DeviceRecipe.BackgroundImage")));
            this.btn_DeviceRecipe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_DeviceRecipe.FlatAppearance.BorderSize = 0;
            this.btn_DeviceRecipe.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_DeviceRecipe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DeviceRecipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_DeviceRecipe.ForeColor = System.Drawing.Color.White;
            this.btn_DeviceRecipe.Location = new System.Drawing.Point(3, 254);
            this.btn_DeviceRecipe.Name = "btn_DeviceRecipe";
            this.btn_DeviceRecipe.Size = new System.Drawing.Size(180, 60);
            this.btn_DeviceRecipe.TabIndex = 25;
            this.btn_DeviceRecipe.Text = "     Device Recipe";
            this.btn_DeviceRecipe.UseVisualStyleBackColor = false;
            this.btn_DeviceRecipe.Click += new System.EventHandler(this.btn_DeviceRecipe_Click);
            // 
            // btn_AccessConfig
            // 
            this.btn_AccessConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_AccessConfig.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_AccessConfig.BackgroundImage")));
            this.btn_AccessConfig.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_AccessConfig.FlatAppearance.BorderSize = 0;
            this.btn_AccessConfig.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_AccessConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_AccessConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AccessConfig.ForeColor = System.Drawing.Color.White;
            this.btn_AccessConfig.Location = new System.Drawing.Point(3, 386);
            this.btn_AccessConfig.Name = "btn_AccessConfig";
            this.btn_AccessConfig.Size = new System.Drawing.Size(180, 60);
            this.btn_AccessConfig.TabIndex = 21;
            this.btn_AccessConfig.Text = "     Access Config";
            this.btn_AccessConfig.UseVisualStyleBackColor = false;
            this.btn_AccessConfig.Click += new System.EventHandler(this.btn_AccessConfig_Click);
            // 
            // lbl_Version
            // 
            this.lbl_Version.AutoSize = true;
            this.lbl_Version.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Version.ForeColor = System.Drawing.Color.White;
            this.lbl_Version.Location = new System.Drawing.Point(50, 12);
            this.lbl_Version.Name = "lbl_Version";
            this.lbl_Version.Size = new System.Drawing.Size(107, 15);
            this.lbl_Version.TabIndex = 8;
            this.lbl_Version.Text = "Version: 1.0.0.0";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.btn_CloseAll);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 722);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(187, 100);
            this.panel6.TabIndex = 7;
            // 
            // btn_CloseAll
            // 
            this.btn_CloseAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_CloseAll.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_CloseAll.BackgroundImage")));
            this.btn_CloseAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_CloseAll.FlatAppearance.BorderSize = 0;
            this.btn_CloseAll.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btn_CloseAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CloseAll.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_CloseAll.ForeColor = System.Drawing.Color.White;
            this.btn_CloseAll.Location = new System.Drawing.Point(3, 26);
            this.btn_CloseAll.Name = "btn_CloseAll";
            this.btn_CloseAll.Size = new System.Drawing.Size(180, 60);
            this.btn_CloseAll.TabIndex = 6;
            this.btn_CloseAll.Text = "Close";
            this.btn_CloseAll.UseVisualStyleBackColor = false;
            this.btn_CloseAll.Click += new System.EventHandler(this.btn_CloseAll_Click);
            // 
            // btn_Setup
            // 
            this.btn_Setup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Setup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Setup.BackgroundImage")));
            this.btn_Setup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Setup.FlatAppearance.BorderSize = 0;
            this.btn_Setup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Setup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Setup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Setup.ForeColor = System.Drawing.Color.White;
            this.btn_Setup.Location = new System.Drawing.Point(3, 188);
            this.btn_Setup.Name = "btn_Setup";
            this.btn_Setup.Size = new System.Drawing.Size(180, 60);
            this.btn_Setup.TabIndex = 5;
            this.btn_Setup.Text = "Setup";
            this.btn_Setup.UseVisualStyleBackColor = false;
            this.btn_Setup.Click += new System.EventHandler(this.btn_Setup_Click);
            // 
            // btn_frmIODiag
            // 
            this.btn_frmIODiag.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_frmIODiag.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_frmIODiag.BackgroundImage")));
            this.btn_frmIODiag.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_frmIODiag.FlatAppearance.BorderSize = 0;
            this.btn_frmIODiag.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_frmIODiag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_frmIODiag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_frmIODiag.ForeColor = System.Drawing.Color.White;
            this.btn_frmIODiag.Location = new System.Drawing.Point(3, 320);
            this.btn_frmIODiag.Name = "btn_frmIODiag";
            this.btn_frmIODiag.Size = new System.Drawing.Size(180, 60);
            this.btn_frmIODiag.TabIndex = 4;
            this.btn_frmIODiag.Text = "     Diagnostic";
            this.btn_frmIODiag.UseVisualStyleBackColor = false;
            this.btn_frmIODiag.Click += new System.EventHandler(this.btn_frmIODiag_Click);
            // 
            // btn_Auto
            // 
            this.btn_Auto.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Auto.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Auto.BackgroundImage")));
            this.btn_Auto.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Auto.FlatAppearance.BorderSize = 0;
            this.btn_Auto.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Auto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Auto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Auto.ForeColor = System.Drawing.Color.White;
            this.btn_Auto.Location = new System.Drawing.Point(3, 122);
            this.btn_Auto.Name = "btn_Auto";
            this.btn_Auto.Size = new System.Drawing.Size(180, 60);
            this.btn_Auto.TabIndex = 3;
            this.btn_Auto.Text = "Auto";
            this.btn_Auto.UseVisualStyleBackColor = false;
            this.btn_Auto.Click += new System.EventHandler(this.btn_Auto_Click);
            // 
            // btn_Main
            // 
            this.btn_Main.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.btn_Main.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Main.BackgroundImage")));
            this.btn_Main.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Main.FlatAppearance.BorderSize = 0;
            this.btn_Main.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Main.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Main.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Main.ForeColor = System.Drawing.Color.White;
            this.btn_Main.Location = new System.Drawing.Point(3, 56);
            this.btn_Main.Name = "btn_Main";
            this.btn_Main.Size = new System.Drawing.Size(180, 60);
            this.btn_Main.TabIndex = 2;
            this.btn_Main.Text = "Main";
            this.btn_Main.UseVisualStyleBackColor = false;
            this.btn_Main.Click += new System.EventHandler(this.btn_Main_Click);
            // 
            // btn_SlideIn
            // 
            this.btn_SlideIn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_SlideIn.FlatAppearance.BorderSize = 0;
            this.btn_SlideIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SlideIn.Image = ((System.Drawing.Image)(resources.GetObject("btn_SlideIn.Image")));
            this.btn_SlideIn.Location = new System.Drawing.Point(157, 6);
            this.btn_SlideIn.Name = "btn_SlideIn";
            this.btn_SlideIn.Size = new System.Drawing.Size(29, 28);
            this.btn_SlideIn.TabIndex = 1;
            this.btn_SlideIn.UseVisualStyleBackColor = true;
            this.btn_SlideIn.Click += new System.EventHandler(this.btn_SlideIn_Click_1);
            // 
            // btn_SlideOut
            // 
            this.btn_SlideOut.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_SlideOut.FlatAppearance.BorderSize = 0;
            this.btn_SlideOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_SlideOut.Image = ((System.Drawing.Image)(resources.GetObject("btn_SlideOut.Image")));
            this.btn_SlideOut.Location = new System.Drawing.Point(4, 6);
            this.btn_SlideOut.Name = "btn_SlideOut";
            this.btn_SlideOut.Size = new System.Drawing.Size(29, 28);
            this.btn_SlideOut.TabIndex = 0;
            this.btn_SlideOut.UseVisualStyleBackColor = true;
            this.btn_SlideOut.Visible = false;
            this.btn_SlideOut.Click += new System.EventHandler(this.btn_SlideOut_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(76)))));
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lbl_Welcome);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.btn_Close);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.ForeColor = System.Drawing.Color.Transparent;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1272, 98);
            this.panel3.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(76)))));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(196, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 9);
            this.label1.TabIndex = 10;
            this.label1.Text = "MACHINE POWERED BY TTOT";
            // 
            // lbl_Welcome
            // 
            this.lbl_Welcome.AutoSize = true;
            this.lbl_Welcome.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(76)))));
            this.lbl_Welcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Welcome.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_Welcome.Location = new System.Drawing.Point(194, 59);
            this.lbl_Welcome.Name = "lbl_Welcome";
            this.lbl_Welcome.Size = new System.Drawing.Size(115, 25);
            this.lbl_Welcome.TabIndex = 9;
            this.lbl_Welcome.Text = "Welcome!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(76)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(193, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(674, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "SYSTEM FOR GREASESEPARATOR MACHINE INTEGRATION ";
            // 
            // btn_Close
            // 
            this.btn_Close.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_Close.BackgroundImage")));
            this.btn_Close.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_Close.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Close.ForeColor = System.Drawing.Color.White;
            this.btn_Close.Location = new System.Drawing.Point(914, 3);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(30, 30);
            this.btn_Close.TabIndex = 0;
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Visible = false;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.pictureBox1);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(187, 98);
            this.panel5.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(187, 98);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.lbl_Recipe);
            this.panel4.Controls.Add(this.lbl_Time);
            this.panel4.Controls.Add(this.lbl_Date);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(950, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(322, 98);
            this.panel4.TabIndex = 2;
            // 
            // lbl_Recipe
            // 
            this.lbl_Recipe.AutoSize = true;
            this.lbl_Recipe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(76)))));
            this.lbl_Recipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Recipe.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_Recipe.Location = new System.Drawing.Point(179, 14);
            this.lbl_Recipe.Name = "lbl_Recipe";
            this.lbl_Recipe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Recipe.Size = new System.Drawing.Size(62, 16);
            this.lbl_Recipe.TabIndex = 22;
            this.lbl_Recipe.Text = "Recipe:";
            // 
            // lbl_Time
            // 
            this.lbl_Time.AutoSize = true;
            this.lbl_Time.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(76)))));
            this.lbl_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Time.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_Time.Location = new System.Drawing.Point(179, 63);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(47, 16);
            this.lbl_Time.TabIndex = 21;
            this.lbl_Time.Text = "Time:";
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(18)))), ((int)(((byte)(76)))));
            this.lbl_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Date.ForeColor = System.Drawing.Color.Transparent;
            this.lbl_Date.Location = new System.Drawing.Point(179, 38);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Date.Size = new System.Drawing.Size(45, 16);
            this.lbl_Date.TabIndex = 20;
            this.lbl_Date.Text = "Date:";
            // 
            // tmr_slider
            // 
            this.tmr_slider.Interval = 10;
            this.tmr_slider.Tick += new System.EventHandler(this.tmr_slider_Tick);
            // 
            // tmr_Main
            // 
            this.tmr_Main.Enabled = true;
            this.tmr_Main.Interval = 10;
            this.tmr_Main.Tick += new System.EventHandler(this.tmr_Main_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1272, 920);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ES UI";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.panel2.ResumeLayout(false);
            this.pnl_Control.ResumeLayout(false);
            this.pnl_Control.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnl_Control;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Timer tmr_slider;
        private System.Windows.Forms.Button btn_SlideOut;
        private System.Windows.Forms.Button btn_SlideIn;
        private System.Windows.Forms.Button btn_frmIODiag;
        private System.Windows.Forms.Button btn_Auto;
        private System.Windows.Forms.Button btn_Main;
        private System.Windows.Forms.Button btn_Setup;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btn_CloseAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Version;
        public System.Windows.Forms.Panel pnl_Form;
        private System.Windows.Forms.Timer tmr_Main;
        private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.Button btn_AccessConfig;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lbl_Welcome;
        private System.Windows.Forms.Button btn_DeviceRecipe;
        private System.Windows.Forms.Label lbl_Recipe;
        private System.Windows.Forms.Button btn_Close;
        private System.Windows.Forms.Label label1;
    }
}

