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
    static void Main(string[] args)
    {
      HttpListener listener = new HttpListener();
      listener.Prefixes.Add("http://localhost:10001/");
      listener.Start();
      Console.WriteLine("Server started at http://localhost:10001/");
    }
  }
}
