using System;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace client
{
  // The register class represents the registration form for the client application
  public partial class register : Form
  {
    // HttpClient instance for making HTTP requests
    private readonly HttpClient httpClient;

    // Constructor for the registration form
    public register()
    {
      InitializeComponent();
      httpClient = new HttpClient();
    }

    // Event handler for the register button click
    private async void MTCGRegister_Click(object sender, EventArgs e)
    {
      // Retrieve user input from text fields
      string email = textEmail.Text;
      string name = textName.Text;
      string password = textPassword.Text;

      // Validate input
      if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(password))
      {
        MessageBox.Show("Username, Email and Password cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return; // Exit the method if validation fails
      }

      // Create an anonymous object to hold the registration request data
      var registerRequest = new
      {
        Name = name,
        Email = email,
        Password = password
      };

      // Serialize the registration request to JSON format
      var json = JsonConvert.SerializeObject(registerRequest);
      var content = new StringContent(json, Encoding.UTF8, "application/json");

      Console.WriteLine($"Data sent in the registration request: {json}");
      try
      {
        HttpResponseMessage response = await httpClient.PostAsync("http://localhost:8000/register", content);
        string responseBody = await response.Content.ReadAsStringAsync();

        // Display more detailed error information
        if (response.IsSuccessStatusCode)
        {
          MessageBox.Show($"Successful registration！Server Response: {responseBody}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
          this.Close();
          }
        else
        {
          // Display detailed error information
          MessageBox.Show($"Registration failed，Status Code: {response.StatusCode}\nServer Response: {responseBody}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
      textName.Clear();
    }

    private void textEmail_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void textName_TextChanged(object sender, EventArgs e)
    {
      
    }

    private void textPassword_TextChanged(object sender, EventArgs e)
    {
     
    }

    private void MTCGEmail_Click(object sender, EventArgs e)
    {

    }
  }
}
