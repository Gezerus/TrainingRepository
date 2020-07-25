using ServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            MessageSaver saver = new MessageSaver(server);

            server.Listen();

            server.MessageReceived += (id, msg) =>
            {
                Console.Write(id + "     ");
                Console.WriteLine(msg);
            };

            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(1000);
                Console.WriteLine(server.Clients.Count());
                server.SendMessage("Server");
            }
        }
    }
}
