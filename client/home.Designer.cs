namespace client
{
  partial class home
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
            this.MTCGName = new System.Windows.Forms.Label();
            this.MTCGGold = new System.Windows.Forms.Label();
            this.MTCGToken = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // MTCGName
            // 
            this.MTCGName.AutoSize = true;
            this.MTCGName.Location = new System.Drawing.Point(211, 74);
            this.MTCGName.Name = "MTCGName";
            this.MTCGName.Size = new System.Drawing.Size(62, 18);
            this.MTCGName.TabIndex = 0;
            this.MTCGName.Text = "label1";
            this.MTCGName.Click += new System.EventHandler(this.MTCGName_Click);
            // 
            // MTCGGold
            // 
            this.MTCGGold.AutoSize = true;
            this.MTCGGold.Location = new System.Drawing.Point(211, 162);
            this.MTCGGold.Name = "MTCGGold";
            this.MTCGGold.Size = new System.Drawing.Size(62, 18);
            this.MTCGGold.TabIndex = 1;
            this.MTCGGold.Text = "label2";
            this.MTCGGold.Click += new System.EventHandler(this.MTCGGold_Click);
            // 
            // MTCGToken
            // 
            this.MTCGToken.AutoSize = true;
            this.MTCGToken.Location = new System.Drawing.Point(211, 249);
            this.MTCGToken.Name = "MTCGToken";
            this.MTCGToken.Size = new System.Drawing.Size(62, 18);
            this.MTCGToken.TabIndex = 2;
            this.MTCGToken.Text = "label3";
            // 
            // home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MTCGToken);
            this.Controls.Add(this.MTCGGold);
            this.Controls.Add(this.MTCGName);
            this.Name = "home";
            this.Text = "home";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label MTCGName;
    private System.Windows.Forms.Label MTCGGold;
    private System.Windows.Forms.Label MTCGToken;
  }
}