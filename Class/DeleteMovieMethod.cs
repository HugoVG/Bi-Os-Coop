using System;
using System.Text.Json;

namespace Bi_Os_Coop
{
    public class DeleteMovieMethod
    {
        public static void DeleteMovie(string json, Films jsonFilms, string movieToRemove)
        {
            CPeople.Admin admin = new CPeople.Admin();

            int index = jsonFilms.movieList.FindIndex(movie => movie.name.ToLower().Replace(" ", "") == movieToRemove.ToLower().Replace(" ", ""));
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
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Weet u zeker dat u hem wilt verwijderen? (j/n)"); 
                
                ConsoleKey keypressed = Console.ReadKey(true).Key;
                while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
                {
                    keypressed = Console.ReadKey(true).Key;
                }
                if (keypressed == ConsoleKey.Escape) { goto exit; }
                if (keypressed == ConsoleKey.J)
                {
                    jsonFilms.movieList.RemoveAt(index);
                    json = JsonSerializer.Serialize(jsonFilms);
                    Json.WriteJson("Films", json);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Film is succesvol verwijderd.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Clear();
                }
                else if (keypressed == ConsoleKey.N)
                {
                    Console.WriteLine("Wilt u een andere film verwijderen? (j/n)");
                    keypressed = Console.ReadKey(true).Key;
                    while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
                    {
                        keypressed = Console.ReadKey(true).Key;
                    }
                    if (keypressed == ConsoleKey.Escape) { goto exit; }
                    if (keypressed == ConsoleKey.J)
                    {
                        admin.DeleteMovies();
                    }
                    else if (keypressed == ConsoleKey.N)
                    {
                        Console.WriteLine("U wordt nu teruggestuurd naar het admin menu.");
                        System.Threading.Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
            exit:
                return;
            }
        }
    }
}
