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

        // 检查是否为 POST /register 请求
        if (request.HttpMethod == "POST" && request.Url.AbsolutePath == "/register")
        {
          // 处理用户注册请求
          responseString = HandleRegister(request);
        }

        // 返回响应
        byte[] buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);
        response.OutputStream.Close();
      }
    }
  }
}
