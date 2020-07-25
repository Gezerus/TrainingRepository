using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    /// <summary>
    /// Describes from English to Russian transliteration
    /// </summary>
    public class FromEngToRusTranslitor
    {
        public List<string> TranslitedMessages { get; set; }

        private static Dictionary<string, string> _engRusDictionary = new Dictionary<string, string>()
        {
            {"a", "а" },
            {"b", "б" },
            {"c", "ц" },
            {"d", "д" },
            {"e", "е" },
            {"f", "ф" },
            {"g", "г" },
            {"h", "ш" },
            {"i", "и" },
            {"j", "ж" },
            {"k", "к" },
            {"l", "л" },
            {"m", "м" },
            {"n", "н" },
            {"o", "о" },
            {"p", "п" },
            {"q", "кь" },
            {"r", "р" },
            {"s", "с" },
            {"t", "т" },
            {"u", "ю" },
            {"v", "в" },
            {"w", "у" },
            {"x", "къ" },
            {"y", "й" },
            {"z", "з" },
        };

        /// <summary>
        /// Initializes translitor 
        /// </summary>
        /// <param name="client"></param>
        public FromEngToRusTranslitor(Client client)
        {
            TranslitedMessages = new List<string>();
            client.MessageReceived += (message) => TranslitedMessages.Add(Translit(message));
        }

        /// <summary>
        /// translit a string from English to Russian
        /// </summary>
        /// <param name="engMessage"></param>
        /// <returns></returns>
        private string Translit(string engMessage)
        {
            StringBuilder result = new StringBuilder();
            foreach(char ch in engMessage)
            {
                string temp = null;
                if (_engRusDictionary.TryGetValue(ch.ToString().ToLower(), out temp))
                    if (ch.ToString() == ch.ToString().ToLower())
                        result.Append(temp);
                    else
                        result.Append(temp.ToUpper());

                else
                    result.Append(ch);
            }

            return result.ToString();
        }
    }
}
