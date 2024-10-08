using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

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
          // 解析响应内容
          var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody);
          string name = loginResponse.Name; // 假设服务器返回的 JSON 包含名称
          int gold = loginResponse.Gold; // 假设服务器返回的 JSON 包含金币数量
          string token = loginResponse.Token; // 假设服务器返回的 JSON 包含令牌

          // 显示 Home 窗体并传递用户信息
          home homeForm = new home(name, gold, token);
          homeForm.Show();
          this.Hide(); // 隐藏当前登录窗体
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
                           //this.Hide(); // 隐藏当前登录窗体
    }

    // 将 LoginResponse 类嵌入到 login 类中
    private class LoginResponse
    {
      public string Name { get; set; } // 假设服务器返回的用户名称
      public int Gold { get; set; } // 假设服务器返回的金币数量
      public string Token { get; set; } // 假设服务器返回的令牌
    }
  }
}
