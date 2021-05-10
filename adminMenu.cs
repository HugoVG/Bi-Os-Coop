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
        public static void AM(List<dynamic> mainmenuthings)
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
            bool isCoronaFilter = coronaCheck();
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
                    MainMenu.MainMenuShow(mainmenuthings[0], mainmenuthings[1], mainmenuthings[2], mainmenuthings[3], mainmenuthings[4]);
                }
                else if (keuze == ConsoleKey.D2)
                {
                    inDitMenu = false;
                    Console.Clear();
                    admin.AddMovies();
                }

                else if (keuze == ConsoleKey.D3)
                {
                    inDitMenu = false;
                    Console.Clear();
                    admin.UpdateMovies();
                }
                else if (keuze == ConsoleKey.D4)
                {
                    inDitMenu = false;
                    Console.Clear();
                    // koppelen met boeken; if (!geboekt) > delete movie 
                    admin.DeleteMovies();
                }
                else if (keuze == ConsoleKey.D5)
                {
                    inDitMenu = false;
                    Console.Clear();
                    admin.AddCinemaHall();
                }
                //else if (keuze == ConsoleKey.D6)
                //{
                //    admin.DeleteCinemaHall();
                //}
                //else if (keuze == ConsoleKey.D6)
                //{
                //    AddAdmin();
                //}
                else if (keuze == ConsoleKey.D8)
                {
                    inDitMenu = false;
                    Console.Clear();
                    CoronaFilter(isCoronaFilter);
                }
                else
                {
                    Console.Clear();
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
            Console.WriteLine($"8) Corona filter toepassen \t {coronaCheck()}");
            Console.WriteLine("Of type '0' om te stoppen");
            Console.Write("\nKies een pagina: ");
            ConsoleKey keuze = Console.ReadKey(true).Key;
            return keuze;
        }
        public static bool coronaCheck()
        {
            Zalen zalen = new Zalen();
            string jsonZalen = Json.ReadJson("Zalen");
            zalen = zalen.FromJson(jsonZalen);
            foreach (Zaal zaal in zalen.zalenList)
            {
                List<Stoel> stoel = zaal.stoelen;
                foreach (Stoel stoel2 in stoel)
                {
                    if (stoel2.isOccupied == true && stoel2.Price == 0 && stoel2.isOccupiedBy == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public static void CoronaFilter(bool isCoronaFilter)
        {
            Zalen zalen = new Zalen();
            string jsonZalen = Json.ReadJson("Zalen");
            zalen = zalen.FromJson(jsonZalen);
            if (!isCoronaFilter)
            {
                foreach (Zaal zaal in zalen.zalenList)
                {
                    int count = 0;
                    List<Stoel> stoel = zaal.stoelen;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied == true)
                        {
                            count += 1;
                        }
                    }
                    Tuple<bool, int, double, int>[] occupiedStoelen = new Tuple<bool, int, double, int>[count];
                    int tempIndex = 0;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied == true && tempIndex < count)
                        {
                            occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel2.Price, stoel.FindIndex(a => a == stoel2));
                        }
                    }
                    zaal.setZaal(10, zaal.date, zaal.time, 100, zaal.film);
                    for (int j = 0, i = 0; j < 100; j++)
                    {
                        if (j % 3 == 0)
                        {
                            zaal.stoelen[j].isOccupied = false;
                            zaal.stoelen[j].isOccupiedBy = 1;
                            zaal.stoelen[j].Price = 10;
                        }
                        else
                        {
                            zaal.stoelen[j].isOccupied = true;
                            zaal.stoelen[j].isOccupiedBy = 0;
                            zaal.stoelen[j].Price = 0;
                        }
                        int index = zaal.stoelen.FindIndex(st => st == zaal.stoelen[j]);
                        if (i < occupiedStoelen.Length && occupiedStoelen[i].Item4 == index)
                        {
                            zaal.stoelen[j].isOccupied = occupiedStoelen[i].Item1;
                            zaal.stoelen[j].isOccupiedBy = occupiedStoelen[i].Item2;
                            zaal.stoelen[j].Price = occupiedStoelen[i].Item3;
                            i++;
                        }
                    }
                    Json.WriteJson("Zalen", zalen.ToJson());
                }
            }
            else
            {
                foreach (Zaal zaal in zalen.zalenList)
                {
                    int count = 0;
                    List<Stoel> stoel = zaal.stoelen;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied == true && stoel2.Price != 0)
                        {
                            count += 1;
                        }
                    }
                    Tuple<bool, int, double, int>[] occupiedStoelen = new Tuple<bool, int, double, int>[count];
                    int tempIndex = 0;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied == true && stoel2.Price != 0 && tempIndex < count)
                        {
                            occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel2.Price, stoel.FindIndex(a => a == stoel2));
                        }
                    }
                    zaal.setZaal(10, zaal.date, zaal.time, 100, zaal.film);
                    for (int j = 0, i = 0; j < 100; j++)
                    {
                        int index = zaal.stoelen.FindIndex(st => st == zaal.stoelen[j]);
                        if (i < occupiedStoelen.Length && occupiedStoelen[i].Item4 == index)
                        {
                            zaal.stoelen[j].isOccupied = occupiedStoelen[i].Item1;
                            zaal.stoelen[j].isOccupiedBy = occupiedStoelen[i].Item2;
                            zaal.stoelen[j].Price = occupiedStoelen[i].Item3;
                            i++;
                        }
                    }
                    Json.WriteJson("Zalen", zalen.ToJson());
                }
            }
        }
    }
}
