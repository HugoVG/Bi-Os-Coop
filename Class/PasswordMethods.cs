using System;
using System.Linq;
using System.Text.Json;
using System.Security;

namespace Bi_Os_Coop.Class
{
    public class PasswordMethods
    {
        public static dynamic SetNewPassword(string emailIngelogd, string passwordIngelogd)
        {
            MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
            string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;
            MainMenu.ClearAndShowLogoPlusEsc("Admin");
            Console.WriteLine("Vul nu uw nieuwe wachtwoord in:");
            string newPassword = loginscherm.newwayoftyping();
            if (newPassword != "1go2to3main4menu5")
            {
                if (newPassword != passwordIngelogd)
                {
                    Console.WriteLine("Vul nogmaals uw nieuwe wachtwoord in:");
                    string tempNewPassword = loginscherm.newwayoftyping();
                    if (tempNewPassword != "1go2to3main4menu5")
                    {
                        if (tempNewPassword == newPassword)
                        {
                            string json = Json.ReadJson("Accounts");
                            CPeople.People jsonPeople = JsonSerializer.Deserialize<CPeople.People>(json);
                            try
                            {
                                if (jsonPeople != null)
                                {
                                    CPeople.Person tempPerson = jsonPeople.peopleList.Single(person => person.email == emailIngelogd && person.password == passwordIngelogd);
                                    tempPerson.password = newPassword;
                                    MainMenu.JsonMainMenuSave(tempPerson, sort, reverse, login, language);
                                }

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
                                System.Threading.Thread.Sleep(2000);
                                Console.Clear();
                                MainMenu.MainMenuShow();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Uw wachtwoord komt niet overeen.");
                            System.Threading.Thread.Sleep(2000);
                            SetNewPassword(emailIngelogd, passwordIngelogd);
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nUw wachtwoord is al eens gebruikt.");
                    System.Threading.Thread.Sleep(2000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    SetNewPassword(emailIngelogd, passwordIngelogd);
                }
            }
            return "1go2to3main4menu5";
        }

        public static void PasswordEntries()
        {
            int amountOfPasswordEntries = 3;
            ConsoleKey keypressed = Console.ReadKey(true).Key;
            while (amountOfPasswordEntries > 0 && keypressed != ConsoleKey.Escape)
            {
                MainMenu.ClearAndShowLogoPlusEsc("Update");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Wachtwoord of email onjuist. U heeft nog {amountOfPasswordEntries} pogingen.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Vul nogmaals uw emailadres in:");
                string email = loginscherm.newwayoftyping();

                if (email == "1go2to3main4menu5") { goto exit; }
                if (email != "1go2to3main4menu5")
                {
                    Console.WriteLine("Vul nogmaals uw wachtwoord in:");
                    SecureString pass = loginscherm.maskInputString();
                    string password = new System.Net.NetworkCredential(string.Empty, pass).Password;
                    if (password == "1go2to3main4menu5") { goto exit; }
                    if (password != "1go2to3main4menu5")
                    {
                        if (MailWachtwoordCheck(email, password))
                        {
                            SetNewPassword(email, password);
                        }
                        else
                        {
                            amountOfPasswordEntries--;
                            if (amountOfPasswordEntries == 0)
                            {
                                MainMenu.ClearAndShowLogoPlusEsc("Update");
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("U heeft geen pogingen meer. Probeer het later nog eens.");
                                System.Threading.Thread.Sleep(1500);
                            }
                        }
                    }
                }                
            }
        exit:
            return;
        }

        public static bool MailLeeftijdCheck(string username, string age)
        {
            string account = Json.ReadJson("Accounts");
            CPeople.People accounts = CPeople.People.FromJson(account);
            try
            {
                CPeople.Person persoon = accounts.peopleList.Single(person => person.email == username && person.age == age);
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
            CPeople.People accounts = CPeople.People.FromJson(account);
            try
            {
                CPeople.Person persoon = accounts.peopleList.Single(person => person.email == username && person.password == password);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        public static bool NameBirthdayCheck(string name, string age)
        {
            string account = Json.ReadJson("Accounts");
            CPeople.People accounts = CPeople.People.FromJson(account);
            try
            {
                CPeople.Person persoon = accounts.peopleList.Single(person => person.name.ToLower() == name.ToLower() && person.age == age);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
    }
}
