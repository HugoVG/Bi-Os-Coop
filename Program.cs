﻿#define DEBUG
//#undef DEBUG
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// dertigste test - Hajar

namespace Bi_Os_Coop
{
    class Program
    {
        static void Main(string[] args)
        {
            //Test Gelukt
            //ZAALTESTERNIETGEBRUIKEN test = new ZAALTESTERNIETGEBRUIKEN();
            //test.Test();

            Registerscreen.CreateAccount();

            //loginscherm.login();
            /*
            newEntry("A1", ConsoleColor.Red, ConsoleColor.Black);
            newEntry("B1", ConsoleColor.Red, ConsoleColor.Black);
            newEntry("C1", ConsoleColor.Red, ConsoleColor.Black);
            newEntry("D1", ConsoleColor.Red, ConsoleColor.Black);
            string read = Console.ReadLine();

            //CPeople.Poggers();

#if (DEBUG)
            Console.ReadKey();
#endif
        }
        /// <summary>
        /// Will write a new text to the console making the Main take up less space
        /// </summary>
        /// <param name="text">Required Paramater: Text that will be displayed to screen</param>
        /// <param name="colorfg">Foreground Color</param>
        /// <param name="colorbg">Background Color</param>
        public static void newEntry(string text, ConsoleColor colorfg = ConsoleColor.White, ConsoleColor colorbg = ConsoleColor.Black)
        {
            Console.BackgroundColor = colorbg;
            Console.ForegroundColor = colorfg;
            Console.Write(text);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}