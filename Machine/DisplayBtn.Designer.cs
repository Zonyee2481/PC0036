
namespace Machine
{
    partial class DisplayBtn
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
            this.btn_AxisName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_AxisName
            // 
            this.btn_AxisName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_AxisName.Location = new System.Drawing.Point(0, 0);
            this.btn_AxisName.Margin = new System.Windows.Forms.Padding(0);
            this.btn_AxisName.Name = "btn_AxisName";
            this.btn_AxisName.Size = new System.Drawing.Size(250, 60);
            this.btn_AxisName.TabIndex = 5;
            this.btn_AxisName.Text = "Axis Name";
            this.btn_AxisName.UseVisualStyleBackColor = true;
            this.btn_AxisName.Click += new System.EventHandler(this.btn_AxisName_Click);
            // 
            // DisplayBtn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_AxisName);
            this.Name = "DisplayBtn";
            this.Size = new System.Drawing.Size(250, 60);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_AxisName;
    }
}
