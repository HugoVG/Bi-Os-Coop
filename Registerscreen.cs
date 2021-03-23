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
        public static void accountInfo()
        {
            int id = createID();
            string naam = validCheck("voor- en achternaam", lengthCheck);
            string email = validCheck("e-mailadres", emailCheck);
            string password = validCheck("wachtwoord", lengthCheck);
            string date = validCheck("geboortedatum (dd/mm/jjjj)", dateCheck);

            CPeople.Person customer = new CPeople.Person();
            customer.setPerson(id, naam, email, password, date);
            //ik ga er van uit dat je hier json wilt lezen en schrijven

            CPeople.People jsonPeople = new CPeople.People();
            jsonPeople = jsonPeople.FromJson("");
        }

        public static int createID()
        {
            string ret = "";
            Random randint = new Random();

            for(int i = 0; i < 5; i++)
            {
                ret = ret + randint.Next(0, 10).ToString();
            }
            return int.Parse(ret);
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

        public static bool emailCheck(string email)
        {
            bool at = false;
            bool dot = false;

            foreach (char element in email)
            {
                if (element == '@'){
                    at = true;
                }
                if (element == '.')
                {
                    dot = true;
                }
            }
            if (at && dot)
                return true;
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

        public static int ageCalc(string date)
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
