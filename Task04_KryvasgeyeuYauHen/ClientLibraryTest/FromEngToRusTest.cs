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
    public class FromEngToRusTest
    {
        [TestMethod]
        public void  Translit_ShouldTranslitCorrectly()
        {
            //arrange
            Client client = new Client();
            FromEngToRusTranslitor translitor = new FromEngToRusTranslitor(client);

            Server server = new Server();
            server.Listen();

            

            client.Connect("127.0.0.1", 7777);

            Thread.Sleep(2000);
            //act
            server.SendMessage("Hello");
            server.SendMessage("HELLO WORLD");
            server.SendMessage("Привет");

            Thread.Sleep(2000);
            //assert
            Assert.AreEqual(translitor.TranslitedMessages[0], "Шелло");
            Assert.AreEqual(translitor.TranslitedMessages[1], "ШЕЛЛО УОРЛД");
            Assert.AreEqual(translitor.TranslitedMessages[2], "Привет");
        }
    }
}
