using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace game
{
  internal class Program
  {
    private static List<User> userList = new List<User>();

    static void Main(string[] args)
    {
      HttpListener listener = new HttpListener();
      listener.Prefixes.Add("http://localhost:10001/");
      listener.Start();
      Console.WriteLine("Server started at http://localhost:10001/");

      while (true)
      {
        HttpListenerContext context = listener.GetContext();
        HttpListenerRequest request = context.Request;
        HttpListenerResponse response = context.Response;

        string responseString = string.Empty;

        if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/register")
        {
          responseString = HandleRegister(request);
        }

        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
      }
    }
    static string HandleRegister(HttpListenerRequest request)
    {
      try
      {
        Console.WriteLine($"Received {request.HttpMethod} request for {request.Url.AbsolutePath}");

        var body = new System.IO.StreamReader(request.InputStream).ReadToEnd();
        Console.WriteLine($"Request body: {body}"); 

        var registerData = JsonConvert.DeserializeObject<RegisterRequest>(body);

        if (userList.Exists(user => user.Username == registerData.Username))
        {
          return JsonConvert.SerializeObject(new { error = "Username already exists" });
        }

        userList.Add(new User { Username = registerData.Username, Password = registerData.Password });
        Console.WriteLine($"User {registerData.Username} registered successfully.");

        return JsonConvert.SerializeObject(new { success = true });
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error: {ex.Message}"); 
        return JsonConvert.SerializeObject(new { error = "An error occurred." });
      }
    }
  }
  public class User
  {
    public string Username { get; set; }
    public string Password { get; set; }
  }
  public class RegisterRequest
  {
    public string Username { get; set; }
    public string Password { get; set; }
  }
}
