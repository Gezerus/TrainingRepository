using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerLibraryTest
{
    //The tests contain Thread.Sleep.This is necessary to correctly model the server-client connection.
    [TestClass]
    public class MessageServerTest
    {
        [TestMethod]
        public void Constructor_ShouldSaveIdAndMessages()
        {
            //arrange
            Server server = new Server();
            MessageSaver saver = new MessageSaver(server);

            server.Listen();
            //act
            TcpClient tcpClient1 = new TcpClient();
            tcpClient1.Connect("127.0.0.1", 7777);
            NetworkStream stream1 = tcpClient1.GetStream();
            
            BinaryWriter writer1 = new BinaryWriter(stream1);
            writer1.Write("Hello");
            writer1.Write("I am the first client");

            TcpClient tcpClient2 = new TcpClient();
            tcpClient2.Connect("127.0.0.1", 7777);
            NetworkStream stream2 = tcpClient2.GetStream();
            BinaryWriter writer2 = new BinaryWriter(stream2);
            writer2.Write("Hello");
            writer2.Write("I am the second client");

            Thread.Sleep(5000);

            //assert
            Assert.AreEqual(saver.Messages.Count(), 2);
            Assert.AreEqual(saver.Messages[server.Clients[0].Id][0], "Hello");
            Assert.AreEqual(saver.Messages[server.Clients[0].Id][1], "I am the first client");
            Assert.AreEqual(saver.Messages[server.Clients[1].Id][0], "Hello");
            Assert.AreEqual(saver.Messages[server.Clients[1].Id][1], "I am the second client");


            stream1.Close();
            stream2.Close();
            tcpClient1.Close();
            tcpClient2.Close();
            server.Disconnect();
        }
    }
}
