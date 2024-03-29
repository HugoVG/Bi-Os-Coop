using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    /// <summary>
    /// In deze class staan alle functies die met het hoofdmenu te maken hebben.
    /// </summary>
    class MainMenu
    {
        /// <summary>
        /// Deze method print het logo wanneer deze geroepen wordt.
        /// </summary>
        public static void Logo()
        {
            // Dit berekent het aantal streepjes dat boven en onder het logo komen te staan. Dit maakt het mogelijk om de grootte
            // van de console te veranderen zonder dat de streepjes er opeens mee stoppen.
            string dashes = LengthMaker(Console.WindowWidth, '-');
            string spaces = LengthMaker((Console.WindowWidth - 105) / 2, ' ');

            // Hier wordt het logo geprint.
            Program.newEntry(dashes + "\n");
            Program.newEntry(spaces + "oooooooooo  o88", ConsoleColor.Magenta);
            Program.newEntry("              ooooooo", ConsoleColor.DarkMagenta);
            Program.newEntry("                          oooooooo8\n", ConsoleColor.DarkBlue);
            Program.newEntry(spaces + " 888    888 oooo", ConsoleColor.Magenta);
            Program.newEntry("           o888   888o  oooooooo8", ConsoleColor.DarkMagenta);
            Program.newEntry("           o888     88   ooooooo     ooooooo  ooooooooo\n", ConsoleColor.DarkBlue);
            Program.newEntry(spaces + " 888oooo88   888", ConsoleColor.Magenta);
            Program.newEntry(" ooooooooo", ConsoleColor.DarkYellow);
            Program.newEntry(" 888     888 888ooooooo", ConsoleColor.DarkMagenta);
            Program.newEntry(" ooooooooo", ConsoleColor.DarkYellow);
            Program.newEntry(" 888         888     888 888     888 888    888\n", ConsoleColor.DarkBlue);
            Program.newEntry(spaces + " 888    888  888", ConsoleColor.Magenta);
            Program.newEntry("           888o   o888         888", ConsoleColor.DarkMagenta);
            Program.newEntry("          888o     oo 888     888 888     888 888    888\n", ConsoleColor.DarkBlue);
            Program.newEntry(spaces + "o888ooo888  o888o", ConsoleColor.Magenta);
            Program.newEntry("            88ooo88   88oooooo88", ConsoleColor.DarkMagenta);
            Program.newEntry("            888oooo88    88ooo88     88ooo88   888ooo88\n" + spaces + "                                                                                               o888\n", ConsoleColor.DarkBlue);
            Program.newEntry(dashes + "\n");
        }

        /// <summary>
        /// Deze method verwijderd alles van de console en print het logo.
        /// </summary>
        public static void ClearAndShowLogoPlusEsc(string menu)
        {
            Console.Clear();
            Logo();
            Console.Write($"Terug naar het {menu} Menu (Esc)\n");
        }

        /// <summary>
        /// Deze method returnt een lijst met film id's (uit de films json) die gesorteerd zijn op publicatiedatum.
        /// </summary>
        public static List<int> SortByName()
        {
            // Maakt een dictionary van films uit jsonFilms met de movieid en naam.
            MovieMenu.Films jsonFilms = GetFilmList();
            var movielistname = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, name => name.name);

            // Checkt of er niet een film is zonder naam.
            List<KeyValuePair<int, string>> namesort = new List<KeyValuePair<int, string>>();
            foreach (KeyValuePair<int, string> name in movielistname) { if (name.Value != null) { namesort.Add(name); } }

            // sorteert de lijst op alfabetische volgorde.
            namesort = namesort.OrderBy(q => q.Value).ToList();

            // zet de id's van de films uit de dictionary in een lijst en returnt deze.
            List<int> movieids = new List<int>();
            foreach (KeyValuePair<int, string> id in namesort) { movieids.Add(id.Key); }
            return movieids;
        }

        /// <summary>
        /// In deze method geef je de lijst mee die al gesorteerd is en als reverse true is keert deze functie de lijst om en returnt het deze.
        /// </summary>
        public static List<int> Reversing(List<int> listing, bool reverse)
        {
            if (reverse) { listing.Reverse(); }
            return listing;
        }
      
        /// <summary>
        /// Deze method returnt de films uit de json.
        /// </summary>
        public static MovieMenu.Films GetFilmList()
        {
            string json = Json.ReadJson("Films");
            return JsonSerializer.Deserialize<MovieMenu.Films>(json);
        }

        /// <summary>
        /// Deze method returnt een lijst met film id's (uit de films json) die gesorteerd zijn op publicatiedatum.
        /// </summary>
        public static List<int> SortByRelease()
        {
            // Maakt een dictionary van films uit jsonFilms met de movieid en releasedate.
            MovieMenu.Films jsonFilms = GetFilmList();
            var movielistrelease = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, releasedate => releasedate.releasedate);

            // Vergelijkt elke datum met elkaar en plaatst deze in de lijst newlist met de movie id en publicatiedatum.
            List<KeyValuePair<int, string>> newlist = new List<KeyValuePair<int, string>>();
            foreach (KeyValuePair<int, string> release in movielistrelease)
            {
                // Checkt of de lijst newlist leeg is en of de publicatiedatum bestaat.
                if (newlist.Count == 0 && release.Value != null) { newlist.Add(release); }
                else if (newlist.Count > 0 && release.Value != null)
                {
                    try
                    {
                        // Maakt de datum een integer.
                        int year = int.Parse(release.Value.Substring(6));
                        int monthnumber = int.Parse(release.Value.Substring(3, 2));
                        int day = int.Parse(release.Value.Substring(0, 2));

                        // Checkt waar in de gesorteerde lijst newlist de volgende datum wordt toegevoegd. Wanneer de plek is gevonden breakt de loop.
                        foreach (KeyValuePair<int, string> member in newlist)
                        {
                            // Maakt de datum een integer.
                            int year2 = int.Parse(member.Value.Substring(6));
                            int monthnumber2 = int.Parse(member.Value.Substring(3, 2));
                            int day2 = int.Parse(member.Value.Substring(0, 2));

                            // Checkt of het jaar van de nog niet toegevoegde datum later is dan het jaar van de datum die hij checkt op dat moment.
                            if (year > year2)
                            {
                                int index = newlist.IndexOf(member);
                                newlist.Insert(index, release);
                                break;
                            }

                            // Als beide jaren hetzelfde zijn check hij of de maand van de nieuwe datum later is dan de maand van de datum die hij checkt op dat moment.
                            else if (year == year2)
                            {
                                if (monthnumber > monthnumber2)
                                {
                                    int index = newlist.IndexOf(member);
                                    newlist.Insert(index, release);
                                    break;
                                }

                                // Als ook beide maanden hetzelfde zijn check hij of de dag van de nieuwe datum later is dan de dag van de datum die hij checkt op dat moment.
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

                            // Als niets van het bovenstaande waar is checkt hij of het programma alle datums uit de lijst newlist heeft gehad. Als dit zo is betekent het dus dat de nieuwe datum het oudste is in de lijst newlist.
                            if (newlist.IndexOf(member) == (newlist.Count - 1)) { newlist.Add(release); break; }
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            // zet de id's van de films uit de dictionary in een lijst en returnt deze.
            List<int> sortlist = new List<int>();
            foreach (KeyValuePair<int, string> movie in newlist) { sortlist.Add(movie.Key); }
            return sortlist;
        }

        /// <summary>
        /// Deze method returnt een lijst met film id's (uit de films json) die gesorteerd zijn op beoordeling.
        /// </summary>
        public static List<int> SortByRating()
        {
            // Maakt een dictionary van films uit jsonFilms met de movieid en naam.
            MovieMenu.Films jsonFilms = GetFilmList();
            var movielistrating = jsonFilms.movieList.ToDictionary(movieid => movieid.movieid, beoordeling => beoordeling.beoordeling);

            // Checkt of er een film is zonder beoordeling.
            List<KeyValuePair<int, double>> namesort = new List<KeyValuePair<int, double>>();
            foreach (KeyValuePair<int, double> rate in movielistrating) { if (rate.Value > 0) { namesort.Add(rate); } }

            // sorteert de lijst op publicatiedatum.
            namesort = namesort.OrderBy(q => q.Value).ToList();

            // zet de id's van de films uit de dictionary in een lijst en returnt deze.
            List<int> sortlist = new List<int>();
            foreach (KeyValuePair<int, double> rating in namesort) { sortlist.Insert(0, rating.Key); }
            return sortlist;
        }

        /// <summary>
        /// Deze method returnt een lijst met alle films.
        /// </summary>
        public static List<string> ReturnAllFilms(List<int> printablelist)
        {
            // Pakt de films uit de json.
            MovieMenu.Films jsonFilms = GetFilmList();

            // Zet alle films uit de json in een lijst en returnt deze.
            List<string> listthing = new List<string>();
            for (int i = 1; i < jsonFilms.movieList.Count(); i++)
            {
                try
                {
                    MovieMenu.MovieInterpreter mov = jsonFilms.movieList.Single(movie1 => movie1.movieid == printablelist[i - 1]);
                    listthing.Add(mov.name);
                }
                catch { break; }
            }
            return listthing;
        }

        /// <summary>
        /// Deze method print de naam, publicatiedatum en minimumleeftijd van 10 films uit een list van movie id's die op een aangegeven pagina staan (index). Bijvoorbeeld: index 2 print films 11 t/m 20.
        /// </summary>
        public static List<string> PrintList(List<int> printablelist, int index)
        {
            // Pakt de films uit de json.
            MovieMenu.Films jsonFilms = GetFilmList();

            // Berekent eerst de start en stop index. Daarna print het de naam, publicatiedatum en minimumleeftijd van de films tussen de start en stop index.
            for (int i = ((index * 10 + 1) - 10); i < (index * 10 + 1); i++)
            {
                try
                {
                    // Verkrijgt de informatie over de film door te vragen naar een film met movieid.
                    MovieMenu.MovieInterpreter mov = jsonFilms.movieList.Single(movie1 => movie1.movieid == printablelist[i - 1]);

                    // Zorgt ervoor dat namen die te lang zijn worden afgekort met '...'.
                    string name;
                    if (mov.name.Length > 35) { name = mov.name.Substring(0, 35).Trim() + "..."; }
                    else { name = mov.name; }

                    // Print de film met gelijke ruimte tussen naam, releasedate en minimumleeftijd.
                    Console.WriteLine($"{i}.\t{name} ({mov.releasedate}){LengthMaker(58 - $"e.\t{name} ({mov.releasedate})".Length, ' ')}Leeftijd: {mov.leeftijd}\tBeoordeling: {mov.beoordeling}");
                }
                catch { break; }
            }
            // Returnt alle namen van films in een gesorteerde lijst.
            return ReturnAllFilms(printablelist);
        }

        /// <summary>
        /// Deze method maakt een string met een ingevulde karakter van een ingevulde lengte.
        /// </summary>
        public static string LengthMaker(int length, char character)
        {
            string spaces = "";
            for (int j = 1; j < length; j++) { spaces += character; }
            return spaces;
        }

        /// <summary>
        /// Deze method print "Inloggen (I)" en "Registreren (O)" aan de rechterkant van het scherm.
        /// </summary>
        public static void LogInText()
        {
            string loginstructions = "Inloggen (I), Registreren (R)";
            Console.WriteLine(LengthMaker(Console.WindowWidth - loginstructions.Length, ' ') + loginstructions);
        }

        /// <summary>
        /// Deze method print "Uitloggen (U)" aan de rechterkant van het scherm.
        /// </summary>
        public static void LogOutText()
        {
            string logoutstructions = "Uitloggen (U)";
            Console.WriteLine(LengthMaker(Console.WindowWidth - logoutstructions.Length, ' ') + logoutstructions);
        }

        /// <summary>
        /// Deze method regelt op welke manier gesorteerd wordt, met welk paginanummer (index) en returnt de lijst met film namen.
        /// </summary>
        public static List<string> ActualMovies(string sort, bool reverse, int index)
        {
            List<string> returninglist = new List<string>();
            if (sort == "name") { returninglist = PrintList(Reversing(SortByName(), reverse), index); }
            else if (sort == "release") { returninglist = PrintList(Reversing(SortByRelease(), reverse), index); }
            else if (sort == "rating") { returninglist = PrintList(Reversing(SortByRating(), reverse), index); }
            return returninglist;
        }

        /// <summary>
        /// Deze method regelt het inloggen. Het vangt de inloggegevens op, kijkt vervolgens wat voor type het is en returnt een tuple met de type en de inloggegevens.
        /// </summary>
        public static Tuple<string, dynamic> LoginScreenThing()
        {
            // Gaat naar het inlogscherm.
            Console.Clear();
            var user = loginscherm.login();

            // Wanneer er op escape is gedrukt ga je terug naar het main menu.
            try
            {
                if (user == "1go2to3main4menu5")
                {
                    return new Tuple<string, dynamic>("1go2to3main4menu5", "1go2to3main4menu5");
                }
            }
            catch { }

            Console.Clear();

            // Vraagt om de Type en returnt de type en de inloggegevens.
            Type userType = user.GetType();
            if (userType.Equals(typeof(CPeople.Person))) { return new Tuple<string, dynamic>("Person", user); }
            if (userType.Equals(typeof(CPeople.Admin))) { return new Tuple<string, dynamic>("Admin", user); }
            if (userType.Equals(typeof(CPeople.Employee))) { return new Tuple<string, dynamic>("Employee", user); }
            return new Tuple<string, dynamic>("None", false);
        }

        /// <summary>
        /// Print de sorteermethodes. Wanneer er een sorteermethode geselecteerd is wordt deze rood.
        /// </summary>
        public static void SortText(string sort, bool reverse)
        {
            // Zorgt ervoor dat de text in de rechterkant van de console geprint wordt.
            Program.newEntry(LengthMaker(Console.WindowWidth - 46, ' '));

            // Wanneer de naam sorteermode is geselecteerd wordt "Naam (R)" rood en blijft de rest zwart.
            if (sort == "name")
            {
                Program.newEntry("Naam (N)", ConsoleColor.Red);
                Program.newEntry(", Beoordeling (T), Publicatiedatum (Y)\n");
            }

            // Wanneer de publicatiedatum sorteermode is geselecteerd wordt "Publicatiedatum (Y)" rood en blijft de rest zwart.
            if (sort == "release")
            {
                Program.newEntry("Naam (N), Beoordeling (T), ");
                Program.newEntry("Publicatiedatum (Y)\n", ConsoleColor.Red);
            }

            // Wanneer de beoordeling sorteermode is geselecteerd wordt "Beoordeling (T)" rood en blijft de rest zwart.
            if (sort == "rating")
            {
                Program.newEntry("Naam (N), ");
                Program.newEntry("Beoordeling (T)", ConsoleColor.Red);
                Program.newEntry(", Publicatiedatum (Y)\n");
            }

            // Zorgt ervoor dat de text in de rechterkant van de console geprint wordt.
            Program.newEntry(LengthMaker(Console.WindowWidth - 11, ' '));

            // Wanneer omkeren is geselecteerd wordt "Omkeren (P)" rood uitgeprint. Anders wordt dit zwart uitgeprint.
            if (reverse) { Program.newEntry("Omkeren (P)\n", ConsoleColor.Red); }
            else { Program.newEntry("Omkeren (P)\n"); }
        }

        /// <summary>
        /// Slaat gegevens uit mainmenu op in MainMenu.json
        /// </summary>
        public static void JsonMainMenuSave(dynamic user, string sort, bool reverse, string login, string language)
        {
            // Maakt een MainMenuThings object aan met de mainmenu gegevens
            MainMenuThings jj = new MainMenuThings();
            jj.SetGegevens(user, sort, reverse, login, language);

            // Slaat het MainMenuThings object op in MainMenu.json
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jj, opt);
            Json.WriteJson("MainMenu", json);
        }

        /// <summary>
        /// Dit is het scherm voor de contactpagina.
        /// </summary>
        public static void ContactPage()
        {
            Console.Clear();
            ClearAndShowLogoPlusEsc("Main");
            CPeople.People accountlist = JsonSerializer.Deserialize<CPeople.People>(Json.ReadJson("Accounts"));
            List<CPeople.Admin> adminaccounts = accountlist.adminList;
            Console.Write("\nDevelopers:\t\t\t\t\tAdmins:\n\n  Hajar Akkouh\t\t(1008536@hr.nl)\t\t  ");

            if (adminaccounts.Count() >= 1)
            {
                int length = 27 - adminaccounts[0].name.Length;
                string adminname = adminaccounts[0].name;
                if (length < 0) { length = 5; }
                string space = LengthMaker(length, ' ');
                Console.Write($"{adminname}{space}{adminaccounts[0].email}\n");
            }
            else
            {
                Console.Write("\n");
            }
            Console.Write("  Thijs van Rixoort\t(1005205@hr.nl)\t\t  ");
            
            if (adminaccounts.Count() >= 2)
            {
                int length = 27 - adminaccounts[1].name.Length;
                string adminname = adminaccounts[1].name;
                if (length < 0) { length = 5; }
                string space = LengthMaker(length, ' ');
                Console.Write($"{adminname}{space}{adminaccounts[1].email}\n");
            }
            else
            {
                Console.Write("\n");
            }
            Console.Write("  Rick Slingerland\t(1007523@hr.nl)\t\t  ");

            if (adminaccounts.Count() >= 3)
            {
                int length = 27 - adminaccounts[2].name.Length;
                string adminname = adminaccounts[2].name;
                if (length < 0) { length = 5; }
                string space = LengthMaker(length, ' ');
                Console.Write($"{adminname}{space}{adminaccounts[2].email}\n");
            }
            else
            {
                Console.Write("\n");
            }
            Console.Write("  Hugo van Geijn\t(1014168@hr.nl)\t\t  ");

            if (adminaccounts.Count() >= 4)
            {
                int length = 27 - adminaccounts[3].name.Length;
                string adminname = adminaccounts[3].name;
                if (length < 0) { length = 5; }
                string space = LengthMaker(length, ' ');
                Console.Write($"{adminname}{space}{adminaccounts[3].email}\n");
            }
            else
            {
                Console.Write("\n");
            }
            Console.Write("  Victor Roskam\t\t(1007353@hr.nl)\t\t  ");

            if (adminaccounts.Count() >= 5)
            {
                int length = 27 - adminaccounts[4].name.Length;
                string adminname = adminaccounts[4].name;
                if (length < 0) { length = 5; }
                string space = LengthMaker(length, ' ');
                Console.Write($"{adminname}{space}{adminaccounts[4].email}\n");
            }
            else
            {
                Console.Write("\n");
            }
            Console.Write("  Bjorn Mooldijk\t(1017503@hr.nl)\t\t  ");

            if (adminaccounts.Count() >= 6)
            {
                int length = 27 - adminaccounts[5].name.Length;
                string adminname = adminaccounts[5].name;
                if (length < 0) { length = 5; }
                string space = LengthMaker(length, ' ');
                Console.Write($"{adminname}{space}{adminaccounts[5].email}\n\n");
            }
            else
            {
                Console.Write("\n\n");
            }
            Console.Write("Met dank aan:\n\n  Diana van Roon\t(d.a.van.roon@hr.nl)\n  Bart Westhoff\t\t(0991807@hr.nl)\n\n\nAdres:\t\tWijnhaven 107, 3011 WN Rotterdam\nTelefoon:\t010 794 4000");

            ConsoleKey keypressed = Console.ReadKey(true).Key;
            while (keypressed != ConsoleKey.Escape)
            {
                keypressed = Console.ReadKey(true).Key;
            }
            if (keypressed == ConsoleKey.Escape) { return; }
        }

        public static void JsonChecker()
        {
            try
            {
                MainMenuThings Things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
            }
            catch
            {
                JsonMainMenuSave(null, "name", false, "None", "Nederlands");
            }

            try
            {
                CPeople.People Accounts = JsonSerializer.Deserialize<CPeople.People>(Json.ReadJson("Accounts"));
            }
            catch
            {
                CPeople.People Accounts = new CPeople.People();
                CPeople.Admin AdminUser = new CPeople.Admin();
                AdminUser.setPerson(1, "Hugo", "coolste@min.com", "nietzoveilig", "05/01/1991", "06123456789");
                Accounts.AddAdmin(AdminUser);
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(Accounts, opt);
                Json.WriteJson("Accounts", json);
            }

            try
            {
                MovieMenu.Films Films = JsonSerializer.Deserialize<MovieMenu.Films>(Json.ReadJson("Films"));
            }
            catch
            {
                MovieMenu.Films Films = new MovieMenu.Films();
                MovieMenu.MovieInterpreter Film = new MovieMenu.MovieInterpreter();
                List<string> Genres = new List<string>() { "Actie", "Thriller" };
                List<string> Acteurs = new List<string>() { "Json Bjorn", "Henkie Henk" };
                Film.setFilm(1, "JsonBjorn", "14/04/2021", Genres, 13, 8, Acteurs, 123, "Engels", "De gevaarlijkste voormalige piloot van de KLM wordt uit zijn schuilplaats gehaald om meer explosieve waarheden over zijn verleden te ontdekken.", "https://www.youtube.com/watch?v=v71ce1Dqqns");
                Films.addFilm(Film);
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(Films, opt);
                Json.WriteJson("Films", json);
            }

            try
            {
                Zalen zaln = JsonSerializer.Deserialize<Zalen>(Json.ReadJson("Zalen"));
            }
            catch
            {
                Zalen zaln = new Zalen();
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                string json = JsonSerializer.Serialize(zaln, opt);
                Json.WriteJson("Zalen", json);
            }

            Console.Clear();
        }

        /// <summary>
        /// Dit is het mainmenu scherm.
        /// </summary>
        public static void MainMenuShow()
        {
            JsonChecker();
            MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
            CPeople.Person user = things.user; string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;
            bool sav = false;
            Logo();
            if (language == "Nederlands")
            {
                Console.Write("Afsluiten (Esc)");

                Console.Write(LengthMaker(Console.WindowWidth - 34, ' ') + "Over Bi-Os-Coop (Q)\n");
                if (login == "None") { LogInText(); }
                else { LogOutText(); }
                string goAM = "Admin Menu (A)";
                if (login == "Admin") { Console.WriteLine(LengthMaker(Console.WindowWidth - goAM.Length, ' ') + goAM); }
                string goUserProfile = "Profiel (W)";
                if (login == "Person" || login == "Employee") { Console.WriteLine(LengthMaker(Console.WindowWidth - goUserProfile.Length, ' ') + goUserProfile); }
                SortText(sort, reverse);

                Console.WriteLine("ACTUELE FILMS:");
                ActualMovies(sort, reverse, 1);
                string moviemenugo = "Meer Films (E)";
                Console.WriteLine(LengthMaker(Console.WindowWidth - moviemenugo.Length - 22, ' ') + moviemenugo);
                ConsoleKey keypressed = Console.ReadKey(true).Key;
                while (keypressed != ConsoleKey.E && keypressed != ConsoleKey.A && keypressed != ConsoleKey.W && keypressed != ConsoleKey.Escape
                    && keypressed != ConsoleKey.R && keypressed != ConsoleKey.T && keypressed != ConsoleKey.Y && keypressed != ConsoleKey.U
                    && keypressed != ConsoleKey.I && keypressed != ConsoleKey.N && keypressed != ConsoleKey.P && keypressed != ConsoleKey.Q)
                {
                    keypressed = Console.ReadKey(true).Key;
                }
                if (keypressed == ConsoleKey.E) { MovieMenu.mainPagina(); }
                else if (keypressed == ConsoleKey.I && login == "None")
                {
                    Tuple<string, dynamic> login2 = LoginScreenThing();
                    if (login2.Item1 != "1go2to3main4menu5")
                    {
                        login = login2.Item1;
                        if (login2.Item1 != "None") { user = login2.Item2; }
                        JsonMainMenuSave(user, sort, reverse, login, language);
                        if (login == "Admin") { adminMenu.AM(user, login); }
                    }
                }
                else if (keypressed == ConsoleKey.R && login == "None")
                {
                    var newCustomer = Registerscreen.CreateAccount();
                    things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                    user = things.user; sort = things.sort; reverse = things.reverse; login = things.login; language = things.language;
                    MainMenu.JsonMainMenuSave(loginscherm.mailwachtvragen(newCustomer.email, newCustomer.password), sort, reverse, "Person", language);
                }
                else if (keypressed == ConsoleKey.A) { if (login == "Admin") { adminMenu.AM(user, login); } }
                else if (keypressed == ConsoleKey.W) { if (login == "Person" || login == "Employee") { UserProfile.ProfileMenu(); } }
                else if (keypressed == ConsoleKey.N && sort != "name") { sort = "name"; sav = true; }
                else if (keypressed == ConsoleKey.T && sort != "rating") { sort = "rating"; sav = true; }
                else if (keypressed == ConsoleKey.Y && sort != "release") { sort = "release"; sav = true; }
                else if (keypressed == ConsoleKey.U && login != "None") { login = "None"; user = null; sav = true; }
                else if (keypressed == ConsoleKey.P) { reverse = !reverse; sav = true; }
                else if (keypressed == ConsoleKey.Escape) { Environment.Exit(0); }
                else if (keypressed == ConsoleKey.Q) { ContactPage(); }
            }
            if (sav) { JsonMainMenuSave(user, sort, reverse, login, language); }
            Console.Clear();
            MainMenuShow();
        }
    }

    /// <summary>
    /// Maakt een object van de ingelogde user, sorteermethode, omkeren (true or false), het type van de ingelogde user en de taal.
    /// </summary>
    public class MainMenuThings
    {

        /// <summary>
        /// Variabele voor ingelogde gebruiker.
        /// </summary>
        public CPeople.Person user { get; set; }

        /// <summary>
        /// Variabele voor welke sorteermethode gebruikt wordt.
        /// </summary>
        public string sort { get; set; }

        /// <summary>
        /// Variabele voor omkeren die waar of onwaar kan zijn.
        /// </summary>
        public bool reverse { get; set; }

        /// <summary>
        /// Variabele voor het type van de ingelogde gebruiker.
        /// </summary>
        public string login { get; set; }

        /// <summary>
        /// Variabele voor taal voorkeur.
        /// </summary>
        public string language { get; set; }

        /// <summary>
        /// Deze method maakt een MainMenuThings object van benodigde variabelen.
        /// </summary>
        public void SetGegevens(CPeople.Person user, string sort, bool reverse, string login, string language)
        {
            this.user = user;
            this.sort = sort;
            this.reverse = reverse;
            this.login = login;
            this.language = language;
        }
    }
}