using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    class Registerscreen
    {
        public static void accountInfo()
        {
            int id = createID();
            string naam = "";
            string email = "";
            bool validEmail = false;
            string password = "";
            string date = "";

            Console.WriteLine("Typ hier uw voor- en achternaam:");
            naam = Console.ReadLine();

            while(!validEmail){
                Console.WriteLine("\nTyp hier uw e-mailadres:");
                email = Console.ReadLine();
                if (emailCheck(email))
                    validEmail = true;
            }

            Console.WriteLine("\nTyp hier uw wachtwoord:");
            password = Console.ReadLine();

            Console.WriteLine("\nTyp hier uw geboortedatum (dd/mm/jjjj):");
            date = Console.ReadLine();

            var customer = new CPeople.Person();
            customer.setPerson(id, naam, email, password, date);
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
            Program.newEntry("\nVoer alstublieft een valide e-mailadres in!", ConsoleColor.Red);
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
