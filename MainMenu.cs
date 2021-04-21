using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Bi_Os_Coop
{
    class MainMenu
    {
        public static void Logo()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int origWidth = Console.WindowWidth;
            string dashes = "";
            for (int i = 1; i < origWidth; i++) { dashes += "-"; }
            Console.Write(dashes + "\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("oooooooooo  o88");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("              ooooooo");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("                          oooooooo8\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" 888    888 oooo");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("           o888   888o  oooooooo8");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("           o888     88   ooooooo     ooooooo  ooooooooo\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" 888oooo88   888");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ooooooooo");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 888     888 888ooooooo");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ooooooooo");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(" 888         888     888 888     888 888    888\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" 888    888  888");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("           888o   o888         888");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("          888o     oo 888     888 888     888 888    888\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("o888ooo888  o888o");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("            88ooo88   88oooooo88");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("            888oooo88    88ooo88     88ooo88   888ooo88\n                                                                                               o888\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(dashes + "\n\n");
        }
        public static List<string> sortbyname()
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            var movielistname = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, name => name.name);
            List<string> namesort = new List<string>();
            foreach (KeyValuePair<int, string> name in movielistname)
            {
                if (name.Value != null)
                {
                    namesort.Add(name.Value);
                }
            }
            namesort = namesort.OrderBy(q => q).ToList();
            return namesort;
        }
        public static List<string> sortbyrelease()
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            var movielistname = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, name => name.name);
            var movielistrelease = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, releasedate => releasedate.releasedate);
            List<KeyValuePair<int, string>> newlist = new List<KeyValuePair<int, string>>();
            foreach(KeyValuePair<int, string> release in movielistrelease)
            {
                if (newlist.Count == 0){
                    if (release.Value != null)
                    {
                        newlist.Add(release);
                    }
                }
                else if (newlist.Count > 0 && release.Value != null)
                {
                    int year = int.Parse(release.Value.Substring(6));
                    int monthnumber = int.Parse(release.Value.Substring(3, 2));
                    int day = int.Parse(release.Value.Substring(0, 2));
                    foreach (KeyValuePair<int, string> member in newlist)
                    {
                        int year2 = int.Parse(member.Value.Substring(6));
                        int monthnumber2 = int.Parse(member.Value.Substring(3, 2));
                        int day2 = int.Parse(member.Value.Substring(0, 2));
                        if (year > year2)
                        {
                            int index = newlist.IndexOf(member);
                            newlist.Insert(index, release);
                            break;
                        }
                        else if (year == year2)
                        {
                            if (monthnumber > monthnumber2)
                            {
                                int index = newlist.IndexOf(member);
                                newlist.Insert(index, release);
                                break;
                            }
                            else if (monthnumber == monthnumber2){
                                if (day > day2)
                                {
                                    int index = newlist.IndexOf(member);
                                    newlist.Insert(index, release);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            List<string> sortlist = new List<string>();
            foreach (KeyValuePair<int, string> movie in newlist)
            {
                MovieInterpreter Moviesorted = jsonFilms.movieList.Single(movie1 => movie1.movieid == movie.Key);
                sortlist.Add(Moviesorted.name);
            }
            return sortlist;
        }
        public static void printlist(List<string> printablelist)
        {
            for (int i = 1; i < 11; i++)
            {
                try
                {
                    Console.WriteLine($"{i}. {printablelist[i - 1]}");
                }
                catch
                {
                    break;
                }
            }
        }
        public static int MainMenuShow(bool login = true, string language = "Nederlands", string sort = "name")
        {
            Logo();
            if (language == "Nederlands")
            {
                if (!login){
                    string loginstructions = "Druk op 'I' om in te loggen";
                    int stringlength = loginstructions.Length;
                    int origWidth = Console.WindowWidth - stringlength;
                    string spaces = "";
                    for (int j = 1; j < origWidth; j++) { spaces += " "; }
                    Console.WriteLine(spaces + loginstructions);
                }
                Console.WriteLine("ACTUELE FILMS:");
                //var movielistrating = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, beoordeling => beoordeling.beoordeling);
                if (sort == "name")
                {
                    printlist(sortbyname());
                }
                else if (sort == "release")
                {
                    printlist(sortbyrelease());
                }
                if (Console.ReadKey(true).Key == ConsoleKey.I && !login)
                {
                    Console.Clear();
                    return 1;
                }
                return -1;
            }
            else
            {
                return -1;
            }
        }
    }
}
