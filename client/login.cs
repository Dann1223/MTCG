using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace client
{
  // The login class represents the login form for the client application
  public partial class login : Form
  {
    // HttpClient instance for making HTTP requests to the server
    private readonly HttpClient httpClient;

    public login()
    {
      InitializeComponent();
      httpClient = new HttpClient();
    }

    // Use the specified control name
    private async void MTCGLogin_Click(object sender, EventArgs e)
    {
      string email = textEmail.Text;
      string password = textPassword.Text;

      if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
      {
        MessageBox.Show("Email and Password cannot be empty。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      // Create a login request and use Email as the attribute name directly
      var loginRequest = new
      {
        Email = email,
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
          // Parse the response content
          var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseBody);
          string name = loginResponse.Name; 
          int gold = loginResponse.Gold; 
          string token = loginResponse.Token; 

          // Display the Home form and pass user information
          home homeForm = new home(name, gold, token);
          homeForm.Show();
          this.Hide(); 
        }
        else
        {
          MessageBox.Show($"Login Failed: {responseBody}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show($"Request Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void MTCGClear_Click(object sender, EventArgs e)
    {
      // Clear the input box
      textEmail.Clear();
      textPassword.Clear();
    }

    private void textEmail_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void textPassword_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void MTCGRegister_Click(object sender, EventArgs e)
    {
      register registerForm = new register(); // Make sure to use lowercase class names
      registerForm.Show(); 
      //this.Hide(); // Hide the current login form
    }

    // Embed the LoginResponse class into the login class
    private class LoginResponse
    {
      public string Name { get; set; }
      public int Gold { get; set; }
      public string Token { get; set; } 
    }

    private void MTCGEmail_Click(object sender, EventArgs e)
    {

    }
  }
}
