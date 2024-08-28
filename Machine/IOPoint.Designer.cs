namespace Machine
{
    partial class IOPoint
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
            this.button1 = new System.Windows.Forms.Button();
            this.lbl_Label = new System.Windows.Forms.Label();
            this.lbl_Display = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Black;
            this.button1.Enabled = false;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.Location = new System.Drawing.Point(61, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(2, 25);
            this.button1.TabIndex = 6;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // lbl_Label
            // 
            this.lbl_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Label.Location = new System.Drawing.Point(3, 0);
            this.lbl_Label.Name = "lbl_Label";
            this.lbl_Label.Size = new System.Drawing.Size(52, 23);
            this.lbl_Label.TabIndex = 5;
            this.lbl_Label.Text = "IOLabel";
            this.lbl_Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Label.Click += new System.EventHandler(this.IOPoint_Click);
            // 
            // lbl_Display
            // 
            this.lbl_Display.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Display.Location = new System.Drawing.Point(69, 0);
            this.lbl_Display.Name = "lbl_Display";
            this.lbl_Display.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Display.Size = new System.Drawing.Size(170, 23);
            this.lbl_Display.TabIndex = 4;
            this.lbl_Display.Text = "IOPoint";
            this.lbl_Display.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_Display.Click += new System.EventHandler(this.IOPoint_Click);
            // 
            // IOPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_Label);
            this.Controls.Add(this.lbl_Display);
            this.Name = "IOPoint";
            this.Size = new System.Drawing.Size(238, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbl_Label;
        private System.Windows.Forms.Label lbl_Display;
    }
}
