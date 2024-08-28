namespace Machine
{
  partial class uctrlMotorParam
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
            this.lbl_Title = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_JogP = new System.Windows.Forms.Button();
            this.btn_JogN = new System.Windows.Forms.Button();
            this.btn_Spd = new System.Windows.Forms.Button();
            this.btn_Home = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_Pos = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lbl_HomeTimeOut = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lbl_HomeFastV = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lbl_HomeSlowV = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Encoder = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_HomeDir = new System.Windows.Forms.Label();
            this.lbl_Resulotion = new System.Windows.Forms.Label();
            this.lbl_DistPerPulse = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_MtrAlmLogic = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_InvertMtrOn = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_InvertDir = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lbl_MedV = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lbl_FastV = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_SlowV = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_StartV = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lbl_Accel = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.lbl_JogFastV = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lbl_JogMedV = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.lbl_JogSlowV = new System.Windows.Forms.Label();
            this.tc_Axis = new System.Windows.Forms.TabControl();
            this.tmr_UpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
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
            this.lbl_Title.Size = new System.Drawing.Size(1600, 45);
            this.lbl_Title.TabIndex = 106;
            this.lbl_Title.Text = "Title";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(1424, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(176, 854);
            this.panel1.TabIndex = 107;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_Save);
            this.panel2.Controls.Add(this.btn_JogP);
            this.panel2.Controls.Add(this.btn_JogN);
            this.panel2.Controls.Add(this.btn_Spd);
            this.panel2.Controls.Add(this.btn_Home);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.lbl_Pos);
            this.panel2.Controls.Add(this.groupBox6);
            this.panel2.Controls.Add(this.groupBox7);
            this.panel2.Controls.Add(this.groupBox4);
            this.panel2.Controls.Add(this.groupBox5);
            this.panel2.Controls.Add(this.tc_Axis);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1424, 854);
            this.panel2.TabIndex = 108;
            // 
            // btn_Save
            // 
            this.btn_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Save.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Save.Location = new System.Drawing.Point(814, 400);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(241, 62);
            this.btn_Save.TabIndex = 150;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_JogP
            // 
            this.btn_JogP.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_JogP.Location = new System.Drawing.Point(916, 81);
            this.btn_JogP.Name = "btn_JogP";
            this.btn_JogP.Size = new System.Drawing.Size(139, 60);
            this.btn_JogP.TabIndex = 149;
            this.btn_JogP.Text = "Jog +";
            this.btn_JogP.UseVisualStyleBackColor = true;
            this.btn_JogP.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_JogP_MouseDown);
            this.btn_JogP.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_JogP_MouseUp);
            // 
            // btn_JogN
            // 
            this.btn_JogN.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_JogN.Location = new System.Drawing.Point(771, 81);
            this.btn_JogN.Name = "btn_JogN";
            this.btn_JogN.Size = new System.Drawing.Size(139, 60);
            this.btn_JogN.TabIndex = 148;
            this.btn_JogN.Text = "Jog -";
            this.btn_JogN.UseVisualStyleBackColor = true;
            this.btn_JogN.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_JogN_MouseDown);
            this.btn_JogN.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_JogN_MouseUp);
            // 
            // btn_Spd
            // 
            this.btn_Spd.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Spd.Location = new System.Drawing.Point(916, 147);
            this.btn_Spd.Name = "btn_Spd";
            this.btn_Spd.Size = new System.Drawing.Size(139, 60);
            this.btn_Spd.TabIndex = 147;
            this.btn_Spd.Text = "btn_Spd";
            this.btn_Spd.UseVisualStyleBackColor = true;
            this.btn_Spd.Click += new System.EventHandler(this.btn_Spd_Click);
            // 
            // btn_Home
            // 
            this.btn_Home.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_Home.Location = new System.Drawing.Point(771, 147);
            this.btn_Home.Name = "btn_Home";
            this.btn_Home.Size = new System.Drawing.Size(139, 60);
            this.btn_Home.TabIndex = 146;
            this.btn_Home.Text = "Initialize";
            this.btn_Home.UseVisualStyleBackColor = true;
            this.btn_Home.Click += new System.EventHandler(this.btn_Home_Click);
            // 
            // label1
            // 
            this.label1.AccessibleDescription = "";
            this.label1.AutoEllipsis = true;
            this.label1.BackColor = System.Drawing.SystemColors.Control;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.ForeColor = System.Drawing.Color.DarkBlue;
            this.label1.Location = new System.Drawing.Point(771, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 32);
            this.label1.TabIndex = 145;
            this.label1.Text = "Position:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Pos
            // 
            this.lbl_Pos.BackColor = System.Drawing.Color.White;
            this.lbl_Pos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Pos.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbl_Pos.Location = new System.Drawing.Point(862, 37);
            this.lbl_Pos.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Pos.Name = "lbl_Pos";
            this.lbl_Pos.Size = new System.Drawing.Size(80, 32);
            this.lbl_Pos.TabIndex = 144;
            this.lbl_Pos.Text = "-";
            this.lbl_Pos.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox6
            // 
            this.groupBox6.AccessibleDescription = "";
            this.groupBox6.AutoSize = true;
            this.groupBox6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.lbl_HomeTimeOut);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.lbl_HomeFastV);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.lbl_HomeSlowV);
            this.groupBox6.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox6.Location = new System.Drawing.Point(283, 269);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(6, 5, 6, 0);
            this.groupBox6.Size = new System.Drawing.Size(240, 141);
            this.groupBox6.TabIndex = 142;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Home";
            // 
            // label15
            // 
            this.label15.AccessibleDescription = "Timeout";
            this.label15.AutoEllipsis = true;
            this.label15.BackColor = System.Drawing.SystemColors.Control;
            this.label15.Location = new System.Drawing.Point(8, 94);
            this.label15.Margin = new System.Windows.Forms.Padding(2);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 32);
            this.label15.TabIndex = 121;
            this.label15.Text = "Timeout (ms)";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_HomeTimeOut
            // 
            this.lbl_HomeTimeOut.BackColor = System.Drawing.Color.White;
            this.lbl_HomeTimeOut.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_HomeTimeOut.ForeColor = System.Drawing.Color.Navy;
            this.lbl_HomeTimeOut.Location = new System.Drawing.Point(152, 94);
            this.lbl_HomeTimeOut.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_HomeTimeOut.Name = "lbl_HomeTimeOut";
            this.lbl_HomeTimeOut.Size = new System.Drawing.Size(80, 32);
            this.lbl_HomeTimeOut.TabIndex = 120;
            this.lbl_HomeTimeOut.Text = "-";
            this.lbl_HomeTimeOut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_HomeTimeOut.Click += new System.EventHandler(this.lbl_HomeTimeOut_Click);
            // 
            // label16
            // 
            this.label16.AccessibleDescription = "Fast Speed";
            this.label16.AutoEllipsis = true;
            this.label16.BackColor = System.Drawing.SystemColors.Control;
            this.label16.Location = new System.Drawing.Point(8, 58);
            this.label16.Margin = new System.Windows.Forms.Padding(2);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(140, 32);
            this.label16.TabIndex = 119;
            this.label16.Text = "Fast Speed (mm/s)";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_HomeFastV
            // 
            this.lbl_HomeFastV.BackColor = System.Drawing.Color.White;
            this.lbl_HomeFastV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_HomeFastV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_HomeFastV.Location = new System.Drawing.Point(152, 58);
            this.lbl_HomeFastV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_HomeFastV.Name = "lbl_HomeFastV";
            this.lbl_HomeFastV.Size = new System.Drawing.Size(80, 32);
            this.lbl_HomeFastV.TabIndex = 118;
            this.lbl_HomeFastV.Text = "-";
            this.lbl_HomeFastV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_HomeFastV.Click += new System.EventHandler(this.lbl_HomeFastV_Click);
            // 
            // label17
            // 
            this.label17.AccessibleDescription = "Slow Speed";
            this.label17.AutoEllipsis = true;
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.Location = new System.Drawing.Point(8, 22);
            this.label17.Margin = new System.Windows.Forms.Padding(2);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(140, 32);
            this.label17.TabIndex = 117;
            this.label17.Text = "Slow Speed (mm/s)";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_HomeSlowV
            // 
            this.lbl_HomeSlowV.BackColor = System.Drawing.Color.White;
            this.lbl_HomeSlowV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_HomeSlowV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_HomeSlowV.Location = new System.Drawing.Point(152, 22);
            this.lbl_HomeSlowV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_HomeSlowV.Name = "lbl_HomeSlowV";
            this.lbl_HomeSlowV.Size = new System.Drawing.Size(80, 32);
            this.lbl_HomeSlowV.TabIndex = 116;
            this.lbl_HomeSlowV.Text = "-";
            this.lbl_HomeSlowV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_HomeSlowV.Click += new System.EventHandler(this.lbl_HomeSlowV_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.AccessibleDescription = "";
            this.groupBox7.AutoSize = true;
            this.groupBox7.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Controls.Add(this.lbl_Encoder);
            this.groupBox7.Controls.Add(this.label14);
            this.groupBox7.Controls.Add(this.lbl_HomeDir);
            this.groupBox7.Controls.Add(this.lbl_Resulotion);
            this.groupBox7.Controls.Add(this.lbl_DistPerPulse);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.lbl_MtrAlmLogic);
            this.groupBox7.Controls.Add(this.label7);
            this.groupBox7.Controls.Add(this.lbl_InvertMtrOn);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Controls.Add(this.lbl_InvertDir);
            this.groupBox7.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox7.Location = new System.Drawing.Point(283, 13);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(6, 5, 6, 0);
            this.groupBox7.Size = new System.Drawing.Size(240, 252);
            this.groupBox7.TabIndex = 143;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Motor Parameter";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = "Encoder";
            this.label2.AutoEllipsis = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(8, 205);
            this.label2.Margin = new System.Windows.Forms.Padding(2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 32);
            this.label2.TabIndex = 135;
            this.label2.Text = "Encoder";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Encoder
            // 
            this.lbl_Encoder.BackColor = System.Drawing.Color.White;
            this.lbl_Encoder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Encoder.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Encoder.Location = new System.Drawing.Point(152, 205);
            this.lbl_Encoder.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Encoder.Name = "lbl_Encoder";
            this.lbl_Encoder.Size = new System.Drawing.Size(80, 32);
            this.lbl_Encoder.TabIndex = 134;
            this.lbl_Encoder.Text = "-";
            this.lbl_Encoder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Encoder.Click += new System.EventHandler(this.lbl_Encoder_Click);
            // 
            // label14
            // 
            this.label14.AccessibleDescription = "Home Direction";
            this.label14.AutoEllipsis = true;
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(8, 169);
            this.label14.Margin = new System.Windows.Forms.Padding(2);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(140, 32);
            this.label14.TabIndex = 133;
            this.label14.Text = "Home Direction";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_HomeDir
            // 
            this.lbl_HomeDir.BackColor = System.Drawing.Color.White;
            this.lbl_HomeDir.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_HomeDir.ForeColor = System.Drawing.Color.Navy;
            this.lbl_HomeDir.Location = new System.Drawing.Point(152, 169);
            this.lbl_HomeDir.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_HomeDir.Name = "lbl_HomeDir";
            this.lbl_HomeDir.Size = new System.Drawing.Size(80, 32);
            this.lbl_HomeDir.TabIndex = 132;
            this.lbl_HomeDir.Text = "-";
            this.lbl_HomeDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_HomeDir.Click += new System.EventHandler(this.lbl_HomeDir_Click);
            // 
            // lbl_Resulotion
            // 
            this.lbl_Resulotion.AccessibleDescription = "Dist Per Pulse";
            this.lbl_Resulotion.AutoEllipsis = true;
            this.lbl_Resulotion.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_Resulotion.Location = new System.Drawing.Point(8, 24);
            this.lbl_Resulotion.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Resulotion.Name = "lbl_Resulotion";
            this.lbl_Resulotion.Size = new System.Drawing.Size(140, 32);
            this.lbl_Resulotion.TabIndex = 127;
            this.lbl_Resulotion.Text = "Dist Per Pulse (mm)";
            this.lbl_Resulotion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_DistPerPulse
            // 
            this.lbl_DistPerPulse.BackColor = System.Drawing.Color.White;
            this.lbl_DistPerPulse.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_DistPerPulse.ForeColor = System.Drawing.Color.Navy;
            this.lbl_DistPerPulse.Location = new System.Drawing.Point(152, 24);
            this.lbl_DistPerPulse.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_DistPerPulse.Name = "lbl_DistPerPulse";
            this.lbl_DistPerPulse.Size = new System.Drawing.Size(80, 32);
            this.lbl_DistPerPulse.TabIndex = 126;
            this.lbl_DistPerPulse.Text = "-";
            this.lbl_DistPerPulse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_DistPerPulse.Click += new System.EventHandler(this.lbl_DistPerPulse_Click);
            // 
            // label8
            // 
            this.label8.AccessibleDescription = "Motor Alarm Logic";
            this.label8.AutoEllipsis = true;
            this.label8.BackColor = System.Drawing.SystemColors.Control;
            this.label8.Location = new System.Drawing.Point(8, 96);
            this.label8.Margin = new System.Windows.Forms.Padding(2);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 32);
            this.label8.TabIndex = 125;
            this.label8.Text = "Motor Alarm Logic";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MtrAlmLogic
            // 
            this.lbl_MtrAlmLogic.BackColor = System.Drawing.Color.White;
            this.lbl_MtrAlmLogic.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_MtrAlmLogic.ForeColor = System.Drawing.Color.Navy;
            this.lbl_MtrAlmLogic.Location = new System.Drawing.Point(152, 96);
            this.lbl_MtrAlmLogic.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_MtrAlmLogic.Name = "lbl_MtrAlmLogic";
            this.lbl_MtrAlmLogic.Size = new System.Drawing.Size(80, 32);
            this.lbl_MtrAlmLogic.TabIndex = 124;
            this.lbl_MtrAlmLogic.Text = "-";
            this.lbl_MtrAlmLogic.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_MtrAlmLogic.Click += new System.EventHandler(this.lbl_MtrAlmLogic_Click);
            // 
            // label7
            // 
            this.label7.AccessibleDescription = "Invert Motor On";
            this.label7.AutoEllipsis = true;
            this.label7.BackColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(8, 133);
            this.label7.Margin = new System.Windows.Forms.Padding(2);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 32);
            this.label7.TabIndex = 121;
            this.label7.Text = "Invert Motor On";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_InvertMtrOn
            // 
            this.lbl_InvertMtrOn.BackColor = System.Drawing.Color.White;
            this.lbl_InvertMtrOn.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_InvertMtrOn.ForeColor = System.Drawing.Color.Navy;
            this.lbl_InvertMtrOn.Location = new System.Drawing.Point(152, 133);
            this.lbl_InvertMtrOn.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_InvertMtrOn.Name = "lbl_InvertMtrOn";
            this.lbl_InvertMtrOn.Size = new System.Drawing.Size(80, 32);
            this.lbl_InvertMtrOn.TabIndex = 120;
            this.lbl_InvertMtrOn.Text = "-";
            this.lbl_InvertMtrOn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_InvertMtrOn.Click += new System.EventHandler(this.lbl_InvertMtrOn_Click);
            // 
            // label9
            // 
            this.label9.AccessibleDescription = "Invert Direction";
            this.label9.AutoEllipsis = true;
            this.label9.BackColor = System.Drawing.SystemColors.Control;
            this.label9.Location = new System.Drawing.Point(8, 60);
            this.label9.Margin = new System.Windows.Forms.Padding(2);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 32);
            this.label9.TabIndex = 119;
            this.label9.Text = "Invert Direction";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_InvertDir
            // 
            this.lbl_InvertDir.BackColor = System.Drawing.Color.White;
            this.lbl_InvertDir.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_InvertDir.ForeColor = System.Drawing.Color.Navy;
            this.lbl_InvertDir.Location = new System.Drawing.Point(152, 60);
            this.lbl_InvertDir.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_InvertDir.Name = "lbl_InvertDir";
            this.lbl_InvertDir.Size = new System.Drawing.Size(80, 32);
            this.lbl_InvertDir.TabIndex = 118;
            this.lbl_InvertDir.Text = "-";
            this.lbl_InvertDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_InvertDir.Click += new System.EventHandler(this.lbl_InvertDir_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.AutoSize = true;
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.lbl_MedV);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.lbl_FastV);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.lbl_SlowV);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.lbl_StartV);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.lbl_Accel);
            this.groupBox4.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox4.Location = new System.Drawing.Point(527, 163);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(6, 5, 6, 0);
            this.groupBox4.Size = new System.Drawing.Size(241, 217);
            this.groupBox4.TabIndex = 140;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Operation";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = "Fast Speed";
            this.label3.AutoEllipsis = true;
            this.label3.BackColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(8, 128);
            this.label3.Margin = new System.Windows.Forms.Padding(2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 32);
            this.label3.TabIndex = 111;
            this.label3.Text = "Medium Speed (mm/s)";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_MedV
            // 
            this.lbl_MedV.BackColor = System.Drawing.Color.White;
            this.lbl_MedV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_MedV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_MedV.Location = new System.Drawing.Point(152, 125);
            this.lbl_MedV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_MedV.Name = "lbl_MedV";
            this.lbl_MedV.Size = new System.Drawing.Size(80, 32);
            this.lbl_MedV.TabIndex = 110;
            this.lbl_MedV.Text = "-";
            this.lbl_MedV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_MedV.Click += new System.EventHandler(this.lbl_MedV_Click);
            // 
            // label13
            // 
            this.label13.AccessibleDescription = "Fast Speed";
            this.label13.AutoEllipsis = true;
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(8, 161);
            this.label13.Margin = new System.Windows.Forms.Padding(2);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(140, 32);
            this.label13.TabIndex = 107;
            this.label13.Text = "Fast Speed (mm/s)";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_FastV
            // 
            this.lbl_FastV.BackColor = System.Drawing.Color.White;
            this.lbl_FastV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_FastV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_FastV.Location = new System.Drawing.Point(152, 161);
            this.lbl_FastV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_FastV.Name = "lbl_FastV";
            this.lbl_FastV.Size = new System.Drawing.Size(80, 32);
            this.lbl_FastV.TabIndex = 106;
            this.lbl_FastV.Text = "-";
            this.lbl_FastV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_FastV.Click += new System.EventHandler(this.lbl_FastV_Click);
            // 
            // label11
            // 
            this.label11.AccessibleDescription = "Slow Speed";
            this.label11.AutoEllipsis = true;
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(8, 95);
            this.label11.Margin = new System.Windows.Forms.Padding(2);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 32);
            this.label11.TabIndex = 105;
            this.label11.Text = "Slow Speed (mm/s)";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_SlowV
            // 
            this.lbl_SlowV.BackColor = System.Drawing.Color.White;
            this.lbl_SlowV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_SlowV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_SlowV.Location = new System.Drawing.Point(152, 92);
            this.lbl_SlowV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_SlowV.Name = "lbl_SlowV";
            this.lbl_SlowV.Size = new System.Drawing.Size(80, 32);
            this.lbl_SlowV.TabIndex = 104;
            this.lbl_SlowV.Text = "-";
            this.lbl_SlowV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_SlowV.Click += new System.EventHandler(this.lbl_SlowV_Click);
            // 
            // label10
            // 
            this.label10.AccessibleDescription = "Start Speed";
            this.label10.AutoEllipsis = true;
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(8, 59);
            this.label10.Margin = new System.Windows.Forms.Padding(2);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 32);
            this.label10.TabIndex = 103;
            this.label10.Text = "Start Speed (mm/s)";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_StartV
            // 
            this.lbl_StartV.BackColor = System.Drawing.Color.White;
            this.lbl_StartV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_StartV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_StartV.Location = new System.Drawing.Point(152, 56);
            this.lbl_StartV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_StartV.Name = "lbl_StartV";
            this.lbl_StartV.Size = new System.Drawing.Size(80, 32);
            this.lbl_StartV.TabIndex = 102;
            this.lbl_StartV.Text = "-";
            this.lbl_StartV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_StartV.Click += new System.EventHandler(this.lbl_StartV_Click);
            // 
            // label12
            // 
            this.label12.AccessibleDescription = "Accel";
            this.label12.AutoEllipsis = true;
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(8, 23);
            this.label12.Margin = new System.Windows.Forms.Padding(2);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 32);
            this.label12.TabIndex = 101;
            this.label12.Text = "Acceleration (mm/s)";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Accel
            // 
            this.lbl_Accel.BackColor = System.Drawing.Color.White;
            this.lbl_Accel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Accel.ForeColor = System.Drawing.Color.Navy;
            this.lbl_Accel.Location = new System.Drawing.Point(152, 20);
            this.lbl_Accel.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_Accel.Name = "lbl_Accel";
            this.lbl_Accel.Size = new System.Drawing.Size(80, 32);
            this.lbl_Accel.TabIndex = 100;
            this.lbl_Accel.Text = "-";
            this.lbl_Accel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Accel.Click += new System.EventHandler(this.lbl_Accel_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.AutoSize = true;
            this.groupBox5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.lbl_JogFastV);
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.lbl_JogMedV);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.lbl_JogSlowV);
            this.groupBox5.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox5.Location = new System.Drawing.Point(527, 13);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(6, 5, 6, 0);
            this.groupBox5.Size = new System.Drawing.Size(240, 143);
            this.groupBox5.TabIndex = 141;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Jog";
            // 
            // label23
            // 
            this.label23.AccessibleDescription = "Fast Speed";
            this.label23.AutoEllipsis = true;
            this.label23.BackColor = System.Drawing.SystemColors.Control;
            this.label23.Location = new System.Drawing.Point(8, 96);
            this.label23.Margin = new System.Windows.Forms.Padding(2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(140, 32);
            this.label23.TabIndex = 111;
            this.label23.Text = "Fast Speed (mm/s)";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_JogFastV
            // 
            this.lbl_JogFastV.BackColor = System.Drawing.Color.White;
            this.lbl_JogFastV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_JogFastV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_JogFastV.Location = new System.Drawing.Point(152, 96);
            this.lbl_JogFastV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_JogFastV.Name = "lbl_JogFastV";
            this.lbl_JogFastV.Size = new System.Drawing.Size(80, 32);
            this.lbl_JogFastV.TabIndex = 110;
            this.lbl_JogFastV.Text = "-";
            this.lbl_JogFastV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_JogFastV.Click += new System.EventHandler(this.lbl_JogFastV_Click);
            // 
            // label19
            // 
            this.label19.AccessibleDescription = "Med Speed";
            this.label19.AutoEllipsis = true;
            this.label19.BackColor = System.Drawing.SystemColors.Control;
            this.label19.Location = new System.Drawing.Point(8, 60);
            this.label19.Margin = new System.Windows.Forms.Padding(2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(140, 32);
            this.label19.TabIndex = 109;
            this.label19.Text = "Med Speed (mm/s)";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_JogMedV
            // 
            this.lbl_JogMedV.BackColor = System.Drawing.Color.White;
            this.lbl_JogMedV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_JogMedV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_JogMedV.Location = new System.Drawing.Point(152, 60);
            this.lbl_JogMedV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_JogMedV.Name = "lbl_JogMedV";
            this.lbl_JogMedV.Size = new System.Drawing.Size(80, 32);
            this.lbl_JogMedV.TabIndex = 108;
            this.lbl_JogMedV.Text = "-";
            this.lbl_JogMedV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_JogMedV.Click += new System.EventHandler(this.lbl_JogMedV_Click);
            // 
            // label21
            // 
            this.label21.AccessibleDescription = "";
            this.label21.AutoEllipsis = true;
            this.label21.BackColor = System.Drawing.SystemColors.Control;
            this.label21.Location = new System.Drawing.Point(8, 24);
            this.label21.Margin = new System.Windows.Forms.Padding(2);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(140, 32);
            this.label21.TabIndex = 107;
            this.label21.Text = "Slow Speed (mm/s)";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_JogSlowV
            // 
            this.lbl_JogSlowV.BackColor = System.Drawing.Color.White;
            this.lbl_JogSlowV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_JogSlowV.ForeColor = System.Drawing.Color.Navy;
            this.lbl_JogSlowV.Location = new System.Drawing.Point(152, 24);
            this.lbl_JogSlowV.Margin = new System.Windows.Forms.Padding(2);
            this.lbl_JogSlowV.Name = "lbl_JogSlowV";
            this.lbl_JogSlowV.Size = new System.Drawing.Size(80, 32);
            this.lbl_JogSlowV.TabIndex = 106;
            this.lbl_JogSlowV.Text = "-";
            this.lbl_JogSlowV.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_JogSlowV.Click += new System.EventHandler(this.lbl_JogSlowV_Click);
            // 
            // tc_Axis
            // 
            this.tc_Axis.Location = new System.Drawing.Point(14, 13);
            this.tc_Axis.Name = "tc_Axis";
            this.tc_Axis.SelectedIndex = 0;
            this.tc_Axis.Size = new System.Drawing.Size(264, 449);
            this.tc_Axis.TabIndex = 0;
            this.tc_Axis.SelectedIndexChanged += new System.EventHandler(this.tc_Axis_SelectedIndexChanged);
            // 
            // tmr_UpdateDisplay
            // 
            this.tmr_UpdateDisplay.Enabled = true;
            this.tmr_UpdateDisplay.Tick += new System.EventHandler(this.tmr_UpdateDisplay_Tick);
            // 
            // uctrlMotorParam
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lbl_Title);
            this.Name = "uctrlMotorParam";
            this.Size = new System.Drawing.Size(1600, 899);
            this.Load += new System.EventHandler(this.uctrlMotorParam_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl_Pos;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbl_HomeTimeOut;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbl_HomeFastV;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lbl_HomeSlowV;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_HomeDir;
        private System.Windows.Forms.Label lbl_Resulotion;
        private System.Windows.Forms.Label lbl_DistPerPulse;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_MtrAlmLogic;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_InvertMtrOn;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_InvertDir;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lbl_FastV;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_SlowV;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_StartV;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lbl_Accel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lbl_JogFastV;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label lbl_JogMedV;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lbl_JogSlowV;
        private System.Windows.Forms.TabControl tc_Axis;
        private System.Windows.Forms.Button btn_Home;
        private System.Windows.Forms.Button btn_Spd;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_JogP;
        private System.Windows.Forms.Button btn_JogN;
        private System.Windows.Forms.Timer tmr_UpdateDisplay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Encoder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_MedV;
    }
}
