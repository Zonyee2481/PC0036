
namespace Machine
{
    partial class uctrlAuto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(uctrlAuto));
            this.pnlRight = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_Mode = new System.Windows.Forms.Label();
            this.btn_SystemInit = new System.Windows.Forms.Button();
            this.btn_LotInfo = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lbl_MachineState = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Restart = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Start = new System.Windows.Forms.Button();
            this.pnlTitle = new System.Windows.Forms.Panel();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvSeqNum = new System.Windows.Forms.DataGridView();
            this.pnlLog = new System.Windows.Forms.Panel();
            this.lbox_Log = new System.Windows.Forms.ListBox();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlLeftRight = new System.Windows.Forms.Panel();
            this.pnlLeftLeft = new System.Windows.Forms.Panel();
            this.gbMcPerformance = new System.Windows.Forms.GroupBox();
            this.lbl_McRunTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_McIdleTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_McOPTime = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.pnlMiddle = new System.Windows.Forms.Panel();
            this.pbMachine = new System.Windows.Forms.PictureBox();
            this.pnlLotInfo = new System.Windows.Forms.Panel();
            this.gbLotInfo = new System.Windows.Forms.GroupBox();
            this.txtDeviceID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtLotNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProdNotFound = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pnlRight.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.pnlTitle.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeqNum)).BeginInit();
            this.pnlLog.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.pnlLeftRight.SuspendLayout();
            this.pnlLeftLeft.SuspendLayout();
            this.gbMcPerformance.SuspendLayout();
            this.pnlMiddle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMachine)).BeginInit();
            this.pnlLotInfo.SuspendLayout();
            this.gbLotInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlRight
            // 
            this.pnlRight.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlRight.Controls.Add(this.groupBox2);
            this.pnlRight.Controls.Add(this.btn_SystemInit);
            this.pnlRight.Controls.Add(this.btn_LotInfo);
            this.pnlRight.Controls.Add(this.groupBox7);
            this.pnlRight.Controls.Add(this.groupBox1);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.Location = new System.Drawing.Point(1079, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(295, 838);
            this.pnlRight.TabIndex = 106;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_Mode);
            this.groupBox2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox2.Location = new System.Drawing.Point(4, 91);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(288, 108);
            this.groupBox2.TabIndex = 96;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mode";
            // 
            // lbl_Mode
            // 
            this.lbl_Mode.BackColor = System.Drawing.Color.Yellow;
            this.lbl_Mode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Mode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Mode.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Mode.ForeColor = System.Drawing.Color.White;
            this.lbl_Mode.Location = new System.Drawing.Point(11, 31);
            this.lbl_Mode.Name = "lbl_Mode";
            this.lbl_Mode.Size = new System.Drawing.Size(271, 46);
            this.lbl_Mode.TabIndex = 74;
            this.lbl_Mode.Text = "Auto Mode";
            this.lbl_Mode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_SystemInit
            // 
            this.btn_SystemInit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_SystemInit.Enabled = false;
            this.btn_SystemInit.FlatAppearance.BorderSize = 0;
            this.btn_SystemInit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_SystemInit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_SystemInit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SystemInit.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_SystemInit.Location = new System.Drawing.Point(4, 205);
            this.btn_SystemInit.Name = "btn_SystemInit";
            this.btn_SystemInit.Size = new System.Drawing.Size(288, 55);
            this.btn_SystemInit.TabIndex = 67;
            this.btn_SystemInit.Text = "Initialize";
            this.btn_SystemInit.UseVisualStyleBackColor = true;
            this.btn_SystemInit.Visible = false;
            // 
            // btn_LotInfo
            // 
            this.btn_LotInfo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_LotInfo.BackgroundImage")));
            this.btn_LotInfo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_LotInfo.FlatAppearance.BorderSize = 0;
            this.btn_LotInfo.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_LotInfo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_LotInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_LotInfo.ForeColor = System.Drawing.Color.DarkBlue;
            this.btn_LotInfo.Location = new System.Drawing.Point(7, 494);
            this.btn_LotInfo.Name = "btn_LotInfo";
            this.btn_LotInfo.Size = new System.Drawing.Size(285, 57);
            this.btn_LotInfo.TabIndex = 103;
            this.btn_LotInfo.Text = "Lot Info";
            this.btn_LotInfo.UseVisualStyleBackColor = true;
            this.btn_LotInfo.Visible = false;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lbl_MachineState);
            this.groupBox7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox7.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox7.Location = new System.Drawing.Point(4, 3);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(288, 80);
            this.groupBox7.TabIndex = 102;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Machine State";
            // 
            // lbl_MachineState
            // 
            this.lbl_MachineState.BackColor = System.Drawing.Color.DarkBlue;
            this.lbl_MachineState.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_MachineState.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_MachineState.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MachineState.ForeColor = System.Drawing.Color.White;
            this.lbl_MachineState.Location = new System.Drawing.Point(11, 24);
            this.lbl_MachineState.Name = "lbl_MachineState";
            this.lbl_MachineState.Size = new System.Drawing.Size(271, 46);
            this.lbl_MachineState.TabIndex = 73;
            this.lbl_MachineState.Text = "Idle";
            this.lbl_MachineState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Restart);
            this.groupBox1.Controls.Add(this.btn_Stop);
            this.groupBox1.Controls.Add(this.btn_Start);
            this.groupBox1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox1.Location = new System.Drawing.Point(2, 273);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(290, 215);
            this.groupBox1.TabIndex = 95;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Main Control";
            this.groupBox1.Visible = false;
            // 
            // btn_Restart
            // 
            this.btn_Restart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Restart.Enabled = false;
            this.btn_Restart.FlatAppearance.BorderSize = 0;
            this.btn_Restart.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Restart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Restart.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Restart.ForeColor = System.Drawing.Color.Orange;
            this.btn_Restart.Location = new System.Drawing.Point(5, 147);
            this.btn_Restart.Name = "btn_Restart";
            this.btn_Restart.Size = new System.Drawing.Size(290, 55);
            this.btn_Restart.TabIndex = 70;
            this.btn_Restart.Text = "Restart";
            this.btn_Restart.UseVisualStyleBackColor = true;
            // 
            // btn_Stop
            // 
            this.btn_Stop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Stop.Enabled = false;
            this.btn_Stop.FlatAppearance.BorderSize = 0;
            this.btn_Stop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Stop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Stop.ForeColor = System.Drawing.Color.Red;
            this.btn_Stop.Location = new System.Drawing.Point(5, 86);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(290, 55);
            this.btn_Stop.TabIndex = 69;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Start
            // 
            this.btn_Start.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btn_Start.FlatAppearance.BorderSize = 0;
            this.btn_Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.RoyalBlue;
            this.btn_Start.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Start.ForeColor = System.Drawing.Color.Green;
            this.btn_Start.Location = new System.Drawing.Point(5, 25);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(290, 55);
            this.btn_Start.TabIndex = 68;
            this.btn_Start.Text = "Start";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lbl_Title);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(1079, 45);
            this.pnlTitle.TabIndex = 107;
            // 
            // lbl_Title
            // 
            this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_Title.Font = new System.Drawing.Font("Times New Roman", 15.75F);
            this.lbl_Title.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbl_Title.Location = new System.Drawing.Point(0, 0);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(1079, 45);
            this.lbl_Title.TabIndex = 105;
            this.lbl_Title.Text = "Auto";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.panel1);
            this.pnlBottom.Controls.Add(this.pnlLog);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 678);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1079, 160);
            this.pnlBottom.TabIndex = 108;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvSeqNum);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(613, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(466, 160);
            this.panel1.TabIndex = 3;
            // 
            // dgvSeqNum
            // 
            this.dgvSeqNum.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSeqNum.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvSeqNum.Location = new System.Drawing.Point(0, 0);
            this.dgvSeqNum.Name = "dgvSeqNum";
            this.dgvSeqNum.Size = new System.Drawing.Size(466, 160);
            this.dgvSeqNum.TabIndex = 156;
            // 
            // pnlLog
            // 
            this.pnlLog.Controls.Add(this.lbox_Log);
            this.pnlLog.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLog.Location = new System.Drawing.Point(0, 0);
            this.pnlLog.Name = "pnlLog";
            this.pnlLog.Size = new System.Drawing.Size(613, 160);
            this.pnlLog.TabIndex = 2;
            // 
            // lbox_Log
            // 
            this.lbox_Log.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbox_Log.ForeColor = System.Drawing.Color.Black;
            this.lbox_Log.FormattingEnabled = true;
            this.lbox_Log.Location = new System.Drawing.Point(0, 0);
            this.lbox_Log.Name = "lbox_Log";
            this.lbox_Log.Size = new System.Drawing.Size(613, 160);
            this.lbox_Log.TabIndex = 1;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnlLeftRight);
            this.pnlLeft.Controls.Add(this.pnlLeftLeft);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 45);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(400, 633);
            this.pnlLeft.TabIndex = 109;
            // 
            // pnlLeftRight
            // 
            this.pnlLeftRight.Controls.Add(this.groupBox3);
            this.pnlLeftRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftRight.Location = new System.Drawing.Point(200, 0);
            this.pnlLeftRight.Name = "pnlLeftRight";
            this.pnlLeftRight.Size = new System.Drawing.Size(200, 633);
            this.pnlLeftRight.TabIndex = 1;
            // 
            // pnlLeftLeft
            // 
            this.pnlLeftLeft.Controls.Add(this.gbMcPerformance);
            this.pnlLeftLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeftLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftLeft.Name = "pnlLeftLeft";
            this.pnlLeftLeft.Size = new System.Drawing.Size(200, 633);
            this.pnlLeftLeft.TabIndex = 0;
            // 
            // gbMcPerformance
            // 
            this.gbMcPerformance.Controls.Add(this.lbl_McRunTime);
            this.gbMcPerformance.Controls.Add(this.label6);
            this.gbMcPerformance.Controls.Add(this.lbl_McIdleTime);
            this.gbMcPerformance.Controls.Add(this.label7);
            this.gbMcPerformance.Controls.Add(this.lbl_McOPTime);
            this.gbMcPerformance.Controls.Add(this.label10);
            this.gbMcPerformance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMcPerformance.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.gbMcPerformance.ForeColor = System.Drawing.Color.DarkBlue;
            this.gbMcPerformance.Location = new System.Drawing.Point(0, 0);
            this.gbMcPerformance.Name = "gbMcPerformance";
            this.gbMcPerformance.Size = new System.Drawing.Size(200, 633);
            this.gbMcPerformance.TabIndex = 0;
            this.gbMcPerformance.TabStop = false;
            this.gbMcPerformance.Text = "Machine Perfomance";
            // 
            // lbl_McRunTime
            // 
            this.lbl_McRunTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_McRunTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_McRunTime.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_McRunTime.Location = new System.Drawing.Point(6, 44);
            this.lbl_McRunTime.Name = "lbl_McRunTime";
            this.lbl_McRunTime.Size = new System.Drawing.Size(188, 22);
            this.lbl_McRunTime.TabIndex = 70;
            this.lbl_McRunTime.Text = "0";
            this.lbl_McRunTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(188, 22);
            this.label6.TabIndex = 69;
            this.label6.Text = "Machine Run Time:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_McIdleTime
            // 
            this.lbl_McIdleTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_McIdleTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_McIdleTime.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_McIdleTime.Location = new System.Drawing.Point(6, 129);
            this.lbl_McIdleTime.Name = "lbl_McIdleTime";
            this.lbl_McIdleTime.Size = new System.Drawing.Size(188, 22);
            this.lbl_McIdleTime.TabIndex = 68;
            this.lbl_McIdleTime.Text = "0";
            this.lbl_McIdleTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 107);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(188, 22);
            this.label7.TabIndex = 67;
            this.label7.Text = "Machine Idle Time:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_McOPTime
            // 
            this.lbl_McOPTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_McOPTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_McOPTime.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_McOPTime.Location = new System.Drawing.Point(6, 85);
            this.lbl_McOPTime.Name = "lbl_McOPTime";
            this.lbl_McOPTime.Size = new System.Drawing.Size(188, 22);
            this.lbl_McOPTime.TabIndex = 66;
            this.lbl_McOPTime.Text = "0";
            this.lbl_McOPTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(188, 22);
            this.label10.TabIndex = 65;
            this.label10.Text = "Machine Operation Time:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMiddle
            // 
            this.pnlMiddle.Controls.Add(this.pbMachine);
            this.pnlMiddle.Controls.Add(this.pnlLotInfo);
            this.pnlMiddle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMiddle.Location = new System.Drawing.Point(400, 45);
            this.pnlMiddle.Name = "pnlMiddle";
            this.pnlMiddle.Size = new System.Drawing.Size(679, 633);
            this.pnlMiddle.TabIndex = 110;
            // 
            // pbMachine
            // 
            this.pbMachine.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.pbMachine.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMachine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMachine.Image = global::Machine.Properties.Resources.machine;
            this.pbMachine.Location = new System.Drawing.Point(0, 156);
            this.pbMachine.Name = "pbMachine";
            this.pbMachine.Size = new System.Drawing.Size(679, 477);
            this.pbMachine.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMachine.TabIndex = 1;
            this.pbMachine.TabStop = false;
            // 
            // pnlLotInfo
            // 
            this.pnlLotInfo.Controls.Add(this.gbLotInfo);
            this.pnlLotInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLotInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlLotInfo.Name = "pnlLotInfo";
            this.pnlLotInfo.Size = new System.Drawing.Size(679, 156);
            this.pnlLotInfo.TabIndex = 0;
            // 
            // gbLotInfo
            // 
            this.gbLotInfo.Controls.Add(this.lblProdNotFound);
            this.gbLotInfo.Controls.Add(this.txtDeviceID);
            this.gbLotInfo.Controls.Add(this.label2);
            this.gbLotInfo.Controls.Add(this.txtLotNo);
            this.gbLotInfo.Controls.Add(this.label1);
            this.gbLotInfo.Location = new System.Drawing.Point(6, 6);
            this.gbLotInfo.Name = "gbLotInfo";
            this.gbLotInfo.Size = new System.Drawing.Size(667, 147);
            this.gbLotInfo.TabIndex = 0;
            this.gbLotInfo.TabStop = false;
            this.gbLotInfo.Text = "Lot Info";
            // 
            // txtDeviceID
            // 
            this.txtDeviceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeviceID.Location = new System.Drawing.Point(185, 15);
            this.txtDeviceID.Name = "txtDeviceID";
            this.txtDeviceID.Size = new System.Drawing.Size(476, 31);
            this.txtDeviceID.TabIndex = 14;
            this.txtDeviceID.TextChanged += new System.EventHandler(this.txtDeviceID_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 25);
            this.label2.TabIndex = 13;
            this.label2.Text = "Product Number:";
            // 
            // txtLotNo
            // 
            this.txtLotNo.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtLotNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNo.Location = new System.Drawing.Point(153, 100);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.Size = new System.Drawing.Size(256, 26);
            this.txtLotNo.TabIndex = 6;
            this.txtLotNo.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Lot Number:";
            this.label1.Visible = false;
            // 
            // lblProdNotFound
            // 
            this.lblProdNotFound.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblProdNotFound.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProdNotFound.ForeColor = System.Drawing.Color.Red;
            this.lblProdNotFound.Location = new System.Drawing.Point(182, 49);
            this.lblProdNotFound.Name = "lblProdNotFound";
            this.lblProdNotFound.Size = new System.Drawing.Size(188, 22);
            this.lblProdNotFound.TabIndex = 70;
            this.lblProdNotFound.Text = "** Product Not Found! **";
            this.lblProdNotFound.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProdNotFound.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.groupBox3.ForeColor = System.Drawing.Color.DarkBlue;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 633);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Machine Perfomance";
            // 
            // uctrlAuto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMiddle);
            this.Controls.Add(this.pnlLeft);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTitle);
            this.Controls.Add(this.pnlRight);
            this.Name = "uctrlAuto";
            this.Size = new System.Drawing.Size(1374, 838);
            this.pnlRight.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.pnlTitle.ResumeLayout(false);
            this.pnlBottom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSeqNum)).EndInit();
            this.pnlLog.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLeftRight.ResumeLayout(false);
            this.pnlLeftLeft.ResumeLayout(false);
            this.gbMcPerformance.ResumeLayout(false);
            this.pnlMiddle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbMachine)).EndInit();
            this.pnlLotInfo.ResumeLayout(false);
            this.gbLotInfo.ResumeLayout(false);
            this.gbLotInfo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_Mode;
        private System.Windows.Forms.Button btn_SystemInit;
        private System.Windows.Forms.Button btn_LotInfo;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label lbl_MachineState;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Restart;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.ListBox lbox_Log;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlMiddle;
        private System.Windows.Forms.Panel pnlLotInfo;
        private System.Windows.Forms.GroupBox gbLotInfo;
        private System.Windows.Forms.TextBox txtDeviceID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtLotNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvSeqNum;
        private System.Windows.Forms.Panel pnlLog;
        private System.Windows.Forms.PictureBox pbMachine;
        private System.Windows.Forms.Panel pnlLeftRight;
        private System.Windows.Forms.Panel pnlLeftLeft;
        private System.Windows.Forms.GroupBox gbMcPerformance;
        private System.Windows.Forms.Label lbl_McRunTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_McIdleTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_McOPTime;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblProdNotFound;
        private System.Windows.Forms.GroupBox groupBox3;
    }
}
