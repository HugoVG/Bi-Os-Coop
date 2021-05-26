using System;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    public class UserProfile
    {
        public static void ProfileMenu(CPeople.Person loggedinPersonTest)
        {
            // retrieve account that belongs to the logged in person
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = JsonSerializer.Deserialize<CPeople.People>(json);
            CPeople.Person accountUser = jsonPeople.peopleList.Single(person => person.name == loggedinPersonTest.name);

            bool done = false;
            while (done == false)
            {
                Console.Clear();
                MainMenu.Logo();
                Console.WriteLine($"Profiel: {accountUser.name}\n");
                Console.WriteLine("======Profiel Menu======");
                Console.WriteLine("1. Naam aanpassen");
                Console.WriteLine("2. Email aanpassen");
                Console.WriteLine("3. Telefoonnummer aanpassen");
                Console.WriteLine("4. Wachtwoord veranderen");
                Console.WriteLine("5. Reserveringen bekijken");
                Console.WriteLine("6. Account verwijderen");
                Console.WriteLine("7. Terug naar hoofdmenu");

                ConsoleKeyInfo keyReaded = Console.ReadKey();

                switch (keyReaded.Key)
                {
                    case ConsoleKey.D1:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        UpdateName(json, jsonPeople, accountUser);
                        break;
                    case ConsoleKey.D2:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        UpdateEmail(json, jsonPeople, accountUser);
                        break;
                    case ConsoleKey.D3:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        UpdatePhoneNumber(json, jsonPeople, accountUser);
                        break;
                    case ConsoleKey.D4:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        accountUser.ChangePassword();
                        break;
                    case ConsoleKey.D5:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        ProfileMenu(accountUser);
                        break;
                    case ConsoleKey.D6:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        accountUser.DeleteAccount(accountUser);
                        break;
                    case ConsoleKey.D7:
                        done = true;
                        Console.Clear();
                        MainMenu.MainMenuShow();
                        break;
                }
            }
        }

        public static Tuple<string, CPeople.People, CPeople.Person> UpdateName(string json, CPeople.People jsonPeople, CPeople.Person ingelogdePersoon)
        {
            Console.WriteLine($"Je huidige naam: {ingelogdePersoon.name}");
            Console.WriteLine("Wat is je nieuwe naam?");
            string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (newName == "1go2to3main4menu5") { goto exit; }
            ingelogdePersoon.name = newName;

            // writing it to accounts json
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonPeople, opt);
            Json.WriteJson("Accounts", json);

            // writing it to logged in user json
            MainMenuThings things = MainMenu.JsonMainMenuLoad();
            CPeople.Person user = things.user; string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;
            user.name = newName;
            MainMenu.JsonMainMenuSave(user, sort, reverse, login, language);

            // end of method
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Je naam is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, CPeople.People, CPeople.Person>(json, jsonPeople, ingelogdePersoon);
        exit:
            return new Tuple<string, CPeople.People, CPeople.Person>(json, jsonPeople, ingelogdePersoon);
        }

        public static Tuple<string, CPeople.People, CPeople.Person> UpdateEmail(string json, CPeople.People jsonPeople, CPeople.Person ingelogdePersoon)
        {
            Console.Write($"Je huidige email: {ingelogdePersoon.email}");
            string newEmail = Registerscreen.validCheck("nieuwe e-mailadres", Registerscreen.emailCheck);
            if (newEmail == "1go2to3main4menu5") { goto exit; }
            ingelogdePersoon.email = newEmail;

            // writing it to accounts json
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonPeople, opt);
            Json.WriteJson("Accounts", json);

            // writing it to logged in user json
            MainMenuThings things = MainMenu.JsonMainMenuLoad();
            CPeople.Person user = things.user; string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;
            user.email = newEmail;
            MainMenu.JsonMainMenuSave(user, sort, reverse, login, language);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Je email is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, CPeople.People, CPeople.Person>(json, jsonPeople, ingelogdePersoon);
        exit:
            return new Tuple<string, CPeople.People, CPeople.Person>(json, jsonPeople, ingelogdePersoon);
        }

        public static Tuple<string, CPeople.People, CPeople.Person> UpdatePhoneNumber(string json, CPeople.People jsonPeople, CPeople.Person ingelogdePersoon)
        {
            Console.Write($"Je huidige telefoonnummer: {ingelogdePersoon.phonenumber}");
            string newPhoneNumber = Registerscreen.validCheck("nieuwe telefoonnummer", Registerscreen.phoneCheck);
            if (newPhoneNumber == "1go2to3main4menu5") { goto exit; }
            ingelogdePersoon.email = newPhoneNumber;

            // writing it to accounts json
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            json = JsonSerializer.Serialize(jsonPeople, opt);
            Json.WriteJson("Accounts", json);

            // writing it to logged in user json
            MainMenuThings things = MainMenu.JsonMainMenuLoad();
            CPeople.Person user = things.user; string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;
            user.phonenumber = newPhoneNumber;
            MainMenu.JsonMainMenuSave(user, sort, reverse, login, language);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Je email is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Clear();
            return new Tuple<string, CPeople.People, CPeople.Person>(json, jsonPeople, ingelogdePersoon);
        exit:
            return new Tuple<string, CPeople.People, CPeople.Person>(json, jsonPeople, ingelogdePersoon);
        }

        public static void DeleteAccount(CPeople.Person ingelogdepersoon)
        {
            try
            {
                string json = Json.ReadJson("Accounts");
                CPeople.People jsonPeople = JsonSerializer.Deserialize<CPeople.People>(json);

                if (jsonPeople.peopleList != null)
                {
                    int index = jsonPeople.peopleList.FindIndex(person => person.name == ingelogdepersoon.name);
                    if (index == -1)
                    {
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Account niet gevonden. Neem contact op met de klantenservice.");
                        System.Threading.Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        MainMenu.MainMenuShow();
                    }
                    else
                    {
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Account gevonden");
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Weet u zeker dat u hem wilt verwijderen? (j/n)");
                        ConsoleKey keypressed = Console.ReadKey(true).Key;
                        while (keypressed != ConsoleKey.J && keypressed != ConsoleKey.N && keypressed != ConsoleKey.Escape)
                        {
                            keypressed = Console.ReadKey(true).Key;
                        }
                        if (keypressed == ConsoleKey.Escape) { goto exit; }
                        if (keypressed == ConsoleKey.J)
                        {
                            jsonPeople.peopleList.RemoveAt(index);
                            json = JsonSerializer.Serialize(jsonPeople);
                            Json.WriteJson("Accounts", json);

                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Uw account is succesvol verwijderd.");
                            System.Threading.Thread.Sleep(2000);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            MainMenu.MainMenuShow();
                        }
                        if (keypressed == ConsoleKey.N)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nBedankt voor het blijven!");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("U wordt nu teruggestuurd naar het hoofdmenu.");
                            System.Threading.Thread.Sleep(2000);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Clear();
                            MainMenu.MainMenuShow(); ;
                        }
                    }
                exit:
                    return;
                }
                else
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Update");
                    Console.WriteLine("Account bestaat niet.");
                    Console.WriteLine("U wordt nu teruggestuurd naar het hoofdmenu.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    MainMenu.MainMenuShow();
                }
            }
            catch (InvalidOperationException)
            {
                MainMenu.ClearAndShowLogoPlusEsc("Update");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account niet gevonden. Neem contact op met de klantenservice.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                MainMenu.MainMenuShow();
            }
        }
    }
}
