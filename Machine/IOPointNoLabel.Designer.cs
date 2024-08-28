
namespace Machine
{
    partial class IOPointNoLabel
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
            this.lbl_Display = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Display
            // 
            this.lbl_Display.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Display.Location = new System.Drawing.Point(-1, 0);
            this.lbl_Display.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Display.Name = "lbl_Display";
            this.lbl_Display.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lbl_Display.Size = new System.Drawing.Size(150, 28);
            this.lbl_Display.TabIndex = 4;
            this.lbl_Display.Text = "IOPoint";
            this.lbl_Display.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_Display.Click += new System.EventHandler(this.IOPoint_Click);
            // 
            // IOPointNoLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbl_Display);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "IOPointNoLabel";
            this.Size = new System.Drawing.Size(150, 28);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lbl_Display;
    }
}
