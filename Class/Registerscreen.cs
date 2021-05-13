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
        public static bool CreateAccount()
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = new CPeople.People();
            jsonPeople = jsonPeople.FromJson(json);
            Console.WriteLine("Terug naar hoofdmenu (Esc)");

            string naam = validCheck("voor- en achternaam", lengthCheck);
            if (naam == "1go2to3main4menu5") { return false; }
            string birthdate = validCheck("geboortedatum (dd/mm/jjjj)", dateCheck);
            if (birthdate == "1go2to3main4menu5") { return false; }
            if (AgeVerify(birthdate, 14)) {

                int id = createID();
                string email = validCheck("e-mailadres", emailCheck);
                if (email == "1go2to3main4menu5") { return false; }
                string phoneNumber = validCheck("mobiele telefoonnummer", phoneCheck);
                if (phoneNumber == "1go2to3main4menu5") { return false; }
                string password = validCheck("wachtwoord", lengthCheck);
                if (password == "1go2to3main4menu5") { return false; }

                CPeople.Person customer = new CPeople.Person();
                customer.setPerson(id, naam, email.ToLower(), password, birthdate, phoneNumber);
                jsonPeople.AddPerson(customer);
                string add = jsonPeople.ToJson();
                Json.WriteJson("Accounts", add);
                Program.newEntry("\nUw account is gemaakt.\nDruk op ENTER om verder te gaan.");
                Console.ReadLine();
                Console.Clear();
                return true;
            }
            else{
                Program.newEntry("\nSorry, je kunt pas een account aanmaken als je 14 jaar of ouder bent.", ConsoleColor.Red);
                Program.newEntry("\nDruk op enter om terug te gaan.");
                Console.ReadLine();
                Console.Clear();
                return false;
            }
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
                input = loginscherm.newwayoftyping();
                if (input == "1go2to3main4menu5")
                {
                    return "1go2to3main4menu5";
                }
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
            if (lengthCheck(input)){
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
                        Program.newEntry("Dit e-mailadres is al gekoppeld aan een account.\n", ConsoleColor.Red);
                        return false;
                    }
                    catch (InvalidOperationException)
                    {
                        CPeople.Person persoon = new CPeople.Person();
                        return true;
                    }
                }
            }
            errorMessage("e-mailadres", "iemand@example.nl");
            return false;
        }

        public static bool phoneCheck(string input)
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = new CPeople.People();
            jsonPeople = jsonPeople.FromJson(json);
            int value;
            if (input.Length == 10){
                if (input.Substring(0, 2) == "06"){
                    if (int.TryParse(input, out value)){
                        try
                        {
                            CPeople.Person persoon = jsonPeople.peopleList.Single(x => x.phonenumber == input);
                            Program.newEntry("Dit telefoonnummer is al gekoppeld aan een account.\n", ConsoleColor.Red);
                            return false;
                        }
                        catch (InvalidOperationException)
                        {
                            CPeople.Person persoon = new CPeople.Person();
                            return true;
                        }
                    }
                }
            }
            errorMessage("telefoonnummer", "06XXXXXXXX");
            return false;
        }
        public static bool dateCheck(string date)
        {
            if (lengthCheck(date))
            {
                int i;
                if (!(int.TryParse(date[2].ToString(), out i) && int.TryParse(date[5].ToString(), out i)))
                    return true;
            }
            errorMessage("geboortedatum", "dd/mm/jjjj");
            return false;
        }

        public static bool lengthCheck(string s)
        {
            if (s.Length != 0)
                return true;
            Program.newEntry("Dit is geen geldige input!\n", ConsoleColor.Red);
            return false;
        }
        public static void errorMessage(string s, string t)
        {
            Program.newEntry($"Vul uw {s} alstublieft in volgens de volgende format: {t}\n", ConsoleColor.Red);
        }

        public static bool AgeVerify(string birthdate, int minimalAge)
        {
            DateTime todaysDate = DateTime.Now.Date;
            int currentDay = todaysDate.Day;
            int currentMonth = todaysDate.Month;
            int currentYear = todaysDate.Year;
            int birthYear = int.Parse(birthdate.Substring(6));
            int month = int.Parse(birthdate.Substring(3, 2));
            int day = int.Parse(birthdate.Substring(0, 2));
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
            if (minimalAge < age)
                return true;
            return false;
        }
    }
}
