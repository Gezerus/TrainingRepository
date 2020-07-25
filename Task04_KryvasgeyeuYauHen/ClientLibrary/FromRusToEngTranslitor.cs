using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary
{
    /// <summary>
    /// Describes from Russian to English transliteration
    /// </summary>
    public class FromRusToEngTranslitor
    {
        public List<string> TranslitedMessages { get; set; }
        private static Dictionary<string, string> _rusEngDictionary = new Dictionary<string, string>()
        {
            {"а", "a" },
            {"б", "b" },
            {"в", "v" },
            {"г", "g" },
            {"д", "d" },
            {"е", "e" },
            {"ё", "yo" },
            {"ж", "zh" },
            {"з", "z" },
            {"и", "i" },
            {"й", "j" },
            {"к", "k" },
            {"л", "l" },
            {"м", "m" },
            {"н", "n" },
            {"о", "o" },
            {"п", "p" },
            {"р", "r" },
            {"с", "s" },
            {"т", "t" },
            {"у", "u" },
            {"ф", "f" },
            {"х", "x" },
            {"ц", "cz" },
            {"ч", "ch" },
            {"ш", "sh" },
            {"щ", "shh" },
            {"ъ", "''" },
            {"ы", "y'" },
            {"ь", "'" },
            {"э", "e'" },
            {"ю", "yu" },
            {"я", "ya" },
        };

        /// <summary>
        /// Initializes translitor 
        /// </summary>
        public FromRusToEngTranslitor(Client client)
        {
            TranslitedMessages = new List<string>();
            client.MessageReceived += (message) => TranslitedMessages.Add(Translit(message));
        }
        /// <summary>
        /// translit a string from Russian to English
        /// </summary>
        /// <param name="engMessage"></param>
        /// <returns></returns>
        private string Translit(string rusMessage)
        {
            StringBuilder result = new StringBuilder();
            foreach (char ch in rusMessage)
            {
                string temp = null;
                if (_rusEngDictionary.TryGetValue(ch.ToString().ToLower(), out temp))
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
