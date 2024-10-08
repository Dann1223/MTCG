using System.Windows.Forms;
using System;

namespace client
{
  public partial class home : Form
  {
    //Controls for displaying user information
    private Label labelName;
    private Label labelGold;
    private Label labelToken;

    // Constructor that receives user information
    public home(string name, int gold, string token)
    {
      InitializeComponent();
      InitializeUserInfo(name, gold, token);
    }

    // Function to initialize user information
    private void InitializeUserInfo(string name, int gold, string token)
    {
      // Create and set the control to display user information
      labelName = new Label
      {
        Text = $"Name: {name}",
        Location = new System.Drawing.Point(10, 10), // The position can be adjusted as needed
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

      // Add controls to the form
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
