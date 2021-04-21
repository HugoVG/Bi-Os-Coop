using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Bi_Os_Coop
{
    class Json
    {
        /*
         * How to use Json class
         * for reading and writing you only need the name of the file in Json folder
         * for example Accounts
         * to read the json from this file simply use
         * string jsonTestText = Json.ReadJson("Accounts");
         * then you can manipulate the data you want from it
         * test = test.FromJson(jsonTestText);
         * test.addPersonByFunction(1, "hugo", "mijnfakeEmail@test.com", "H3nk123", 20);
         * string back2Json = test.ToJson(); //so we return this class to json
         * Json.WriteJson("Accounts", back2Json); // to send it back to the Accounts tab
         * Well done you're all set.
         */
        /// <summary>
        /// Reads all text from file
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string ReadJson(string filename)
        {
            try
            {
                string path = File.ReadAllText($"../../Json/{filename}.json");
                return path;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
                throw new IdiotException();
            }
        }
        /// <summary>
        /// Writes the string text to a JSON file
        /// BEWARE: IRREVERSABLE
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        public static void WriteJson(string filename, string content)
        {
            File.WriteAllText($"../../Json/{filename}.json", content);
        }
    }
    [Serializable]
    public class IdiotException : Exception
    {
        public IdiotException() { Console.WriteLine("some idiot fucked up"); }
        public IdiotException(string message) : base(message) { }
        public IdiotException(string message, Exception inner) : base(message, inner) { }
        protected IdiotException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
