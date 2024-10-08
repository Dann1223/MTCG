namespace client
{
  partial class register
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
            this.MTCGRegister = new System.Windows.Forms.Button();
            this.MTCGClear = new System.Windows.Forms.Button();
            this.MTCGEmail = new System.Windows.Forms.Label();
            this.MTCGName = new System.Windows.Forms.Label();
            this.MTCGPassword = new System.Windows.Forms.Label();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.textName = new System.Windows.Forms.TextBox();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MTCGRegister
            // 
            this.MTCGRegister.Location = new System.Drawing.Point(191, 395);
            this.MTCGRegister.Margin = new System.Windows.Forms.Padding(4);
            this.MTCGRegister.Name = "MTCGRegister";
            this.MTCGRegister.Size = new System.Drawing.Size(144, 34);
            this.MTCGRegister.TabIndex = 0;
            this.MTCGRegister.Text = "Register";
            this.MTCGRegister.UseVisualStyleBackColor = true;
            this.MTCGRegister.Click += new System.EventHandler(this.MTCGRegister_Click);
            // 
            // MTCGClear
            // 
            this.MTCGClear.Location = new System.Drawing.Point(432, 395);
            this.MTCGClear.Margin = new System.Windows.Forms.Padding(4);
            this.MTCGClear.Name = "MTCGClear";
            this.MTCGClear.Size = new System.Drawing.Size(141, 34);
            this.MTCGClear.TabIndex = 1;
            this.MTCGClear.Text = "Clear";
            this.MTCGClear.UseVisualStyleBackColor = true;
            this.MTCGClear.Click += new System.EventHandler(this.MTCGClear_Click);
            // 
            // MTCGEmail
            // 
            this.MTCGEmail.AutoSize = true;
            this.MTCGEmail.Location = new System.Drawing.Point(188, 98);
            this.MTCGEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MTCGEmail.Name = "MTCGEmail";
            this.MTCGEmail.Size = new System.Drawing.Size(62, 18);
            this.MTCGEmail.TabIndex = 2;
            this.MTCGEmail.Text = "E-Mail";
            // 
            // MTCGName
            // 
            this.MTCGName.AutoSize = true;
            this.MTCGName.Location = new System.Drawing.Point(188, 200);
            this.MTCGName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MTCGName.Name = "MTCGName";
            this.MTCGName.Size = new System.Drawing.Size(44, 18);
            this.MTCGName.TabIndex = 3;
            this.MTCGName.Text = "Name";
            // 
            // MTCGPassword
            // 
            this.MTCGPassword.AutoSize = true;
            this.MTCGPassword.Location = new System.Drawing.Point(188, 304);
            this.MTCGPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MTCGPassword.Name = "MTCGPassword";
            this.MTCGPassword.Size = new System.Drawing.Size(80, 18);
            this.MTCGPassword.TabIndex = 4;
            this.MTCGPassword.Text = "Password";
            // 
            // textEmail
            // 
            this.textEmail.Location = new System.Drawing.Point(371, 98);
            this.textEmail.Margin = new System.Windows.Forms.Padding(4);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(202, 28);
            this.textEmail.TabIndex = 5;
            this.textEmail.TextChanged += new System.EventHandler(this.textEmail_TextChanged);
            // 
            // textName
            // 
            this.textName.Location = new System.Drawing.Point(371, 200);
            this.textName.Margin = new System.Windows.Forms.Padding(4);
            this.textName.Name = "textName";
            this.textName.Size = new System.Drawing.Size(202, 28);
            this.textName.TabIndex = 6;
            this.textName.TextChanged += new System.EventHandler(this.textName_TextChanged);
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(371, 301);
            this.textPassword.Margin = new System.Windows.Forms.Padding(4);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(202, 28);
            this.textPassword.TabIndex = 7;
            this.textPassword.TextChanged += new System.EventHandler(this.textPassword_TextChanged);
            // 
            // register
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 530);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.textName);
            this.Controls.Add(this.textEmail);
            this.Controls.Add(this.MTCGPassword);
            this.Controls.Add(this.MTCGName);
            this.Controls.Add(this.MTCGEmail);
            this.Controls.Add(this.MTCGClear);
            this.Controls.Add(this.MTCGRegister);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "register";
            this.Text = "register";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button MTCGRegister;
    private System.Windows.Forms.Button MTCGClear;
    private System.Windows.Forms.Label MTCGEmail;
    private System.Windows.Forms.Label MTCGName;
    private System.Windows.Forms.Label MTCGPassword;
    private System.Windows.Forms.TextBox textEmail;
    private System.Windows.Forms.TextBox textName;
    private System.Windows.Forms.TextBox textPassword;
  }
}