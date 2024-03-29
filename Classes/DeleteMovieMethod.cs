﻿using System;
using System.Text.Json;

namespace Bi_Os_Coop
{
    public class DeleteMovieMethod
    {
        public static void DeleteMovie(string json, Films jsonFilms, string movieToRemove)
        {
            CPeople.Admin admin = new CPeople.Admin();

            int index = jsonFilms.movieList.FindIndex(movie => movie.name == movieToRemove);
            if (index == -1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Film niet gevonden. Probeer het nog een keer.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                admin.DeleteMovies();
            }
            else
            {
                Console.WriteLine("Film gevonden. Weet u zeker dat u hem wilt verwijderen? (ja/nee)");
                string answer = Console.ReadLine().ToLower();

                if (answer == "ja")
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
                else if (answer == "nee")
                {
                    Console.WriteLine("Wilt u een andere film verwijderen? (ja/nee)");
                    answer = Console.ReadLine().ToLower();
                    if (answer == "ja")
                    {
                        admin.DeleteMovies();
                    }
                    else if (answer == "nee")
                    {
                        Console.WriteLine("U wordt nu teruggestuurd naar het admin menu.");
                        System.Threading.Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        adminMenu.hoofdPagina();
                    }
                    else
                    {
                        Console.WriteLine("Antwoord niet begrepen. U keert automatisch terug naar het admin menu.");
                        System.Threading.Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        adminMenu.hoofdPagina();
                    }
                }
                else
                {
                    Console.WriteLine("Antwoord niet begrepen. Probeer het nog een keer.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    admin.DeleteMovies();
                }
            }
        }
    }
}
