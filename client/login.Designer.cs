namespace client
{
  partial class lblName
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
            this.lalLogin = new System.Windows.Forms.Button();
            this.lalRegister = new System.Windows.Forms.Button();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lalPassword = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lalLogin
            // 
            this.lalLogin.Location = new System.Drawing.Point(429, 254);
            this.lalLogin.Name = "lalLogin";
            this.lalLogin.Size = new System.Drawing.Size(118, 43);
            this.lalLogin.TabIndex = 0;
            this.lalLogin.Text = "login";
            this.lalLogin.UseVisualStyleBackColor = true;
            this.lalLogin.Click += new System.EventHandler(this.lalLogin_Click);
            // 
            // lalRegister
            // 
            this.lalRegister.Location = new System.Drawing.Point(220, 254);
            this.lalRegister.Name = "lalRegister";
            this.lalRegister.Size = new System.Drawing.Size(133, 43);
            this.lalRegister.TabIndex = 1;
            this.lalRegister.Text = "Register";
            this.lalRegister.UseVisualStyleBackColor = true;
            this.lalRegister.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(407, 106);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(100, 28);
            this.txtUsername.TabIndex = 2;
            this.txtUsername.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(407, 168);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 28);
            this.txtPassword.TabIndex = 3;
            this.txtPassword.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Username";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lalPassword
            // 
            this.lalPassword.AutoSize = true;
            this.lalPassword.Location = new System.Drawing.Point(246, 168);
            this.lalPassword.Name = "lalPassword";
            this.lalPassword.Size = new System.Drawing.Size(80, 18);
            this.lalPassword.TabIndex = 5;
            this.lalPassword.Text = "Password";
            this.lalPassword.Click += new System.EventHandler(this.lalPassword_Click);
            // 
            // lblName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lalPassword);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lalRegister);
            this.Controls.Add(this.lalLogin);
            this.Name = "lblName";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button lalLogin;
    private System.Windows.Forms.Button lalRegister;
    private System.Windows.Forms.TextBox txtUsername;
    private System.Windows.Forms.TextBox txtPassword;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lalPassword;
  }
}

