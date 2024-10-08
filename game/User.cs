namespace game
{
  // User class
  public class User
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Gold { get; set; } = "20"; //Default amount of gold coins
    public string Token { get; set; }
  }
}
