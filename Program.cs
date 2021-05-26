#define DEBUG
#undef DEBUG
using System;
using Bi_Os_Coop.Class;
using static Bi_Os_Coop.Class.WatchLists;

// dertigste test - Hajar

namespace Bi_Os_Coop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(ViewReservations.CheckWhichHall(342));

            WatchLists wls  = FromJson();
            wls.AddList();
            wls.ToJson(true);
            Console.ReadKey(true);
#if (DEBUG)

            //#error Hold there Criminal scum
            CPeople.Admin tempadm = new CPeople.Admin();
            //Console.WriteLine(tempadm.isAdmin());
            //Console.WriteLine(tempadm.isPerson());

            CPeople.Person tempperson = new CPeople.Person();
            tempperson.BookTicket();
            //Console.WriteLine(tempperson.isAdmin());
            //Console.WriteLine(tempperson.isPerson());
            //tempadm.AddCinemaHall();
            //Hier in de testen
            //ZAALTESTERNIETGEBRUIKEN test = new ZAALTESTERNIETGEBRUIKEN();
            //test.Test();
            //Test Gelukt
            //PeopleTest.newADMIN();
            //MainMenu.MainMenuShow();
            //adminMenu.AM();
            //Registerscreen.CreateAccount();

            //loginscherm.login();
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
            MainMenu.MainMenuShow();
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