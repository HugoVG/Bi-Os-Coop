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
            Console.Clear();
            bool done = false;
            while (done == false)
            {
                Console.WriteLine("======FILM AANPASSEN MENU======");
                Console.WriteLine("1. Naam aanpassen");
                Console.WriteLine("2. Releasedatum aanpassen");
                Console.WriteLine("3. Genres aanpassen");
                Console.WriteLine("4. Acteurs aanpassen");
                Console.WriteLine("5. Minimumleeftijd aanpassen");
                Console.WriteLine("6. Beoordeling aanpassen");
                Console.WriteLine("7. Andere film aanpassen");
                Console.WriteLine("8. Exit");
               
                Console.WriteLine($"\nWat wilt u aanpassen aan de film {tempMovie.name}?");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UpdateNameMovie(json, jsonFilms, tempMovie);
                        break;
                    case "2":
                        UpdateReleaseDate(json, jsonFilms, tempMovie);
                        break;
                    case "3":
                        UpdateGenres(json, jsonFilms, tempMovie);
                        break;
                    case "4":
                        UpdateActors(json, jsonFilms, tempMovie);
                        break;
                    case "5":
                        UpdateMinimumAge(json, jsonFilms, tempMovie);
                        break;
                    case "6":
                        UpdateReviewScore(json, jsonFilms, tempMovie);
                        break;
                    case "7":
                        CPeople.Admin admin = new CPeople.Admin();
                        admin.UpdateMovies();
                        break;
                    case "8":
                        adminMenu.hoofdPagina();
                        break;
                    default:
                        Console.WriteLine("Kies voor optie 1-7 of verlaat het menu.");
                        UpdateMovieMenu(json, jsonFilms, tempMovie);
                        break;
                }
            }
        }

        public static void UpdateNameMovie(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.WriteLine($"Wat is de nieuwe naam van de film {tempMovie.name}?");
            string newName = Console.ReadLine();
            tempMovie.name = newName;

            json = JsonSerializer.Serialize(jsonFilms);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Film naam is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            UpdateMovieMenu(json, jsonFilms, tempMovie);
        }

        public static void UpdateReleaseDate(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.WriteLine($"Wat is de nieuwe releasedatum van de film {tempMovie.name}?");
            string newReleaseDate = Console.ReadLine();
            tempMovie.releasedate = newReleaseDate;

            json = JsonSerializer.Serialize(jsonFilms);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De releasedatum is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            UpdateMovieMenu(json, jsonFilms, tempMovie);
        }

        public static void UpdateGenres(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.WriteLine($"Wat is/zijn de nieuwe genre(s) van de film {tempMovie.name}?");
            Console.WriteLine("Voeg tussen elke genre een komma toe, bijv: Komedie, Actie, Thriller");
            Console.WriteLine("Genres film:");
            string genres = Console.ReadLine();
            List<string> newGenres = genres.Split(',').ToList();
            tempMovie.genres = newGenres;

            json = JsonSerializer.Serialize(jsonFilms);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De genre(s) is/zijn succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            UpdateMovieMenu(json, jsonFilms, tempMovie);
        }

        public static void UpdateActors(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.WriteLine($"Wie is/zijn de nieuwe acteurs van de film {tempMovie.name}?");
            Console.WriteLine("Voeg tussen elke acteur een komma toe, bijv: Sean Connery, Ryan Gosling, Ryan Reynolds");
            Console.WriteLine("Acteurs film:");
            string actors = Console.ReadLine();
            List<string> newActors = actors.Split(',').ToList();
            tempMovie.genres = newActors;

            json = JsonSerializer.Serialize(jsonFilms);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De acteur(s) is/zijn succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            UpdateMovieMenu(json, jsonFilms, tempMovie);
        }

        public static void UpdateMinimumAge(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.WriteLine($"Wat is de nieuwe minimum leeftijd van de film {tempMovie.name}? (0-18)");
            string newMinimumAge = Console.ReadLine();
            try
            {
                tempMovie.leeftijd = Convert.ToInt32(newMinimumAge);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Voer een getal tussen de 0 en 18 in.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                UpdateMinimumAge(json, jsonFilms, tempMovie);
            }

            json = JsonSerializer.Serialize(jsonFilms);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De minimumleeftijd is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            UpdateMovieMenu(json, jsonFilms, tempMovie);
        }

        public static void UpdateReviewScore(string json, Films jsonFilms, MovieInterpreter tempMovie)
        {
            Console.Clear();
            Console.WriteLine($"Wat is de nieuwe beoordeling van de film {tempMovie.name}? (0-10.0)");
            string newScore = Console.ReadLine();
            try
            {
                tempMovie.beoordeling = Convert.ToDouble(newScore);
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Voer een getal tussen de 0 en 10.0 in.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                UpdateReviewScore(json, jsonFilms, tempMovie);
            }

            json = JsonSerializer.Serialize(jsonFilms);
            Json.WriteJson("Films", json);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("De beoordeling is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            UpdateMovieMenu(json, jsonFilms, tempMovie);
        }

    }
}
