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
            CPeople.Person adminAccount = new CPeople.Person();
            adminAccount.setPerson(3, "admin", "admin123@gmail.com", "admin123", "42", "0681234567");
            //string path = "../../Json/admin.json";
            //string tekstUitJson = System.IO.File.ReadAllText(path);
            //admin objectTest = new admin();
            //objectTest = JsonSerializer.Deserialize<admin>(tekstUitJson);

            //JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            //tekstUitJson = JsonSerializer.Serialize(objectTest, options);
            //System.IO.File.WriteAllText(path, tekstUitJson);

            //while loop die hoofdPagina loopt tot je "0" in tikt
            bool inDitMenu = true;

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
                    admin.AddMovies();
                }

                else if (keuze == ConsoleKey.D3)
                {
                    admin.UpdateMovies();
                }
                else if (keuze == ConsoleKey.D4)
                {
                    admin.DeleteMovies();
                }
                else if (keuze == ConsoleKey.D5)
                {
                    admin.AddCinemaHall();
                }
                //else if (keuze == "6")
                //{
                //    admin.DeleteCinemaHall();
                //}
                else
                {
                    hoofdPagina();
                }
            }
        }

        public static ConsoleKey hoofdPagina()
        {
            Console.Clear();
            MainMenu.Logo();
            Console.WriteLine("Admin Menu\n");
            Console.WriteLine("Maak een keuze: ");
            Console.WriteLine("1) Naar Main Menu");
            Console.WriteLine("2) Film toevoegen");
            Console.WriteLine("3) Film aanpassen");
            Console.WriteLine("4) Film verwijderen");
            Console.WriteLine("5) Zaal toevoegen");
            //Console.WriteLine("6) Zaal verwijderen");
            //Console.WriteLine("7) Admin Toevoegen");
            Console.WriteLine("Of type '0' om te stoppen");
            Console.Write("\nKies een pagina: ");
            ConsoleKey keuze = Console.ReadKey(true).Key;
            return keuze;
        }
    }
}
