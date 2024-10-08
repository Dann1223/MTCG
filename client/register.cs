using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace client
{
  public partial class register : Form
  {
    private readonly HttpClient httpClient;

    public register()
    {
      InitializeComponent();
      httpClient = new HttpClient();
    }

    private async void MTCGRegister_Click(object sender, EventArgs e)
    {
      string email = textEmail.Text;
      string name = textName.Text;
      string password = textPassword.Text;

      // 验证输入
      if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
      {
        MessageBox.Show("用户名、电子邮件和密码不能为空。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      var registerRequest = new
      {
        Name = name,
        Email = email,
        Password = password
      };

      var json = JsonConvert.SerializeObject(registerRequest);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      Console.WriteLine($"注册请求发送的数据: {json}");
      try
      {
        HttpResponseMessage response = await httpClient.PostAsync("http://localhost:8000/register", content);
        string responseBody = await response.Content.ReadAsStringAsync();

        // 显示更详细的错误信息
        if (response.IsSuccessStatusCode)
        {
          MessageBox.Show($"注册成功！服务器响应: {responseBody}", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
          this.Close();
          }
        else
        {
          // 显示详细的错误信息
          MessageBox.Show($"注册失败，状态码: {response.StatusCode}\n服务器响应: {responseBody}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
      textName.Clear();
    }

    private void textEmail_TextChanged(object sender, EventArgs e)
    {
      // 可选：响应电子邮件文本框内容变化
    }

    private void textName_TextChanged(object sender, EventArgs e)
    {
      // 可选：响应用户名文本框内容变化
    }

    private void textPassword_TextChanged(object sender, EventArgs e)
    {
      // 可选：响应密码文本框内容变化
    }
  }
}
