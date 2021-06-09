using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace Bi_Os_Coop.Class
{
    public class MovieMenu
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
            MainMenuThings mainmenuthings = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
            MainMenu.SortText(mainmenuthings.sort, mainmenuthings.reverse);
            Console.WriteLine("Type S om een film te zoeken");
            Console.WriteLine("Of type '0' om terug te gaan naar de main menu");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Beschikbare films:\n");
            List<string> mainmenulist = MainMenu.ActualMovies(mainmenuthings.sort, mainmenuthings.reverse, index);
            Console.WriteLine($"\t\t\t\t\t\t\t\t\t\t\t\tBladzijde {index} van {highestpage}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Type een paginanummer of sorteerfunctie: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string indexstring = Console.ReadLine();
            if (indexstring.ToLower() == "r" && mainmenuthings.sort != "name")
            {
                MainMenu.JsonMainMenuSave(mainmenuthings.user, "name", mainmenuthings.reverse, mainmenuthings.login,
                    mainmenuthings.language);
            }
            else if (indexstring.ToLower() == "t" && mainmenuthings.sort != "rating")
            {
                MainMenu.JsonMainMenuSave(mainmenuthings.user, "rating", mainmenuthings.reverse, mainmenuthings.login,
                    mainmenuthings.language);
            }
            else if (indexstring.ToLower() == "y" && mainmenuthings.sort != "release")
            {
                MainMenu.JsonMainMenuSave(mainmenuthings.user, "release", mainmenuthings.reverse, mainmenuthings.login,
                    mainmenuthings.language);
            }
            else if (indexstring.ToLower() == "p")
            {
                MainMenu.JsonMainMenuSave(mainmenuthings.user, mainmenuthings.sort, !mainmenuthings.reverse,
                    mainmenuthings.login, mainmenuthings.language);
                Console.Clear();
            }
            else if (indexstring.ToLower() == "s" || indexstring.ToLower() == "search")
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Type hier de film die u wilt zoeken: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string movsearch = Console.ReadLine();
                inputcheck(movsearch, mainmenulist);
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
                    else if (index < 0)
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

            mainPagina(index);
        }

        //functie die de ingetypte film zoekt in de JSON met alle films
        public static Tuple<bool, int> search(string searchmov, List<string> mainmenulist = null,
            bool InMovieMenu = true)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            List<string> moviesearchlist = new List<string>();
            if (InMovieMenu)
            {
                try
                {
                    int result = Int32.Parse(searchmov) - 1;
                    searchmov = mainmenulist[result];
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            searchmov = searchmov.ToLower().Replace(" ", "");
            for (int i = 0; i < jsonFilms.movieList.Count(); i++)
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
                if (searchmov.Count() < moviesearchlist[i].Count() - 6 && searchmov.Count() >= 4)
                {
                    temp = LevenshteinDistance.Compute(searchmov, moviesearchlist[i]) -
                           (moviesearchlist[i].Count() - searchmov.Count());
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
                return Tuple.Create(true, containindex);
            }
            else if (lowest < (searchmov.Count() / 4) + 1)
            {
                Console.Clear();
                return Tuple.Create(true, lowestindex);
            }
            else
            {
                return Tuple.Create(false, 0);
            }
        }

        //functie om alle kenmerken van een film te laten zien
        public static Tuple<string, bool, int, string, List<string>> showmov(string movsearch,
            List<string> mainmenulist = null)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            string gen = null;
            string act = null;
            bool newline = false;
            bool hastrailer = false;
            string trailer = null;
            Tuple<bool, int> SearchResult = search(movsearch, mainmenulist);
            if (SearchResult.Item1)
            {
                int tempMovie = SearchResult.Item2;
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
                        if ((i % 90 == 0 && i != 0) || newline)
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

                return Tuple.Create(trailer, hastrailer, jsonFilms.movieList[tempMovie].movieid, movsearch,
                    mainmenulist);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Deze film is niet gevonden!");
                Console.ForegroundColor = ConsoleColor.White;
                Thread.Sleep(1000);
                return null;
            }
        }

        public static void inputcheck(string movsearch, List<string> mainmenulist = null)
        {
            Tuple<string, bool, int, string, List<string>> MovieInformation = showmov(movsearch, mainmenulist);
            if (MovieInformation != null)
            {
                MovieInterpreter chosenmovie =
                    Films.FromJson().movieList.Single(movie => movie.movieid == MovieInformation.Item3);
                MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                bool oldenough;
                if (things.user == null) { oldenough = false; }
                else
                {
                    oldenough = Registerscreen.AgeVerify(things.user.age, chosenmovie.leeftijd);
                }
                bool hastrailer = MovieInformation.Item2;
                string trailer = MovieInformation.Item1;
                string moviename = chosenmovie.name;
                //hierna moet als er ja geselecteerd is het resrvatie scherm komen!
                IEnumerable<Zaal> selectedzalen = Zalen.FromJson().zalenList.Where(movie => movie.film.name.ToLower() == moviename.ToLower() && DateTime.Parse(movie.date) >= DateTime.Today); //fixt ook de out dated films
                if (things.login == "Admin")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Je kan als admin niet reserveren voor {moviename}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (things.user == null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Je bent niet ingelogd!\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (things.login == "Employee")
                {
                    Reservations.MakeReservationForCustomers(moviename);
                }
                else if (!oldenough)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Je bent niet oud genoeg voor {moviename}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (selectedzalen.Count() == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Vraag aan een medewerker voor een nieuwe screening voor {moviename}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (selectedzalen.Count() != 0 && oldenough && things.user != null)
                {
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
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"Je kan niet reserveren voor {moviename}\n");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                ConsoleKey keypressed = Console.ReadKey(true).Key;
                if (hastrailer)
                {
                    if (keypressed == ConsoleKey.T)
                    {
                        try
                        {
                            Process.Start(trailer);
                            Console.Clear();
                            inputcheck(movsearch, mainmenulist);
                        }
                        catch (Exception)
                        {
                            Console.Clear();
                            inputcheck(movsearch, mainmenulist);
                        }
                    } // Idiot Esception

                    if (selectedzalen.Count() != 0 && things.user != null)
                    {
                        //verander Console.WriteLine("succes"); naar het reserveer scherm van hogo
                        if (keypressed == ConsoleKey.J)
                        {
                            Zalen zalen = Zalen.FromJson();
                            Tuple<bool, List<Zaal>> zalenMetNaam = zalen.selectZalen(moviename);
                            if (zalenMetNaam.Item1)
                            {
                                zalen.menu(zalenMetNaam.Item2);
                                var json = zalen.ToJson();
                                Json.WriteJson(Json.Zalen, json);
                            }
                        }
                    }
                }
            }
        }

        public class MovieInterpreter
        {
            public int movieid { get; set; }
            public string name { get; set; }
            public string releasedate { get; set; }
            public List<string> genres { get; set; }
            public int leeftijd { get; set; }
            public double beoordeling { get; set; }
            public List<string> acteurs { get; set; }
            public string taal { get; set; }
            public string beschrijving { get; set; }
            public string trailer { get; set; }
            public int MovieTime { get; set; }

            public void setFilm(int movieid, string name, string releasedate, List<string> genres, int leeftijd,
                double beoordeling, List<string> acteurs, int movieTime = 0, string taal = null,
                string beschrijving = null,
                string trailer = null)
            {
                this.movieid = movieid;
                this.name = name;
                this.releasedate = releasedate;
                this.genres = genres.ToList();
                this.leeftijd = leeftijd;
                this.beoordeling = beoordeling;
                this.acteurs = acteurs.ToList();
                this.taal = taal;
                this.beschrijving = beschrijving;
                this.trailer = trailer;
                this.MovieTime = movieTime;
            }
        }

        public class Films
        {
            public List<MovieInterpreter> movieList { get; set; }

            public void addFilm(MovieInterpreter newMovie)
            {
                if (movieList == null)
                {
                    List<MovieInterpreter> newMovieList = new List<MovieInterpreter>();
                    newMovieList.Add(newMovie);
                    movieList = newMovieList;
                }
                else
                {
                    movieList.Add(newMovie);
                }
            }

            public string ToJson(bool write = false)
            {
                if (write)
                {
                    JsonSerializerOptions opt = new JsonSerializerOptions() {WriteIndented = true};
                    string json = JsonSerializer.Serialize(this, opt);
                    Json.WriteJson("Films", json);
                    return null;
                }
                else
                {
                    JsonSerializerOptions opt = new JsonSerializerOptions() {WriteIndented = true};
                    return JsonSerializer.Serialize(this, opt);
                }
            }

            public static Films FromJson(string json) => JsonSerializer.Deserialize<Films>(json);

            public static Films FromJson()
            {
                string json = Json.ReadJson("Films");
                return JsonSerializer.Deserialize<Films>(json);
            }
        }

        public class MovieMethods
        {
            public static void UpdateMovieMenu(string json, Films jsonFilms, MovieInterpreter tempMovie)
            {
                bool done = false;
                while (done == false)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    Console.WriteLine($"Gekozen Film: {tempMovie.name}\n");
                    Console.WriteLine("======FILM AANPASSEN MENU======");
                    Console.WriteLine("1. Naam aanpassen");
                    Console.WriteLine("2. Releasedatum aanpassen");
                    Console.WriteLine("3. Genres aanpassen");
                    Console.WriteLine("4. Acteurs aanpassen");
                    Console.WriteLine("5. Minimumleeftijd aanpassen");
                    Console.WriteLine("6. Beoordeling aanpassen");
                    Console.WriteLine("7. Taal aanpassen");
                    Console.WriteLine("8. Beschrijving aanpassen");
                    Console.WriteLine("A. Tijdsduur aanpassen");

                    ConsoleKeyInfo keyReaded = Console.ReadKey();

                    switch (keyReaded.Key)
                    {
                        case ConsoleKey.D1:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            UpdateNameMovie(json, jsonFilms, tempMovie);
                            break;
                        case ConsoleKey.D2:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            UpdateReleaseDate(json, jsonFilms, tempMovie);
                            break;
                        case ConsoleKey.D3:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            UpdateGenres(json, jsonFilms, tempMovie);
                            break;
                        case ConsoleKey.D4:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            UpdateActors(json, jsonFilms, tempMovie);
                            break;
                        case ConsoleKey.D5:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            Tuple<string, Films, MovieInterpreter> e = UpdateMinimumAge(json, jsonFilms, tempMovie);
                            if (e.Item1 == "fail")
                            {
                                while (e.Item1 == "fail")
                                {
                                    e = UpdateMinimumAge(json, jsonFilms, tempMovie);
                                }
                            }

                            break;
                        case ConsoleKey.D6:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            Tuple<string, Films, MovieInterpreter> f = UpdateReviewScore(json, jsonFilms, tempMovie);
                            if (f.Item1 == "fail")
                            {
                                while (f.Item1 == "fail")
                                {
                                    f = UpdateReviewScore(json, jsonFilms, tempMovie);
                                }
                            }

                            break;
                        case ConsoleKey.D7:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            UpdateLanguage(json, jsonFilms, tempMovie);
                            break;
                        case ConsoleKey.D8:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            UpdateDescription(json, jsonFilms, tempMovie);
                            break;
                        case ConsoleKey.D9:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            UpdateTrailer(json, jsonFilms, tempMovie);
                            break;
                        case ConsoleKey.A:
                            MainMenu.ClearAndShowLogoPlusEsc("Update");
                            Tuple<string, Films, MovieInterpreter> g = UpdateMovieTime(json, jsonFilms, tempMovie);
                            if (g.Item1 == "fail")
                            {
                                while (g.Item1 == "fail")
                                {
                                    g = UpdateMovieTime(json, jsonFilms, tempMovie);
                                }
                            }

                            break;

                        case ConsoleKey.Escape:
                            done = true;
                            break;
                    }
                }
            }

            /// <summary>
            /// Checks if movie exists in json, if found returns the movie
            /// </summary>
            /// <param name="movname"></param>
            /// <returns></returns>
            public static Tuple<bool, MovieInterpreter> DoesMovieExist(string movname)
            {
                MovieInterpreter noMovie = new MovieInterpreter();
                try
                {
                    string json = Json.ReadJson("Films");
                    Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
                    try
                    {
                        MovieInterpreter tempMovie = new MovieInterpreter();
                        tempMovie = jsonFilms.movieList.Single(movie => movie.name.ToLower() == movname.ToLower());
                        return Tuple.Create(true, tempMovie);
                    }
                    catch (NullReferenceException)
                    {
                        return Tuple.Create(false, noMovie);
                    }

                }
                catch (Exception)
                {
                    return Tuple.Create(false, noMovie);
                }
            }

            public static MovieInterpreter AddMovie()
            {
                string json = Json.ReadJson("Films");
                Films MovieLibrary = JsonSerializer.Deserialize<Films>(json);

                Console.WriteLine("\nVoeg hier een nieuwe film toe.");
                Console.WriteLine("Naam film:");
                string naamFilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (naamFilm == "1go2to3main4menu5")
                {
                    goto exit;
                }

                Console.WriteLine("Releasedatum film: (dd/mm/yyyy)");
                string releasedatumFilm = loginscherm.getdate();
                if (releasedatumFilm == "1go2to3main4menu5")
                {
                    goto exit;
                }

                Console.WriteLine("Voeg tussen elke genre een komma toe, bijvoorbeeld: Komedie, Actie, Thriller");
                Console.WriteLine("Genres film:");
                string genres = loginscherm.newwayoftyping();
                if (genres == "1go2to3main4menu5")
                {
                    goto exit;
                }

                List<string> genresFilm = genres.Split(',').Select(p => p.Trim()).ToList();

                for (int i = 0; i < genresFilm.Count; i++)
                {
                    genresFilm[i] = loginscherm.FirstCharToUpper(genresFilm[i]);
                }

                Console.WriteLine(
                    "Voeg tussen elke acteur een komma toe, bijvoorbeeld: Sean Connery, Ryan Gosling, Ryan Reynolds");
                Console.WriteLine("Acteurs film:");
                string acteurs = loginscherm.newwayoftyping();

                if (acteurs == "1go2to3main4menu5")
                {
                    goto exit;
                }

                List<string> acteursFilm = acteurs.Split(',').Select(p => p.Trim()).ToList();

                for (int i = 0; i < acteursFilm.Count; i++)
                {
                    List<string> tempActeur = acteursFilm[i].Split(' ').ToList();
                    for (int j = 0; j < tempActeur.Count; j++)
                    {
                        tempActeur[j] = loginscherm.FirstCharToUpper(tempActeur[j]);
                    }

                    acteursFilm[i] = string.Join(" ", tempActeur);
                }

                Console.WriteLine("Minimumleeftijd film:");
                var minimumleeftijd = CPeople.converttoint(loginscherm.newwayoftyping());
                int minimumLeeftijd;
                if (minimumleeftijd is string)
                {
                    goto exit;
                }

                minimumLeeftijd = minimumleeftijd;

                Console.WriteLine("Beoordeling film: (bijv. 8.0)");
                var beoordelingFilm2 = CPeople.converttodouble(loginscherm.newwayoftyping());
                double beoordelingFilm;
                if (beoordelingFilm2 is string)
                {
                    goto exit;
                }

                beoordelingFilm = beoordelingFilm2;

                Console.WriteLine("Taal film:");
                string taalfilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (taalfilm == "1go2to3main4menu5")
                {
                    goto exit;
                }

                Console.WriteLine("Tijdsduur film in minuten:");
                var movietime = CPeople.converttoint(loginscherm.newwayoftyping());
                int movieTime;
                if (movietime is string)
                {
                    goto exit;
                }

                movieTime = movietime;

                Console.WriteLine("Beschrijving film:");
                string beschrijvingfilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (beschrijvingfilm == "1go2to3main4menu5")
                {
                    goto exit;
                }

                Console.WriteLine("Trailer film:");
                string trailerfilm = loginscherm.newwayoftyping();
                if (trailerfilm == "1go2to3main4menu5")
                {
                    goto exit;
                }

                if (MovieLibrary.movieList.Count > 0)
                {
                    var lastMovieInList = MovieLibrary.movieList[MovieLibrary.movieList.Count - 1];
                    MovieInterpreter Movie = new MovieInterpreter();
                    Movie.setFilm(lastMovieInList.movieid + 1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd,
                        beoordelingFilm, acteursFilm, movieTime, taalfilm, beschrijvingfilm, trailerfilm);

                    MovieLibrary.addFilm(Movie);
                    JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                    json = JsonSerializer.Serialize(MovieLibrary, opt);

                    Json.WriteJson("Films", json);

                    Console.Clear();
                    MainMenu.Logo();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Film succesvol toegevoegd aan het aanbod.");
                    Thread.Sleep(1500);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return Movie;
                }
                else
                {
                    MovieInterpreter Movie = new MovieInterpreter();
                    Movie.setFilm(1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd, beoordelingFilm,
                        acteursFilm, movieTime, taalfilm, beschrijvingfilm, trailerfilm);

                    MovieLibrary.addFilm(Movie);
                    JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                    json = JsonSerializer.Serialize(MovieLibrary, opt);

                    Json.WriteJson("Films", json);

                    Console.Clear();
                    MainMenu.Logo();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Film succesvol toegevoegd aan het aanbod.");
                    Thread.Sleep(1500);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return Movie;
                }

                exit:
                MovieInterpreter Movie2 = new MovieInterpreter();
                Movie2.setFilm(234733, "1go2to3main4menu5", "", new List<string>(), 999, 888, new List<string>(), 777,
                    "", "");
                return Movie2;
            }


            public static Tuple<string, Films, MovieInterpreter> UpdateNameMovie(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wat is de nieuwe naam van de film {tempMovie.name}?");
                string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (newName == "1go2to3main4menu5")
                {
                    goto exit;
                }

                tempMovie.name = newName;

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Film naam is succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateReleaseDate(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wat is de nieuwe releasedatum van de film {tempMovie.name}?");
                string newReleaseDate = loginscherm.getdate();
                if (newReleaseDate == "1go2to3main4menu5")
                {
                    goto exit;
                }

                tempMovie.releasedate = newReleaseDate;

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("De releasedatum is succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateGenres(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wat is/zijn de nieuwe genre(s) van de film {tempMovie.name}?");
                Console.WriteLine("Voeg tussen elke genre een komma toe, bijvoorbeeld: Komedie, Actie, Thriller");
                Console.WriteLine("Genres film:");
                string genres = loginscherm.newwayoftyping();

                if (genres == "1go2to3main4menu5")
                {
                    goto exit;
                }

                List<string> newGenres = genres.Split(',').Select(p => p.Trim()).ToList();
                for (int i = 0; i < newGenres.Count; i++)
                {
                    newGenres[i] = loginscherm.FirstCharToUpper(newGenres[i]);
                }

                tempMovie.genres = newGenres;

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("De genre(s) is/zijn succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateActors(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wie zijn de nieuwe acteurs van de film {tempMovie.name}?");
                Console.WriteLine(
                    "Voeg tussen elke acteur een komma toe, bijvoorbeeld: Sean Connery, Ryan Gosling, Ryan Reynolds");
                Console.WriteLine("Acteurs film:");
                string actors = loginscherm.newwayoftyping();

                if (actors == "1go2to3main4menu5")
                {
                    goto exit;
                }

                List<string> newActors = actors.Split(',').Select(p => p.Trim()).ToList();
                for (int i = 0; i < newActors.Count; i++)
                {
                    List<string> tempActeur = newActors[i].Split(' ').ToList();
                    for (int j = 0; j < tempActeur.Count; j++)
                    {
                        tempActeur[j] = loginscherm.FirstCharToUpper(tempActeur[j]);
                    }

                    newActors[i] = string.Join(" ", tempActeur);
                }

                tempMovie.acteurs = newActors;

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("De acteur(s) is/zijn succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static dynamic ErrorMaker()
            {
                return "2a";
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateMinimumAge(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wat is de nieuwe minimum leeftijd van de film {tempMovie.name}? (0-18)");
                string newMinimumAge = loginscherm.newwayoftyping();
                if (newMinimumAge == "1go2to3main4menu5")
                {
                    return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                }

                try
                {
                    int ageing = Convert.ToInt32(newMinimumAge);
                    if (ageing < 0 || ageing > 18)
                    {
                        Convert.ToInt32(ErrorMaker());
                    }

                    tempMovie.leeftijd = ageing;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Voer een getal tussen de 0 en 18 in.");
                    Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return new Tuple<string, Films, MovieInterpreter>("fail", jsonFilms, tempMovie);
                }

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("De minimumleeftijd is succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateReviewScore(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wat is de nieuwe beoordeling van de film {tempMovie.name}? (0-10.0)");
                string newScore = loginscherm.newwayoftyping();
                if (newScore == "1go2to3main4menu5")
                {
                    goto exit;
                }

                try
                {
                    double ageing = Convert.ToDouble(newScore);
                    if (ageing < 0 || ageing > 10)
                    {
                        Convert.ToInt32(ErrorMaker());
                    }

                    tempMovie.beoordeling = ageing;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Voer een getal tussen de 0 en 10.0 in.");
                    Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return new Tuple<string, Films, MovieInterpreter>("fail", jsonFilms, tempMovie);
                }

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("De beoordeling is succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateDescription(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wat is de nieuwe beschrijving van de film {tempMovie.name}?");
                string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (newName == "1go2to3main4menu5")
                {
                    goto exit;
                }

                tempMovie.beschrijving = newName;

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Film beschrijving is succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateLanguage(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.Write("Terug naar het Update Menu (Esc)\n\n");
                Console.WriteLine($"Wat is de nieuwe taal van de film {tempMovie.name}?");
                string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (newName == "1go2to3main4menu5")
                {
                    goto exit;
                }

                tempMovie.taal = newName;

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Film taal is succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateTrailer(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.Clear();
                Console.Write("Terug naar het Update Menu (Esc)\n\n");
                Console.WriteLine($"Wat is de nieuwe trailer van de film {tempMovie.name}?");
                string newTrailer = loginscherm.newwayoftyping();
                if (newTrailer == "1go2to3main4menu5")
                {
                    goto exit;
                }

                tempMovie.trailer = newTrailer;

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Film trailer is succesvol gewijzigd.");
                Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                exit:
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }

            public static Tuple<string, Films, MovieInterpreter> UpdateMovieTime(string json, Films jsonFilms,
                MovieInterpreter tempMovie)
            {
                Console.WriteLine($"Wat is de tijdsduur van de film {tempMovie.name}?");
                string newMinimumAge = loginscherm.newwayoftyping();
                if (newMinimumAge == "1go2to3main4menu5")
                {
                    return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
                }

                try
                {
                    int newMovieTime = Convert.ToInt32(newMinimumAge);
                    if (newMovieTime < 0)
                    {
                        Convert.ToInt32(ErrorMaker());
                    }

                    tempMovie.MovieTime = newMovieTime;
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Voer een positief, heel getal in.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return new Tuple<string, Films, MovieInterpreter>("fail", jsonFilms, tempMovie);
                }

                JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                json = JsonSerializer.Serialize(jsonFilms, opt);
                Json.WriteJson("Films", json);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("De tijdsduur is succesvol gewijzigd.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
                return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
            }


            public static void DeleteMovie(string json, Films jsonFilms, string movieToRemove)
            {
                CPeople.Admin admin = new CPeople.Admin();

                int index = jsonFilms.movieList.FindIndex(movie =>
                    loginscherm.RemoveSpecialCharacters(movie.name) == movieToRemove);
                if (index == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Film niet gevonden. Probeer het nog een keer.");
                    Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    admin.DeleteMovies();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nFilm gevonden.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Weet u zeker dat u hem wilt verwijderen? (j/n)");
                    Console.ForegroundColor = ConsoleColor.Gray;

                    ConsoleKey keypressed = Console.ReadKey(true).Key;
                    while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
                    {
                        keypressed = Console.ReadKey(true).Key;
                    }

                    if (keypressed == ConsoleKey.Escape)
                    {
                        goto exit;
                    }

                    if (keypressed == ConsoleKey.J)
                    {
                        jsonFilms.movieList.RemoveAt(index);
                        JsonSerializerOptions opt = new JsonSerializerOptions {WriteIndented = true};
                        json = JsonSerializer.Serialize(jsonFilms, opt);
                        Json.WriteJson("Films", json);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Film is succesvol verwijderd.");
                        Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Clear();
                    }
                    else if (keypressed == ConsoleKey.N)
                    {
                        Console.Clear();
                        adminMenu.hoofdPagina();
                    }

                    exit: ; //?
                }
            }

            /// <summary>
            /// Berekent hoeveel pauzes er tijdens een film gehouden moeten worden. Bij een film die 150 duurt, wordt 1 pauze gehouden, voor elk uur wat daar bovenop komt wordt een extra pauze gehouden.
            /// Returnt een integer die het aantal pauzes weergeeft.
            /// </summary>
            /// <param name="movieTime"></param>
            /// <returns></returns>
            public static int movieBreaksCalculator(int movieTime)
            {
                int amountOfBreaks = 0;
                if (movieTime > 150)
                {
                    amountOfBreaks = (movieTime - 150) / 60 + 1;
                }

                return amountOfBreaks;
            }
        }
    }
}