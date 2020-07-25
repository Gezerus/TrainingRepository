using System;
using System.IO;
using System.Net.Sockets;


namespace ServerLibrary
{
    /// <summary>
    /// Describes a client on the server
    /// </summary>
    public class ServerClient
    {
        public string Id { get; private set; }
        
        public BinaryReader Reader { get; private set; }
        public BinaryWriter Writer { get; private set; }

        private TcpClient _client;

        public TcpClient TCPClient 
        { 
            get
            {
                return _client;
            }
             
        }
        /// <summary>
        /// Initializes the client on the server
        /// </summary>
        /// <param name="client"></param>
        public ServerClient(TcpClient client)
        {
            Id = Guid.NewGuid().ToString();

            _client = client;


            Reader = new BinaryReader(_client.GetStream());
            Writer = new BinaryWriter(_client.GetStream());
        }
        /// <summary>
        /// closes resources
        /// </summary>
        public void Close()
        {

            if (Reader != null)
                Reader.Close();
            if (Writer != null)
                Writer.Close();
            if (_client != null)
                _client.Close();
        }

    }

    
}
