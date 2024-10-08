using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace game
{
  // Static class containing methods to handle HTTP client requests
  public static class HttpHandlers
  {
    // Method to handle client connection and process HTTP requests
    public static void HandleClient(TcpClient client, List<User> userList, Dictionary<string, string> userTokens)
    {
      using var stream = client.GetStream();
      using var reader = new StreamReader(stream);
      using var writer = new StreamWriter(stream) { AutoFlush = true };

      // HTTP Request
      string? requestLine = reader.ReadLine();
      if (string.IsNullOrEmpty(requestLine))
      {
        Console.WriteLine("Empty request received.");
        return;
      }

      // Split the request line into parts (Method, Path, Version)
      var requestParts = requestLine.Split(' ');
      if (requestParts.Length < 3)
      {
        Console.WriteLine("Invalid HTTP request line.");
        SendResponse(writer, "402 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Invalid HTTP request line." }));
        return;
      }

      // Extract method, path, and version from the request line
      var method = requestParts[0];
      var path = requestParts[1];
      var version = requestParts[2];
      Console.WriteLine($"method: {method}, path: {path}, Version: {version}");

      // HTTP Request - Headers
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
        Console.WriteLine($"head: {headerName} = {headerValue}");

        // Get the content length if specified
        if (headerName.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
        {
          int.TryParse(headerValue, out contentLength);
        }
      }

      // HTTP Request - Body
      string requestBody = string.Empty;
      if (contentLength > 0)
      {
        char[] buffer = new char[contentLength];
        int totalRead = 0;
        while (totalRead < contentLength)
        {
          int read = reader.Read(buffer, totalRead, contentLength - totalRead);
          if (read == 0)
            break; // Client closes the connection
          totalRead += read;
        }
        requestBody = new string(buffer);
      }
      Console.WriteLine($"text: {requestBody}");

      //Route processing
      if (method.Equals("POST", StringComparison.OrdinalIgnoreCase))
      {
        if (path.Equals("/register", StringComparison.OrdinalIgnoreCase))
        {
          HandleRegister(writer, requestBody, userList);
        }
        else if (path.Equals("/login", StringComparison.OrdinalIgnoreCase))
        {
          HandleLogin(writer, requestBody, userList, userTokens);
        }
        else // Handle unknown paths
        {
          SendResponse(writer, "404 Not Found", "application/json", JsonConvert.SerializeObject(new { error = "The endpoint was not found" }));
        }
      }
      else if (method.Equals("GET", StringComparison.OrdinalIgnoreCase))
      {
        SendResponse(writer, "404 Not Found", "application/json", JsonConvert.SerializeObject(new { error = "The endpoint was not found" }));
      }
      else
      {
        SendResponse(writer, "405 Method Not Allowed", "application/json", JsonConvert.SerializeObject(new { error = "This method is not allowed" }));
      }

      client.Close();
    }

    // Method to handle user registration requests
    static void HandleRegister(StreamWriter writer, string requestBody, List<User> userList)
    {
      try
      {
        // Check if the request body is empty
        if (string.IsNullOrWhiteSpace(requestBody))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "The request body cannot be empty" }));
          return;
        }

        // Try to parse the request body as a registration request
        var registerRequest = JsonConvert.DeserializeObject<RegisterRequest>(requestBody);
        if (registerRequest == null || string.IsNullOrWhiteSpace(registerRequest.Name) || string.IsNullOrWhiteSpace(registerRequest.Email) || string.IsNullOrWhiteSpace(registerRequest.Password))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Invalid registration data" }));
          return;
        }

        // Check if username or email already exists
        if (userList.Any(u => u.Name.Equals(registerRequest.Name, StringComparison.OrdinalIgnoreCase)))
        {
          SendResponse(writer, "409 Conflict", "application/json", JsonConvert.SerializeObject(new { error = "Username already exists" }));
          return;
        }

        if (userList.Any(u => u.Email.Equals(registerRequest.Email, StringComparison.OrdinalIgnoreCase)))
        {
          SendResponse(writer, "409 Conflict", "application/json", JsonConvert.SerializeObject(new { error = "Email already exists" }));
          return;
        }

        // Create a new user and set the initial gold coin value
        var newUser = new User { Name = registerRequest.Name, Email = registerRequest.Email, Password = registerRequest.Password, Gold = "20" };
        userList.Add(newUser);
        Console.WriteLine($"user '{newUser.Name}' Successful registration。");

        // Return a successful response
        SendResponse(writer, "200 OK", "application/json", JsonConvert.SerializeObject(new { success = true, message = "User registration successful", gold = newUser.Gold }));
      }
      catch (JsonException)
      {
        SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "JSON Format Error" }));
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Registration Error: {ex.Message}");
        SendResponse(writer, "500 Internal Server Error", "application/json", JsonConvert.SerializeObject(new { error = "Internal Server Error" }));
      }
    }

    // Method to handle user login requests
    static void HandleLogin(StreamWriter writer, string requestBody, List<User> userList, Dictionary<string, string> userTokens)
    {
      try
      {
        // Check if the request body is empty
        if (string.IsNullOrWhiteSpace(requestBody))
        {
          SendResponse(writer, "402 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "The request body cannot be empty" }));
          return;
        }

        var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(requestBody);
        if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
          SendResponse(writer, "403 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "Invalid login data" }));
          return;
        }

        // Find users based on Email and Password
        var user = userList.FirstOrDefault(u => u.Email.Equals(loginRequest.Email, StringComparison.OrdinalIgnoreCase) && u.Password == loginRequest.Password);
        if (user == null)
        {
          SendResponse(writer, "401 Unauthorized", "application/json", JsonConvert.SerializeObject(new { error = "Invalid email or password" }));
          return;
        }

        // Generate a new token for the user
        string token = Guid.NewGuid().ToString();
        userTokens[user.Name] = token; // You can keep the username as a key
        Console.WriteLine($"user '{user.Name}' Login successful. Token: {token}");

        SendResponse(writer, "200 OK", "application/json", JsonConvert.SerializeObject(new { success = true, name = user.Name, token = token, gold = user.Gold }));
      }
      catch (JsonException)
      {
        // Handle JSON parsing errors
        SendResponse(writer, "410 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "JSON Format Error" }));
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Login Error: {ex.Message}");
        SendResponse(writer, "500 Internal Server Error", "application/json", JsonConvert.SerializeObject(new { error = "Internal Server Error" }));
      }
    }

    // Method to send an HTTP response back to the client
    static void SendResponse(StreamWriter writer, string status, string contentType, string body)
    {
      try
      {
        // Write the status line and headers
        writer.WriteLine($"HTTP/1.1 {status}");
        writer.WriteLine($"Content-Type: {contentType}");
        writer.WriteLine($"Content-Length: {Encoding.UTF8.GetByteCount(body)}");
        writer.WriteLine("Connection: close");
        writer.WriteLine();
        writer.WriteLine(body);
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Send Response Error: {ex.Message}");
      }
    }
  }
}
