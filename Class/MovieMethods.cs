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
                Console.Clear();
                Console.WriteLine($"Gekozen Film: {tempMovie.name}\n");
                Console.WriteLine("======FILM AANPASSEN MENU======");
                Console.WriteLine("1. Naam aanpassen");
                Console.WriteLine("2. Releasedatum aanpassen");
                Console.WriteLine("3. Genres aanpassen");
                Console.WriteLine("4. Acteurs aanpassen");
                Console.WriteLine("5. Minimumleeftijd aanpassen");
                Console.WriteLine("6. Beoordeling aanpassen");
                Console.WriteLine("7. taal aanpassen");
                Console.WriteLine("8. Beschrijving aanpassen");
                Console.WriteLine("9. Andere film aanpassen");
                Console.WriteLine("0. Exit");

                ConsoleKeyInfo keyReaded = Console.ReadKey();

                switch (keyReaded.Key)
                {
                    case ConsoleKey.D1: 
                        UpdateNameMovie(json, jsonFilms, tempMovie);
                        break;
                    case ConsoleKey.D2:
                        UpdateReleaseDate(json, jsonFilms, tempMovie);
                        break;
                    case ConsoleKey.D3:
                        UpdateGenres(json, jsonFilms, tempMovie);
                        break;
                    case ConsoleKey.D4:
                        UpdateActors(json, jsonFilms, tempMovie);
                        break;
                    case ConsoleKey.D5:
                        Tuple<string, Films, MovieInterpreter> e = UpdateMinimumAge(json, jsonFilms, tempMovie);
                        if (e.Item1 == "fail") { while (e.Item1 == "fail") { e = UpdateMinimumAge(json, jsonFilms, tempMovie); } }
                        break;
                    case ConsoleKey.D6:
                        Tuple<string, Films, MovieInterpreter> f = UpdateReviewScore(json, jsonFilms, tempMovie);
                        if (f.Item1 == "fail") { while (f.Item1 == "fail") { f = UpdateReviewScore(json, jsonFilms, tempMovie); } }
                        break;
                    case ConsoleKey.D7:
                        UpdateLanguage(json, jsonFilms, tempMovie);
                        break;
                    case ConsoleKey.D8:
                        UpdateDescription(json, jsonFilms, tempMovie);
                        break;
                    case ConsoleKey.D9:
                        CPeople.Admin admin = new CPeople.Admin();
                        admin.UpdateMovies();
                        break;
                    case ConsoleKey.D0:
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Kies voor optie 1-9 of verlaat het menu.");
                        UpdateMovieMenu(json, jsonFilms, tempMovie);
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

        public static Tuple<string,Films,MovieInterpreter> UpdateNameMovie(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wat is de nieuwe naam van de film {tempMovie.name}?");
            string newName = loginscherm.newwayoftyping();
            if (newName == "1go2to3main4menu5") { goto exit; }
            tempMovie.name = newName;

            json = JsonSerializer.Serialize(jsonFilms);
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
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wat is de nieuwe releasedatum van de film {tempMovie.name}?");
            string newReleaseDate = loginscherm.getdate();
            if (newReleaseDate == "1go2to3main4menu5") { goto exit; }
            tempMovie.releasedate = newReleaseDate;

            json = JsonSerializer.Serialize(jsonFilms);
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
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wat is/zijn de nieuwe genre(s) van de film {tempMovie.name}?");
            Console.WriteLine("Voeg tussen elke genre een komma toe, bijv: Komedie, Actie, Thriller");
            Console.WriteLine("Genres film:");
            string genres = loginscherm.newwayoftyping();
            if (genres == "1go2to3main4menu5") { goto exit; }
            List<string> newGenres = genres.Split(',').ToList();
            List<string> newGenres2 = new List<string>();
            foreach (string genre in newGenres)
            {
                string genre2 = genre.Trim();
                newGenres2.Add(genre2);
            }
            tempMovie.genres = newGenres2;

            json = JsonSerializer.Serialize(jsonFilms);
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
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wie is/zijn de nieuwe acteurs van de film {tempMovie.name}?");
            Console.WriteLine("Voeg tussen elke acteur een komma toe, bijv: Sean Connery, Ryan Gosling, Ryan Reynolds");
            Console.WriteLine("Acteurs film:");
            string actors = loginscherm.newwayoftyping();
            if (actors == "1go2to3main4menu5") { goto exit; }
            List<string> newActors = actors.Split(',').ToList();
            tempMovie.genres = newActors;

            json = JsonSerializer.Serialize(jsonFilms);
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
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
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

            json = JsonSerializer.Serialize(jsonFilms);
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
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
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

            json = JsonSerializer.Serialize(jsonFilms);
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
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wat is de nieuwe beschrijving van de film {tempMovie.name}?");
            string newName = loginscherm.newwayoftyping();
            if (newName == "1go2to3main4menu5") { goto exit; }
            tempMovie.beschrijving = newName;

            json = JsonSerializer.Serialize(jsonFilms);
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
            Console.Clear();
            Console.Write("Terug naar het Update Menu (Esc)\n\n");
            Console.WriteLine($"Wat is de nieuwe taal van de film {tempMovie.name}?");
            string newName = loginscherm.newwayoftyping();
            if (newName == "1go2to3main4menu5") { goto exit; }
            tempMovie.taal = newName;

            json = JsonSerializer.Serialize(jsonFilms);
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
    }
}
