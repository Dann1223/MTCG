using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace game
{
  public static class HttpHandlers
  {
    public static void HandleClient(TcpClient client, List<User> userList, Dictionary<string, string> userTokens)
    {
      using var stream = client.GetStream();
      using var reader = new StreamReader(stream);
      using var writer = new StreamWriter(stream) { AutoFlush = true };

      // HTTP 请求 - 第一行
      string? requestLine = reader.ReadLine();
      if (string.IsNullOrEmpty(requestLine))
      {
        Console.WriteLine("收到空请求。");
        return;
      }

      var requestParts = requestLine.Split(' ');
      if (requestParts.Length < 3)
      {
        Console.WriteLine("无效的 HTTP 请求行。");
        SendResponse(writer, "402 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "无效的 HTTP 请求行" }));
        return;
      }

      var method = requestParts[0];
      var path = requestParts[1];
      var version = requestParts[2];
      Console.WriteLine($"方法: {method}, 路径: {path}, 版本: {version}");

      // HTTP 请求 - 头部
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
        Console.WriteLine($"头部: {headerName} = {headerValue}");

        if (headerName.Equals("Content-Length", StringComparison.OrdinalIgnoreCase))
        {
          int.TryParse(headerValue, out contentLength);
        }
      }

      // HTTP 请求 - 正文
      string requestBody = string.Empty;
      if (contentLength > 0)
      {
        char[] buffer = new char[contentLength];
        int totalRead = 0;
        while (totalRead < contentLength)
        {
          int read = reader.Read(buffer, totalRead, contentLength - totalRead);
          if (read == 0)
            break; // 客户端关闭连接
          totalRead += read;
        }
        requestBody = new string(buffer);
      }
      Console.WriteLine($"正文: {requestBody}");

      // 路由处理
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
        else
        {
          SendResponse(writer, "404 Not Found", "application/json", JsonConvert.SerializeObject(new { error = "未找到该端点" }));
        }
      }
      else if (method.Equals("GET", StringComparison.OrdinalIgnoreCase))
      {
        SendResponse(writer, "404 Not Found", "application/json", JsonConvert.SerializeObject(new { error = "未找到该端点" }));
      }
      else
      {
        SendResponse(writer, "405 Method Not Allowed", "application/json", JsonConvert.SerializeObject(new { error = "不允许该方法" }));
      }

      client.Close();
    }

    static void HandleRegister(StreamWriter writer, string requestBody, List<User> userList)
    {
      try
      {
        // 检查请求体是否为空
        if (string.IsNullOrWhiteSpace(requestBody))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "请求正文不能为空" }));
          return;
        }

        // 尝试将请求体解析为注册请求
        var registerRequest = JsonConvert.DeserializeObject<RegisterRequest>(requestBody);
        if (registerRequest == null || string.IsNullOrWhiteSpace(registerRequest.Name) || string.IsNullOrWhiteSpace(registerRequest.Email) || string.IsNullOrWhiteSpace(registerRequest.Password))
        {
          SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "无效的注册数据" }));
          return;
        }

        // 检查用户名或电子邮件是否已经存在
        if (userList.Any(u => u.Name.Equals(registerRequest.Name, StringComparison.OrdinalIgnoreCase)))
        {
          SendResponse(writer, "409 Conflict", "application/json", JsonConvert.SerializeObject(new { error = "用户名已存在" }));
          return;
        }

        if (userList.Any(u => u.Email.Equals(registerRequest.Email, StringComparison.OrdinalIgnoreCase)))
        {
          SendResponse(writer, "409 Conflict", "application/json", JsonConvert.SerializeObject(new { error = "电子邮件已存在" }));
          return;
        }

        // 创建新用户并设置初始金币值
        var newUser = new User { Name = registerRequest.Name, Email = registerRequest.Email, Password = registerRequest.Password, Gold = "20" };
        userList.Add(newUser);
        Console.WriteLine($"用户 '{newUser.Name}' 注册成功。");

        // 返回成功响应
        SendResponse(writer, "200 OK", "application/json", JsonConvert.SerializeObject(new { success = true, message = "用户注册成功", gold = newUser.Gold }));
      }
      catch (JsonException)
      {
        SendResponse(writer, "400 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "JSON 格式错误" }));
      }
      catch (Exception ex)
      {
        Console.WriteLine($"注册错误: {ex.Message}");
        SendResponse(writer, "500 Internal Server Error", "application/json", JsonConvert.SerializeObject(new { error = "内部服务器错误" }));
      }
    }


    static void HandleLogin(StreamWriter writer, string requestBody, List<User> userList, Dictionary<string, string> userTokens)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(requestBody))
        {
          SendResponse(writer, "402 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "请求正文不能为空" }));
          return;
        }

        var loginRequest = JsonConvert.DeserializeObject<LoginRequest>(requestBody);
        if (loginRequest == null || string.IsNullOrWhiteSpace(loginRequest.Email) || string.IsNullOrWhiteSpace(loginRequest.Password))
        {
          SendResponse(writer, "403 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "无效的登录数据" }));
          return;
        }

        // 根据 Email 和 Password 查找用户
        var user = userList.FirstOrDefault(u => u.Email.Equals(loginRequest.Email, StringComparison.OrdinalIgnoreCase) && u.Password == loginRequest.Password);
        if (user == null)
        {
          SendResponse(writer, "401 Unauthorized", "application/json", JsonConvert.SerializeObject(new { error = "无效的电子邮件或密码" }));
          return;
        }

        // 生成 Token
        string token = Guid.NewGuid().ToString();
        userTokens[user.Name] = token; // 你可以保留用户名作为键
        Console.WriteLine($"用户 '{user.Name}' 登录成功。令牌: {token}");

        SendResponse(writer, "200 OK", "application/json", JsonConvert.SerializeObject(new { success = true, name = user.Name, token = token, gold = user.Gold }));
      }
      catch (JsonException)
      {
        SendResponse(writer, "410 Bad Request", "application/json", JsonConvert.SerializeObject(new { error = "JSON 格式错误" }));
      }
      catch (Exception ex)
      {
        Console.WriteLine($"登录错误: {ex.Message}");
        SendResponse(writer, "500 Internal Server Error", "application/json", JsonConvert.SerializeObject(new { error = "内部服务器错误" }));
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
        Console.WriteLine($"发送响应错误: {ex.Message}");
      }
    }
  }
}
