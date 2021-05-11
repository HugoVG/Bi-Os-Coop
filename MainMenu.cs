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
            foreach (KeyValuePair<int, string> release in movielistrelease)
            {
                if (newlist.Count == 0 && release.Value != null) { newlist.Add(release); }
                else if (newlist.Count > 0 && release.Value != null)
                {
                    try
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
                                else if (monthnumber == monthnumber2)
                                {
                                    if (day > day2)
                                    {
                                        int index = newlist.IndexOf(member);
                                        newlist.Insert(index, release);
                                        break;
                                    }
                                }
                            }
                            if (newlist.IndexOf(member) == (newlist.Count - 1)) { newlist.Add(release); break; }
                        }
                    }
                    catch { }
                }
            }
            List<int> sortlist = new List<int>();
            foreach (KeyValuePair<int, string> movie in newlist) { sortlist.Add(movie.Key); }
            return sortlist;
        }
        public static List<int> sortbyrating()
        {
            Films jsonFilms = getfilmlist();
            var movielistrating = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, beoordeling => beoordeling.beoordeling);

            List<KeyValuePair<int, double>> namesort = new List<KeyValuePair<int, double>>();
            foreach (KeyValuePair<int, double> rate in movielistrating) { if (rate.Value > 0) { namesort.Add(rate); } }

            namesort = namesort.OrderBy(q => q.Value).ToList();
            List<int> sortlist = new List<int>();
            foreach (KeyValuePair<int, double> rating in namesort) { sortlist.Insert(0, rating.Key); }
            return sortlist;
        }
        public static List<string> namelistthing(List<int> printablelist, int index)
        {
            List<string> listthing = new List<string>();
            Films jsonFilms = getfilmlist();
            for (int i = 1; i < jsonFilms.movieList.Count(); i++)
            {
                try
                {
                    MovieInterpreter mov = jsonFilms.movieList.Single(movie1 => movie1.movieid == printablelist[i - 1]);
                    listthing.Add(mov.name);
                }
                catch
                {
                    break;
                }
            }
            return listthing;
        }
        public static List<string> printlist(List<int> printablelist, int index)
        {
            Films jsonFilms = getfilmlist();
            for (int i = ((index * 10 + 1) - 10); i < (index * 10 + 1); i++)
            {
                try
                {
                    MovieInterpreter mov = jsonFilms.movieList.Single(movie1 => movie1.movieid == printablelist[i - 1]);
                    string name;
                    if (mov.name.Length > 35) { name = mov.name.Substring(0, 35).Trim() + "..."; }
                    else { name = mov.name; }
                    Console.WriteLine($"{i}.\t{name} ({mov.releasedate}){lengthmakerthing(58 - $"e.\t{name} ({mov.releasedate})".Length, ' ')}Leeftijd: {mov.leeftijd}\tBeoordeling: {mov.beoordeling}");
                }
                catch { break; }
            }
            return namelistthing(printablelist, index);
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

            string reginstructions = "Druk op 'O' om te registreren";
            Console.WriteLine(lengthmakerthing(Console.WindowWidth - reginstructions.Length, ' ') + reginstructions);
        }
        public static void logouttext()
        {
            string logoutstructions = "Druk op 'U' om uit te loggen";
            Console.WriteLine(lengthmakerthing(Console.WindowWidth - logoutstructions.Length, ' ') + logoutstructions);
        }
        public static List<string> actualmovies(string sort, bool reverse, int index)
        {
            List<string> returninglist = new List<string>();
            if (sort == "name") { returninglist = printlist(reversing(sortbyname(), reverse), index); }
            else if (sort == "release") { returninglist = printlist(reversing(sortbyrelease(), reverse), index); }
            else if (sort == "rating") { returninglist = printlist(reversing(sortbyrating(), reverse), index); }
            return returninglist;
        }
        public static Tuple<string, dynamic> loginscreenthing(string login)
        {
            Console.Clear();
            var user = loginscherm.login();
            Console.Clear();
            Type userType = user.GetType();
            if (userType.Equals(typeof(CPeople.Person))) { return new Tuple<string, dynamic>("Person", user); }
            else if (userType.Equals(typeof(CPeople.Admin))) { return new Tuple<string, dynamic>("Admin", user); }
            else if (userType.Equals(typeof(CPeople.Employee))) { return new Tuple<string, dynamic>("Employee", user); }
            return new Tuple<string, dynamic>("None", false);
        }
        public static void sorttext(string sort, bool reverse)
        {
            Program.newEntry(lengthmakerthing(Console.WindowWidth - 46, ' '));
            if (sort == "name")
            {
                Program.newEntry("Naam (R)", ConsoleColor.Red);
                Program.newEntry(", Beoordeling (T), Publicatiedatum (Y)\n");
            }
            if (sort == "release")
            {
                Program.newEntry("Naam (R), Beoordeling (T), ");
                Program.newEntry("Publicatiedatum (Y)\n", ConsoleColor.Red);
            }
            if (sort == "rating")
            {
                Program.newEntry("Naam (R), ");
                Program.newEntry("Beoordeling (T)", ConsoleColor.Red);
                Program.newEntry(", Publicatiedatum (Y)\n");
            }
            Program.newEntry(lengthmakerthing(Console.WindowWidth - 11, ' '));
            if (reverse) { Program.newEntry("Omkeren (P)\n", ConsoleColor.Green); }
            else { Program.newEntry("Omkeren (P)\n"); }
        }
        public static Type Typegetter(dynamic gettype)
        {
            Type userType;
            try { userType = gettype.GetType(); }
            catch
            {
                string strinng = "";
                userType = strinng.GetType();
            }
            return userType;
        }
        public static void MainMenuShow(dynamic user, string sort = "name", bool reverse = false, string login = "None", string language = "Nederlands")
        {
            Logo();
            if (language == "Nederlands")
            {
                if (user == null) { logintext(); }
                else { logouttext(); }
                sorttext(sort, reverse);

                Console.WriteLine("ACTUELE FILMS:");
                actualmovies(sort, reverse, 1);
                string moviemenugo = "Meer Films (E)";
                Console.WriteLine(lengthmakerthing(Console.WindowWidth - moviemenugo.Length - 22, ' ') + moviemenugo);
                ConsoleKey keypressed = Console.ReadKey(true).Key;
                if (keypressed == ConsoleKey.E)
                {
                    List<dynamic> mainmenuthings = new List<dynamic>() { user, sort, reverse, login, language };
                    MovieMenu.mainPagina(mainmenuthings);
                }
                else if (keypressed == ConsoleKey.I && user == null)
                {
                    Tuple<string, dynamic> login2 = loginscreenthing(login);
                    login = login2.Item1;
                    if (login != "None") { user = login2.Item2; }
                    Type userType = Typegetter(user);
                    if (userType.Equals(typeof(CPeople.Admin)))
                    {
                        List<dynamic> mainmenuthings = new List<dynamic>() { user, sort, reverse, login, language };
                        adminMenu.AM(mainmenuthings);
                    }
                }
                else if (keypressed == ConsoleKey.O && user == null)
                {
                    Console.Clear();
                    bool createduser = Registerscreen.CreateAccount();
                    if (createduser)
                    {
                        Tuple<string, dynamic> login2 = loginscreenthing(login);
                        login = login2.Item1;
                        if (login != "None") { user = login2.Item2; }
                    }
                }
                else if (keypressed == ConsoleKey.R && sort != "name") { sort = "name"; }
                else if (keypressed == ConsoleKey.T && sort != "rating") { sort = "rating"; }
                else if (keypressed == ConsoleKey.Y && sort != "release") { sort = "release"; }
                else if (keypressed == ConsoleKey.U && login != "None") { login = "None"; user = null; }
                else if (keypressed == ConsoleKey.P) { reverse = !reverse; }
            }
            Console.Clear();
            MainMenuShow(user, sort, reverse, login, language);
        }
    }
}