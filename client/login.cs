using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using System;

namespace client
{
  public partial class login : Form
  {
    private readonly HttpClient httpClient;

    public login()
    {
      InitializeComponent();
      httpClient = new HttpClient();
    }

    private async void MTCGLogin_Click(object sender, EventArgs e)
    {
      string email = textEmail.Text; // 使用指定的控件名称
      string password = textPassword.Text; // 使用指定的控件名称

      if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
      {
        MessageBox.Show("电子邮件和密码不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // 创建登录请求，直接使用 Email 作为属性名
      var loginRequest = new
      {
        Email = email, // 使用 Email 作为属性名
        Password = password
      };

      var json = JsonConvert.SerializeObject(loginRequest);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      try
      {
        HttpResponseMessage response = await httpClient.PostAsync("http://localhost:8000/login", content);
        string responseBody = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
          MessageBox.Show($"登录成功！服务器响应: {responseBody}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        else
        {
          MessageBox.Show($"登录失败: {responseBody}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"请求错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void MTCGClear_Click(object sender, EventArgs e)
    {
      // 清空输入框
      textEmail.Clear();
      textPassword.Clear();
    }

    private void textEmail_TextChanged(object sender, EventArgs e)
    {
      // 电子邮件文本框内容变化时的逻辑（如有需要可实现）
    }

    private void textPassword_TextChanged(object sender, EventArgs e)
    {
      // 密码文本框内容变化时的逻辑（如有需要可实现）
    }

    private void MTCGRegister_Click(object sender, EventArgs e)
    {
      register registerForm = new register(); // 确保使用小写的类名
      registerForm.Show(); // 显示注册窗体
      this.Hide(); // 隐藏当前登录窗体
    }
  }
}
