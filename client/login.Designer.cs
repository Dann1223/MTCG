namespace client
{
    partial class login
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MTCGLogin = new System.Windows.Forms.Button();
            this.MTCGRegister = new System.Windows.Forms.Button();
            this.MTCGClear = new System.Windows.Forms.Button();
            this.MTCGEmail = new System.Windows.Forms.Label();
            this.MTCGPassword = new System.Windows.Forms.Label();
            this.textPassword = new System.Windows.Forms.TextBox();
            this.textEmail = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // MTCGLogin
            // 
            this.MTCGLogin.Location = new System.Drawing.Point(135, 308);
            this.MTCGLogin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MTCGLogin.Name = "MTCGLogin";
            this.MTCGLogin.Size = new System.Drawing.Size(112, 34);
            this.MTCGLogin.TabIndex = 0;
            this.MTCGLogin.Text = "Login";
            this.MTCGLogin.UseVisualStyleBackColor = true;
            this.MTCGLogin.Click += new System.EventHandler(this.MTCGLogin_Click);
            // 
            // MTCGRegister
            // 
            this.MTCGRegister.Location = new System.Drawing.Point(328, 308);
            this.MTCGRegister.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MTCGRegister.Name = "MTCGRegister";
            this.MTCGRegister.Size = new System.Drawing.Size(112, 34);
            this.MTCGRegister.TabIndex = 1;
            this.MTCGRegister.Text = "Register";
            this.MTCGRegister.UseVisualStyleBackColor = true;
            this.MTCGRegister.Click += new System.EventHandler(this.MTCGRegister_Click);
            // 
            // MTCGClear
            // 
            this.MTCGClear.Location = new System.Drawing.Point(510, 308);
            this.MTCGClear.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MTCGClear.Name = "MTCGClear";
            this.MTCGClear.Size = new System.Drawing.Size(112, 34);
            this.MTCGClear.TabIndex = 2;
            this.MTCGClear.Text = "Clear";
            this.MTCGClear.UseVisualStyleBackColor = true;
            this.MTCGClear.Click += new System.EventHandler(this.MTCGClear_Click);
            // 
            // MTCGEmail
            // 
            this.MTCGEmail.AutoSize = true;
            this.MTCGEmail.Location = new System.Drawing.Point(132, 109);
            this.MTCGEmail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MTCGEmail.Name = "MTCGEmail";
            this.MTCGEmail.Size = new System.Drawing.Size(62, 18);
            this.MTCGEmail.TabIndex = 3;
            this.MTCGEmail.Text = "E-Mail";
            // 
            // MTCGPassword
            // 
            this.MTCGPassword.AutoSize = true;
            this.MTCGPassword.Location = new System.Drawing.Point(132, 205);
            this.MTCGPassword.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.MTCGPassword.Name = "MTCGPassword";
            this.MTCGPassword.Size = new System.Drawing.Size(80, 18);
            this.MTCGPassword.TabIndex = 4;
            this.MTCGPassword.Text = "Password";
            // 
            // textPassword
            // 
            this.textPassword.Location = new System.Drawing.Point(322, 205);
            this.textPassword.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textPassword.Name = "textPassword";
            this.textPassword.Size = new System.Drawing.Size(300, 28);
            this.textPassword.TabIndex = 5;
            this.textPassword.TextChanged += new System.EventHandler(this.textPassword_TextChanged);
            // 
            // textEmail
            // 
            this.textEmail.Location = new System.Drawing.Point(322, 99);
            this.textEmail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textEmail.Name = "textEmail";
            this.textEmail.Size = new System.Drawing.Size(300, 28);
            this.textEmail.TabIndex = 6;
            this.textEmail.TextChanged += new System.EventHandler(this.textEmail_TextChanged);
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 527);
            this.Controls.Add(this.textEmail);
            this.Controls.Add(this.textPassword);
            this.Controls.Add(this.MTCGPassword);
            this.Controls.Add(this.MTCGEmail);
            this.Controls.Add(this.MTCGClear);
            this.Controls.Add(this.MTCGRegister);
            this.Controls.Add(this.MTCGLogin);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "login";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    #endregion

    private System.Windows.Forms.Button MTCGLogin;
    private System.Windows.Forms.Button MTCGRegister;
    private System.Windows.Forms.Button MTCGClear;
    private System.Windows.Forms.Label MTCGEmail;
    private System.Windows.Forms.Label MTCGPassword;
    private System.Windows.Forms.TextBox textPassword;
    private System.Windows.Forms.TextBox textEmail;
  }
}

