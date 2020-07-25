using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary
{
    /// <summary>
    /// saves messages from all cliens
    /// </summary>
    public class MessageSaver
    {
        public Dictionary<string, List<string>> Messages { get; set; }

        public MessageSaver(Server server)
        {
            Messages = new Dictionary<string, List<string>>();

            server.MessageReceived += (id, message) =>
            {
                if (Messages.Keys.Contains(id))
                    Messages[id].Add(message);
                else
                {
                    Messages.Add(id, new List<string>() { message });
                }
            };
        }
    }
}
