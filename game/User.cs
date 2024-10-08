namespace game
{
  // 用户类
  public class User
  {
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Gold { get; set; } = "20"; // 默认金币数量
    public string Token { get; set; }
  }
}
