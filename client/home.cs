using System.Windows.Forms;
using System;

namespace client
{
  public partial class home : Form
  {
    // 用于显示用户信息的控件
    private Label labelName;
    private Label labelGold;
    private Label labelToken;

    // 接收用户信息的构造函数
    public home(string name, int gold, string token)
    {
      InitializeComponent();
      InitializeUserInfo(name, gold, token);
    }

    // 初始化用户信息的函数
    private void InitializeUserInfo(string name, int gold, string token)
    {
      // 创建并设置显示用户信息的控件
      labelName = new Label
      {
        Text = $"Name: {name}",
        Location = new System.Drawing.Point(10, 10), // 位置可以根据需要调整
        AutoSize = true
      };
      labelGold = new Label
      {
        Text = $"Gold: {gold}",
        Location = new System.Drawing.Point(10, 40),
        AutoSize = true
      };
      labelToken = new Label
      {
        Text = $"Token: {token}",
        Location = new System.Drawing.Point(10, 70),
        AutoSize = true
      };

      // 将控件添加到窗体
      MTCGName.Text = $"Welcome, {name}";
      MTCGGold.Text = $"Gold: {gold}";
      MTCGToken.Text = $"Token: {token}";
    }

    private void MTCGName_Click(object sender, EventArgs e)
    {

    }

    private void MTCGGold_Click(object sender, EventArgs e)
    {

    }
  }
}
