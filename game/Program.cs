// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace game
{
  internal class Program
  {
    // User list
    private static List<User> userList = new List<User>();

    // The correspondence between users and tokens
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

          // Process client requests
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

      //  HTTP-Request - 1st line
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

      // HTTP-Request - Headers
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

      // HTTP-Request - Body
      string requestBody = string.Empty;
      if (contentLength > 0)
      {
        char[] buffer = new char[contentLength];
        int totalRead = 0;
        while (totalRead < contentLength)
        {
          int read = reader.Read(buffer, totalRead, contentLength - totalRead);
          if (read == 0)
            break; // Client closed connection
          totalRead += read;
        }
        requestBody = new string(buffer);
      }
      Console.WriteLine($"Body: {requestBody}");

      // Routing processing
      if (method.Equals("POST", StringComparison.OrdinalIgnoreCase))
      {
        if (path.Equals("/register", StringComparison.OrdinalIgnoreCase))
        {
          HandleRegister(writer, requestBody);
        }
        else if (path.Equals("/login", StringComparison.OrdinalIgnoreCase))
        {
          HandleLogin(writer, requestBody);
        }
        else
        {
          SendResponse(writer, "404 Not Found", "application/json", JsonConvert.SerializeObject(new { error = "Endpoint not found" }));
        }
      }
      else if (method.Equals("GET", StringComparison.OrdinalIgnoreCase))
      {
        // Example: Add more GET routes, such as /cards, etc.
        SendResponse(writer, "404 Not Found", "application/json", JsonConvert.SerializeObject(new { error = "Endpoint not found" }));
      }
      else
      {
        SendResponse(writer, "405 Method Not Allowed", "application/json", JsonConvert.SerializeObject(new { error = "Method not allowed" }));
      }

      client.Close();
    }

    static void HandleRegister(StreamWriter writer, string requestBody)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(requestBody))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Request body cannot be empty" }));
          return;
        }

        var registerRequest = JsonConvert.DeserializeObject<RegisterRequest>(requestBody);
        if (registerRequest == null || string.IsNullOrWhiteSpace(registerRequest.Username) || string.IsNullOrWhiteSpace(registerRequest.Password))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Invalid registration data" }));
          return;
        }

        if (userList.Any(u => u.Username.Equals(registerRequest.Username, StringComparison.OrdinalIgnoreCase)))
        {
          SendResponse(writer, "409 Conflict", "application/json", JsonConvert.SerializeObject(new { error = "Username already exists" }));
          return;
        }

        userList.Add(new User { Username = registerRequest.Username, Password = registerRequest.Password });
        Console.WriteLine($"User '{registerRequest.Username}' registered successfully.");

        SendResponse(writer, "200 OK", "application/json", JsonConvert.SerializeObject(new { success = true, message = "User registered successfully" }));
      }
      catch (JsonException)
      {
        SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Malformed JSON" }));
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Register Error: {ex.Message}");
        SendResponse(writer, "500 Internal Server Error", "application/json", JsonConvert.SerializeObject(new { error = "Internal server error" }));
      }
    }

    static void HandleLogin(StreamWriter writer, string requestBody)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(requestBody))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Request body cannot be empty" }));
          return;
        }

        var loginRequest = JsonConvert.DeserializeObject<RegisterRequest>(requestBody);
        if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Username) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Invalid login data" }));
          return;
        }

        var user = userList.FirstOrDefault(u => u.Username.Equals(loginRequest.Username, StringComparison.OrdinalIgnoreCase) && u.Password == loginRequest.Password);
        if (user == null)
        {
          SendResponse(writer, "401 Unauthorized", "application/json", JsonConvert.SerializeObject(new { error = "Invalid username or password" }));
          return;
        }

        // Generate Token
        string token = Guid.NewGuid().ToString();
        userTokens[user.Username] = token;
        Console.WriteLine($"User '{user.Username}' logged in successfully. Token: {token}");

        SendResponse(writer, "200 OK", "application/json", JsonConvert.SerializeObject(new { success = true, token = token }));
      }
      catch (JsonException)
      {
        SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Malformed JSON" }));
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Login Error: {ex.Message}");
        SendResponse(writer, "500 Internal Server Error", "application/json", JsonConvert.SerializeObject(new { error = "Internal server error" }));
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

  // User class


  //Registration and login request class
  public class RegisterRequest
  {
    public string Username { get; set; }
    public string Password { get; set; }
  }
}
