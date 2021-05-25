using System;
using System.Collections.Generic;

namespace Bi_Os_Coop.Class
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
                else if (keuze == ConsoleKey.D5)
                {
                    Console.Clear();
                    admin.AddCinemaHall();
                }
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
            Console.WriteLine($"5) Zaal toevoegen");
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
    public class adminMethods
        {
            public bool coronaCheck()
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
            //public void CoronaFilter(bool isCoronaFilter)
            //{
            //    Zalen zalen = new Zalen();
            //    string jsonZalen = Json.ReadJson("Zalen");
            //    zalen = zalen.FromJson(jsonZalen);
            //    if (!isCoronaFilter)
            //    {
            //        foreach (Zaal zaal in zalen.zalenList)
            //        {
            //            int count = 0;
            //            List<Stoel> stoel = zaal.stoelen;
            //            foreach (Stoel stoel2 in stoel)
            //            {
            //                if (stoel2.isOccupied == true)
            //                {
            //                    count += 1;
            //                }
            //            }
            //            Tuple<bool, int, int, int>[] occupiedStoelen = new Tuple<bool, int, int, int>[count];
            //            int tempIndex = 0;
            //            foreach (Stoel stoel2 in stoel)
            //            {
            //                if (stoel2.isOccupied == true && tempIndex < count)
            //                {
            //                    occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel.stoel2.price, stoel.FindIndex(a => a == stoel2));
            //                }
            //            }
            //            zaal.setZaal(zaal.date, zaal.time, 100, zaal.film);
            //            for (int j = 0, i = 0; j < 100; j++)
            //            {
            //                if (j % 3 == 0)
            //                {
            //                    zaal.stoelen[j].isOccupied = false;
            //                    zaal.stoelen[j].isOccupiedBy = 1;
            //                    zaal.stoelen[j].Price = 10;
            //                }
            //                else
            //                {
            //                    zaal.stoelen[j].isOccupied = true;
            //                    zaal.stoelen[j].isOccupiedBy = 0;
            //                    zaal.stoelen[j].Price = 0;
            //                }
            //                int index = zaal.stoelen.FindIndex(st => st == zaal.stoelen[j]);
            //                if (i < occupiedStoelen.Length && occupiedStoelen[i].Item4 == index)
            //                {
            //                    zaal.stoelen[j].isOccupied = occupiedStoelen[i].Item1;
            //                    zaal.stoelen[j].isOccupiedBy = occupiedStoelen[i].Item2;
            //                    zaal.stoelen[j].Price = occupiedStoelen[i].Item3;
            //                    i++;
            //                }
            //            }
            //            Json.WriteJson("Zalen", zalen.ToJson());
            //        }
            //    }
            //    else
            //    {
            //        foreach (Zaal zaal in zalen.zalenList)
            //        {
            //            int count = 0;
            //            List<Stoel> stoel = zaal.stoelen;
            //            foreach (Stoel stoel2 in stoel)
            //            {
            //                if (stoel2.isOccupied == true && stoel2.Price != 0)
            //                {
            //                    count += 1;
            //                }
            //            }
            //            Tuple<bool, int, double, int>[] occupiedStoelen = new Tuple<bool, int, double, int>[count];
            //            int tempIndex = 0;
            //            foreach (Stoel stoel2 in stoel)
            //            {
            //                if (stoel2.isOccupied == true && stoel2.Price != 0 && tempIndex < count)
            //                {
            //                    occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel2.Price, stoel.FindIndex(a => a == stoel2));
            //                }
            //            }
            //            zaal.setZaal(10, zaal.date, zaal.time, 100, zaal.film);
            //            for (int j = 0, i = 0; j < 100; j++)
            //            {
            //                int index = zaal.stoelen.FindIndex(st => st == zaal.stoelen[j]);
            //                if (i < occupiedStoelen.Length && occupiedStoelen[i].Item4 == index)
            //                {
            //                    zaal.stoelen[j].isOccupied = occupiedStoelen[i].Item1;
            //                    zaal.stoelen[j].isOccupiedBy = occupiedStoelen[i].Item2;
            //                    zaal.stoelen[j].Price = occupiedStoelen[i].Item3;
            //                    i++;
            //                }
            //            }
            //            Json.WriteJson("Zalen", zalen.ToJson());
            //        }
            //    }
            //}

            //public Tuple<int, bool[]> CountCinemaHalls()
            //{
            //    int zalenAmount = 0;
            //    Zalen zalen = new Zalen();
            //    string jsonZalen = Json.ReadJson("Zalen");
            //    zalen = zalen.FromJson(jsonZalen);
            //    foreach (Zaal zaal in zalen.zalenList)
            //    {
            //        zalenAmount++;
            //    }
            //    if (zalenAmount == 0) { return null; }
            //    bool[] delete = new bool[zalenAmount];
            //    int index = 0;
            //    foreach (Zaal zaal in zalen.zalenList)
            //    {
            //        List<Stoel> stoel = zaal.stoelen;
            //        foreach (Stoel stoel2 in stoel)
            //        {
            //            if (stoel2.isOccupied == true && stoel2.Price != 0 && index < delete.Length)
            //            {
            //                delete[index++] = false;
            //            }
            //        }
            //    }
            //    return Tuple.Create(zalenAmount, delete);
            //}

            //public void DeleteCinemaHall()
            //{
            //    Tuple<int, bool[]> zalenInfo = CountCinemaHalls();
            //    Console.Clear();
            //    if (zalenInfo != null)
            //    {
            //        bool asking = true;
            //        while (asking)
            //        {
            //            Console.WriteLine($"Er zijn op dit moment {zalenInfo.Item1} zalen in de Bi-Os-Coop.\n Kies een zaal om te verwijderen: ");
            //            try
            //            {
            //                asking = false;
            //                int antwoord = Convert.ToInt32(Console.ReadLine());
            //                if (antwoord < zalenInfo.Item1)
            //                {
            //                    int index = 1;
            //                    Zalen zalen = new Zalen();
            //                    string jsonZalen = Json.ReadJson("Zalen");
            //                    zalen = zalen.FromJson(jsonZalen);
            //                    foreach (Zaal zaal in zalen.zalenList)
            //                    {
            //                        if (index == antwoord)
            //                        {
            //                            if (zalenInfo.Item2[index - 1])
            //                            {
            //                                Console.WriteLine("lmao de code is nog niet af, sorry");
            //                            }
            //                            else
            //                            {
            //                                Console.WriteLine("Deze zaal kan niet verwijdert worden omdat er al stoelen zijn gereserveerd.");
            //                            }
            //                        }
            //                        else { index++; }
            //                    }
            //                }
            //            }
            //            catch
            //            {
            //                string antwoord = Console.ReadLine();
            //                Console.WriteLine($"De keuze {antwoord} is niet valide. Probeer het opnieuw");
            //                asking = true;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine($"De zaal {zalenInfo.Item2} kan niet verwijdert worden.");
            //        Console.ForegroundColor = ConsoleColor.Gray;
            //        List<dynamic> mainmenuthings = new List<dynamic>() { null, "name", false, "None", "Nederlands" };
            //        adminMenu.AM(mainmenuthings);
            //    }
            //    //nog niet af
            //}

            //public void AddAdmin()
            //{
            //    Console.WriteLine("Vul hier uw e-mailadres in: ");
            //    string accountNaam = Console.ReadLine();
            //    Console.WriteLine("Vul hier uw wachtwoord in: ");
            //    string accountWachtwoord = Console.ReadLine();
            //    //obv nog niet af
            //}

        public void CoronaFilter(bool isCoronaFilter)
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
                    Tuple<bool, int, Stoel.price, int>[] occupiedStoelen = new Tuple<bool, int, Stoel.price, int>[count];
                    int tempIndex = 0;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied == true && tempIndex < count)
                        {
                            occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel2.Price, stoel.FindIndex(a => a == stoel2));
                        }
                    }

                    zaal.setZaal(zaal.date, zaal.time, 100, zaal.film);
                    for (int j = 0, i = 0; j < 100; j++)
                    {
                        if (j % 3 == 0)
                        {
                            zaal.stoelen[j].isOccupied = false;
                            zaal.stoelen[j].isOccupiedBy = 1;
                            zaal.stoelen[j].Price = Stoel.price.LOW;
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
                    Tuple<bool, int, Stoel.price, int>[] occupiedStoelen = new Tuple<bool, int, Stoel.price, int>[count];
                    int tempIndex = 0;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied == true && stoel2.Price != 0 && tempIndex < count)
                        {
                            occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel2.Price, stoel.FindIndex(a => a == stoel2));
                        }
                    }
                    zaal.setZaal(zaal.date, zaal.time, 100, zaal.film);
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