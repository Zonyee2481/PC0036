namespace Machine
{
  partial class uctrlDeviceRecipe
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
      this.btn_DeleteDeviceRecipe = new System.Windows.Forms.Button();
      this.btn_EditDeviceRecipe = new System.Windows.Forms.Button();
      this.btn_AddDeviceRecipe = new System.Windows.Forms.Button();
      this.groupBox43 = new System.Windows.Forms.GroupBox();
      this.lv_DeviceRecipeList = new System.Windows.Forms.ListView();
      this.panel2 = new System.Windows.Forms.Panel();
      this.lbl_Title = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.groupBox43.SuspendLayout();
      this.panel2.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.Controls.Add(this.btn_DeleteDeviceRecipe);
      this.panel1.Controls.Add(this.btn_EditDeviceRecipe);
      this.panel1.Controls.Add(this.btn_AddDeviceRecipe);
      this.panel1.Controls.Add(this.groupBox43);
      this.panel1.Controls.Add(this.panel2);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(1374, 838);
      this.panel1.TabIndex = 1;
      // 
      // btn_DeleteDeviceRecipe
      // 
      this.btn_DeleteDeviceRecipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_DeleteDeviceRecipe.ForeColor = System.Drawing.Color.DarkBlue;
      this.btn_DeleteDeviceRecipe.Location = new System.Drawing.Point(247, 65);
      this.btn_DeleteDeviceRecipe.Name = "btn_DeleteDeviceRecipe";
      this.btn_DeleteDeviceRecipe.Size = new System.Drawing.Size(113, 50);
      this.btn_DeleteDeviceRecipe.TabIndex = 165;
      this.btn_DeleteDeviceRecipe.Text = "Delete";
      this.btn_DeleteDeviceRecipe.UseVisualStyleBackColor = true;
      this.btn_DeleteDeviceRecipe.Click += new System.EventHandler(this.btn_DeleteDeviceRecipe_Click);
      // 
      // btn_EditDeviceRecipe
      // 
      this.btn_EditDeviceRecipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_EditDeviceRecipe.ForeColor = System.Drawing.Color.DarkBlue;
      this.btn_EditDeviceRecipe.Location = new System.Drawing.Point(128, 65);
      this.btn_EditDeviceRecipe.Name = "btn_EditDeviceRecipe";
      this.btn_EditDeviceRecipe.Size = new System.Drawing.Size(113, 50);
      this.btn_EditDeviceRecipe.TabIndex = 164;
      this.btn_EditDeviceRecipe.Text = "Edit";
      this.btn_EditDeviceRecipe.UseVisualStyleBackColor = true;
      this.btn_EditDeviceRecipe.Click += new System.EventHandler(this.btn_EditDeviceRecipe_Click);
      // 
      // btn_AddDeviceRecipe
      // 
      this.btn_AddDeviceRecipe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.btn_AddDeviceRecipe.ForeColor = System.Drawing.Color.DarkBlue;
      this.btn_AddDeviceRecipe.Location = new System.Drawing.Point(9, 65);
      this.btn_AddDeviceRecipe.Name = "btn_AddDeviceRecipe";
      this.btn_AddDeviceRecipe.Size = new System.Drawing.Size(113, 50);
      this.btn_AddDeviceRecipe.TabIndex = 163;
      this.btn_AddDeviceRecipe.Text = "Add";
      this.btn_AddDeviceRecipe.UseVisualStyleBackColor = true;
      this.btn_AddDeviceRecipe.Click += new System.EventHandler(this.btn_AddDeviceRecipe_Click);
      // 
      // groupBox43
      // 
      this.groupBox43.Controls.Add(this.lv_DeviceRecipeList);
      this.groupBox43.Location = new System.Drawing.Point(2, 121);
      this.groupBox43.Name = "groupBox43";
      this.groupBox43.Size = new System.Drawing.Size(1177, 767);
      this.groupBox43.TabIndex = 93;
      this.groupBox43.TabStop = false;
      this.groupBox43.Text = "Device List";
      // 
      // lv_DeviceRecipeList
      // 
      this.lv_DeviceRecipeList.FullRowSelect = true;
      this.lv_DeviceRecipeList.GridLines = true;
      this.lv_DeviceRecipeList.HideSelection = false;
      this.lv_DeviceRecipeList.Location = new System.Drawing.Point(17, 21);
      this.lv_DeviceRecipeList.MultiSelect = false;
      this.lv_DeviceRecipeList.Name = "lv_DeviceRecipeList";
      this.lv_DeviceRecipeList.Size = new System.Drawing.Size(1126, 740);
      this.lv_DeviceRecipeList.TabIndex = 44;
      this.lv_DeviceRecipeList.UseCompatibleStateImageBehavior = false;
      this.lv_DeviceRecipeList.View = System.Windows.Forms.View.Details;
      // 
      // panel2
      // 
      this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.panel2.Controls.Add(this.lbl_Title);
      this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
      this.panel2.Location = new System.Drawing.Point(0, 0);
      this.panel2.Name = "panel2";
      this.panel2.Size = new System.Drawing.Size(1374, 59);
      this.panel2.TabIndex = 0;
      // 
      // lbl_Title
      // 
      this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.lbl_Title.Dock = System.Windows.Forms.DockStyle.Fill;
      this.lbl_Title.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.lbl_Title.Location = new System.Drawing.Point(0, 0);
      this.lbl_Title.Name = "lbl_Title";
      this.lbl_Title.Size = new System.Drawing.Size(1370, 55);
      this.lbl_Title.TabIndex = 62;
      this.lbl_Title.Text = "Device Recipe";
      this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // uctrlDeviceRecipe
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.panel1);
      this.Name = "uctrlDeviceRecipe";
      this.Size = new System.Drawing.Size(1374, 838);
      this.panel1.ResumeLayout(false);
      this.groupBox43.ResumeLayout(false);
      this.panel2.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Button btn_DeleteDeviceRecipe;
    private System.Windows.Forms.Button btn_EditDeviceRecipe;
    private System.Windows.Forms.Button btn_AddDeviceRecipe;
    private System.Windows.Forms.GroupBox groupBox43;
    private System.Windows.Forms.ListView lv_DeviceRecipeList;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Label lbl_Title;
  }
}
