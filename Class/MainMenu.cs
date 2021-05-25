﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
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

        public static void ClearAndShowLogoPlusEsc(string menu)
        {
            Console.Clear();
            Logo();
            Console.Write($"Terug naar het {menu} Menu (Esc)\n\n");
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
        public static Films getfilmlist()
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
            string loginstructions = "Inloggen (I)";
            Console.WriteLine(lengthmakerthing(Console.WindowWidth - loginstructions.Length - 15, ' ') + loginstructions);

            string reginstructions = "Registreren (O)";
            Console.WriteLine(lengthmakerthing(Console.WindowWidth - reginstructions.Length, ' ') + reginstructions);
        }
        public static void logouttext()
        {
            string logoutstructions = "Uitloggen (U)";
            Console.WriteLine(lengthmakerthing(Console.WindowWidth - logoutstructions.Length - 15, ' ') + logoutstructions);
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
            try
            {
                if (user == "1go2to3main4menu5")
                {
                    return new Tuple<string, dynamic>("1go2to3main4menu5", "1go2to3main4menu5");
                }
            }
            catch { }
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
        public static void jsonmainmenu(dynamic user, string sort, bool reverse, string login, string language)
        {
            MainMenuThings jj = new MainMenuThings();
            jj.setlog(user, sort, reverse, login, language);
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jj, opt);
            Json.WriteJson("MainMenu", json);
        }
        public static MainMenuThings jsonfileloader()
        {
            string json = Json.ReadJson("MainMenu");
            return JsonSerializer.Deserialize<MainMenuThings>(json);
        }
        public static void MainMenuShow()
        {
            Logo();
            MainMenuThings things = jsonfileloader();
            CPeople.Person user = things.user; string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;
            bool sav = false;
            if (language == "Nederlands")
            {
                Console.Write("Afsluiten (Esc)");
                if (login == "None") { logintext(); }
                else { logouttext(); }
                string goAM = "Admin Menu (A)";
                if (login == "Admin") { Console.WriteLine(lengthmakerthing(Console.WindowWidth - goAM.Length, ' ') + goAM); }
                string goUserProfile = "Profile (W)";
                if (login == "Person") { Console.WriteLine(lengthmakerthing(Console.WindowWidth - goUserProfile.Length, ' ') + goUserProfile); }
                sorttext(sort, reverse);

                Console.WriteLine("ACTUELE FILMS:");
                actualmovies(sort, reverse, 1);
                string moviemenugo = "Meer Films (E)";
                Console.WriteLine(lengthmakerthing(Console.WindowWidth - moviemenugo.Length - 22, ' ') + moviemenugo);
                ConsoleKey keypressed = Console.ReadKey(true).Key;
                while (keypressed != ConsoleKey.E && keypressed != ConsoleKey.A && keypressed != ConsoleKey.W && keypressed != ConsoleKey.Escape && keypressed != ConsoleKey.R && keypressed != ConsoleKey.T && keypressed != ConsoleKey.Y && keypressed != ConsoleKey.U && keypressed != ConsoleKey.I && keypressed != ConsoleKey.O && keypressed != ConsoleKey.P)
                {
                    keypressed = Console.ReadKey(true).Key;
                }
                Console.WriteLine(keypressed);
                if (keypressed == ConsoleKey.E) { MovieMenu.mainPagina(); }
                else if (keypressed == ConsoleKey.I && login == "None")
                {
                    Tuple<string, dynamic> login2 = loginscreenthing(login);
                    if (login2.Item1 != "1go2to3main4menu5")
                    {
                        login = login2.Item1;
                        if (login2.Item1 != "None") { user = login2.Item2; }
                        jsonmainmenu(user, sort, reverse, login, language);
                        if (login == "Admin") { adminMenu.AM(); }
                    }
                }
                else if (keypressed == ConsoleKey.O && login == "None")
                {
                    Console.Clear();
                    Registerscreen.CreateAccount();
                }
                else if (keypressed == ConsoleKey.A) { if (login == "Admin") { adminMenu.AM(); } }
                else if (keypressed == ConsoleKey.W) { if (login == "Person") { UserProfile.ProfileMenu(user); } }
                else if (keypressed == ConsoleKey.R && sort != "name") { sort = "name"; sav = true; }
                else if (keypressed == ConsoleKey.T && sort != "rating") { sort = "rating"; sav = true; }
                else if (keypressed == ConsoleKey.Y && sort != "release") { sort = "release"; sav = true; }
                else if (keypressed == ConsoleKey.U && login != "None") { login = "None"; user = null; sav = true; }
                else if (keypressed == ConsoleKey.P) { reverse = !reverse; sav = true; }
                else if (keypressed == ConsoleKey.Escape) { Environment.Exit(0); }
            }
            if (sav) { jsonmainmenu(user, sort, reverse, login, language); }
            Console.Clear();
            MainMenuShow();
        }
    }
    class MainMenuThings
    {
        public CPeople.Person user { get; set; }
        public string sort { get; set; }
        public bool reverse { get; set; }
        public string login { get; set; }
        public string language { get; set; }
        public void setlog(CPeople.Person user, string sort, bool reverse, string login, string language)
        {
            this.user = user;
            this.sort = sort;
            this.reverse = reverse;
            this.login = login;
            this.language = language;
        }
    }
}