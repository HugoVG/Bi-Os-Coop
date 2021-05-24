using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Bi_Os_Coop
{
    public class adminMenu
    {
        public static void AM()
        {
            CPeople.Admin admin = new CPeople.Admin();
            adminMethods adminMethod = new adminMethods();

            //while loop die hoofdPagina loopt tot je "0" in tikt
            bool inDitMenu = true;
            bool isCoronaFilter = adminMethod.coronaCheck();
            Console.WriteLine(isCoronaFilter);
            while (inDitMenu)
            {
                ConsoleKey keuze = hoofdPagina();
                if (keuze == ConsoleKey.D0)
                {
                    Environment.Exit(0);
                }
                else if (keuze == ConsoleKey.D1)
                {
                    inDitMenu = false;
                    Console.Clear();
                    MainMenu.MainMenuShow();
                }
                else if (keuze == ConsoleKey.D2)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    admin.AddMovies();
                }

                else if (keuze == ConsoleKey.D3)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    admin.UpdateMovies();
                }
                else if (keuze == ConsoleKey.D4)
                {
                    // koppelen met boeken; if (!geboekt) > delete movie
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    admin.DeleteMovies();
                }
                //else if (keuze == ConsoleKey.D5)
                //{
                //    Console.Clear();
                //    admin.AddCinemaHall();
                //}
                //else if (keuze == ConsoleKey.D6)
                //{
                //    Console.Clear();
                //    admin.DeleteCinemaHall();
                //}
                //else if (keuze == ConsoleKey.D7)
                //{
                //    Console.Clear();
                //    adminMethod.AddAdmin();
                //}
                //else if (keuze == ConsoleKey.D8)
                //{
                //    Console.Clear();
                //    adminMethod.CoronaFilter(isCoronaFilter);
                //}
                //else if (keuze == ConsoleKey.D9)
                //{
                //    inDitMenu = false;
                //    Console.WriteLine("U bent nu uitgelogd en gaat naar het main menu.");
                //    Console.Clear();
                //    MainMenu.MainMenuShow(null, "name", false, "None", "Nederlands");
                //}
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Probeer het opnieuw.");
                    Console.ForegroundColor = ConsoleColor.White;
                    adminMenu.AM();
                }
            }
        }

        public static ConsoleKey hoofdPagina()
        {
            adminMethods adminMethod = new adminMethods();
            //Tuple<int, bool[]> zalenInfo = adminMethod.CountCinemaHalls();
            Console.Clear();
            MainMenu.Logo();
            Console.WriteLine("Admin Menu\n");
            Console.WriteLine("Maak een keuze: ");
            Console.WriteLine("1) Naar Main Menu");
            Console.WriteLine("2) Film toevoegen");
            Console.WriteLine("3) Film aanpassen");
            Console.WriteLine("4) Film verwijderen");
            //if (zalenInfo.Item1 == 1) { Console.WriteLine($"5) Zaal toevoegen \t Er is {zalenInfo.Item1} zaal"); }
            //else if (zalenInfo.Item1 > 1) { Console.WriteLine($"5) Zaal toevoegen \t Er zijn {zalenInfo.Item1} zalen"); }
            //else { Console.WriteLine($"5) Zaal toevoegen \t Er zijn geen zalen"); }
            //Console.WriteLine("6) Zaal verwijderen");
            //Console.WriteLine("7) Admin Toevoegen");
            //Console.WriteLine($"8) Corona filter toepassen \t {adminMethod.coronaCheck()}");
            //Console.WriteLine("9) Uitloggen");
            Console.WriteLine("Of type '0' om te stoppen");
            Console.Write("\nMaak een keuze: ");
            ConsoleKey keuze = Console.ReadKey(true).Key;
            return keuze;
        }
    }
}