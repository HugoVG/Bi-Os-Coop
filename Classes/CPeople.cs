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
            bjorn.DeleteAccount(bjorn, new List<dynamic>());
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
            public void DeleteAccount(Person ingelogdepersoon, List<dynamic> mainmenuthings)
            {
                Console.Clear();
                Console.WriteLine("Wilt u uw account verwijderen? (j/n)");
                //string answer = Console.ReadLine();
                if (Console.ReadKey(true).Key == ConsoleKey.J)
                {
                    // asks for email and password of the person
                    Console.WriteLine("Vul uw emailadres in:");
                    string currentEmail = Console.ReadLine();

                    Console.WriteLine("Vul uw huidige wachtwoord in:");
                    SecureString pass = loginscherm.maskInputString();
                    string currentPassword = new System.Net.NetworkCredential(string.Empty, pass).Password;

                    // checks if email and password are in the peopleList
                    if (PasswordMethods.MailWachtwoordCheck(currentEmail, currentPassword)) // both are correct
                    {
                        DeleteAccountMethod.DeleteAccount(ingelogdepersoon, mainmenuthings);
                    }
                    else
                    {
                        Console.WriteLine("Wachtwoord of email onjuist. Probeer het later nog eens.");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        MainMenu.MainMenuShow(mainmenuthings[0], mainmenuthings[1], mainmenuthings[2], mainmenuthings[3], mainmenuthings[4]);
                    }
                }
                else if (Console.ReadKey(true).Key == ConsoleKey.N)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bedankt voor het blijven!");
                    Console.WriteLine("U wordt nu teruggestuurd naar het hoofdmenu.");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    MainMenu.MainMenuShow(mainmenuthings[0], mainmenuthings[1], mainmenuthings[2], mainmenuthings[3], mainmenuthings[4]);
                }
                else
                {
                    DeleteAccount(ingelogdepersoon, mainmenuthings);
                }
            }

            public void ChangePassword(Person ingelogdepersoon, List<dynamic> mainmenuthings)
            {
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
                        PasswordMethods.SetNewPassword(currentEmail, currentPassword, mainmenuthings);
                    }
                    else
                    {
                        PasswordMethods.PasswordEntries(mainmenuthings); // if one of the two data is incorrect they get 3 entries
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
                        PasswordMethods.SetNewPassword(currentEmail, currentAge, mainmenuthings); // both are correct
                    }
                    else
                    {
                        // if the person doesn't exist we ask if the person wants to make a new account, if not send to Main Menu
                        Console.WriteLine("Sorry, dit account bestaat niet.");
                        Console.WriteLine("Wilt u een nieuw account aanmaken? (j/n)");

                        if (Console.ReadKey(true).Key == ConsoleKey.J) // person wants to create a new account
                        {
                            Console.Clear();
                            Registerscreen.CreateAccount();
                        }
                        else if (Console.ReadKey(true).Key == ConsoleKey.N) // person is send to main menu
                        {
                            Console.Clear();
                            MainMenu.MainMenuShow(mainmenuthings[0], mainmenuthings[1], mainmenuthings[2], mainmenuthings[3], mainmenuthings[4]);
                        }
                    }
                }

                // in case the person has an ID other than 0 or not 0
                else
                {
                    throw new NotImplementedException();
                }
            }


            public void ViewMovies()
            {

            }

            public void BookTicket()
            {

            }

            public void CancelTicket()
            {

            }
        }

        public class Admin : Person
        {
            public MovieInterpreter AddMovies()
            {
                Console.Clear();

                string json = Json.ReadJson("Films");
                Films MovieLibrary = JsonSerializer.Deserialize<Films>(json);

                Console.WriteLine("Voeg hier een nieuwe film toe.");
                Console.WriteLine("Naam film:");
                string naamFilm = Console.ReadLine();
                Console.WriteLine("Releasedatum film:");
                string releasedatumFilm = Console.ReadLine();
                Console.WriteLine("Voeg tussen elke genre een komma toe, bijv: Komedie, Actie, Thriller");
                Console.WriteLine("Genres film:");
                string genres = Console.ReadLine();
                List<string> genresFilm = genres.Split(',').ToList();
                Console.WriteLine("Voeg tussen elke acteur een komma toe, bijv: Sean Connery, Ryan Gosling, Ryan Reynolds");
                Console.WriteLine("Acteurs film:");
                string acteurs = Console.ReadLine();
                List<string> acteursFilm = acteurs.Split(',').ToList();
                Console.WriteLine("Minimumleeftijd film:");
                int minimumLeeftijd = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Beoordeling film:");
                double beoordelingFilm = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Taal film:");
                string taalfilm = Console.ReadLine();
                Console.WriteLine("Beschrijving film:");
                string beschrijvingfilm = Console.ReadLine();
                MovieInterpreter Movie = new MovieInterpreter();
                Movie.setFilm(MovieLibrary.movieList.Count + 1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd, beoordelingFilm, acteursFilm, taalfilm, beschrijvingfilm);

                //MovieLibrary = new Films();
                MovieLibrary.addFilm(Movie);
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                json = JsonSerializer.Serialize(MovieLibrary, opt);

                //jsonFilms.addMovieByFunction(1, naamFilm, releasedatumFilm, genresFilm, minimumLeeftijd, beoordelingFilm, acteursFilm);
                //json = JsonSerializer.Serialize(jsonFilms);
                Json.WriteJson("Films", json);
                return Movie;
            }

            public void UpdateMovies()
            {
                Console.Clear();
                Console.WriteLine("Welke film wilt u updaten?");
                string naamFilm = Console.ReadLine();

                try
                {
                    string json = Json.ReadJson("Films");
                    Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
                    MovieInterpreter tempMovie = jsonFilms.movieList.Single(movie => movie.name == naamFilm);
                    MovieMethods.UpdateMovieMenu(json, jsonFilms, tempMovie);
                }
                catch (InvalidOperationException)
                {
                    Console.WriteLine("Film niet gevonden.");
                    Console.WriteLine("Wilt u een andere film aanpassen? (j/n)");
                    string answer = Console.ReadLine();
                    if (Console.ReadKey(true).Key == ConsoleKey.J)
                    {
                        Console.Clear();
                        UpdateMovies();
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.N)
                    {
                        Console.Clear();
                        adminMenu.hoofdPagina();
                    }
                    else
                    {
                        Console.WriteLine("Antwoord niet begrepen. U keert automatisch terug naar het admin menu.");
                        System.Threading.Thread.Sleep(2000);
                        adminMenu.hoofdPagina();
                    }
                }
            }

            public void DeleteMovies()
            {
                Console.Clear();
                Console.WriteLine("Welke film wilt u verwijderen?");
                string movieToRemove = Console.ReadLine();

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
                        adminMenu.hoofdPagina();
                    }
                }
                catch (InvalidOperationException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Film niet gevonden. Probeer het nog een keer.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    DeleteMovies();
                }
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
                    else if( chonkChart == ConsoleKey.C)
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

                Console.WriteLine("Op welke datum wilt u dat deze film draait? (dd/mm/yyyy)");
                string date = Console.ReadLine();
                Console.WriteLine("Op welk tijdstip wilt u dat deze film draait? (HH:MM)");
                string time = Console.ReadLine();

                zaal.setZaal(date, time, totalChairs, film); // someone has to fix this
                Zalen zalen = new Zalen();
                zalen = zalen.FromJson(Json.ReadJson("Zalen"));
                zalen.AddZaal(zaal);

                Json.WriteJson("Zalen",zalen.ToJson());
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
