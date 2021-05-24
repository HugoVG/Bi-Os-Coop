using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Security;

namespace Bi_Os_Coop
{
    public class PeopleTest
    {
        public static void newADMIN()
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = JsonSerializer.Deserialize<CPeople.People>(json);
            CPeople.Admin adm = new CPeople.Admin();
            adm.setPerson(1, "Hugo", "Coolste@min.com", "nietzoveilig", "30", "06123456789");
            jsonPeople.AddAdmin(adm);
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonPeople, opt);
            Json.WriteJson("Accounts", json);
        }
    }
    public class CPeople
    {
        public static void NewUser()
        {
            string json = Json.ReadJson("Accounts");
            People jsonPeople = JsonSerializer.Deserialize<People>(json);

            jsonPeople.addPersonByFunction(3, "Bjorn", "json@bjorn.com", "jsonBjorn", "30", "06123456789");
            json = JsonSerializer.Serialize(jsonPeople);
            Json.WriteJson("Accounts", json);
            //Console.WriteLine(jsonPeople);
            // mac doet weer eens raar test
            Console.WriteLine(jsonPeople);
        }

        public static void TestMethodPerson()
        {
            CPeople.Person bjorn = new CPeople.Person();
            bjorn.setPerson(3, "Bjorn", "json@bjorn.com", "jsonBjorn", "30", "06123456789");
            bjorn.DeleteAccount(bjorn);
            // kleine aanpassing om te committen
        }

        /// <summary>
        /// Person class
        /// Fields:
        ///     id, name?, email?, password?, age
        /// </summary>
        public class Person
        {
            public int id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string age { get; set; }
            public string phonenumber { get; set; }
            public List<MovieInterpreter> BookedMovies { get; set; }
            //If you gonna edit this EDIT ALL
            public void setPerson(int id, string name, string email, string password, string age, string phonenumber)
            {
                this.id = id;
                this.name = name;
                this.email = email;
                this.password = password;
                this.age = age;
                this.phonenumber = phonenumber;
            }
            public bool isPerson()
            {
                if (this.GetType().Equals(typeof(Person))) {
                    return true;
                }
                else {return false; }
            }
            public bool isAdmin()
            {
                if (this.GetType().Equals(typeof(Admin)))
                {
                    return true;
                }
                else { return false; }
            }
            // general methods
            public Person Login()
            {
                Console.Clear();
                Person Ingelogd = loginscherm.login();
                return Ingelogd;
            }

            public Person Logout()
            {
                Person Ingelogd = new Person();
                return Ingelogd;
            }
            public void DeleteAccount(Person ingelogdepersoon)
            {
                Console.Clear();
                Console.Write("Terug naar Admin Menu (Esc)\n\n");
                Console.WriteLine("Wilt u uw account verwijderen? (j/n)");

                ConsoleKey keypressed = Console.ReadKey(true).Key;
                while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
                {
                    keypressed = Console.ReadKey(true).Key;
                }
                if (keypressed == ConsoleKey.Escape) { goto exit; }
                if (keypressed == ConsoleKey.J)
                    {
                    // asks for email and password of the person
                    Console.WriteLine("Vul uw emailadres in:");
                    string currentEmail = Console.ReadLine();

                    Console.WriteLine("Vul uw huidige wachtwoord in:");
                    SecureString pass = loginscherm.maskInputString();
                    string currentPassword = new System.Net.NetworkCredential(string.Empty, pass).Password;
                    if (currentPassword != "1go2to3main4menu5")
                    {
                        // checks if email and password are in the peopleList
                        if (PasswordMethods.MailWachtwoordCheck(currentEmail, currentPassword)) // both are correct
                        {
                            DeleteAccountMethod.DeleteAccount(ingelogdepersoon);
                        }
                        else
                        {
                            Console.WriteLine("Wachtwoord of email onjuist. Probeer het later nog eens.");
                            System.Threading.Thread.Sleep(2000);
                            Console.Clear();
                            MainMenu.MainMenuShow();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        MainMenu.MainMenuShow();
                    }
                }
                else if (keypressed == ConsoleKey.N)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bedankt voor het blijven!");
                    Console.WriteLine("U wordt nu teruggestuurd naar het hoofdmenu.");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    MainMenu.MainMenuShow();
                }
                else
                {
                    DeleteAccount(ingelogdepersoon);
                }
            exit:
                return;
            }

            public void ChangePassword(Person ingelogdepersoon)
            {
                Console.Clear();
                Console.Write("Terug naar Admin Menu (Esc)\n\n");

                // Checks if the person is logged in by checking if it has an ID
                if (ingelogdepersoon.id != 0) // person is logged in
                {
                    // asks for email and password of the person
                    Console.WriteLine("Vul uw emailadres in:");
                    string currentEmail = Console.ReadLine();

                    Console.WriteLine("Vul uw huidige wachtwoord in:");
                    string currentPassword = Console.ReadLine();

                    // checks if email and password are in the peopleList
                    if (PasswordMethods.MailWachtwoordCheck(currentEmail, currentPassword)) // both are correct
                    {
                        PasswordMethods.SetNewPassword(currentEmail, currentPassword);
                    }
                    else
                    {
                        PasswordMethods.PasswordEntries(); // if one of the two data is incorrect they get 3 entries
                    }
                }

                else if (ingelogdepersoon.id == 0) // person is not logged in
                {
                    // if the person is not logged in we ask for email and birthdate
                    Console.WriteLine("Vul uw emailadres in:");
                    string currentEmail = Console.ReadLine();

                    Console.WriteLine("Vul uw geboortedatum in: (dd/mm/jjjj)");
                    string currentAge = Console.ReadLine();

                    // checks if email and age are in the peopleList
                    if (PasswordMethods.MailLeeftijdCheck(currentEmail, currentAge))
                    {
                        PasswordMethods.SetNewPassword(currentEmail, currentAge); // both are correct
                    }
                    else
                    {
                        // if the person doesn't exist we ask if the person wants to make a new account, if not send to Main Menu
                        Console.WriteLine("Sorry, dit account bestaat niet.");
                        Console.WriteLine("Wilt u een nieuw account aanmaken? (j/n)");

                        ConsoleKey keypressed = Console.ReadKey(true).Key;
                        while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
                        {
                            keypressed = Console.ReadKey(true).Key;
                        }
                        if (keypressed == ConsoleKey.Escape) { goto exit; }
                        if (keypressed == ConsoleKey.J) // person wants to create a new account
                        {
                            Console.Clear();
                            Registerscreen.CreateAccount();
                        }
                        else if (keypressed == ConsoleKey.N) // person is send to main menu
                        {
                            Console.Clear();
                            MainMenu.MainMenuShow();
                        }
                    }
                exit:
                    return;
                }

                // in case the person has an ID other than 0 or not 0
                else
                {
                    throw new NotImplementedException();
                }
            }


            public void ViewReservedMovies()
            {

            }

            public void BookTicket()
            {

            }

            public void CancelTicket()
            {

            }
        }
        public static dynamic converttoint(string input)
        {
            try
            {
                return Convert.ToInt32(input);
            }
            catch
            {
                return "1go2to3main4menu5";
            }
        }
        public static dynamic converttodouble(string input)
        {
            try
            {
                return Convert.ToDouble(input);
            }
            catch
            {
                return "1go2to3main4menu5";
            }
        }

        public class Admin : Person
        {
            public MovieInterpreter AddMovies()
            {
                Console.Clear();
                Console.Write("Terug naar Admin Menu (Esc)\n\n");
                string json = Json.ReadJson("Films");
                Films MovieLibrary = JsonSerializer.Deserialize<Films>(json);

                Console.WriteLine("Voeg hier een nieuwe film toe.");
                Console.WriteLine("Naam film:");
                string naamFilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (naamFilm == "1go2to3main4menu5") { goto exit; }
                Console.WriteLine(naamFilm);

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

                Console.WriteLine("Tijdsduur film in minuten:");
                var movietime = converttoint(loginscherm.newwayoftyping());
                int movieTime;
                if (movietime is string) { goto exit; }
                else { movieTime = movietime; }

                Console.WriteLine("Minimumleeftijd film:");
                var minimumleeftijd = converttoint(loginscherm.newwayoftyping());
                int minimumLeeftijd;
                if (minimumleeftijd is string) { goto exit; }
                else { minimumLeeftijd = minimumleeftijd; }

                Console.WriteLine("Beoordeling film: (bijv. 8.0)");
                var beoordelingFilm2 = converttodouble(loginscherm.newwayoftyping());
                double beoordelingFilm;
                if (beoordelingFilm2 is string) { goto exit; }
                else { beoordelingFilm = beoordelingFilm2; }

                Console.WriteLine("Taal film:");
                string taalfilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (taalfilm == "1go2to3main4menu5") { goto exit; }

                Console.WriteLine("Beschrijving film:");
                string beschrijvingfilm = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
                if (beschrijvingfilm == "1go2to3main4menu5") { goto exit; }

                if (MovieLibrary.movieList.Count > 0)
                {
                    var lastMovieInList = MovieLibrary.movieList[MovieLibrary.movieList.Count - 1];
                    MovieInterpreter Movie = new MovieInterpreter();
                    Movie.setFilm(lastMovieInList.movieid + 1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd, beoordelingFilm, acteursFilm, movieTime, taalfilm, beschrijvingfilm);

                    MovieLibrary.addFilm(Movie);
                    JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                    json = JsonSerializer.Serialize(MovieLibrary, opt);

                    Json.WriteJson("Films", json);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Film succesvol toegevoegd aan het aanbod.");
                    System.Threading.Thread.Sleep(1500);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return Movie;
                }
                else
                {
                    MovieInterpreter Movie = new MovieInterpreter();
                    Movie.setFilm(1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd, beoordelingFilm, acteursFilm, movieTime, taalfilm, beschrijvingfilm);

                    MovieLibrary.addFilm(Movie);
                    JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                    json = JsonSerializer.Serialize(MovieLibrary, opt);

                    Json.WriteJson("Films", json);

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Film succesvol toegevoegd aan het aanbod.");
                    System.Threading.Thread.Sleep(1500);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    return Movie;
                }

            exit:
                MovieInterpreter Movie2 = new MovieInterpreter();
                Movie2.setFilm(234733, "1go2to3main4menu5", "", new List<string>(), 999, 888, new List<string>(), 0, "", "");
                return Movie2;
            }

            public void UpdateMovies()
            {
                Console.Clear();
                Console.Write("Terug naar Admin Menu (Esc)\n\n");
                Console.WriteLine("Welke film wilt u updaten?");
                string naamFilm = loginscherm.RemoveSpecialCharacters(loginscherm.newwayoftyping());

                if (naamFilm == "1go2to3main4menu5") { goto exit; }
                try
                {
                    string json = Json.ReadJson("Films");
                    Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
                    MovieInterpreter tempMovie = jsonFilms.movieList.Single(movie => loginscherm.RemoveSpecialCharacters(movie.name) == naamFilm);
                    MovieMethods.UpdateMovieMenu(json, jsonFilms, tempMovie);
                    goto exit;
                }
                catch (InvalidOperationException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nFilm niet gevonden.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Wilt u een andere film aanpassen? (j/n)");

                    ConsoleKeyInfo keyReaded = Console.ReadKey();

                    switch (keyReaded.Key)
                    {
                        case ConsoleKey.J:
                            Console.Clear();
                            UpdateMovies();
                            break;
                        case ConsoleKey.N:
                            Console.Clear();
                            adminMenu.hoofdPagina();
                            break;
                    }
                }
            exit:
                return;
            }

            public void DeleteMovies()
            {
                Console.Clear();
                Console.Write("Terug naar Admin Menu (Esc)\n\n");
                Console.WriteLine("Welke film wilt u verwijderen?");
                string movieToRemove = loginscherm.RemoveSpecialCharacters(loginscherm.newwayoftyping());

                if (movieToRemove == "1go2to3main4menu5") { goto exit; }
                try
                {
                    string json = Json.ReadJson("Films");
                    Films jsonFilms = JsonSerializer.Deserialize<Films>(json);

                    if (jsonFilms.movieList != null)
                    {
                        DeleteMovieMethod.DeleteMovie(json, jsonFilms, movieToRemove);
                    }
                    else
                    {
                        Console.WriteLine("Filmlijst is op dit moment leeg.");
                        Console.WriteLine("U wordt nu teruggestuurd naar het admin menu.");
                        System.Threading.Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Film niet gevonden. Probeer het nog een keer.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    DeleteMovies();
                }
            exit:
                return;
            }

            public void AddCinemaHall()
            {
                Console.Clear();
                Zaal zaal = new Zaal();
                Console.WriteLine();
                int totalChairs;
                while (true)
                {
                    Console.WriteLine("Hoeveel stoelen heeft de zaal in totaal? Options \t(M)egaChonker = 500, \t(H)eftyChonk = 300 \t(C)honk = 150\tOr Q to quit");
                    ConsoleKey chonkChart = Console.ReadKey(true).Key;
                    if (chonkChart == ConsoleKey.M)
                    {
                        totalChairs = (int)Zaal.Size.MegaChonker;
                        break;
                    }
                    else if (chonkChart == ConsoleKey.H)
                    {
                        totalChairs = (int)Zaal.Size.heftyChonk;
                        break;
                    }
                    else if (chonkChart == ConsoleKey.C)
                    {
                        totalChairs = (int)Zaal.Size.chonk;
                        break;
                    }
                    else if (chonkChart == ConsoleKey.Q)
                    {
                        goto exit; // EXIT uit de Addmovies
                    }
                }

                MovieInterpreter film = new MovieInterpreter();
                while (true)
                {
                    Console.WriteLine("Welke film wilt u dat er op dit tijdstip draait?");

                    string moviename = Console.ReadLine();
                    var movie = MovieMethods.DoesMovieExist(moviename);
                    if (movie.Item1)
                    {
                        film = movie.Item2;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("looks like the movie does not exist do you want to add it or Search or Quit? A/S/Q");
                        ConsoleKey k = Console.ReadKey(true).Key;
                        if (k == ConsoleKey.A)
                        {
                            film = AddMovies();
                            break;
                        }
                        else if (k == ConsoleKey.S) { }
                        else if (k == ConsoleKey.Q) { goto exit; } // EXIT uit de Addmovies
                    }
                }
                if (film.name == "1go2to3main4menu5") { goto exit; }

                Console.WriteLine("Op welke datum wilt u dat deze film draait? (dd/mm/yyyy)");
                string date = Console.ReadLine();
                Console.WriteLine("Op welk tijdstip wilt u dat deze film draait? (HH:MM)");
                string time = Console.ReadLine();

                zaal.setZaal(date, time, totalChairs, film); // someone has to fix this
                Zalen zalen = new Zalen();
                zalen = zalen.FromJson(Json.ReadJson("Zalen"));
                zalen.AddZaal(zaal);

                Json.WriteJson("Zalen", zalen.ToJson());
            exit:
                return;
            }

            public void UpdateCinemaHall()
            {

            }

            public void DeleteCinemaHall()
            {

            }
        }

        public class Employee : Person
        {

        }


        /// <summary>
        /// People class for JSON
        /// </summary>
        public class People
        {
            public List<Person> peopleList { get; set; }
            public List<Admin> adminList { get; set; }
            public List<Employee> employeeList { get; set; }

            /// <summary>
            /// adds an Person class Object to the peopleList
            /// adds an Admin class Object to the adminList
            /// adds an Employee class Object to the employeeList
            /// </summary>
            /// <param name="personToAdd"></param>
            public void AddPerson(Person personToAdd)
            {
                if (peopleList == null)
                {
                    List<Person> newPerson = new List<Person>();
                    newPerson.Add(personToAdd);
                    peopleList = newPerson;
                }
                else
                {
                    peopleList.Add(personToAdd);
                }
            }

            public void AddAdmin(Admin personToAdd)
            {
                if (adminList == null)
                {
                    List<Admin> newAdmin = new List<Admin>();
                    newAdmin.Add(personToAdd);
                    adminList = newAdmin;
                }
                else
                {
                    adminList.Add(personToAdd);
                }
            }

            public void AddEmployee(Employee personToAdd)
            {
                if (employeeList == null)
                {
                    List<Employee> newEmployee = new List<Employee>();
                    newEmployee.Add(personToAdd);
                    employeeList = newEmployee;
                }
                else
                {
                    employeeList.Add(personToAdd);
                }
            }


            /// <summary>
            ///     Makes a new Person using a function and adding it to the object,
            ///     so it will put the json good
            /// </summary>
            /// <param name="id"></param>
            /// <param name="name"></param>
            /// <param name="email"></param>
            /// <param name="password"></param>
            /// <param name="age"></param>
            public void addPersonByFunction(int id, string name, string email, string password, string age, string phonenumber)
            {
                Person temp = new Person();
                temp.setPerson(id, name, email, password, age, phonenumber);
                AddPerson(temp);
            }


            public void writePeople()
            {
                foreach (Person person in this.peopleList)
                {
                    Console.Write($"id:{person.id} \t");
                    Console.Write($"name:{person.name} \t");
                    Console.Write($"email:{person.email} \t");
                    Console.Write($"password:{person.password} \t");
                    Console.Write($"age:{person.age} \n");
                    Console.Write($"phonenumber:{person.phonenumber} \n");
                }
            }


            /// <summary>
            /// Will return This object
            /// </summary>
            /// <returns></returns>
            public string ToJson()
            {
                JsonSerializerOptions opt = new JsonSerializerOptions() { WriteIndented = true };
                return JsonSerializer.Serialize(this, opt);
            }
            public People FromJson(string json)
            {
                return JsonSerializer.Deserialize<People>(json);
            }
        }
    }
}
