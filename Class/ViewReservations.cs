using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    static class ViewReservations
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
            if (jsonZalen != null)
                for (int i = 0; i < jsonZalen.zalenList.Count(); i++)
                {
                    var seatlist = new List<int>();
                    seatlist.Add(i);
                    for (int j = 0; j < jsonZalen.zalenList[i].stoelen.Count(); j++)
                    {
                        if (jsonZalen.zalenList[i].stoelen[j].isOccupiedBy == id && jsonZalen.zalenList[i].stoelen[j].isOccupied)
                        {seatlist.Add(j + 1);} // j is de seat index
                    }
                    if (seatlist.Count() > 1) 
                        reservationslist.Add(seatlist);
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
        /// <param name="index"></param>
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
                        if (jsonZalen != null)
                        {
                            Console.WriteLine(
                                $"{jsonZalen.zalenList[reservationlist[i][0]].film.name}".PadRight(Console.WindowWidth -
                                    10));
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Datum: ");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(
                                $"{jsonZalen.zalenList[reservationlist[i][0]].date}"
                                    .PadRight(Console.WindowWidth - 10));
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Tijd: ");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(
                                $"{jsonZalen.zalenList[reservationlist[i][0]].time}".PadRight(Console.WindowWidth - 9));
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.Write("Zaal: ");
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine(
                                $"{CheckWhichHall(jsonZalen.zalenList[reservationlist[i][0]].stoelen.Count())}"
                                    .PadRight(Console.WindowWidth - 9));
                        }

                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine("Stoelen: ".PadRight(Console.WindowWidth - 3));
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Titel: ");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        if (jsonZalen != null)
                        {
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
                            Console.WriteLine(
                                CheckWhichHall(jsonZalen.zalenList[reservationlist[i][0]].stoelen.Count()));
                        }

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
            Console.WriteLine($"2) Reservering anuleren");

            ConsoleKey keypressed = Console.ReadKey(true).Key;
            while (keypressed != ConsoleKey.Escape && keypressed != ConsoleKey.D1 && keypressed != ConsoleKey.D2 && keypressed != ConsoleKey.D3) { keypressed = Console.ReadKey(true).Key; }
            if (keypressed == ConsoleKey.Escape) { Console.Clear(); ShowRes(id); }
            else if (keypressed == ConsoleKey.D1) { Console.Clear(); ShowMovieDetails(id, moviename, movieid, datum, tijd); }
            //vragen aan hugo of dit ingebouwd zit, zo ja, link het dan aan d2
            else if (keypressed == ConsoleKey.D2)
            {
                Console.Clear();
                DeleteReservation(moviename, id);
            }
        }

        public static void DeleteReservation(string moviename, int id)
        {
            MainMenu.Logo();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"Wilt u de reservering voor {moviename} anuleren? (");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("J");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("/");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("N");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(")\n");

            ConsoleKey keypressed = Console.ReadKey(true).Key;
            if (keypressed == ConsoleKey.J)
            {
                string jsonZalen = Json.ReadJson("Zalen");
                Zalen zalen = Zalen.FromJson(jsonZalen);
                if (jsonZalen != null)
                {
                    for (int i = 0; i < zalen.zalenList.Count(); i++)
                    {
                        for (int j = 0; j < zalen.zalenList[i].stoelen.Count(); j++)
                        {
                            if (zalen.zalenList[i].stoelen[j].isOccupiedBy == id && zalen.zalenList[i].stoelen[j].isOccupied && zalen.zalenList[i].film.name == moviename)
                            {
                                zalen.zalenList[i].stoelen[j].isOccupiedBy = 1;
                                zalen.zalenList[i].stoelen[j].isOccupied = false;
                            }
                        }
                    }
                }
                Json.WriteJson("Zalen", zalen.ToJson());
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Resservering succesvol geanuleerd");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Druk op esc om terug te gaan");
                Console.ReadKey();
                Console.Clear();
                ShowRes(id);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Resservering niet geanuleerd");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Druk op esc om terug te gaan");
                Console.ReadKey();
                Console.Clear();
                ShowRes(id);
            }
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
            Tuple<string, bool, int, string, List<string>> MovieInformation = MovieMenu.showmov(moviename, null);

            ConsoleKey keypressed = Console.ReadKey(true).Key;
            if (MovieInformation.Item2)
            {
                if (keypressed == ConsoleKey.T) { System.Diagnostics.Process.Start(MovieInformation.Item1); Console.Clear(); ShowMovieDetails(id, moviename, movieid, datum, tijd); }
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
            int zaalnummer = 0b110010100;
            switch (stoelen)
            {
                case 0b1001110110:
                    zaalnummer = 0b1;
                    break;
                case 0b101010110:
                    zaalnummer = 0b10;
                    break;
                case 0b10101000:
                    zaalnummer = 0b11;
                    break;
            }
            return zaalnummer;
        }
    }

    public static class Reservations
    {
        public static int NumberOfPeople()
        {
            Console.WriteLine("\nVoor hoeveel personen moet je een reservering maken?");
            try
            {
                int count = Convert.ToInt32(Console.ReadLine());
                return count;
            }
            catch (FormatException)
            {
                Program.newEntry("Dit is geen geldig nummer. Probeer het nog eens", ConsoleColor.Yellow);
                System.Threading.Thread.Sleep(1000);
                MainMenu.ClearAndShowLogoPlusEsc("Film menu");
                return NumberOfPeople();
            }
        }

        public static CPeople.Person NotAnExistingCustomer()
        {
            MainMenu.ClearAndShowLogoPlusEsc("Film");
            Console.WriteLine("\nPersoon heeft nog geen account. Een account is verplicht om naar de film te kunnen gaan.");
            Console.WriteLine("Wil de persoon zich registreren? (j/n)");
            ConsoleKey keypressed = Console.ReadKey(true).Key;
            while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
            {
                keypressed = Console.ReadKey(true).Key;
            }
            if (keypressed == ConsoleKey.Escape) { goto exit; }
            if (keypressed == ConsoleKey.J) // person wants to create a new account
            {
                Console.Clear();
                return Registerscreen.CreateAccount();

            }
            else if (keypressed == ConsoleKey.N) // person is send to main menu
            {
                Console.WriteLine("\nZonder account kan er geen film gereserveerd worden. U keert nu terug naar het filmoverzicht.");
                System.Threading.Thread.Sleep(2500);
                Console.Clear();
                MovieMenu.mainPagina();
            }
        exit:
            return null;
        }

        public static void MakeReservationForCustomers(string movieName)
        {
            MainMenu.ClearAndShowLogoPlusEsc("Film");
            List<CPeople.Person> personsToMakeReservationFor = new List<CPeople.Person>();
            int numberOfPeople = NumberOfPeople();

            for (int i = 1; i <= numberOfPeople; i++)
            {
                Console.WriteLine($"\nNaam Persoon {i}:");
                string name = loginscherm.newwayoftyping();
                if (name == "1go2to3main4menu5") { goto exit; }

                Console.WriteLine($"Geboortedatum Persoon {i}:");
                string currentAge = loginscherm.getdate();
                if (currentAge == "1go2to3main4menu5") { goto exit; }

                // checks if name and age are in the peopleList
                if (PasswordMethods.NameBirthdayCheck(name, currentAge))
                {
                    string account = Json.ReadJson("Accounts");
                    CPeople.People accounts = CPeople.People.FromJson(account);
                    CPeople.Person existingPerson = accounts.peopleList.Single(person => person.name.ToLower() == name.ToLower() && person.age == currentAge);
                    personsToMakeReservationFor.Add(existingPerson);
                }
                else
                {
                    CPeople.Person newCustomer = NotAnExistingCustomer();
                    if (newCustomer != null)
                        personsToMakeReservationFor.Add(newCustomer);
                    else
                        MovieMenu.mainPagina();
                }
            }

            Zalen zalen = Zalen.FromJson();
            Tuple<bool, List<Zaal>> zalenMetNaam = zalen.selectZalen(movieName);
            if (zalenMetNaam.Item1)
            {
                string beforeChange = Json.ReadJson("Zalen");
                Zalen zalenBeforeChange = Zalen.FromJson(beforeChange);
                int count = 0;
                foreach (Zaal zaal in zalenMetNaam.Item2)
                {
                        foreach (Stoel stoel in zaal.stoelen)
                        {
                            if (stoel.isOccupied)
                            {
                                count += 1;
                            }
                        }
                }
                Tuple<int, int>[] occupiedStoelen = new Tuple<int, int>[count];
                Tuple<int, int>[] occupiedStoelen2 = new Tuple<int, int>[count+numberOfPeople];
                int tempIndex = 0;
                foreach (Zaal zaal in zalenMetNaam.Item2)
                {
                    int index = 0;
                    foreach (Stoel stoel in zaal.stoelen)
                    {
                        if (stoel.isOccupied && tempIndex < count)
                        {
                            occupiedStoelen[tempIndex++] = Tuple.Create(stoel.isOccupiedBy, index);
                        }
                        index++;
                    }
                }
                zalen.menu(zalenMetNaam.Item2);
                tempIndex = 0;
                foreach (Zaal zaal in zalenMetNaam.Item2)
                {
                    int index = 0;
                    foreach (Stoel stoel in zaal.stoelen)
                    {
                        if (stoel.isOccupied && tempIndex < count)
                        {
                            occupiedStoelen2[tempIndex++] = Tuple.Create(stoel.isOccupiedBy, index);
                        }
                        index++;
                    }
                }
                int person = 0;
                int chairs = 0;
                for (int i = 0; i < occupiedStoelen2.Length; i++)
                {
                    if (i < occupiedStoelen.Length)
                    {
                        if (occupiedStoelen[i].Item2 != occupiedStoelen2[i].Item2)
                        {
                            foreach (Zaal zaal in zalenMetNaam.Item2)
                            {
                                int index2 = 0;
                                foreach (Stoel stoel in zaal.stoelen)
                                {
                                    if (index2 == occupiedStoelen2[i].Item2 && chairs < numberOfPeople)
                                    {
                                        stoel.isOccupiedBy = personsToMakeReservationFor[person].id;
                                        chairs++;
                                    }
                                    index2++;
                                }
                            }
                        }
                    }
                }
                var json = zalen.ToJson();
                Json.WriteJson(Json.Zalen, json);
            }

        exit:
            return;
        }

        public static void MakeReservation(string movieName)
        {
            MainMenu.ClearAndShowLogoPlusEsc("Film");

            Console.WriteLine($"\nNaam Persoon:");
            string name = loginscherm.newwayoftyping();
            if (name == "1go2to3main4menu5") { goto exit; }

            Console.WriteLine($"Geboortedatum Persoon:");
            string currentAge = loginscherm.getdate();
            if (currentAge == "1go2to3main4menu5") { goto exit; }

            // checks if name and age are in the peopleList
            if (PasswordMethods.NameBirthdayCheck(name, currentAge))
            {
                string account = Json.ReadJson("Accounts");
                CPeople.People accounts = CPeople.People.FromJson(account);
                CPeople.Person existingPerson = accounts.peopleList.Single(person => person.name.ToLower() == name.ToLower() && person.age == currentAge);
            }
            else
            {
                CPeople.Person tempPerson = NotAnExistingCustomer();
                CPeople.Person existingPerson = null;
                if (tempPerson != null)
                    existingPerson = tempPerson;
                else
                    MovieMenu.mainPagina();
            }

            Zalen zalen = Zalen.FromJson();
            Tuple<bool, List<Zaal>> zalenMetNaam = zalen.selectZalen(movieName);
            if (zalenMetNaam.Item1)
            {
                zalen.menu(zalenMetNaam.Item2);
                var json = zalen.ToJson();
                Json.WriteJson(Json.Zalen, json);
            }

        exit:
            return;
        }
    }
}
