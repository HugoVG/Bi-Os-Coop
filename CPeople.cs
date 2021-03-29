using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Bi_Os_Coop
{
    class CPeople
    {
        public static void NewUser()
        {
            string json = Json.ReadJson("Accounts");
            People jsonPeople = JsonSerializer.Deserialize<People>(json);

            jsonPeople.addPersonByFunction(3, "Bjorn", "json@bjorn.com", "jsonBjorn", "30", "06123456789");
            json = JsonSerializer.Serialize(jsonPeople);
            Json.WriteJson("Accounts", json);
            //Console.WriteLine(jsonPeople);
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
            public void Login()
            {
                Console.Clear();
                //Person Ingelogd = loginscherm.login();
                //return Ingelogd;
            }

            public Person Logout()
            {
                Person Ingelogd = new Person();
                return Ingelogd;
            }

            public void ChangePassword(Person ingelogdepersoon)
            {
                if (ingelogdepersoon.id != 0) // person is logged in
                {
                    Console.WriteLine("Vul uw emailadres in:");
                    string currentEmail = Console.ReadLine();

                    Console.WriteLine("Vul uw huidige wachtwoord in:");
                    string currentPassword = Console.ReadLine();

                    if (MailWachtwoordCheck(currentEmail, currentPassword))
                    {
                        SetNewPassword();
                    }
                    else
                    {
                        int amountOfPasswordEntries = 3;
                        while (amountOfPasswordEntries > 0)
                        {
                            Console.WriteLine($"Wachtwoord of email onjuist. U heeft nog {amountOfPasswordEntries} pogingen.");
                            Console.WriteLine("Vul nogmaals uw emailadres in:");
                            currentEmail = Console.ReadLine();

                            Console.WriteLine("Vul nogmaals uw wachtwoord in:");
                            currentPassword = Console.ReadLine();

                            if (currentPassword == password && currentEmail == email)
                            {
                                SetNewPassword();
                                break;
                            }
                            else
                            {
                                amountOfPasswordEntries--;
                                if (amountOfPasswordEntries == 0)
                                {
                                    Console.WriteLine("0 pogingen. Probeer het later nog eens.");
                                }
                            }
                        }
                    }
                }

                else if (ingelogdepersoon.id == 0) // person is not logged in
                {
                    Console.WriteLine("Vul uw emailadres in:");
                    string currentEmail = Console.ReadLine();

                    Console.WriteLine("Vul uw geboortedatum in: (dd/mm/jjjj)");
                    string currentAge = Console.ReadLine();

                    if (MailLeeftijdCheck(currentEmail, currentAge))
                    {
                        SetNewPassword();
                    }
                    else
                    {
                        Console.WriteLine("Sorry, dit account bestaat niet.");
                        Console.WriteLine("Wilt u een nieuw account aanmaken? (ja/nee)");
                        string antwoordAanmakenNieuwAccount = Console.ReadLine();
                        if (antwoordAanmakenNieuwAccount.ToLower() == "ja")
                        {
                            Registerscreen.createAccount();
                        }
                        else if (antwoordAanmakenNieuwAccount.ToLower() == "nee")
                        {

                        }
                    }
                }

                else
                {
                    throw new NotImplementedException();
                }
            }

            public void SetNewPassword()
            {
                Console.Clear();
                Console.WriteLine("Vul nu uw nieuwe wachtwoord in:");
                string newPassword = Console.ReadLine();
                Console.WriteLine("Vul nogmaals uw nieuwe wachtwoord in:");
                string tempNewPassword = Console.ReadLine();

                if (tempNewPassword == newPassword)
                {
                    string json = Json.ReadJson("Accounts");
                    People jsonPeople = JsonSerializer.Deserialize<People>(json);
                    try
                    {
                        Person tempPerson = jsonPeople.peopleList.Single(person => person.email == email && person.password == password);
                        password = newPassword;
                        tempPerson.password = newPassword;

                        json = JsonSerializer.Serialize(jsonPeople);
                        Json.WriteJson("Accounts", json);

                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("U heeft uw wachtwoord succesvol gewijzigd.");
                        System.Threading.Thread.Sleep(2000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Clear();
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Dit account bestaat niet.");
                    }
                }
                else
                {
                    Console.WriteLine("U had niet tweemaal hetzelfde nieuwe wachtwoord ingevoerd.");
                    System.Threading.Thread.Sleep(2000);
                    SetNewPassword();
                }
            }


            public static bool MailLeeftijdCheck(string username, string age)
            {
                string account = Json.ReadJson("Accounts");
                People accounts = new People();
                accounts = accounts.FromJson(account);
                try
                {
                    Person persoon = accounts.peopleList.Single(person => person.email == username && person.age == age);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
                }
            }

            public static bool MailWachtwoordCheck(string username, string password)
            {
                string account = Json.ReadJson("Accounts");
                People accounts = new People();
                accounts = accounts.FromJson(account);
                try
                {
                    Person persoon = accounts.peopleList.Single(person => person.email == username && person.password == password);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    return false;
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
            public void AddMovies()
            {

            }

            public void UpdateMovies()
            {

            }

            public void DeleteMovies()
            {

            }

            public void ChangeCinemaHalls()
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
                return JsonSerializer.Serialize(this);
            }
            public People FromJson(string json)
            {
                return JsonSerializer.Deserialize<People>(json);
            }
        }
    }
}
