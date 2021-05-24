using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

namespace Bi_Os_Coop
{
    public class PasswordMethods
    {
        public static dynamic SetNewPassword(string emailIngelogd, string passwordIngelogd)
        {
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
                                CPeople.Person tempPerson = jsonPeople.peopleList.Single(person => person.email == emailIngelogd && person.password == passwordIngelogd);
                                tempPerson.password = newPassword;

                                json = JsonSerializer.Serialize(jsonPeople);
                                Json.WriteJson("Accounts", json);

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("U heeft uw wachtwoord succesvol gewijzigd.");
                                System.Threading.Thread.Sleep(2000);
                                Console.ForegroundColor = ConsoleColor.Gray;
                                Console.Clear();
                                MainMenu.MainMenuShow();
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
            while (amountOfPasswordEntries > 0)
            {
                Console.WriteLine($"Wachtwoord of email onjuist. U heeft nog {amountOfPasswordEntries} pogingen.");
                Console.WriteLine("Vul nogmaals uw emailadres in:");
                string currentEmail = Console.ReadLine();

                Console.WriteLine("Vul nogmaals uw wachtwoord in:");
                string currentPassword = Console.ReadLine();

                if (MailWachtwoordCheck(currentEmail, currentPassword))
                {
                    SetNewPassword(currentEmail, currentPassword);
                }
                else
                {
                    amountOfPasswordEntries--;
                    if (amountOfPasswordEntries == 0)
                    {
                        Console.WriteLine("U heeft geen pogingen meer. Probeer het later nog eens.");
                        System.Threading.Thread.Sleep(2000);
                        Console.Clear();
                        MainMenu.MainMenuShow();
                    }
                }
            }
        }

        public static bool MailLeeftijdCheck(string username, string age)
        {
            string account = Json.ReadJson("Accounts");
            CPeople.People accounts = new CPeople.People();
            accounts = accounts.FromJson(account);
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
            CPeople.People accounts = new CPeople.People();
            accounts = accounts.FromJson(account);
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
    }
}
