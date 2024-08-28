
namespace Machine
{
    partial class frmChart
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
            this.motionIO_groupBox = new System.Windows.Forms.GroupBox();
            this.button_UpdChart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // motionIO_groupBox
            // 
            this.motionIO_groupBox.Location = new System.Drawing.Point(2, 40);
            this.motionIO_groupBox.Margin = new System.Windows.Forms.Padding(2);
            this.motionIO_groupBox.Name = "motionIO_groupBox";
            this.motionIO_groupBox.Padding = new System.Windows.Forms.Padding(2);
            this.motionIO_groupBox.Size = new System.Drawing.Size(1596, 857);
            this.motionIO_groupBox.TabIndex = 178;
            this.motionIO_groupBox.TabStop = false;
            // 
            // button_UpdChart
            // 
            this.button_UpdChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_UpdChart.ForeColor = System.Drawing.Color.DarkBlue;
            this.button_UpdChart.Location = new System.Drawing.Point(0, 0);
            this.button_UpdChart.Name = "button_UpdChart";
            this.button_UpdChart.Size = new System.Drawing.Size(97, 35);
            this.button_UpdChart.TabIndex = 179;
            this.button_UpdChart.Text = "Update";
            this.button_UpdChart.UseVisualStyleBackColor = true;
            this.button_UpdChart.Click += new System.EventHandler(this.button_UpdChart_Click);
            // 
            // frmChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_UpdChart);
            this.Controls.Add(this.motionIO_groupBox);
            this.Name = "frmChart";
            this.Size = new System.Drawing.Size(1600, 899);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox motionIO_groupBox;
        private System.Windows.Forms.Button button_UpdChart;
    }
}