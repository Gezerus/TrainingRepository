using System;
using System.Linq;
using System.Threading;
using ClientLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLibrary;

namespace ClientLibraryTest
{
    //The tests contain Thread.Sleep.This is necessary to correctly model the server-client connection.
    [TestClass]
    public class ClientTest
    {
        [TestMethod]
        public void Connect_ShouldConnectAndListenToServer()
        {
            //arrange
            Server server = new Server();
            server.Listen();

            Client client = new Client();
            string message = null;
            client.MessageReceived += (msg) => message = msg;

            //act
            client.Connect("127.0.0.1", 7777);

            Thread.Sleep(2000);

            var count = server.Clients.Count();

            server.SendMessage("Hello");
            //assert
            Assert.AreEqual(count, 1);
            Assert.AreEqual(message, "Hello");

        }

        [TestMethod]
        public void SendMessage_ShouldSendMessageToServer()
        {
            //arrange
            Server server = new Server();
            string message = null;            
            server.MessageReceived += (id, msg) => message = msg;
            server.Listen();

            Client client1 = new Client();
            client1.Connect("127.0.0.1", 7777);

            Client client2 = new Client();
            client2.Connect("127.0.0.1", 7777);
            //act
            client1.SendMessage("Client 1");
            Thread.Sleep(2000);
            var res1 = message;

            client2.SendMessage("Client 2");
            Thread.Sleep(2000);
            var res2 = message;
            //assert
            Assert.AreEqual(res1, "Client 1");
            Assert.AreEqual(res2, "Client 2");
        }
    }
}
