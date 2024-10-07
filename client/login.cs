using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace client
{
  public partial class lblName : Form
  {
    public lblName()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      // 窗体加载时可以执行的逻辑
      // Logic that can be executed when the form loads
    }

    private void label1_Click(object sender, EventArgs e)
    {
      // username显示框的逻辑可以放这里
      // The logic of the username display box can be placed here
    }

    private void lalPassword_Click(object sender, EventArgs e)
    {
      // password显示框的逻辑可以放这里
      // The logic of the password display box can be placed here
    }

    private void button2_Click(object sender, EventArgs e)
    {
      // Register button logic
      string username = txtUsername.Text;
      string password = txtPassword.Text;

      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        MessageBox.Show("Username or password cannot be empty！");
        return;
      }

      var requestBody = new
      {
        Username = username,
        Password = password
      };

      string response = SendPostRequest("http://localhost:8000/register", requestBody);

      if (!string.IsNullOrEmpty(response))
      {
        MessageBox.Show("Registration successful! Server returns: " + response);
      }
    }

    private void lalLogin_Click(object sender, EventArgs e)
    {
      // 登录按钮的逻辑
      // Login Button Logic
      string username = txtUsername.Text;
      string password = txtPassword.Text;

      if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
      {
        MessageBox.Show("Username or password cannot be empty！");
        return;
      }

      var requestBody = new
      {
        Username = username,
        Password = password
      };

      string response = SendPostRequest("http://localhost:8000/login", requestBody);

      if (!string.IsNullOrEmpty(response))
      {
        MessageBox.Show("Login successful! Server returns: " + response);
      }
    }

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
      // username输入框的逻辑可以放这里
      //The logic of the username input box can be placed here
    }

    private void textBox2_TextChanged(object sender, EventArgs e)
    {
      // password输入框的逻辑可以放这里
      //The logic of the password input box can be placed here
    }

    // 用于发送 HTTP POST 请求
    //// Used to send HTTP POST request
    private string SendPostRequest(string url, object requestBody)
    {
      try
      {
        var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "application/json";
        httpWebRequest.Method = "POST";

        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
        {
          string json = JsonConvert.SerializeObject(requestBody);
          streamWriter.Write(json);
          streamWriter.Flush();
        }

        var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
        {
          var result = streamReader.ReadToEnd();
          return result;
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Request failed: " + ex.Message);
        return null;
      }
    }
  }
}
