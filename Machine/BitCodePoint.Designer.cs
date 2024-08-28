
namespace Machine
{
    partial class BitCodePoint
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblIndex = new System.Windows.Forms.Label();
            this.cbBitCode_8 = new System.Windows.Forms.CheckBox();
            this.cbBitCode_4 = new System.Windows.Forms.CheckBox();
            this.cbBitCode_2 = new System.Windows.Forms.CheckBox();
            this.cbBitCode_1 = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.lblIndex);
            this.panel1.Controls.Add(this.cbBitCode_8);
            this.panel1.Controls.Add(this.cbBitCode_4);
            this.panel1.Controls.Add(this.cbBitCode_2);
            this.panel1.Controls.Add(this.cbBitCode_1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 29);
            this.panel1.TabIndex = 0;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(373, 5);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.ReadOnly = true;
            this.txtDescription.Size = new System.Drawing.Size(240, 20);
            this.txtDescription.TabIndex = 7;
            this.txtDescription.DoubleClick += new System.EventHandler(this.txtDescription_DoubleClick);
            // 
            // lblIndex
            // 
            this.lblIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIndex.Location = new System.Drawing.Point(3, 5);
            this.lblIndex.Name = "lblIndex";
            this.lblIndex.Size = new System.Drawing.Size(40, 19);
            this.lblIndex.TabIndex = 5;
            this.lblIndex.Text = "No";
            this.lblIndex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbBitCode_8
            // 
            this.cbBitCode_8.AutoSize = true;
            this.cbBitCode_8.Location = new System.Drawing.Point(292, 7);
            this.cbBitCode_8.Name = "cbBitCode_8";
            this.cbBitCode_8.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_8.TabIndex = 4;
            this.cbBitCode_8.Text = "Bit Code 8";
            this.cbBitCode_8.UseVisualStyleBackColor = true;
            this.cbBitCode_8.Click += new System.EventHandler(this.cbBitCode_8_Click);
            // 
            // cbBitCode_4
            // 
            this.cbBitCode_4.AutoSize = true;
            this.cbBitCode_4.Location = new System.Drawing.Point(211, 7);
            this.cbBitCode_4.Name = "cbBitCode_4";
            this.cbBitCode_4.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_4.TabIndex = 3;
            this.cbBitCode_4.Text = "Bit Code 4";
            this.cbBitCode_4.UseVisualStyleBackColor = true;
            this.cbBitCode_4.Click += new System.EventHandler(this.cbBitCode_4_Click);
            // 
            // cbBitCode_2
            // 
            this.cbBitCode_2.AutoSize = true;
            this.cbBitCode_2.Location = new System.Drawing.Point(130, 7);
            this.cbBitCode_2.Name = "cbBitCode_2";
            this.cbBitCode_2.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_2.TabIndex = 2;
            this.cbBitCode_2.Text = "Bit Code 2";
            this.cbBitCode_2.UseVisualStyleBackColor = true;
            this.cbBitCode_2.Click += new System.EventHandler(this.cbBitCode_2_Click);
            // 
            // cbBitCode_1
            // 
            this.cbBitCode_1.AutoSize = true;
            this.cbBitCode_1.Location = new System.Drawing.Point(49, 7);
            this.cbBitCode_1.Name = "cbBitCode_1";
            this.cbBitCode_1.Size = new System.Drawing.Size(75, 17);
            this.cbBitCode_1.TabIndex = 1;
            this.cbBitCode_1.Text = "Bit Code 1";
            this.cbBitCode_1.UseVisualStyleBackColor = true;
            this.cbBitCode_1.Click += new System.EventHandler(this.cbBitCode_1_Click);
            // 
            // BitCodePoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "BitCodePoint";
            this.Size = new System.Drawing.Size(628, 29);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblIndex;
        private System.Windows.Forms.CheckBox cbBitCode_8;
        private System.Windows.Forms.CheckBox cbBitCode_4;
        private System.Windows.Forms.CheckBox cbBitCode_2;
        private System.Windows.Forms.CheckBox cbBitCode_1;
        private System.Windows.Forms.TextBox txtDescription;
    }
}
