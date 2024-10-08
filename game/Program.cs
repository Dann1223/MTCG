using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;

namespace game
{
  internal class Program
  {
    // 用户列表
    private static List<User> userList = new List<User>();

    // 用户与令牌之间的映射
    private static Dictionary<string, string> userTokens = new Dictionary<string, string>();

    static void Main(string[] args)
    {
      Console.WriteLine("HttpServer-Demo: use http://localhost:8000/");

      // ===== 启动 HTTP 服务器 =====
      var server = new TcpListener(IPAddress.Any, 8000);
      server.Start();

      while (true)
      {
        try
        {
          // 1. 等待客户端连接
          var client = server.AcceptTcpClient();
          Console.WriteLine("接受新的客户端连接。");

          // 处理客户端请求
          HttpHandlers.HandleClient(client, userList, userTokens);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"服务器错误: {ex.Message}");
        }
      }
    }
  }
}
