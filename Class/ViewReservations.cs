using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    class ViewReservations
    {
        /// <summary>
        /// returned een jagged list met in de list de eerste index de film en de rest de stoelen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<List<int>> ViewRes(int id)
        {
            string json = Json.ReadJson("Zalen");
            Zalen jsonZalen = System.Text.Json.JsonSerializer.Deserialize<Zalen>(json);
            List<List<int>> reservationslist = new List<List<int>>();
            List<int> seatlist = new List<int>();
            int movieindex = 0;
            int seatindex = 0;

            for (int i = 0; i < jsonZalen.zalenList.Count(); i++)
            {
                seatlist = new List<int>();
                movieindex = i;
                seatlist.Add(movieindex);
                for (int j = 0; j < jsonZalen.zalenList[i].stoelen.Count(); j++)
                {
                    if(jsonZalen.zalenList[i].stoelen[j].isOccupiedBy == id && jsonZalen.zalenList[i].stoelen[j].isOccupied)
                    {
                        seatindex = j + 1;
                        seatlist.Add(seatindex);
                    }
                }
                if (seatlist.Count() > 2)
                {
                    reservationslist.Add(seatlist);
                }
                else
                {
                    seatlist = new List<int>();
                }
            }
            //het eerste item in de list is de filmindex, de rest van de items zijn de stoelen
            return reservationslist;
        }
        /// <summary>
        /// laat de gereserveerde stoel(en) per film zien en de data van de film.
        /// call deze functie als je het scherm met reserveringen wilt laten zien.
        /// voer voor users bij id automatisch hun eigen id in vanuit de MainMenu.json
        /// </summary>
        /// <param name="id"></param>
        public static void ShowRes(int id, int index = 0)
        {
            string json = Json.ReadJson("Zalen");
            Zalen jsonZalen = System.Text.Json.JsonSerializer.Deserialize<Zalen>(json);
            //call to the function that does all the work (ViewRes)
            List<List<int>> reservationlist = ViewRes(id);
            MainMenu.Logo();
            Console.ForegroundColor = ConsoleColor.White;
            //wijzig dit later naar het profiel menu van Bjorn
            Console.WriteLine("Main menu (Esc)");
            if (reservationlist.Count() < 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("U heeft nog geen reserveringen!");
                Console.ForegroundColor = ConsoleColor.Gray;
                ConsoleKey keypressed = Console.ReadKey(true).Key;
                while (keypressed != ConsoleKey.Escape) { keypressed = Console.ReadKey(true).Key; }
                    //verander esc later naar Bjorn's menu
                if (keypressed == ConsoleKey.Escape) { Console.Clear(); MainMenu.MainMenuShow(); }
            }
            else
            {
                Console.WriteLine("Uw reserveringen: \n");
                Console.ForegroundColor = ConsoleColor.Gray;
                for (int i = 0; i < reservationlist.Count(); i++)
                {
                    //prints all the movie information
                    if (index == i)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("Titel: ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{jsonZalen.zalenList[reservationlist[i][0]].film.name}".PadRight(Console.WindowWidth - 10));
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("Datum: ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{jsonZalen.zalenList[reservationlist[i][0]].date}".PadRight(Console.WindowWidth - 10));
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("Tijd: ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{jsonZalen.zalenList[reservationlist[i][0]].time}".PadRight(Console.WindowWidth - 9));
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write("Zaal: ");
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.WriteLine($"{CheckWhichHall(jsonZalen.zalenList[reservationlist[i][0]].stoelen.Count())}".PadRight(Console.WindowWidth - 9));
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Stoelen: ".PadRight(Console.WindowWidth - 3));
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Titel: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(jsonZalen.zalenList[reservationlist[i][0]].film.name);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Datum: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(jsonZalen.zalenList[reservationlist[i][0]].date);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Tijd: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(jsonZalen.zalenList[reservationlist[i][0]].time);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Zaal: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine(CheckWhichHall(jsonZalen.zalenList[reservationlist[i][0]].stoelen.Count()));
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Stoelen: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    //prints all the chairs
                    int count = 0;
                    for (int j = 1; j < reservationlist[i].Count(); j++)
                    {
                        if (j != 1)
                        {
                            Console.Write($", ");
                        }
                        if (j % 20 == 0 && reservationlist[i].Count() > 20)
                        {
                            Console.Write("".PadRight(Console.WindowWidth - count - 3));
                            Console.Write("\n");
                            count = 0;
                        }
                        Console.Write($"{reservationlist[i][j]}");
                        count += reservationlist[i][j].ToString().Count() + 2;
                    }
                    Console.Write("".PadRight(Console.WindowWidth - count - 1));
                    Console.Write("\n\n");
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                ConsoleKey keypressed = Console.ReadKey(true).Key;
                //verander esc later naar Bjorn's menu
                if (keypressed == ConsoleKey.Escape) { Console.Clear(); MainMenu.MainMenuShow(); }
                else if (keypressed == ConsoleKey.DownArrow && index != reservationlist.Count() - 1) { Console.Clear(); ShowRes(id, index + 1); }
                else if (keypressed == ConsoleKey.UpArrow && index != 0) { Console.Clear(); ShowRes(id, index - 1); }
                else if (keypressed == ConsoleKey.Enter) { Console.Clear(); SelectedMovieMenu(id, jsonZalen.zalenList[reservationlist[index][0]].film.name, jsonZalen.zalenList[reservationlist[index][0]].film.movieid, jsonZalen.zalenList[reservationlist[index][0]].date, jsonZalen.zalenList[reservationlist[index][0]].time); }
                else
                {
                    Console.Clear();
                    ShowRes(id, index);
                }
            }
        }
        /// <summary>
        /// this menu let's you see what you can do with the resrvation you seleccted.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moviename"></param>
        /// <param name="movieid"></param>
        /// <param name="datum"></param>
        /// <param name="tijd"></param>
        public static void SelectedMovieMenu(int id, string moviename, int movieid, string datum, string tijd)
        {
            Console.Clear();
            MainMenu.Logo();
            Console.WriteLine("Uw reserveringen (Esc)");
            Console.WriteLine($"Titel: {moviename}\nDatum: {datum}\nTijd: {tijd}");
            Console.WriteLine($"\nKies hier wat u met de film {moviename} wilt doen:");
            Console.WriteLine($"1) Film details bekijken");
            Console.WriteLine($"2) Reservering wijzigen");
            Console.WriteLine($"3) Reservering anuleren");

            ConsoleKey keypressed = Console.ReadKey(true).Key;
            while (keypressed != ConsoleKey.Escape && keypressed != ConsoleKey.D1 && keypressed != ConsoleKey.D2 && keypressed != ConsoleKey.D3) { keypressed = Console.ReadKey(true).Key; }
            if (keypressed == ConsoleKey.Escape) { Console.Clear(); ShowRes(id); }
            else if (keypressed == ConsoleKey.D1) { Console.Clear(); ShowMovieDetails(id, moviename, movieid, datum, tijd); }
            //vragen aan hugo of dit ingebouwd zit, zo ja, link het dan aan d2
            else if (keypressed == ConsoleKey.D2) { Console.Clear(); ShowRes(id); }
            //vragen of hugo hier iets voor heeft, anders zelf maken
            else if (keypressed == ConsoleKey.D3) { Console.Clear(); ShowRes(id); }
        }

        /// <summary>
        /// Displays all the details about the movie (almost same as it is displayed in moviemenu).
        /// </summary>
        /// <param name="id"></param>
        /// <param name="moviename"></param>
        /// <param name="movieid"></param>
        /// <param name="datum"></param>
        /// <param name="tijd"></param>
        public static void ShowMovieDetails(int id, string moviename = null, int movieid = 0, string datum = null, string tijd = null)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            int tempMovie = 0;

            for (int i = 0; i < jsonFilms.movieList.Count(); i++)
            {
                if (jsonFilms.movieList[i].movieid == movieid)
                {
                    tempMovie = i;
                }
            }

            string gen = null;
            string act = null;
            bool newline = false;
            bool hastrailer = false;
            string trailer = null;
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
            Console.WriteLine("Film reservering menu (Esc)\n");
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
                    if ((i % 90 == 0 && i != 0) || newline == true)
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

            ConsoleKey keypressed = Console.ReadKey(true).Key;
            if (hastrailer == true)
            {
                if (keypressed == ConsoleKey.T) { System.Diagnostics.Process.Start(trailer); Console.Clear(); ShowMovieDetails(id, moviename, movieid, datum, tijd); }
            }
            while (keypressed != ConsoleKey.Escape) { keypressed = Console.ReadKey(true).Key; }
            if (keypressed == ConsoleKey.Escape) { SelectedMovieMenu(id, moviename, movieid, datum, tijd); }
        }
        /// <summary>
        /// checks what the movie hall is depending on the amount of seats
        /// </summary>
        /// <param name="stoelen"></param>
        /// <returns></returns>
        public static int CheckWhichHall(int stoelen)
        {
            int zaalnummer = 404;
            if (stoelen == 630) { zaalnummer = 1; }
            else if (stoelen == 342) { zaalnummer = 2; }
            else if (stoelen == 168) { zaalnummer = 3; }
            return zaalnummer;
        }
    }
}
