using System;
using System.Text.Json;

namespace Bi_Os_Coop
{
    public class DeleteAccountMethod
    {
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
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Account niet gevonden. Neem contact op met de klantenservice.");
                        System.Threading.Thread.Sleep(1000);
                        Console.ForegroundColor = ConsoleColor.Gray;
                        MainMenu.MainMenuShow();
                    }
                    else
                    {
                        Console.WriteLine("Account gevonden. Weet u zeker dat u hem wilt verwijderen? (j/n)");

                        if (Console.ReadKey(true).Key == ConsoleKey.J)
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
                        else if (Console.ReadKey(true).Key == ConsoleKey.N)
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
                            Console.WriteLine("Antwoord niet begrepen. U wordt nu teruggestuurd naar het hoofdmenu.");
                            System.Threading.Thread.Sleep(1000);
                            Console.ForegroundColor = ConsoleColor.Gray;
                            MainMenu.MainMenuShow();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Account bestaat niet.");
                    Console.WriteLine("U wordt nu teruggestuurd naar het hoofdmenu.");
                    System.Threading.Thread.Sleep(1000);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    MainMenu.MainMenuShow();
                }
            }
            catch (InvalidOperationException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Account niet gevonden. Neem contact op met de klantenservice.");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                MainMenu.MainMenuShow();
            }
        }
    }
}
