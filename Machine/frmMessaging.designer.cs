using System.Collections.Generic;

namespace Machine
{
    partial class frmMessaging
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
            this.lbl_MessageInfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.lbl_Msg = new System.Windows.Forms.Label();
            this.btn_Retry = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_AlmClr = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl_MessageInfo);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.btn_Retry);
            this.panel1.Controls.Add(this.btn_OK);
            this.panel1.Controls.Add(this.btn_Stop);
            this.panel1.Controls.Add(this.btn_Cancel);
            this.panel1.Controls.Add(this.btn_AlmClr);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(544, 258);
            this.panel1.TabIndex = 8;
            // 
            // lbl_MessageInfo
            // 
            this.lbl_MessageInfo.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_MessageInfo.Location = new System.Drawing.Point(10, 228);
            this.lbl_MessageInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_MessageInfo.Name = "lbl_MessageInfo";
            this.lbl_MessageInfo.Size = new System.Drawing.Size(483, 15);
            this.lbl_MessageInfo.TabIndex = 3;
            this.lbl_MessageInfo.Text = "lbl_MessageInfo";
            this.lbl_MessageInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_MessageInfo.Visible = false;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.lbl_Msg);
            this.panel3.Location = new System.Drawing.Point(10, 62);
            this.panel3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(523, 119);
            this.panel3.TabIndex = 9;
            // 
            // lbl_Msg
            // 
            this.lbl_Msg.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Msg.Location = new System.Drawing.Point(2, 11);
            this.lbl_Msg.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Msg.Name = "lbl_Msg";
            this.lbl_Msg.Size = new System.Drawing.Size(520, 97);
            this.lbl_Msg.TabIndex = 4;
            this.lbl_Msg.Text = "lbl_Msg";
            this.lbl_Msg.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btn_Retry
            // 
            this.btn_Retry.Location = new System.Drawing.Point(118, 186);
            this.btn_Retry.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Retry.Name = "btn_Retry";
            this.btn_Retry.Size = new System.Drawing.Size(98, 41);
            this.btn_Retry.TabIndex = 8;
            this.btn_Retry.Text = "Retry";
            this.btn_Retry.UseVisualStyleBackColor = true;
            this.btn_Retry.Click += new System.EventHandler(this.btn_Retry_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(224, 186);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(98, 41);
            this.btn_OK.TabIndex = 7;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(329, 186);
            this.btn_Stop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(98, 41);
            this.btn_Stop.TabIndex = 6;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(435, 186);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(98, 41);
            this.btn_Cancel.TabIndex = 5;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_AlmClr
            // 
            this.btn_AlmClr.Location = new System.Drawing.Point(10, 186);
            this.btn_AlmClr.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_AlmClr.Name = "btn_AlmClr";
            this.btn_AlmClr.Size = new System.Drawing.Size(98, 41);
            this.btn_AlmClr.TabIndex = 4;
            this.btn_AlmClr.Text = "Alarm Clear";
            this.btn_AlmClr.UseVisualStyleBackColor = true;
            this.btn_AlmClr.Click += new System.EventHandler(this.btn_AlmClr_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(10, 12);
            this.panel2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(523, 46);
            this.panel2.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(-1, 15);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(523, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "SYSTEM MESSAGE";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMessaging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 258);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmMessaging";
            this.Text = "frmMessaging";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_MessageInfo;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lbl_Msg;
        public System.Windows.Forms.Button btn_Retry;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Stop;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.Button btn_AlmClr;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
    }
}