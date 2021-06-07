using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text.Json;
using static Bi_Os_Coop.Class.MovieMenu;

namespace Bi_Os_Coop.Class
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

            public string ViewDetails()
            {
                return $"E-mail: {this.email}, Geboortedatum: {this.age}, Telefoonnummer: {this.phonenumber}";
            }


            public bool isPerson()
            {
                if (this.GetType().Equals(typeof(Person)))
                {
                    return true;
                }
                else { return false; }
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
                MainMenu.ClearAndShowLogoPlusEsc("Update");
                Console.WriteLine("\nWilt u uw account verwijderen? (j/n)");

                ConsoleKey keypressed = Console.ReadKey(true).Key;
                while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
                {
                    keypressed = Console.ReadKey(true).Key;
                }
                if (keypressed == ConsoleKey.Escape) { goto exit; }
                if (keypressed == ConsoleKey.J)
                {
                    // asks for email and password of the person
                    MainMenu.ClearAndShowLogoPlusEsc("Update");
                    Console.WriteLine("\nVul uw emailadres in:");
                    string email = loginscherm.newwayoftyping();
                    if (email != "1go2to3main4menu5")
                    {
                        Console.WriteLine("Vul uw huidige wachtwoord in:");
                        SecureString pass = loginscherm.maskInputString();
                        string password = new System.Net.NetworkCredential(string.Empty, pass).Password;
                        if (password != "1go2to3main4menu5")
                        {
                            // checks if email and password are in the peopleList
                            if (PasswordMethods.MailWachtwoordCheck(email, password)) // both are correct
                            {
                                UserProfile.DeleteAccount(ingelogdepersoon);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine("\n\nWachtwoord of email onjuist. Probeer het later nog eens.");
                                System.Threading.Thread.Sleep(1500);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Clear();
                            }
                        }
                        else
                        {
                            Console.Clear();
                        }
                    }
                }
                else if (keypressed == ConsoleKey.N)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Bedankt voor het blijven!");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
            exit:
                return;
            }

            public void ChangePassword()
            {
                // asks for email and password of the person
                Console.WriteLine("\nVul uw emailadres in:");
                string email = loginscherm.newwayoftyping();
                if (email == "1go2to3main4menu5") { goto exit; }
                if (email != "1go2to3main4menu5")
                {
                    Console.WriteLine("Vul uw huidige wachtwoord in:");
                    SecureString pass = loginscherm.maskInputString();
                    string password = new System.Net.NetworkCredential(string.Empty, pass).Password;
                    if (password == "1go2to3main4menu5") { goto exit; }
                    if (password != "1go2to3main4menu5")
                    {
                        // checks if email and password are in the peopleList
                        if (PasswordMethods.MailWachtwoordCheck(email, password)) // both are correct
                        {
                            PasswordMethods.SetNewPassword(email, password);
                        }
                        else
                        {
                            PasswordMethods.PasswordEntries(); // if one of the two data is incorrect they get 3 entries
                        }
                    }
                }
            exit:
                return;
            }

            public static void ForgotPassword()
            {
                MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                if (things.login == "None") // person is not logged in
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Main");
                    Console.WriteLine("\nHerstel wachtwoord\n");
                    // if the person is not logged in we ask for email and birthdate
                    Console.WriteLine("Vul uw emailadres in:");
                    string currentEmail = loginscherm.newwayoftyping();
                    if (currentEmail == "1go2to3main4menu5") { goto exit; }

                    Console.WriteLine("Vul uw geboortedatum in: (dd/mm/jjjj)");
                    string currentAge = loginscherm.getdate();
                    if (currentAge == "1go2to3main4menu5") { goto exit; }

                    // checks if email and age are in the peopleList
                    if (PasswordMethods.MailLeeftijdCheck(currentEmail, currentAge))
                    {
                        string account = Json.ReadJson("Accounts");
                        People accounts = People.FromJson(account);
                        Person persoon = accounts.peopleList.Single(person => person.email == currentEmail && person.age == currentAge);
                        PasswordMethods.SetNewPassword(currentEmail, persoon.password); // both are correct
                    }
                    else
                    {
                        // if the person doesn't exist we ask if the person wants to make a new account, if not send to Main Menu
                        MainMenu.ClearAndShowLogoPlusEsc("Main");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Sorry, dit account bestaat niet.");
                        Console.ForegroundColor = ConsoleColor.Gray;
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
                }
                // in case the person has an ID other than 0 or not 0
                else
                {
                    throw new NotImplementedException();
                }
            exit:
                return;
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

        public class Employee : Person
        {
            
        }

        public class Admin : Person
        {
            public void NewEmployee(string name, string email, string password, string age, string phoneNumber)
            {
                string json = Json.ReadJson("Accounts");
                People jsonPeople = JsonSerializer.Deserialize<CPeople.People>(json);
                Employee newEmployee = new Employee();
                newEmployee.setPerson(1, name, email, password, age, phoneNumber);
                jsonPeople.AddEmployee(newEmployee);
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                json = JsonSerializer.Serialize(jsonPeople, opt);
                Json.WriteJson("Accounts", json);
            }


            public MovieInterpreter AddMovies()
            {
                MovieInterpreter movie = MovieMethods.AddMovie();
                return movie;
            }

            public void UpdateMovies()
            {
                Console.WriteLine("\nWelke film wilt u updaten?");
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
                Console.WriteLine("\nWelke film wilt u verwijderen?");
                string movieToRemove = loginscherm.RemoveSpecialCharacters(loginscherm.newwayoftyping());

                if (movieToRemove == "1go2to3main4menu5") { goto exit; }
                try
                {
                    string json = Json.ReadJson("Films");
                    Films jsonFilms = JsonSerializer.Deserialize<Films>(json);

                    if (jsonFilms.movieList != null)
                    {
                        MovieMethods.DeleteMovie(json, jsonFilms, movieToRemove);
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

                MovieInterpreter film;
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
                        Console.WriteLine("Het lijkt erop dat die film niet bestaat. Wilt u de film toevoegen, verderzoeken of teruggaan? A/S/Q");
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
                Zalen zalen = Zalen.FromJson(Json.ReadJson("Zalen"));
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
            public static People FromJson(string json)
            {
                return JsonSerializer.Deserialize<People>(json);
            }
        }
    }
}
