using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    /// <summary>
    /// Describes client
    /// </summary>
    public class Client
    {
        public delegate void ClientHandler(string message);
        public event ClientHandler MessageReceived;

        private TcpClient _client;

        private BinaryReader _reader;
        private BinaryWriter _writer;

        public int Port { get; private set; }

        public string Host { get; private set; }

        /// <summary>
        /// Initializes a client
        /// </summary>
        public Client()
        {
            _client = new TcpClient();
        }

        /// <summary>
        /// connects connects to a server and start listening on a separate thread
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        public void Connect(string host, int port)
        {
            try
            {
                Port = port;
                Host = host;
                _client.Connect(host, port);
                _reader = new BinaryReader(_client.GetStream());
                _writer = new BinaryWriter(_client.GetStream());
                Task listenTask = new Task(StartListening);
                listenTask.Start();
            }
            catch
            {
                Disconnect();
            }

        }

        /// <summary>
        /// starts listening to the server
        /// </summary>
        private void StartListening()
        {            
            try
            {
                while(true)
                {
                    string message = _reader.ReadString();
                    if (message != null && MessageReceived != null)
                        MessageReceived(message);
                }
            }
            finally
            {                
                Disconnect();
            }
        }

        /// <summary>
        /// disconnect from the server
        /// </summary>
        public void Disconnect()
        {
            if (_reader != null)
                _reader.Close();
            if (_writer != null)
                _writer.Close();
            if (_client != null)
                _client.Close();
        }

        /// <summary>
        /// send the message to the server
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {  
            _writer.Write(message);
        }
    }
}
