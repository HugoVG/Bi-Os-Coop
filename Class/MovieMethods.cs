using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop
{
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
                Console.WriteLine("9. Trailer aanpassen");

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
                        if (e.Item1 == "fail") { while (e.Item1 == "fail") { e = UpdateMinimumAge(json, jsonFilms, tempMovie); } }
                        break;
                    case ConsoleKey.D6:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        Tuple<string, Films, MovieInterpreter> f = UpdateReviewScore(json, jsonFilms, tempMovie);
                        if (f.Item1 == "fail") { while (f.Item1 == "fail") { f = UpdateReviewScore(json, jsonFilms, tempMovie); } }
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
                    case ConsoleKey.Escape:
                        done = true;
                        break;
                }
            }
        }
        /// <summary>
        /// Checks if movie exists in json, if found returns the movie
        /// </summary>
        /// <param name="name"></param>
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

            Console.WriteLine("Voeg hier een nieuwe film toe.");
            Console.WriteLine("Naam film:");
            string naamFilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (naamFilm == "1go2to3main4menu5") { goto exit; }

            Console.WriteLine("Releasedatum film: (dd/mm/yyyy)");
            string releasedatumFilm = loginscherm.getdate();
            if (releasedatumFilm == "1go2to3main4menu5") { goto exit; }

            Console.WriteLine("Voeg tussen elke genre een komma toe, bijv: Komedie, Actie, Thriller");
            Console.WriteLine("Genres film:");
            string genres = loginscherm.newwayoftyping();
            if (genres == "1go2to3main4menu5") { goto exit; }
            List<string> genresFilm = genres.Split(',').Select(p => p.Trim()).ToList();

            for (int i = 0; i < genresFilm.Count; i++)
            {
                genresFilm[i] = loginscherm.FirstCharToUpper(genresFilm[i]);
            }

            Console.WriteLine("Voeg tussen elke acteur een komma toe, bijv: Sean Connery, Ryan Gosling, Ryan Reynolds");
            Console.WriteLine("Acteurs film:");
            string acteurs = loginscherm.newwayoftyping();

            if (acteurs == "1go2to3main4menu5") { goto exit; }
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
            if (minimumleeftijd is string) { goto exit; }
            else { minimumLeeftijd = minimumleeftijd; }

            Console.WriteLine("Beoordeling film: (bijv. 8.0)");
            var beoordelingFilm2 = CPeople.converttodouble(loginscherm.newwayoftyping());
            double beoordelingFilm;
            if (beoordelingFilm2 is string) { goto exit; }
            else { beoordelingFilm = beoordelingFilm2; }

            Console.WriteLine("Taal film:");
            string taalfilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (taalfilm == "1go2to3main4menu5") { goto exit; }

            Console.WriteLine("Tijdsduur film in minuten:");
            var movietime = CPeople.converttoint(loginscherm.newwayoftyping());
            int movieTime;
            if (movietime is string) { goto exit; }
            else { movieTime = movietime; }

            Console.WriteLine("Beschrijving film:");
            string beschrijvingfilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (beschrijvingfilm == "1go2to3main4menu5") { goto exit; }

            Console.WriteLine("Trailer film:");
            string trailerfilm = loginscherm.newwayoftyping();
            if (trailerfilm == "1go2to3main4menu5") { goto exit; }

            if (MovieLibrary.movieList.Count > 0)
            {
                var lastMovieInList = MovieLibrary.movieList[MovieLibrary.movieList.Count - 1];
                MovieInterpreter Movie = new MovieInterpreter();
                Movie.setFilm(lastMovieInList.movieid + 1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd, beoordelingFilm, acteursFilm, movieTime, taalfilm, beschrijvingfilm, trailerfilm);

                MovieLibrary.addFilm(Movie);
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                json = JsonSerializer.Serialize(MovieLibrary, opt);

                Json.WriteJson("Films", json);

                Console.Clear();
                MainMenu.Logo();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Film succesvol toegevoegd aan het aanbod.");
                System.Threading.Thread.Sleep(1500);
                Console.ForegroundColor = ConsoleColor.Gray;
                return Movie;
            }
            else
            {
                MovieInterpreter Movie = new MovieInterpreter();
                Movie.setFilm(1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd, beoordelingFilm, acteursFilm, movieTime, taalfilm, beschrijvingfilm, trailerfilm);

                MovieLibrary.addFilm(Movie);
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                json = JsonSerializer.Serialize(MovieLibrary, opt);

                Json.WriteJson("Films", json);

                Console.Clear();
                MainMenu.Logo();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Film succesvol toegevoegd aan het aanbod.");
                System.Threading.Thread.Sleep(1500);
                Console.ForegroundColor = ConsoleColor.Gray;
                return Movie;
            }

        exit:
            MovieInterpreter Movie2 = new MovieInterpreter();
            Movie2.setFilm(234733, "1go2to3main4menu5", "", new List<string>(), 999, 888, new List<string>(), 777, "", "");
            return Movie2;
        }


        public static Tuple<string, Films, MovieInterpreter> UpdateNameMovie(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.WriteLine($"Wat is de nieuwe naam van de film {tempMovie.name}?");
            string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (newName == "1go2to3main4menu5") { goto exit; }
            tempMovie.name = newName;

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Film naam is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static Tuple<string, Films, MovieInterpreter> UpdateReleaseDate(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.WriteLine($"Wat is de nieuwe releasedatum van de film {tempMovie.name}?");
            string newReleaseDate = loginscherm.getdate();
            if (newReleaseDate == "1go2to3main4menu5") { goto exit; }
            tempMovie.releasedate = newReleaseDate;

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De releasedatum is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static Tuple<string, Films, MovieInterpreter> UpdateGenres(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.WriteLine($"Wat is/zijn de nieuwe genre(s) van de film {tempMovie.name}?");
            Console.WriteLine("Voeg tussen elke genre een komma toe, bijv: Komedie, Actie, Thriller");
            Console.WriteLine("Genres film:");
            string genres = loginscherm.newwayoftyping();

            if (genres == "1go2to3main4menu5") { goto exit; }

            List<string> newGenres = genres.Split(',').Select(p => p.Trim()).ToList();
            for (int i = 0; i < newGenres.Count; i++)
            {
                newGenres[i] = loginscherm.FirstCharToUpper(newGenres[i]);
            }

            tempMovie.genres = newGenres;

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De genre(s) is/zijn succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static Tuple<string, Films, MovieInterpreter> UpdateActors(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.WriteLine($"Wie zijn de nieuwe acteurs van de film {tempMovie.name}?");
            Console.WriteLine("Voeg tussen elke acteur een komma toe, bijv: Sean Connery, Ryan Gosling, Ryan Reynolds");
            Console.WriteLine("Acteurs film:");
            string actors = loginscherm.newwayoftyping();

            if (actors == "1go2to3main4menu5") { goto exit; }

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

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De acteur(s) is/zijn succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static dynamic errormaker()
        {
            return "2a";
        }

        public static Tuple<string, Films, MovieInterpreter> UpdateMinimumAge(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.WriteLine($"Wat is de nieuwe minimum leeftijd van de film {tempMovie.name}? (0-18)");
            string newMinimumAge = loginscherm.newwayoftyping();
            if (newMinimumAge == "1go2to3main4menu5") { return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie); }
            try
            {
                int ageing = Convert.ToInt32(newMinimumAge);
                if (ageing < 0 || ageing > 18) { Convert.ToInt32(errormaker()); }
                tempMovie.leeftijd = ageing;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Voer een getal tussen de 0 en 18 in.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                return new Tuple<string, Films, MovieInterpreter>("fail", jsonFilms, tempMovie);
            }

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De minimumleeftijd is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static Tuple<string, Films, MovieInterpreter> UpdateReviewScore(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.WriteLine($"Wat is de nieuwe beoordeling van de film {tempMovie.name}? (0-10.0)");
            string newScore = loginscherm.newwayoftyping();
            if (newScore == "1go2to3main4menu5") { goto exit; }
            try
            {
                double ageing = Convert.ToDouble(newScore);
                if (ageing < 0 || ageing > 10) { Convert.ToInt32(errormaker()); }
                tempMovie.beoordeling = ageing;
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Voer een getal tussen de 0 en 10.0 in.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                return new Tuple<string, Films, MovieInterpreter>("fail", jsonFilms, tempMovie);
            }

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De beoordeling is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static Tuple<string, Films, MovieInterpreter> UpdateDescription(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.WriteLine($"Wat is de nieuwe beschrijving van de film {tempMovie.name}?");
            string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (newName == "1go2to3main4menu5") { goto exit; }
            tempMovie.beschrijving = newName;

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Film beschrijving is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }
        public static Tuple<string, Films, MovieInterpreter> UpdateLanguage(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wat is de nieuwe taal van de film {tempMovie.name}?");
            string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (newName == "1go2to3main4menu5") { goto exit; }
            tempMovie.taal = newName;

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Film taal is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static Tuple<string, Films, MovieInterpreter> UpdateTrailer(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wat is de nieuwe trailer van de film {tempMovie.name}?");
            string newTrailer = loginscherm.newwayoftyping();
            if (newTrailer == "1go2to3main4menu5") { goto exit; }
            tempMovie.trailer = newTrailer;

            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonFilms, opt);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Film trailer is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        exit:
            return new Tuple<string, Films, MovieInterpreter>(json, jsonFilms, tempMovie);
        }

        public static void DeleteMovie(string json, Films jsonFilms, string movieToRemove)
        {
            CPeople.Admin admin = new CPeople.Admin();

            int index = jsonFilms.movieList.FindIndex(movie => loginscherm.RemoveSpecialCharacters(movie.name) == movieToRemove);
            if (index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Film niet gevonden. Probeer het nog een keer.");
                System.Threading.Thread.Sleep(1000);
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
                if (keypressed == ConsoleKey.Escape) { goto exit; }
                if (keypressed == ConsoleKey.J)
                {
                    jsonFilms.movieList.RemoveAt(index);
                    JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                    json = JsonSerializer.Serialize(jsonFilms, opt);
                    Json.WriteJson("Films", json);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Film is succesvol verwijderd.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Clear();
                }
                else if (keypressed == ConsoleKey.N)
                {
                    Console.Clear();
                    adminMenu.hoofdPagina();
                }
            exit:
                return;
            }
        }

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
