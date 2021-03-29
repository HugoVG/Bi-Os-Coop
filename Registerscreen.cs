using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Bi_Os_Coop
{
    class Registerscreen
    {
        public static void CreateAccount()
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = new CPeople.People();
            jsonPeople = jsonPeople.FromJson(json);

            int id = createID();
            string naam = validCheck("voor- en achternaam", lengthCheck);
            string email = validCheck("e-mailadres", emailCheck);
            string password = validCheck("wachtwoord", lengthCheck);
            string date = validCheck("geboortedatum (dd/mm/jjjj)", dateCheck);

            CPeople.Person customer = new CPeople.Person();
            customer.setPerson(id, naam, email, password, date);
            jsonPeople.AddPerson(customer);
            string add = jsonPeople.ToJson();
            Json.WriteJson("Accounts", add);
        }

        public static int createID()
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = new CPeople.People();
            jsonPeople = jsonPeople.FromJson(json);
            string ret = "";
            Random randint = new Random();

            for(int i = 0; i < 5; i++)
            {
                ret = ret + randint.Next(0, 10).ToString();
            }
            try
            {
                CPeople.Person persoon = jsonPeople.peopleList.Single(x => x.id == int.Parse(ret));
                return createID();
            }
            catch (InvalidOperationException)
            {
                CPeople.Person persoon = new CPeople.Person();
                Console.WriteLine(ret);
                return int.Parse(ret);
            }
        }

        public static string validCheck(string print, Func<string, bool> function)
        {
            bool valid = false;
            string input = "";

            while (!valid)
            {
                Console.WriteLine($"\nTyp hier uw {print}:");
                input = Console.ReadLine();
                if (function(input))
                    valid = true;
            }


            return input;
        }

        public static bool emailCheck(string input)
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = new CPeople.People();
            jsonPeople = jsonPeople.FromJson(json);
            bool at = false;
            bool dot = false;

            foreach (char element in input)
            {
                if (element == '@'){
                    at = true;
                }
                if (element == '.')
                {
                    dot = true;
                }
            }
            if (at && dot){
                try
                {
                    CPeople.Person persoon = jsonPeople.peopleList.Single(x => x.email == input);
                    Program.newEntry("Dit e-mailadres is al gekoppeld aan een account.", ConsoleColor.Red);
                    return false;
                }
                catch (InvalidOperationException)
                {
                    CPeople.Person persoon = new CPeople.Person();
                    return true;
                }
            }
            Program.newEntry("Dit is geen geldige input!\n", ConsoleColor.Red);
            return false;
        }

        public static bool dateCheck(string date)
        {
            if (lengthCheck(date))
            {
                if (date[2] == '/' && date[5] == '/')
                    return true;
            }
            Program.newEntry("Vul uw geboortedatum alstublieft in volgens de volgende format: dd/mm/jjjj\n", ConsoleColor.Red);
            return false;
        }

        public static bool lengthCheck(string s)
        {
            if (s.Length != 0)
                return true;
            Program.newEntry("Dit is geen geldige input!\n", ConsoleColor.Red);
            return false;
        }

        public static int AgeCalc(string date)
        {
            DateTime todaysDate = DateTime.Now.Date;
            int currentDay = todaysDate.Day;
            int currentMonth = todaysDate.Month;
            int currentYear = todaysDate.Year;
            int birthYear = int.Parse(date.Substring(6));
            int month = int.Parse(date.Substring(3, 2));
            int day = int.Parse(date.Substring(0, 2));
            int age = currentYear - birthYear;

            if (month > currentMonth)
            {
                age -= 1;
            }
            else if (currentMonth >= month)
            {
                if (day > currentDay)
                {
                    age -= 1;
                }
            }
            return age;
        }
    }
}
