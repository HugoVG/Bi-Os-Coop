using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Bi_Os_Coop
{
    class MovieMenu
    {
        public static void mainPagina(List<dynamic> mainmenuthings, int index = 1)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            int highestpage;
            if ((jsonFilms.movieList.Count() - 1) % 10 > 0)
            {
                highestpage = (jsonFilms.movieList.Count() - 1) / 10 + 1;
            }
            else
            {
                highestpage = (jsonFilms.movieList.Count() - 1) / 10;
            }
            Console.Clear();
            MainMenu.Logo();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Film Menu\n");
            MainMenu.sorttext(mainmenuthings[1], mainmenuthings[2]);
            Console.WriteLine("Type S om een film te zoeken");
            Console.WriteLine("Of type '0' om terug te gaan naar de main menu");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Beschikbare films:\n");
            List<string> mainmenulist = MainMenu.actualmovies(mainmenuthings[1], mainmenuthings[2], index);
            Console.WriteLine($"\t\t\t\t\t\t\t\t\t\t\t\tBladzijde {index} van {highestpage}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Type een paginanummer of sorteerfunctie: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string indexstring = Console.ReadLine();
            if (indexstring.ToLower() == "r" && mainmenuthings[1] != "name")
            {
                mainmenuthings[1] = "name";
            }
            else if (indexstring.ToLower() == "t" && mainmenuthings[1] != "rating")
            {
                mainmenuthings[1] = "rating";
            }
            else if (indexstring.ToLower() == "y" && mainmenuthings[1] != "release")
            {
                mainmenuthings[1] = "release";
            }
            else if (indexstring.ToLower() == "p")
            {
                mainmenuthings[2] = !mainmenuthings[2];
                Console.Clear();
            }
            else if (indexstring.ToLower() == "s")
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
                MainMenu.MainMenuShow(mainmenuthings[0], mainmenuthings[1], mainmenuthings[2], mainmenuthings[3], mainmenuthings[4]);
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
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invoer onjuist, probeer opnieuw!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(1000);
                }
            }
            MovieMenu.mainPagina(mainmenuthings, index);
        }
        public static void search(string searchmov, List<string> mainmenulist)
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
            for(int i = 1; i < jsonFilms.movieList.Count(); i++)
            {
                    moviesearchlist.Add(jsonFilms.movieList[i].name.ToLower()); 
            }
            if (moviesearchlist.Contains(searchmov.ToLower()))
            {
                int tempMovie = moviesearchlist.IndexOf(searchmov.ToLower()) + 1;
                Console.Clear();
                MovieMenu.showmov(tempMovie);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Film niet gevonden, probeer opnieuw!");
                Console.ForegroundColor = ConsoleColor.Gray;
                Thread.Sleep(1000);
            }
        }
        public static void showmov(int tempMovie)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            string gen = null;
            string act = null;
            bool newline = false;
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
            MainMenu.Logo();
            Console.WriteLine($"{jsonFilms.movieList[tempMovie].name}\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            if (jsonFilms.movieList[tempMovie].releasedate != null)
            {
                Console.WriteLine($"Publicatiedatum: {jsonFilms.movieList[tempMovie].releasedate}");
            }
            if (jsonFilms.movieList[tempMovie].acteurs != null)
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
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nWilt u deze film reserveren? (J/N)");
            Console.ReadLine();
        }
    }
}
