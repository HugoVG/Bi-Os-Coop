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
            string dashes = "";
            for (int i = 1; i < Console.WindowWidth; i++) { dashes += "-"; }
            Program.newEntry(dashes + "\n");
            Program.newEntry("oooooooooo  o88", ConsoleColor.Magenta);
            Program.newEntry("              ooooooo", ConsoleColor.DarkMagenta);
            Program.newEntry("                          oooooooo8\n", ConsoleColor.DarkBlue);
            Program.newEntry(" 888    888 oooo", ConsoleColor.Magenta);
            Program.newEntry("           o888   888o  oooooooo8", ConsoleColor.DarkMagenta);
            Program.newEntry("           o888     88   ooooooo     ooooooo  ooooooooo\n", ConsoleColor.DarkBlue);
            Program.newEntry(" 888oooo88   888", ConsoleColor.Magenta);
            Program.newEntry(" ooooooooo", ConsoleColor.DarkYellow);
            Program.newEntry(" 888     888 888ooooooo", ConsoleColor.DarkMagenta);
            Program.newEntry(" ooooooooo", ConsoleColor.DarkYellow);
            Program.newEntry(" 888         888     888 888     888 888    888\n", ConsoleColor.DarkBlue);
            Program.newEntry(" 888    888  888", ConsoleColor.Magenta);
            Program.newEntry("           888o   o888         888", ConsoleColor.DarkMagenta);
            Program.newEntry("          888o     oo 888     888 888     888 888    888\n", ConsoleColor.DarkBlue);
            Program.newEntry("o888ooo888  o888o", ConsoleColor.Magenta);
            Program.newEntry("            88ooo88   88oooooo88", ConsoleColor.DarkMagenta);
            Program.newEntry("            888oooo88    88ooo88     88ooo88   888ooo88\n                                                                                               o888\n", ConsoleColor.DarkBlue);
            Program.newEntry(dashes + "\n");
        }
        public static List<int> sortbyname()
        {
            Films jsonFilms = getfilmlist();
            var movielistname = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, name => name.name);
            List<KeyValuePair<int, string>> namesort = new List<KeyValuePair<int, string>>();
            foreach (KeyValuePair<int, string> name in movielistname)
            {
                if (name.Value != null) { namesort.Add(name); }
            }
            namesort = namesort.OrderBy(q => q.Value).ToList();
            List<int> movieids = new List<int>();
            foreach (KeyValuePair<int, string> id in namesort) { movieids.Add(id.Key); }
            return movieids;
        }
        public static List<int> reversing(List<int> listing, bool reverse)
        {
            if (reverse) { listing.Reverse(); }
            return listing;
        }
        public static Bi_Os_Coop.Films getfilmlist()
        {
            string json = Json.ReadJson("Films");
            return JsonSerializer.Deserialize<Films>(json);
            
        }
        public static List<int> sortbyrelease()
        {
            Films jsonFilms = getfilmlist();
            var movielistrelease = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, releasedate => releasedate.releasedate);

            List<KeyValuePair<int, string>> newlist = new List<KeyValuePair<int, string>>();
            foreach(KeyValuePair<int, string> release in movielistrelease){
                if (newlist.Count == 0 && release.Value != null) { newlist.Add(release); }
                else if (newlist.Count > 0 && release.Value != null){
                    int year = int.Parse(release.Value.Substring(6));
                    int monthnumber = int.Parse(release.Value.Substring(3, 2));
                    int day = int.Parse(release.Value.Substring(0, 2));
                    foreach (KeyValuePair<int, string> member in newlist){
                        int year2 = int.Parse(member.Value.Substring(6));
                        int monthnumber2 = int.Parse(member.Value.Substring(3, 2));
                        int day2 = int.Parse(member.Value.Substring(0, 2));
                        if (year > year2) {
                            int index = newlist.IndexOf(member);
                            newlist.Insert(index, release);
                            break;
                        }
                        else if (year == year2){
                            if (monthnumber > monthnumber2){
                                int index = newlist.IndexOf(member);
                                newlist.Insert(index, release);
                                break;
                            }
                            else if (monthnumber == monthnumber2){
                                if (day > day2){
                                    int index = newlist.IndexOf(member);
                                    newlist.Insert(index, release);
                                    break;
                                }
                            }
                        }
                        if (newlist.IndexOf(member) == (newlist.Count - 1)){ newlist.Add(release); break;}
                    }
                }
            }
            List<int> sortlist = new List<int>();
            foreach (KeyValuePair<int, string> movie in newlist){ sortlist.Add(movie.Key); }
            return sortlist;
        }
        public static List<int> sortbyrating()
        {
            Films jsonFilms = getfilmlist();
            var movielistrating = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, beoordeling => beoordeling.beoordeling);

            List<KeyValuePair<int, double>> namesort = new List<KeyValuePair<int, double>>();
            foreach (KeyValuePair<int, double> rate in movielistrating){ if (rate.Value > 0){ namesort.Add(rate);}}

            namesort = namesort.OrderBy(q => q.Value).ToList();
            List<int> sortlist = new List<int>();
            foreach (KeyValuePair<int, double> rating in namesort){sortlist.Insert(0, rating.Key);}
            return sortlist;
        }
        public static void printlist(List<int> printablelist, int index)
        {
            Films jsonFilms = getfilmlist();
            for (int i = ((index * 10 + 1) - 10); i < (index * 10 + 1); i++){
                try{
                    MovieInterpreter mov = jsonFilms.movieList.Single(movie1 => movie1.movieid == printablelist[i - 1]);
                    string name;
                    if (mov.name.Length > 35){ name = mov.name.Substring(0, 35).Trim() + "..."; }
                    else{ name = mov.name; }
                    Console.WriteLine($"{i}.\t{name} ({mov.releasedate}){lengthmakerthing(58 - $"e.\t{name} ({mov.releasedate})".Length, ' ')}Leeftijd: {mov.leeftijd}\tBeoordeling: {mov.beoordeling}");
                }
                catch{break;}
            }
        }
        public static string lengthmakerthing(int length, char character)
        {
            string spaces = "";
            for (int j = 1; j < length; j++) { spaces += character; }
            return spaces;
        }
        public static void logintext()
        {
            string loginstructions = "Druk op 'I' om in te loggen";
            Console.WriteLine(lengthmakerthing(Console.WindowWidth - loginstructions.Length, ' ') + loginstructions);

            string reginstructions = "Druk op 'R' om te registreren";
            Console.WriteLine(lengthmakerthing(Console.WindowWidth - reginstructions.Length, ' ') + reginstructions);
        }
        public static void actualmovies(string sort, bool reverse)
        {
            if (sort == "name") { printlist(reversing(sortbyname(), reverse), 1); }
            else if (sort == "release") { printlist(reversing(sortbyrelease(), reverse), 1); }
            else if (sort == "rating") { printlist(reversing(sortbyrating(), reverse), 1); }
        }
        public static int MainMenuShow(bool login = true, string sort = "release", bool reverse = false, string language = "Nederlands")
        {
            Logo();
            if (language == "Nederlands")
            {
                if (!login){ logintext(); }

                Console.WriteLine("ACTUELE FILMS:");
                actualmovies(sort, reverse);

                ConsoleKey keypressed = Console.ReadKey(true).Key;
                if (keypressed == ConsoleKey.I && !login) { Console.Clear(); return 1; }
                if (keypressed == ConsoleKey.R && !login) { Console.Clear(); return 0; }
                return -1;
            }
            else
            {
                return -1;
            }
        }
    }
}
