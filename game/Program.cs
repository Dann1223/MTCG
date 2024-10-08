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
    // User list
    private static List<User> userList = new List<User>();

    // Mapping between users and tokens
    private static Dictionary<string, string> userTokens = new Dictionary<string, string>();

    static void Main(string[] args)
    {
      User u = new User();
      u.Name = "dandan";
      u.Email = "666@666";
      u.Password = "987654321";
      u.Gold = "20";
      u.Token = "66666666";
      userList.Add(u);

      MagicCard WaterGun = new MagicCard("watergun", "Fire", 20, "Shoot waterguns");
      //WaterGun.Display();//show card

      //Console.WriteLine(); 
      MonsterCard dragon = new MonsterCard("Blakc Dragon", "Water", 40, "Dragon");
      //dragon.Display();

      Console.WriteLine("HttpServer-Demo: use http://localhost:8000/");

      // ===== Start HTTP server =====
      var server = new TcpListener(IPAddress.Any, 8000);
      server.Start();

      while (true)
      {
        try
        {
          //  Wait for client to connect
          var client = server.AcceptTcpClient();
          Console.WriteLine("Accept new client connections.");

          // Process client requests
          HttpHandlers.HandleClient(client, userList, userTokens);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Server Error: {ex.Message}");
        }
      }
    }
  }
}
