
namespace Machine
{
    partial class frmLotInformation
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
            this.txtLotNo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbProductNo = new System.Windows.Forms.ComboBox();
            this.txtProductNo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDateTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCounterNo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnEndLot = new System.Windows.Forms.Button();
            this.btnStartLot = new System.Windows.Forms.Button();
            this.tmrUpdateDisplay = new System.Windows.Forms.Timer(this.components);
            this.txt2DID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtLotNo
            // 
            this.txtLotNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLotNo.Location = new System.Drawing.Point(159, 20);
            this.txtLotNo.Name = "txtLotNo";
            this.txtLotNo.Size = new System.Drawing.Size(256, 26);
            this.txtLotNo.TabIndex = 4;
            this.txtLotNo.TextChanged += new System.EventHandler(this.txtLotNo_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lot Number:";
            // 
            // cmbProductNo
            // 
            this.cmbProductNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbProductNo.FormattingEnabled = true;
            this.cmbProductNo.Location = new System.Drawing.Point(159, 52);
            this.cmbProductNo.Name = "cmbProductNo";
            this.cmbProductNo.Size = new System.Drawing.Size(161, 28);
            this.cmbProductNo.TabIndex = 14;
            this.cmbProductNo.SelectedIndexChanged += new System.EventHandler(this.cmbProductNo_SelectedIndexChanged);
            // 
            // txtProductNo
            // 
            this.txtProductNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProductNo.Location = new System.Drawing.Point(159, 52);
            this.txtProductNo.Name = "txtProductNo";
            this.txtProductNo.Size = new System.Drawing.Size(256, 26);
            this.txtProductNo.TabIndex = 13;
            this.txtProductNo.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.Location = new System.Drawing.Point(12, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 12;
            this.label2.Text = "Product Number:";
            // 
            // txtDateTime
            // 
            this.txtDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDateTime.Location = new System.Drawing.Point(159, 86);
            this.txtDateTime.Name = "txtDateTime";
            this.txtDateTime.ReadOnly = true;
            this.txtDateTime.Size = new System.Drawing.Size(256, 26);
            this.txtDateTime.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.Location = new System.Drawing.Point(12, 89);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Year/Month:";
            // 
            // txtCounterNo
            // 
            this.txtCounterNo.Enabled = false;
            this.txtCounterNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCounterNo.Location = new System.Drawing.Point(159, 118);
            this.txtCounterNo.Name = "txtCounterNo";
            this.txtCounterNo.ReadOnly = true;
            this.txtCounterNo.Size = new System.Drawing.Size(89, 26);
            this.txtCounterNo.TabIndex = 18;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label3.Location = new System.Drawing.Point(12, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 17;
            this.label3.Text = "Counter Number:";
            // 
            // btnEndLot
            // 
            this.btnEndLot.Location = new System.Drawing.Point(306, 193);
            this.btnEndLot.Name = "btnEndLot";
            this.btnEndLot.Size = new System.Drawing.Size(109, 33);
            this.btnEndLot.TabIndex = 20;
            this.btnEndLot.Text = "End Lot";
            this.btnEndLot.UseVisualStyleBackColor = true;
            this.btnEndLot.Click += new System.EventHandler(this.btnEndLot_Click);
            // 
            // btnStartLot
            // 
            this.btnStartLot.Location = new System.Drawing.Point(191, 193);
            this.btnStartLot.Name = "btnStartLot";
            this.btnStartLot.Size = new System.Drawing.Size(109, 33);
            this.btnStartLot.TabIndex = 19;
            this.btnStartLot.Text = "Start Lot";
            this.btnStartLot.UseVisualStyleBackColor = true;
            this.btnStartLot.Click += new System.EventHandler(this.btnStartLot_Click);
            // 
            // tmrUpdateDisplay
            // 
            this.tmrUpdateDisplay.Enabled = true;
            this.tmrUpdateDisplay.Tick += new System.EventHandler(this.tmrUpdateDisplay_Tick);
            // 
            // txt2DID
            // 
            this.txt2DID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt2DID.Location = new System.Drawing.Point(159, 150);
            this.txt2DID.Name = "txt2DID";
            this.txt2DID.ReadOnly = true;
            this.txt2DID.Size = new System.Drawing.Size(256, 26);
            this.txt2DID.TabIndex = 22;
            this.txt2DID.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label5.Location = new System.Drawing.Point(12, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 20);
            this.label5.TabIndex = 21;
            this.label5.Text = "Barcode:";
            this.label5.Visible = false;
            // 
            // btn_Close
            // 
            this.btn_Close.Location = new System.Drawing.Point(306, 232);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(109, 33);
            this.btn_Close.TabIndex = 23;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // frmLotInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(435, 276);
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.txt2DID);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnEndLot);
            this.Controls.Add(this.btnStartLot);
            this.Controls.Add(this.txtCounterNo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtDateTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbProductNo);
            this.Controls.Add(this.txtProductNo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtLotNo);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmLotInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLotInformation";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmLotInformation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtLotNo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbProductNo;
        private System.Windows.Forms.TextBox txtProductNo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDateTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCounterNo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnEndLot;
        private System.Windows.Forms.Button btnStartLot;
        private System.Windows.Forms.Timer tmrUpdateDisplay;
        private System.Windows.Forms.TextBox txt2DID;
        private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Button btn_Close;
  }
}