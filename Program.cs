#define DEBUG
#undef DEBUG
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

#if (DEBUG)
            //Hier in de testen
            ZAALTESTERNIETGEBRUIKEN test = new ZAALTESTERNIETGEBRUIKEN();
            test.Test();
            //Test Gelukt

            MainMenu.MainMenuShow();
            adminMenu.AM();
            Registerscreen.CreateAccount();

            loginscherm.login();
            /*
            newEntry("A1", ConsoleColor.Red, ConsoleColor.Black);
            newEntry("B1", ConsoleColor.Red, ConsoleColor.Black);
            newEntry("C1", ConsoleColor.Red, ConsoleColor.Black);
            newEntry("D1", ConsoleColor.Red, ConsoleColor.Black);
            string read = Console.ReadLine();
            */
            //CPeople.Poggers();
            Console.ReadKey();
#endif
            // Hieronder normaal programma
            int MM = MainMenu.MainMenuShow(false);
            // maakt een user aan met een dynamic type
            bool isPerson = false; // for some reason mocht dit niet als Bool isPerson, isAdmin, isEmployee = false;
            bool isAdmin = false;
            bool isEmployee = false;
            if (MM == 1)
            {
                var user = loginscherm.login();
                Console.Clear();
                Type userType = user.GetType();
                // krijgt de type van Variabele User

                if (userType.Equals(typeof(CPeople.Person))) { isPerson = true; }
                else if (userType.Equals(typeof(CPeople.Admin))) { isAdmin = true; }
                else if (userType.Equals(typeof(CPeople.Employee))) { isEmployee = true; }

                if (isAdmin) { adminMenu.hoofdPagina(); }
                else if (isPerson) { MainMenu.MainMenuShow(); }
                else if (isEmployee) { throw new IdiotException(); }
            }
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