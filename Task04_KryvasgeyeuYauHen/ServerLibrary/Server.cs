using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerLibrary
{
    /// <summary>
    /// Describesa server
    /// </summary>
    public class Server
    {
        public delegate void ServerHandler(string id, string message);
        public event ServerHandler MessageReceived;

        private static  TcpListener _listener;

        public List<ServerClient> Clients { get; private set; }
        public Server()
        {
            Clients = new List<ServerClient>();
            _listener = new TcpListener(IPAddress.Any, 7777);
        }
        /// <summary>
        /// Adds the client to the collection
        /// </summary>
        /// <param name="client"></param>
        private void AddConection(ServerClient client)
        {
            Clients.Add(client);
        }

        /// <summary>
        /// Remove the cliend from the collection
        /// </summary>
        /// <param name="id">client id</param>
        private void RemoveConnection(string id)
        {
            ServerClient client = Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
                Clients.Remove(client);
        }

        /// <summary>
        /// starts lestening to clients
        /// </summary>
        private void StartListening()
        {
            try
            {                
                _listener.Start();
                while(true)
                {
                    TcpClient tcpClient =  _listener.AcceptTcpClient();                    
                    ServerClient client = new ServerClient(tcpClient);
                    AddConection(client);
                    Task.Run(() => ListenToClient(client));
                }
            }
            catch
            {
                Disconnect();
            }
        }

        /// <summary>
        /// starts listening on a separate thread
        /// </summary>
        public void Listen()
        {
            Task listener = new Task(StartListening);
            listener.Start();
        }

        /// <summary>
        /// stops listening
        /// </summary>
        public void Disconnect()
        {
            _listener.Stop();

            foreach (ServerClient client in Clients)
                client.Close();            
        }
        /// <summary>
        /// listen to one client
        /// </summary>
        /// <param name="client"></param>
        private void ListenToClient(ServerClient client)
        {
            try
            {
                while(true)
                {
                    string message = client.Reader.ReadString();
                    if (message != null && MessageReceived != null)
                        MessageReceived(client.Id, message);
                }
            }
            finally
            {                
                RemoveConnection(client.Id);                
                client.Close();
            }

        }

        /// <summary>
        /// send the message to all connected clients
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(string message)
        {
            if (Clients.Count() != 0)
                foreach (ServerClient client in Clients)
                {                     
                    client.Writer.Write(message);
                }
        }
    }
}
