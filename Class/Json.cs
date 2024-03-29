﻿using System;
using System.IO;

namespace Bi_Os_Coop.Class
{
    class Json
    {
        public const string accounts = "Accounts";
        public const string Films = "Films";
        public const string MainMenu = "MainMenu";
        public const string WatchList = "WatchList";
        public const string Zalen = "Zalen";
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
            try
            {
                File.WriteAllText($"../../Json/{filename}.json", content);
            }
            catch
            {
                System.IO.Directory.CreateDirectory("../../Json/");
                File.WriteAllText($"../../Json/{filename}.json", content);
            }

        }
    }
    [Serializable]
    public class IdiotException : Exception
    {
        public IdiotException() { Console.WriteLine("get Nae Naed"); }
        public IdiotException(string message) : base(message) { }
        public IdiotException(string message, Exception inner) : base(message, inner) { }
        protected IdiotException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
