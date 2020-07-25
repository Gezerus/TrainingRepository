using ClientLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClientLibraryTest
{
    //The tests contain Thread.Sleep.This is necessary to correctly model the server-client connection.
    [TestClass]
    public class FromRusToEngTranslitorTest
    {
        [TestMethod]
        public void Translit_ShouldTranslitCorrectly()
        {
            Client client = new Client();
            FromRusToEngTranslitor translitor = new FromRusToEngTranslitor(client);

            Server server = new Server();
            server.Listen();



            client.Connect("127.0.0.1", 7777);

            Thread.Sleep(2000);

            server.SendMessage("Привет");
            server.SendMessage("Привет мир");
            server.SendMessage("HELLO");

            Thread.Sleep(2000);

            Assert.AreEqual(translitor.TranslitedMessages[0], "Privet");
            Assert.AreEqual(translitor.TranslitedMessages[1], "Privet mir");
            Assert.AreEqual(translitor.TranslitedMessages[2], "HELLO");
        }
    }
}