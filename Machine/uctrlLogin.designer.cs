namespace Machine
{
    partial class uctrlLogin
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
      this.lbl_Welcome = new System.Windows.Forms.Label();
      this.pnl_Login = new System.Windows.Forms.Panel();
      this.txt_ID = new System.Windows.Forms.TextBox();
      this.btn_AccessSetup = new System.Windows.Forms.Button();
      this.btn_Cancel = new System.Windows.Forms.Button();
      this.cbox_Name = new System.Windows.Forms.ComboBox();
      this.btn_OK = new System.Windows.Forms.Button();
      this.tbox_Pswd = new System.Windows.Forms.TextBox();
      this.lbl_Pswd = new System.Windows.Forms.Label();
      this.lbl_User = new System.Windows.Forms.Label();
      this.llb_Login = new System.Windows.Forms.LinkLabel();
      this.pnl_Login.SuspendLayout();
      this.SuspendLayout();
      // 
      // lbl_Welcome
      // 
      this.lbl_Welcome.AutoSize = true;
      this.lbl_Welcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Welcome.Location = new System.Drawing.Point(12, 11);
      this.lbl_Welcome.Name = "lbl_Welcome";
      this.lbl_Welcome.Size = new System.Drawing.Size(146, 16);
      this.lbl_Welcome.TabIndex = 8;
      this.lbl_Welcome.Text = "Welcome Operator !";
      // 
      // pnl_Login
      // 
      this.pnl_Login.Controls.Add(this.txt_ID);
      this.pnl_Login.Controls.Add(this.btn_AccessSetup);
      this.pnl_Login.Controls.Add(this.btn_Cancel);
      this.pnl_Login.Controls.Add(this.cbox_Name);
      this.pnl_Login.Controls.Add(this.btn_OK);
      this.pnl_Login.Controls.Add(this.tbox_Pswd);
      this.pnl_Login.Controls.Add(this.lbl_Pswd);
      this.pnl_Login.Controls.Add(this.lbl_User);
      this.pnl_Login.ForeColor = System.Drawing.Color.DarkBlue;
      this.pnl_Login.Location = new System.Drawing.Point(15, 86);
      this.pnl_Login.Name = "pnl_Login";
      this.pnl_Login.Size = new System.Drawing.Size(421, 285);
      this.pnl_Login.TabIndex = 7;
      // 
      // txt_ID
      // 
      this.txt_ID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.txt_ID.ForeColor = System.Drawing.Color.DarkBlue;
      this.txt_ID.Location = new System.Drawing.Point(131, 11);
      this.txt_ID.Name = "txt_ID";
      this.txt_ID.Size = new System.Drawing.Size(224, 22);
      this.txt_ID.TabIndex = 1;
      this.txt_ID.WordWrap = false;
      this.txt_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_ID_KeyDown);
      // 
      // btn_AccessSetup
      // 
      this.btn_AccessSetup.AccessibleDescription = "";
      this.btn_AccessSetup.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btn_AccessSetup.BackColor = System.Drawing.SystemColors.Control;
      this.btn_AccessSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.btn_AccessSetup.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
      this.btn_AccessSetup.FlatAppearance.BorderSize = 2;
      this.btn_AccessSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
      this.btn_AccessSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
      this.btn_AccessSetup.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AccessSetup.ForeColor = System.Drawing.Color.DarkBlue;
      this.btn_AccessSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btn_AccessSetup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.btn_AccessSetup.Location = new System.Drawing.Point(131, 154);
      this.btn_AccessSetup.Name = "btn_AccessSetup";
      this.btn_AccessSetup.Size = new System.Drawing.Size(224, 44);
      this.btn_AccessSetup.TabIndex = 118;
      this.btn_AccessSetup.Text = "Access Setup";
      this.btn_AccessSetup.UseVisualStyleBackColor = false;
      this.btn_AccessSetup.Visible = false;
      this.btn_AccessSetup.Click += new System.EventHandler(this.btn_AccessSetup_Click);
      // 
      // btn_Cancel
      // 
      this.btn_Cancel.AccessibleDescription = "CANCEL";
      this.btn_Cancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btn_Cancel.BackColor = System.Drawing.SystemColors.Control;
      this.btn_Cancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.btn_Cancel.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
      this.btn_Cancel.FlatAppearance.BorderSize = 2;
      this.btn_Cancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
      this.btn_Cancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
      this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_Cancel.ForeColor = System.Drawing.Color.DarkBlue;
      this.btn_Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btn_Cancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.btn_Cancel.Location = new System.Drawing.Point(248, 67);
      this.btn_Cancel.Name = "btn_Cancel";
      this.btn_Cancel.Size = new System.Drawing.Size(108, 44);
      this.btn_Cancel.TabIndex = 4;
      this.btn_Cancel.Text = "CANCEL";
      this.btn_Cancel.UseVisualStyleBackColor = false;
      this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
      // 
      // cbox_Name
      // 
      this.cbox_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cbox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.cbox_Name.ForeColor = System.Drawing.Color.DarkBlue;
      this.cbox_Name.FormattingEnabled = true;
      this.cbox_Name.Location = new System.Drawing.Point(131, 204);
      this.cbox_Name.Name = "cbox_Name";
      this.cbox_Name.Size = new System.Drawing.Size(224, 24);
      this.cbox_Name.TabIndex = 111;
      this.cbox_Name.Visible = false;
      // 
      // btn_OK
      // 
      this.btn_OK.AccessibleDescription = "OK";
      this.btn_OK.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.btn_OK.BackColor = System.Drawing.SystemColors.Control;
      this.btn_OK.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
      this.btn_OK.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
      this.btn_OK.FlatAppearance.BorderSize = 2;
      this.btn_OK.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightGray;
      this.btn_OK.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightGray;
      this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_OK.ForeColor = System.Drawing.Color.DarkBlue;
      this.btn_OK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
      this.btn_OK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
      this.btn_OK.Location = new System.Drawing.Point(131, 67);
      this.btn_OK.Name = "btn_OK";
      this.btn_OK.Size = new System.Drawing.Size(108, 44);
      this.btn_OK.TabIndex = 3;
      this.btn_OK.Text = "OK";
      this.btn_OK.UseVisualStyleBackColor = false;
      this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
      // 
      // tbox_Pswd
      // 
      this.tbox_Pswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.tbox_Pswd.ForeColor = System.Drawing.Color.DarkBlue;
      this.tbox_Pswd.Location = new System.Drawing.Point(131, 39);
      this.tbox_Pswd.Name = "tbox_Pswd";
      this.tbox_Pswd.Size = new System.Drawing.Size(224, 22);
      this.tbox_Pswd.TabIndex = 2;
      this.tbox_Pswd.UseSystemPasswordChar = true;
      this.tbox_Pswd.WordWrap = false;
      this.tbox_Pswd.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbox_Pswd_KeyDown);
      // 
      // lbl_Pswd
      // 
      this.lbl_Pswd.BackColor = System.Drawing.SystemColors.Control;
      this.lbl_Pswd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Pswd.ForeColor = System.Drawing.Color.DarkBlue;
      this.lbl_Pswd.Location = new System.Drawing.Point(8, 39);
      this.lbl_Pswd.Name = "lbl_Pswd";
      this.lbl_Pswd.Size = new System.Drawing.Size(117, 23);
      this.lbl_Pswd.TabIndex = 112;
      this.lbl_Pswd.Text = "Password";
      this.lbl_Pswd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // lbl_User
      // 
      this.lbl_User.BackColor = System.Drawing.SystemColors.Control;
      this.lbl_User.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_User.ForeColor = System.Drawing.Color.DarkBlue;
      this.lbl_User.Location = new System.Drawing.Point(3, 9);
      this.lbl_User.Name = "lbl_User";
      this.lbl_User.Size = new System.Drawing.Size(114, 23);
      this.lbl_User.TabIndex = 110;
      this.lbl_User.Text = "Badge Number";
      this.lbl_User.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // llb_Login
      // 
      this.llb_Login.AutoSize = true;
      this.llb_Login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.llb_Login.LinkColor = System.Drawing.Color.DarkBlue;
      this.llb_Login.Location = new System.Drawing.Point(12, 41);
      this.llb_Login.Name = "llb_Login";
      this.llb_Login.Size = new System.Drawing.Size(41, 16);
      this.llb_Login.TabIndex = 6;
      this.llb_Login.TabStop = true;
      this.llb_Login.Text = "Login";
      this.llb_Login.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llb_Login_LinkClicked);
      // 
      // uctrlLogin
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.lbl_Welcome);
      this.Controls.Add(this.pnl_Login);
      this.Controls.Add(this.llb_Login);
      this.ForeColor = System.Drawing.Color.DarkBlue;
      this.Name = "uctrlLogin";
      this.Size = new System.Drawing.Size(845, 636);
      this.Load += new System.EventHandler(this.uctrlLogin_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.uctrlLogin_KeyDown);
      this.pnl_Login.ResumeLayout(false);
      this.pnl_Login.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Welcome;
        private System.Windows.Forms.Panel pnl_Login;
        private System.Windows.Forms.TextBox txt_ID;
        private System.Windows.Forms.Button btn_AccessSetup;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.ComboBox cbox_Name;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TextBox tbox_Pswd;
        private System.Windows.Forms.Label lbl_Pswd;
        private System.Windows.Forms.Label lbl_User;
        private System.Windows.Forms.LinkLabel llb_Login;
    }
}
