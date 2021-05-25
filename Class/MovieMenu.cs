using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Bi_Os_Coop.Class
{
    class MovieMenu
    {
        public static void mainPagina(int index = 1)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            int highestpage;
            if ((jsonFilms.movieList.Count()) % 10 > 0)
            {
                highestpage = (jsonFilms.movieList.Count() / 10) + 1;
            }
            else
            {
                highestpage = (jsonFilms.movieList.Count() / 10);
            }
            Console.Clear();
            MainMenu.Logo();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Film Menu");
            MainMenuThings mainmenuthings = MainMenu.jsonfileloader();
            MainMenu.sorttext(mainmenuthings.sort, mainmenuthings.reverse);
            Console.WriteLine("Type S om een film te zoeken");
            Console.WriteLine("Of type '0' om terug te gaan naar de main menu");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Beschikbare films:\n");
            List<string> mainmenulist = MainMenu.actualmovies(mainmenuthings.sort, mainmenuthings.reverse, index);
            Console.WriteLine($"\t\t\t\t\t\t\t\t\t\t\t\tBladzijde {index} van {highestpage}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Type een paginanummer of sorteerfunctie: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string indexstring = Console.ReadLine();
            if (indexstring.ToLower() == "r" && mainmenuthings.sort != "name")
            {
                MainMenu.jsonmainmenu(mainmenuthings.user, "name", mainmenuthings.reverse, mainmenuthings.login, mainmenuthings.language);
            }
            else if (indexstring.ToLower() == "t" && mainmenuthings.sort != "rating")
            {
                MainMenu.jsonmainmenu(mainmenuthings.user, "rating", mainmenuthings.reverse, mainmenuthings.login, mainmenuthings.language);
            }
            else if (indexstring.ToLower() == "y" && mainmenuthings.sort != "release")
            {
                MainMenu.jsonmainmenu(mainmenuthings.user, "release", mainmenuthings.reverse, mainmenuthings.login, mainmenuthings.language);
            }
            else if (indexstring.ToLower() == "p")
            {
                MainMenu.jsonmainmenu(mainmenuthings.user, mainmenuthings.sort, !mainmenuthings.reverse, mainmenuthings.login, mainmenuthings.language);
                Console.Clear();
            }
            else if (indexstring.ToLower() == "s" || indexstring.ToLower() == "search")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Type hier de film die u wilt zoeken: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string movsearch = Console.ReadLine();
                MovieMenu.search(movsearch, mainmenulist);
            }
            else if (indexstring == "0")
            {
                Console.Clear();
                MainMenu.MainMenuShow();
            }
            else
            {
                try
                {
                    index = int.Parse(indexstring);
                    if (index > highestpage)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deze bladzijde bestaat niet!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(1500);
                        index = highestpage;
                    }
                    else if(index < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deze bladzijde bestaat niet!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(1500);
                        index = 1;
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invoer onjuist, probeer opnieuw!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(1000);
                }
            }
            MovieMenu.mainPagina(index);
        }
        //functie die de ingetypte film zoekt in de JSON met alle films
        public static void search(string searchmov, List<string> mainmenulist = null)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            List<string> moviesearchlist = new List<string>();
            try
            {
                int result = Int32.Parse(searchmov) - 1;
                searchmov = mainmenulist[result];
            }
            catch(Exception)
            {

            }
            searchmov = searchmov.ToLower().Replace(" ", "");
            for(int i = 0; i < jsonFilms.movieList.Count(); i++)
            {
                    moviesearchlist.Add(jsonFilms.movieList[i].name.ToLower().Replace(" ", ""));
            }
            int lowest = LevenshteinDistance.Compute(searchmov, moviesearchlist[0]);
            int lowestindex = 0;
            bool contains = false;
            int containindex = 0;
            for (int i = 0; i < moviesearchlist.Count(); i++)
            {
                int temp = LevenshteinDistance.Compute(searchmov, moviesearchlist[i]);
                if(searchmov.Count() < moviesearchlist[i].Count() - 6 && searchmov.Count() >= 4)
                {
                    temp = LevenshteinDistance.Compute(searchmov, moviesearchlist[i]) - (moviesearchlist[i].Count() - searchmov.Count());
                }
                if (temp < lowest)
                {
                    lowest = temp;
                    lowestindex = i;
                }
                if (searchmov == moviesearchlist[i])
                {
                    lowest = temp;
                    lowestindex = i;
                    break;
                }
                if (moviesearchlist[i].Contains(searchmov))
                {
                    contains = true;
                    containindex = i;
                }
            }
            //hoeveel typefouten er mogen gemaakt worden (momenteel 3) als het hoger wordt pakt hij altijd film loro omdat die 4 letters lang is!
            if (contains && searchmov.Count() >= 3 && lowest != 0)
            {
                Console.Clear();
                MovieMenu.showmov(containindex);
            }
            else if (lowest < (searchmov.Count() / 4) + 1  )
            {
                Console.Clear();
                MovieMenu.showmov(lowestindex);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Film niet gevonden, probeer opnieuw!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Thread.Sleep(1000);
            }
        }
        //functie om alle kenmerken van een film te laten zien
        public static void showmov(int tempMovie)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            string gen = null;
            string act = null;
            bool newline = false;
            bool hastrailer = false;
            string trailer = null;
            if (jsonFilms.movieList[tempMovie].genres != null)
            {
                if (jsonFilms.movieList[tempMovie].genres.Count() <= 1)
                {
                    gen = "Genre";
                }
                else
                {
                    gen = "Genres";
                }
            }
            if (jsonFilms.movieList[tempMovie].acteurs != null)
            {
                if (jsonFilms.movieList[tempMovie].acteurs.Count() <= 1)
                {
                    act = "Acteur";
                }
                else
                {
                    act = "Acteurs";
                }
            }
            if (jsonFilms.movieList[tempMovie].trailer != null)
            {
                trailer = jsonFilms.movieList[tempMovie].trailer;
                hastrailer = true;
            }
            MainMenu.Logo();
            Console.WriteLine($"{jsonFilms.movieList[tempMovie].name}");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"{(hastrailer ? "Trailer(T)\n" : "")}");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (jsonFilms.movieList[tempMovie].releasedate != null)
            {
                Console.WriteLine($"Publicatiedatum: {jsonFilms.movieList[tempMovie].releasedate}");
            }
            if (jsonFilms.movieList[tempMovie].taal != null)
            {
                Console.WriteLine($"Taal: {jsonFilms.movieList[tempMovie].taal}");
            }
            Console.WriteLine($"Minimumleeftijd: {jsonFilms.movieList[tempMovie].leeftijd}");
            if (jsonFilms.movieList[tempMovie].genres != null)
            {
                Console.WriteLine($"{gen}: {String.Join(", ", jsonFilms.movieList[tempMovie].genres)}");
            }
            if (jsonFilms.movieList[tempMovie].acteurs != null)
            {
                Console.WriteLine($"{act}: {String.Join(", ", jsonFilms.movieList[tempMovie].acteurs)}");
            }
            Console.WriteLine($"Beoordeling: {jsonFilms.movieList[tempMovie].beoordeling}");
            if (jsonFilms.movieList[tempMovie].beschrijving != null)
            {
                Console.WriteLine("\nBeschrijving: ");
                for (int i = 0; i < jsonFilms.movieList[tempMovie].beschrijving.Length; i++)
                {
                    char c = jsonFilms.movieList[tempMovie].beschrijving[i];
                    //zorgt ervoor dat na 90 characters er bij de eerstvolgende spatie een nieuwe regel wordt gestart.
                    if ((i % 90 == 0 && i != 0) || newline == true)
                    {
                        if (c == ' ')
                        {
                            Console.Write("\n");
                            newline = false;
                        }
                        else
                        {
                            Console.Write(c);
                            newline = true;
                        }
                    }
                    else
                    {
                        Console.Write(c);
                        newline = false;
                    }
                }
                Console.Write("\n");
            }
            //hierna moet als er ja geselecteerd is het resrvatie scherm komen!
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nWilt u deze film reserveren? (");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("J");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("/");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("N");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(")\n");

            inputcheck(tempMovie, trailer, hastrailer, jsonFilms.movieList[tempMovie].name);
        }
        public static void inputcheck(int tempMovie, string trailer, bool hastrailer, string moviename)
        {
            ConsoleKey keypressed = Console.ReadKey(true).Key;
            if (hastrailer == true)
            {
                if (keypressed == ConsoleKey.T) { System.Diagnostics.Process.Start(trailer); Console.Clear(); showmov(tempMovie); }
            }
            //verander Console.WriteLine("succes"); naar het reserveer scherm van hogo
            if (keypressed == ConsoleKey.J) { Console.WriteLine("succes"); }
        }
    }
}
