using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    public class UserProfile
    {
        public static void ProfileMenu()
        {
            bool done = false;
            while (done == false)
            {
                // retrieve account that's currently logged in
                MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                var loggedInPerson = things.user;
                string login = things.login;

                // retrieve account from the database that belongs to the logged in person
                string json = Json.ReadJson("Accounts");
                CPeople.People jsonPeople = JsonSerializer.Deserialize<CPeople.People>(json);
                var accountUser = RetrievePerson(jsonPeople, loggedInPerson, login);
                
                Console.Clear();
                MainMenu.Logo();
                Console.WriteLine("Afsluiten (Esc)\n");
                Console.WriteLine($"Naam: {loggedInPerson.name}");
                Console.WriteLine($"{loggedInPerson.ViewDetails()}\n");
                Console.WriteLine("======Profiel Menu======");
                Console.WriteLine("1. Naam aanpassen");
                Console.WriteLine("2. Email aanpassen");
                Console.WriteLine("3. Telefoonnummer aanpassen");
                Console.WriteLine("4. Wachtwoord veranderen");
                if (login == "Person" || login == "Employee") { Console.WriteLine($"5. Reserveringen bekijken"); }
                if (login == "Person") { Console.WriteLine($"6. Account verwijderen"); }

                ConsoleKeyInfo keyReaded = Console.ReadKey();

                switch (keyReaded.Key)
                {
                    case ConsoleKey.D1:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        UpdateName(jsonPeople, accountUser, things);
                        break;
                    case ConsoleKey.D2:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        UpdateEmail(jsonPeople, accountUser, things);
                        break;
                    case ConsoleKey.D3:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        UpdatePhoneNumber(jsonPeople, accountUser, things);
                        break;
                    case ConsoleKey.D4:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        loggedInPerson.ChangePassword();
                        break;
                    case ConsoleKey.D5:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        Console.Clear();
                        if (login == "Person" || login == "Employee") { ViewReservations.ShowRes(loggedInPerson.id); }
                        break;
                    case ConsoleKey.D6:
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        if (login == "Person") { loggedInPerson.DeleteAccount(loggedInPerson); }
                        break;
                    case ConsoleKey.Escape:
                        done = true;
                        Console.Clear();
                        break;
                }
            }
        }

        public static dynamic RetrievePerson(CPeople.People jsonPeople, dynamic loggedInPerson, string login)
        {
            if (login == "Person") { return jsonPeople.peopleList.Single(person => person.id == loggedInPerson.id); }
            if (login == "Employee") { return jsonPeople.employeeList.Single(person => person.id == loggedInPerson.id); }
            if (login == "Admin") { return jsonPeople.adminList.Single(person => person.id == loggedInPerson.id); }
            else { return null; }
        }

        public static Tuple<CPeople.People, dynamic, MainMenuThings> UpdateName(CPeople.People jsonPeople, dynamic ingelogdePersoon, MainMenuThings things)
        {
            string nm;
            if (ingelogdePersoon.name == null) { nm = "Niet Ingevuld"; }
            else { nm = ingelogdePersoon.name; }
            Console.WriteLine($"\nJe huidige naam: {nm}");
            Console.WriteLine("Wat is je nieuwe naam?");
            string newName = loginscherm.FirstCharToUpper(loginscherm.newwayoftyping());
            if (newName == "1go2to3main4menu5") { goto exit; }

            // ensuring that the first letter of every name in their full name starts with a Capital
            List<string> tempNewName = newName.Split(' ').ToList();
            for (int i = 0; i < tempNewName.Count; i++)
            {
                // excluding Dutch words (tussenvoegsels) that are part of a surname but shouldn't be written with a capital
                if (tempNewName[i].ToLower() == "de" || tempNewName[i].ToLower() == "der" || tempNewName[i].ToLower() == "van")
                    tempNewName[i] = tempNewName[i].ToLower();
                else
                    tempNewName[i] = loginscherm.FirstCharToUpper(tempNewName[i]);
            }
            newName = string.Join(" ", tempNewName);

            ingelogdePersoon.name = newName; // setting the new name to the account

            // Updating the account with the new name in the Accounts Json file
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonPeople, opt);
            Json.WriteJson("Accounts", json);

            // Updating the logged in details of the user so it matches with his account
            MainMenu.JsonMainMenuSave(ingelogdePersoon, things.sort, things.reverse, things.login, things.language);

            // end of method
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Je naam is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);

            return new Tuple<CPeople.People, dynamic, MainMenuThings>(jsonPeople, ingelogdePersoon, things);
        exit:
            return new Tuple<CPeople.People, dynamic, MainMenuThings>(jsonPeople, ingelogdePersoon, things);
        }

        public static Tuple<CPeople.People, dynamic, MainMenuThings> UpdateEmail(CPeople.People jsonPeople, dynamic ingelogdePersoon, MainMenuThings things)
        {
            string nm;
            if (ingelogdePersoon.email == null) { nm = "Niet Ingevuld"; }
            else { nm = ingelogdePersoon.email; }
            Console.Write($"\nJe huidige email: {nm}");
            string newEmail = Registerscreen.validCheck("nieuwe e-mailadres", Registerscreen.emailCheck);
            if (newEmail == "1go2to3main4menu5") { goto exit; }
            ingelogdePersoon.email = newEmail;

            // Updating the account with the new email in the Accounts Json file
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonPeople, opt);
            Json.WriteJson("Accounts", json);

            // Updating the logged in details of the user so it matches with his account
            MainMenu.JsonMainMenuSave(ingelogdePersoon, things.sort, things.reverse, things.login, things.language);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Je email is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);

            return new Tuple<CPeople.People, dynamic, MainMenuThings>(jsonPeople, ingelogdePersoon, things);
        exit:
            return new Tuple<CPeople.People, dynamic, MainMenuThings>(jsonPeople, ingelogdePersoon, things);
        }

        public static Tuple<CPeople.People, dynamic, MainMenuThings> UpdatePhoneNumber(CPeople.People jsonPeople, dynamic ingelogdePersoon, MainMenuThings things)
        {
            string nm;
            if (ingelogdePersoon.phonenumber == null) { nm = "Niet Ingevuld"; }
            else { nm = ingelogdePersoon.phonenumber; }
            Console.Write($"\nJe huidige telefoonnummer: {nm}");
            string newPhoneNumber = Registerscreen.validCheck("nieuwe telefoonnummer", Registerscreen.phoneCheck);
            if (newPhoneNumber == "1go2to3main4menu5") { goto exit; }
            ingelogdePersoon.phonenumber = newPhoneNumber;

            // Updating the account with the new phone number in the Accounts Json file
            JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(jsonPeople, opt);
            Json.WriteJson("Accounts", json);

            // Updating the logged in details of the user so it matches with his account
            MainMenu.JsonMainMenuSave(ingelogdePersoon, things.sort, things.reverse, things.login, things.language);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Je telefoonnummer is succesvol gewijzigd.");
            System.Threading.Thread.Sleep(1000);

            return new Tuple<CPeople.People, dynamic, MainMenuThings>(jsonPeople, ingelogdePersoon, things);
        exit:
            return new Tuple<CPeople.People, dynamic, MainMenuThings>(jsonPeople, ingelogdePersoon, things);
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
                        Console.WriteLine("\nAccount niet gevonden. Neem contact op met de klantenservice.");
                        System.Threading.Thread.Sleep(1500);
                    }
                    else
                    {
                        MainMenu.ClearAndShowLogoPlusEsc("Update");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nAccount gevonden");
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
                            MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                            string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;

                            jsonPeople.peopleList.RemoveAt(index);
                            json = JsonSerializer.Serialize(jsonPeople);
                            Json.WriteJson("Accounts", json);

                            MainMenu.JsonMainMenuSave(null, sort, reverse, "None", language);

                            Console.WriteLine("Uw account is succesvol verwijderd.");
                            System.Threading.Thread.Sleep(2000);

                            Console.Clear();
                            MainMenu.MainMenuShow();
                        }
                        if (keypressed == ConsoleKey.N)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nBedankt voor het blijven!");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            Console.WriteLine("U wordt nu teruggestuurd naar het hoofdmenu.");
                            System.Threading.Thread.Sleep(1500);
                        }
                    }
                exit:
                    return;
                }
                else
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Update");
                    Console.WriteLine("\nAccount niet gevonden. Neem contact op met de klantenservice.");
                    System.Threading.Thread.Sleep(1500);
                }
            }
            catch (InvalidOperationException)
            {
                MainMenu.ClearAndShowLogoPlusEsc("Update");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nAccount niet gevonden. Neem contact op met de klantenservice.");
                System.Threading.Thread.Sleep(1500);
            }
        }
    }
}
