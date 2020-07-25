using ClientLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();

            client.MessageReceived += (msg) => Console.WriteLine(msg);

            client.Connect("127.0.0.1", 7777);

            client.SendMessage("Hello");
            client.SendMessage("i am the first client");

            Console.ReadLine();

            client.Disconnect();
        }
    }
}
