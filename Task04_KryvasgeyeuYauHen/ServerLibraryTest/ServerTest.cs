using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLibrary;

namespace ServerLibraryTest
{
    //The tests contain Thread.Sleep.This is necessary to correctly model the server-client connection.
    public class ServerTest
    {
        [TestMethod]
        public void Listen_WhenServerIsListening_ShouldGetMessage()
        {
            //arrange            
            Server server = new Server();
            string testString = null;
            server.MessageReceived += (id, msg) => testString = msg;
            server.Listen();

            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect("127.0.0.1", 7777);
            NetworkStream stream = tcpClient.GetStream();
            Thread.Sleep(1000);
            string message = "hello";
            //act
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(message);
            stream.Close();
            tcpClient.Close();
            writer.Close();


            server.Disconnect();

            //assert
            Assert.AreEqual(testString, message);
        }

        [TestMethod]
        public void AddConnection_WhenServerIsListening_ShouldAddClients()
        {
            //arrange            
            Server server = new Server();

            server.Listen();             
            
            //act
            TcpClient tcpClient1 = new TcpClient();
            tcpClient1.Connect("127.0.0.1", 7777);
            NetworkStream stream1 = tcpClient1.GetStream();

            TcpClient tcpClient2 = new TcpClient();
            tcpClient2.Connect("127.0.0.1", 7777);
            NetworkStream stream2 = tcpClient2.GetStream();            

            Thread.Sleep(5000);
            var res = server.Clients.Count();  

            stream1.Close();
            stream2.Close();
            tcpClient1.Close();
            tcpClient2.Close();
            server.Disconnect();
            //assert
            Assert.AreEqual(res, 2);
        }

        [TestMethod]
        public void RemoveConnection_WhenServerIsListening_ShouldRemoveClients()
        {
            //arrange            
            Server server = new Server();

            server.Listen();
            
            TcpClient tcpClient1 = new TcpClient();
            tcpClient1.Connect("127.0.0.1", 7777);
            NetworkStream stream1 = tcpClient1.GetStream();

            TcpClient tcpClient2 = new TcpClient();
            tcpClient2.Connect("127.0.0.1", 7777);
            NetworkStream stream2 = tcpClient2.GetStream();            
            //act
            stream1.Close();            
            tcpClient1.Close();
            Thread.Sleep(2000);
            var res1 = server.Clients.Count();           

            stream2.Close();
            tcpClient2.Close();

            Thread.Sleep(2000);

            var res2 = server.Clients.Count();
            server.Disconnect();
            //assert
            Assert.AreEqual(res1, 1);
            Assert.AreEqual(res2, 0);

        }

        [TestMethod]
        public void SendMessange_WhenServerIsListening_ClientsShouldGetMessange()
        {
            //arrange            
            Server server = new Server();

            server.Listen();
            
            TcpClient tcpClient1 = new TcpClient();
            tcpClient1.Connect("127.0.0.1", 7777);
            NetworkStream stream1 = tcpClient1.GetStream();

            TcpClient tcpClient2 = new TcpClient();
            tcpClient2.Connect("127.0.0.1", 7777);
            NetworkStream stream2 = tcpClient2.GetStream();

            Thread.Sleep(5000);
            //act
            server.SendMessage("Hello");

            BinaryReader reader1 = new BinaryReader(stream1);
            string message1 = reader1.ReadString();

            BinaryReader reader2 = new BinaryReader(stream2);
            string message2 = reader2.ReadString();


            stream1.Close();
            stream2.Close();
            tcpClient1.Close();
            tcpClient2.Close();
            server.Disconnect();
            //assert
            Assert.AreEqual(message1, "Hello");
            Assert.AreEqual(message2, "Hello");
        }
    }
}
