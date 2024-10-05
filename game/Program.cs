// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace CustomHttpServer
{
  internal class Program
  {
    private static List<User> userList = new List<User>();

    private static Dictionary<string, string> userTokens = new Dictionary<string, string>();

    static void Main(string[] args)
    {
      Console.WriteLine("HttpServer-Demo: use http://localhost:8000/");

      // ===== Start HTTP Server =====
      // Start TCP-Server and interpret textual data as HTTP
      var server = new TcpListener(IPAddress.Any, 8000);
      server.Start();

      while (true)
      {
        try
        {
          // 1. Wait for incoming connection
          var client = server.AcceptTcpClient();
          Console.WriteLine("Accepted new client connection.");

          HandleClient(client);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"Server Error: {ex.Message}");
        }
      }
    }

    static void HandleClient(TcpClient client)
    {
      using var stream = client.GetStream();
      using var reader = new StreamReader(stream);
      using var writer = new StreamWriter(stream) { AutoFlush = true };

      // 2. HTTP-Request - 1st line
      // e.g., "POST /register HTTP/1.1"
      string? requestLine = reader.ReadLine();
      if (string.IsNullOrEmpty(requestLine))
      {
        Console.WriteLine("Received empty request.");
        return;
      }

      var requestParts = requestLine.Split(' ');
      if (requestParts.Length < 3)
      {
        Console.WriteLine("Invalid HTTP request line.");
        SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Invalid HTTP request line" }));
        return;
      }

      var method = requestParts[0];
      var path = requestParts[1];
      var version = requestParts[2];
      Console.WriteLine($"Method: {method}, Path: {path}, Version: {version}");

      // 3. HTTP-Request - Headers
      // Read headers until an empty line is encountered
      Dictionary<string, string> headers = new Dictionary<string, string>();
      int contentLength = 0;
      string? line;
      while (!string.IsNullOrEmpty(line = reader.ReadLine()))
      {
        var headerParts = line.Split(new[] { ':' }, 2);
        if (headerParts.Length != 2)
          continue;

        var headerName = headerParts[0].Trim();
        var headerValue = headerParts[1].Trim();
        headers[headerName] = headerValue;
        Console.WriteLine($"Header: {headerName} = {headerValue}");

        if (headerName.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
        {
          int.TryParse(headerValue, out contentLength);
        }
      }


      static void SendResponse(StreamWriter writer, string status, string contentType, string body)
      {
        try
        {
          writer.WriteLine($"HTTP/1.1 {status}");
          writer.WriteLine($"Content-Type: {contentType}");
          writer.WriteLine($"Content-Length: {Encoding.UTF8.GetByteCount(body)}");
          writer.WriteLine("Connection: close");
          writer.WriteLine();
          writer.WriteLine(body);
        }
        catch (Exception ex)
        {
          Console.WriteLine($"SendResponse Error: {ex.Message}");
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
}
