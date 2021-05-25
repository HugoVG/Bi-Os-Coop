using System;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    class Registerscreen
    {

        /// <summary>
        /// CreateAccount is een functie waarin alle functies uit de class registerscreen achter elkaar worden uitgevoerd. Als je dus een heel nieuw account wilt maken moet je deze functie uitvoeren.
        /// </summary>
        public static void CreateAccount()
        {
            //In de 4 regels hieronder wordt alle info uit Accounts.json gehaald en in de variabele jsonPeople gestopt.
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = CPeople.People.FromJson(json);
            Console.WriteLine("Terug naar hoofdmenu (Esc)");

            //De if statements hieronder checken of de input van de gebruiker volgens format is en of het e-mailadres of het telefoonnummer al gekoppeld is aa een account.
            string naam = validCheck("voor- en achternaam", lengthCheck);
            if (naam == "1go2to3main4menu5") { return; }
            string birthdate = validCheck("geboortedatum (dd/mm/jjjj)", dateCheck);
            if (birthdate == "1go2to3main4menu5") { return; }
            if (AgeVerify(birthdate, 14)) {

                int id = createID();
                string email = validCheck("e-mailadres", emailCheck);
                if (email == "1go2to3main4menu5") { return; }
                string phoneNumber = validCheck("mobiele telefoonnummer", phoneCheck);
                if (phoneNumber == "1go2to3main4menu5") { return; }
                string password = validCheck("wachtwoord", lengthCheck);
                if (password == "1go2to3main4menu5") { return; }

                //In de volgende code worden alle inputs van de gebruiker opgeslagen
                CPeople.Person customer = new CPeople.Person();
                customer.setPerson(id, naam, email.ToLower(), password, birthdate, phoneNumber);
                jsonPeople.AddPerson(customer);
                string add = jsonPeople.ToJson();
                Json.WriteJson("Accounts", add);
                Program.newEntry("\nUw account is gemaakt.\nDruk op ENTER om verder te gaan.");
                Console.ReadLine();
                Console.Clear();
                MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                dynamic user = things.user; string sort = things.sort; bool reverse = things.reverse; string login = things.login; string language = things.language;
                MainMenu.jsonmainmenu(loginscherm.mailwachtvragen(email, password), sort, reverse, "Person", language);
            }
            //Zodra je je geboortedatum invult wordt je leeftijd berekent. Als blijkt dat je nog geen 14 bent, dan spring je deze else in en sluit de functie af.
            else{
                Program.newEntry("\nSorry, je kunt pas een account aanmaken als je 14 jaar of ouder bent.", ConsoleColor.Red);
                Program.newEntry("\nDruk op enter om terug te gaan.");
                Console.ReadLine();
                Console.Clear();
            }
        }

        /// <summary>
        /// CreateID creëert een random ID die nog niet de lijst in Accounts.json staat. Als het random gegenereerde ID al wel in de lijst staat wordt de functie opnieuw gestart.
        /// </summary>
        /// <returns></returns>
        public static int createID()
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = CPeople.People.FromJson(json);
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
            catch (Exception)
            {
                try
                {
                    CPeople.Admin admin = jsonPeople.adminList.Single(x => x.id == int.Parse(ret));
                    return createID();
                }
                catch (Exception)
                {
                    try
                    {
                        CPeople.Employee employee = jsonPeople.employeeList.Single(x => x.id == int.Parse(ret));
                        return createID();
                    }
                    catch (Exception)
                    {
                        CPeople.Person persoon = new CPeople.Person();
                        return int.Parse(ret);
                    }
                }
            }
        }

        /// <summary>
        /// Deze functie heeft als parameters 'print' en 'function'. Print staat voor hetgene wat er in de console gevraagd wordt (bv. geboortedatum, telefoonnummer, voor- en achternaam, etc).
        /// Function staat voor de naam van de functie die verder checkt of de input goed is. (bv. emailCheck, phoneCheck, dateCheck, etc).
        /// In de functie zelf wordt er gecheckt of de input van de gebruiker geen lege string is. Als dat wel zo is, wordt er opnieuw gevraagd om de gevraagde informatie in de console te typen.
        /// </summary>
        /// <param name="print"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static string validCheck(string print, Func<string, bool> function)
        {
            bool valid = false;
            string input = "";

            while (!valid)
            {
                if (print == "geboortedatum (dd/mm/jjjj)")
                {
                    Console.WriteLine($"\nTyp hier uw {print}:");
                    input = loginscherm.getdate();
                    if (input == "1go2to3main4menu5")
                    {
                        return "1go2to3main4menu5";
                    }
                    if (function(input))
                        valid = true;
                }
                else
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
            }


            return input;
        }

        /// <summary>
        /// emailCheck kijkt of er een '@' en een '.' in variabele 'input' zitten. Als dat het geval is checkt hij of dit e-mailadres al in Accounts.json gevonden kan worden.
        /// Als hij nog niet gekoppeld is aan een account is èn er zit een '@' en een '.' in, dan returnt de functie 'true', anders returnt hij 'false'.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool emailCheck(string input)
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = CPeople.People.FromJson(json);
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
                    catch (Exception)
                    {
                        try
                        {
                            CPeople.Admin admin = jsonPeople.adminList.Single(x => x.email == input);
                            Program.newEntry("Dit e-mailadres is al gekoppeld aan een account.\n", ConsoleColor.Red);
                            return false;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                CPeople.Employee employee = jsonPeople.employeeList.Single(x => x.email == input);
                                Program.newEntry("Dit e-mailadres is al gekoppeld aan een account.\n", ConsoleColor.Red);
                                return false;
                            }
                            catch (Exception)
                            {
                                CPeople.Person persoon = new CPeople.Person();
                                return true;
                            }
                        }
                    }
                }
            }
            errorMessage("e-mailadres", "iemand@example.nl");
            return false;
        }

        /// <summary>
        /// phoneCheck checkt of het ingevoerde telefoonnummer begint met '06' en bestaat uit 10 cijfers. Als dat het geval is checkt hij of dit telefoonnummer al in Accounts.json gevonden kan worden.
        /// Als hij nog niet gekoppeld is aan een account en hij voldoet aan alle voorwaarden, dan returnt de functie 'true', anders returnt hij 'false'.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool phoneCheck(string input)
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = CPeople.People.FromJson(json);
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
                        catch (Exception)
                        {
                            try
                            {
                                CPeople.Admin admin = jsonPeople.adminList.Single(x => x.phonenumber == input);
                                Program.newEntry("Dit telefoonnummer is al gekoppeld aan een account.\n", ConsoleColor.Red);
                                return false;
                            }
                            catch (Exception)
                            {
                                try
                                {
                                    CPeople.Employee employee = jsonPeople.employeeList.Single(x => x.phonenumber == input);
                                    Program.newEntry("Dit telefoonnummer is al gekoppeld aan een account.\n", ConsoleColor.Red);
                                    return false;
                                }
                                catch (Exception)
                                {
                                    CPeople.Person persoon = new CPeople.Person();
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            errorMessage("telefoonnummer", "06XXXXXXXX");
            return false;
        }

        /// <summary>
        /// dateCheck checkt of de ingevoerde datum voldoet aan de format. Als index 2 en 5 geen integers zijn keurt de functie het goed en returnt hij 'true', anders returnt hij 'false'.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
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

        /// <summary>
        /// lengthCheck kijkt letterlijk alleen maar of een string geen lege string is. Als je namelijk een lege string opslaat in een .json bestand en je probeert het later weer uit te lezen,
        /// dan crasht je .json bestand.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool lengthCheck(string s)
        {
            if (s.Length != 0)
                return true;
            Program.newEntry("Dit is geen geldige input!\n", ConsoleColor.Red);
            return false;
        }

        /// <summary>
        /// Letterlijk gewoon een functie waarin ik de format van de errormessages al heb klaar staan zodat alle errormessages dezelfde format hebben.
        /// Vooral gemaakt omdat ik heel erg lui ben tho xD
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        public static void errorMessage(string s, string t)
        {
            Program.newEntry($"Vul uw {s} alstublieft in volgens de volgende format: {t}\n", ConsoleColor.Red);
        }

        /// <summary>
        /// AgeVerify berekent de leeftijd aan de hand van variabele 'birthdate'. Je kunt ook een minimumleeftijd instellen, als je die niet meestuurt is het by default '0'.
        /// 'birthdate' moet als format dd/mm/jjjj. Het maakt niet uit wat er op index 2 en 5 staat, zolang de volgorde maar dag/maand/jaar is en zolang de data
        /// maar op index [0,1][3,4][6,7,8,9] staan.
        /// Als de berekende leeftijd hetzelfde, of hoger, is dan de minimumleeftijd, dan returnt de functie 'true'. Anders returtn de functie 'false'
        /// </summary>
        /// <param name="birthdate"></param>
        /// <param name="minimumAge"></param>
        /// <returns></returns>
        public static bool AgeVerify(string birthdate, int minimumAge = 0)
        {

            //Hieronder wordt de huidige datum gecheckt en wordt de datum uit de 'birthdate' variabele gehaald.
            DateTime todaysDate = DateTime.Now.Date;
            int currentDay = todaysDate.Day;
            int currentMonth = todaysDate.Month;
            int currentYear = todaysDate.Year;
            int birthYear = int.Parse(birthdate.Substring(6));
            int month = int.Parse(birthdate.Substring(3, 2));
            int day = int.Parse(birthdate.Substring(0, 2));
            int age = currentYear - birthYear;

            //Hieronder wordt berekend wat de leeftijd is.
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
            if (minimumAge < age)
                return true;
            return false;
        }
    }
}
