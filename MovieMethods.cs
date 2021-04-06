using System;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop
{
    public class MovieMethods
    {
        public static void UpdateMovieMenu(string naamFilm)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);

            MovieInterpreter tempMovie = jsonFilms.movieList.Single(movie => movie.name == naamFilm);

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
                Console.WriteLine("7. Exit");
               
                Console.WriteLine($"Wat wilt u aanpassen aan de film {naamFilm}");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        UpdateNameMovie(tempMovie.name);
                        break;
                    case "2":
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        break;
                    case "5":
                        Console.WriteLine();
                        break;
                    case "6":
                        Console.WriteLine();
                        break;
                    case "7":
                        adminMenu.hoofdPagina();
                        break;
                    default:
                        UpdateMovieMenu(naamFilm);
                        break;
                }
            }
        }

        public static void UpdateNameMovie(string naamFilm)
        {

        }
    }
}
