using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    public class adminMenu
    {
        public static void AM(dynamic user = null, string login = null)
        {
            CPeople.Admin admin = new CPeople.Admin();
            adminMethods adminMethod = new adminMethods();

            //while loop die hoofdPagina loopt tot je "0" in tikt
            bool inDitMenu = true;
            bool isCoronaFilter = adminMethod.coronaCheck();
            while (inDitMenu)
            {
                ConsoleKey keuze = hoofdPagina();
                if (keuze == ConsoleKey.D0)
                {
                    Environment.Exit(0);
                }
                else if (keuze == ConsoleKey.D1)
                {
                    inDitMenu = false;
                    Console.Clear();
                    MainMenu.MainMenuShow();
                }
                else if (keuze == ConsoleKey.D2)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    admin.AddMovies();
                }

                else if (keuze == ConsoleKey.D3)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    admin.UpdateMovies();
                }
                else if (keuze == ConsoleKey.D4)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    admin.DeleteMovies();
                }
                else if (keuze == ConsoleKey.D5)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    admin.AddCinemaHall();
                }
                else if (keuze == ConsoleKey.D6)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    adminMethod.DeleteCinemaHall();
                }
                else if (keuze == ConsoleKey.D7)
                {
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    adminMethod.AddAdminOrWorker();
                }
                else if (keuze == ConsoleKey.D8)
                {
                    adminMethod.CoronaFilter(isCoronaFilter);
                }
                else if (keuze == ConsoleKey.D9)
                {
                    MainMenuThings things = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                    things.user = null;
                    things.sort = "name";
                    things.reverse = false;
                    things.login = "None";
                    things.language = "Nederlands";
                    MainMenu.JsonMainMenuSave(things.user, things.sort, things.reverse, things.login, things.language);
                    Console.Clear();
                    MainMenu.MainMenuShow();
                }
                else
                {
                    adminMenu.AM();
                }
            }
        }

        public static ConsoleKey hoofdPagina()
        {
            adminMethods adminMethod = new adminMethods();
            Tuple<int, bool[]> zalenInfo = adminMethod.CountCinemaHalls();
            Console.Clear();
            MainMenu.Logo();
            Console.WriteLine("Admin Menu\n");
            Console.WriteLine("Maak een keuze: ");
            Console.WriteLine("1) Naar Main Menu");
            Console.WriteLine("2) Film toevoegen");
            Console.WriteLine("3) Film aanpassen");
            Console.WriteLine("4) Film verwijderen");
            if (zalenInfo.Item1 == 1) { Console.WriteLine($"5) Zaal toevoegen \t\t Er is {zalenInfo.Item1} zaal"); }
            else if (zalenInfo.Item1 > 1) { Console.WriteLine($"5) Zaal toevoegen \t\t Er zijn {zalenInfo.Item1} zalen"); }
            else { Console.WriteLine("5) Zaal toevoegen \t\t Er zijn geen zalen"); }
            Console.WriteLine("6) Zaal verwijderen");
            Console.WriteLine("7) Admin of medewerker toevoegen");
            Console.WriteLine($"8) Corona filter toepassen \t {adminMethod.coronaCheck()}");
            Console.WriteLine("9) Uitloggen");
            Console.WriteLine("Of type '0' om te stoppen");
            Console.Write("\nMaak een keuze: ");
            ConsoleKey keuze = Console.ReadKey(true).Key;
            return keuze;
        }
    }
    public class adminMethods
    {
        public bool coronaCheck()
        {
            string jsonZalen = Json.ReadJson("Zalen");
            Zalen zalen = Zalen.FromJson(jsonZalen);
            foreach (Zaal zaal in zalen.zalenList)
            {
                List<Stoel> stoel = zaal.stoelen;
                foreach (Stoel stoel2 in stoel)
                {
                    if (stoel2.isOccupied && stoel2.Price == 0 && stoel2.isOccupiedBy == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Tuple<int, bool[]> CountCinemaHalls()
        {
            MainMenu.ClearAndShowLogoPlusEsc("Admin");
            int zalenAmount = 0;
            Zalen zalen = new Zalen();
            string jsonZalen = Json.ReadJson("Zalen");
            zalen = Zalen.FromJson(jsonZalen);
            foreach (Zaal zaal in zalen.zalenList)
            {
                zalenAmount++;
            }
            if (zalenAmount == 0) { return null; }
            bool[] delete = new bool[zalenAmount];
            for (int i = 0; i < delete.Length; i++) { delete[i] = true; }
            int index = 0;
            foreach (Zaal zaal in zalen.zalenList)
            {
                List<Stoel> stoel = zaal.stoelen;
                foreach (Stoel stoel2 in stoel)
                {
                    if (stoel2.isOccupied == true && stoel2.Price != 0 && index < delete.Length && stoel2.isOccupiedBy != 1)
                    {
                        delete[index++] = false;
                    }
                }
            }
            return Tuple.Create(zalenAmount, delete);
        }

        public void DeleteCinemaHall()
        {
            Tuple<int, bool[]> zalenInfo = CountCinemaHalls();
            if (zalenInfo != null)
            {
                Zalen zalen = new Zalen();
                string jsonZalen = Json.ReadJson("Zalen");
                zalen = Zalen.FromJson(jsonZalen);
                Console.WriteLine($"Er zijn op dit moment {zalenInfo.Item1} zalen in de Bi-Os-Coop.\nKies een zaal om te verwijderen of tik '0' voor alle info rondom de zalen: ");
                string antwoord = loginscherm.newwayoftyping().ToLower();
                if (antwoord == "1go2to3main4menu5") { return; }
                try
                {
                    int antwoordAlsGetal = Convert.ToInt32(antwoord);
                    if (antwoordAlsGetal == 0) { zalen.writeZalen(zalen.zalenList); }
                    Console.WriteLine("\nKies een zaal om te verwijderen met de getallen links van het scherm: ");
                    antwoord = loginscherm.newwayoftyping().ToLower();
                    if (antwoord == "1go2to3main4menu5") { return; }
                    try { antwoordAlsGetal = Convert.ToInt32(antwoord); }
                    catch
                    {
                        Console.WriteLine($"De keuze {antwoord} is niet valide. Probeer het opnieuw");
                        MainMenu.ClearAndShowLogoPlusEsc("Admin");
                        DeleteCinemaHall();
                    }
                    if (antwoordAlsGetal <= zalenInfo.Item1)
                    {
                        int index = 0;

                        foreach (Zaal zaal in zalen.zalenList)
                        {
                            if (index + 1 == antwoordAlsGetal)
                            {
                                if (zalenInfo.Item2[index])
                                {
                                    zalen.zalenList.RemoveAt(index);
                                    Json.WriteJson("Zalen", zalen.ToJson());
                                    break;
                                }
                                else if (zalenInfo.Item2[index] == false)
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Deze zaal kan niet verwijdert worden omdat er al stoelen zijn gereserveerd.");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("Wilt u een andere zaal verwijderen? (J/N)");
                                    string antwoord2 = loginscherm.newwayoftyping().ToLower();
                                    if (antwoord2 == "1go2to3main4menu5") { return; }
                                    if (antwoord2 == "j" || antwoord2 == "ja" || antwoord2 == "y" || antwoord2 == "yes")
                                    {
                                        MainMenu.ClearAndShowLogoPlusEsc("Admin");
                                        DeleteCinemaHall();
                                    }
                                    else
                                    {
                                        adminMenu.AM();
                                    }
                                }
                            }
                            index++;
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"De zaal {antwoord} kon niet gevonden worden.");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.WriteLine("Wilt u het opnieuw proberen?");
                        string antwoord2 = loginscherm.newwayoftyping().ToLower();
                        if (antwoord2 == "j" || antwoord2 == "ja" || antwoord2 == "y" || antwoord2 == "yes")
                        {
                            MainMenu.ClearAndShowLogoPlusEsc("Admin");
                            DeleteCinemaHall();
                        }
                        else
                        {
                            adminMenu.AM();
                        }
                    }
                }
                catch
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"De keuze {antwoord} is niet valide. Probeer het opnieuw");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    MainMenu.ClearAndShowLogoPlusEsc("Admin");
                    DeleteCinemaHall();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Er zijn op dit moment geen zalen.");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("Wilt u een zaal aanmaken voor een film?");
                string antwoord2 = loginscherm.newwayoftyping().ToLower();
                if (antwoord2 == "j" || antwoord2 == "ja" || antwoord2 == "y" || antwoord2 == "yes")
                {
                    CPeople.Admin admin = new CPeople.Admin();
                    admin.AddCinemaHall();
                }
                else
                {
                    adminMenu.AM();
                }
            }
        }

        public void AddAdminOrWorker()
        {
            string json = Json.ReadJson("Accounts");
            CPeople.People jsonPeople = JsonSerializer.Deserialize<CPeople.People>(json);
            Console.WriteLine("Wilt u een medewerker of admin toevoegen? 'A'/'M'");
            string antwoord = loginscherm.newwayoftyping().ToLower();
            if (antwoord == "1go2to3main4menu5") { return; }
            if (antwoord == "admin" || antwoord == "a")
            {
                Console.WriteLine("Vul hier het e-mailadres in van de medewerker: ");
                string accountNaam = loginscherm.newwayoftyping().ToLower();
                if (accountNaam == "1go2to3main4menu5") { return; }
                Console.WriteLine("Vul hier het wachtwoord in: ");
                SecureString pass = loginscherm.maskInputString();
                string password = new System.Net.NetworkCredential(string.Empty, pass).Password;
                string accountWachtwoord = password;
                if (accountWachtwoord == "1go2to3main4menu5") { return; }
                var feedback = loginscherm.mailwachtvragen(accountNaam, accountWachtwoord);
                bool accountFound = false;
                try
                {
                    if (feedback == false)
                    {
                        accountFound = false;
                    }
                }
                catch
                {
                    accountFound = true;
                }
                if (accountFound)
                {
                    if (feedback.isAdmin())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deze persoon is al een admin en dit kan niet veranderd worden.");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Wilt u het opnieuw proberen? (J/N)");
                        string antwoord2 = loginscherm.newwayoftyping().ToLower();
                        if (antwoord2 == "1go2to3main4menu5") { return; }
                        if (antwoord2 == "j" || antwoord2 == "ja" || antwoord2 == "yes" || antwoord2 == "y")
                        {
                            Console.Clear();
                            AddAdminOrWorker();
                        }
                        else
                        {
                            adminMenu.AM();
                        }
                    }
                    else if (isEmployee(feedback))
                    {
                        CPeople.Admin user = new CPeople.Admin();
                        user.setPerson(feedback.id, feedback.name, feedback.email, feedback.password, feedback.age, feedback.phonenumber);
                        jsonPeople.AddAdmin(user);
                        JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                        int index = jsonPeople.employeeList.FindIndex(person => person.name == feedback.name);
                        jsonPeople.employeeList.RemoveAt(index);
                        json = JsonSerializer.Serialize(jsonPeople, opt);
                        Json.WriteJson("Accounts", json);
                    }
                }
                else if (feedback == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    Console.WriteLine("Dit account bestaat niet in ons systeem.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Wilt u het opnieuw proberen? (J/N)");
                    string opnieuw = loginscherm.newwayoftyping().ToLower();
                    if (opnieuw == "1go2to3main4menu5") { return; }
                    if (opnieuw == "j" || opnieuw == "ja" || opnieuw == "yes" || opnieuw == "y")
                    {
                        Console.Clear();
                        AddAdminOrWorker();
                    }
                    else
                    {
                        adminMenu.AM();
                    }
                }
            }
            else if (antwoord == "medewerker" || antwoord == "m")
            {
                Console.Clear();
                CPeople.Person person = AddWorker();
                CPeople.Employee user = new CPeople.Employee();
                user.setPerson(person.id, person.name, person.email, person.password, person.age, person.phonenumber);
                jsonPeople.AddEmployee(user);
                JsonSerializerOptions opt = new JsonSerializerOptions { WriteIndented = true };
                json = JsonSerializer.Serialize(jsonPeople, opt);
                if (person.name != null && user.name != null) { Json.WriteJson("Accounts", json); }
                if (user.name != null && person.name != null)
                {
                    Console.WriteLine("Wilt u de medewerker een admin maken? (J/N)");
                    string antwoord2 = loginscherm.newwayoftyping().ToLower();
                    if (antwoord2 == "j" || antwoord2 == "ja" || antwoord2 == "yes" || antwoord2 == "y")
                    {
                        CPeople.Admin AdminUser = new CPeople.Admin();
                        AdminUser.setPerson(user.id, user.name, user.email, user.password, user.age, user.phonenumber);
                        jsonPeople.AddAdmin(AdminUser);
                        int index = jsonPeople.employeeList.FindIndex(Person => Person.name == AdminUser.name);
                        json = JsonSerializer.Serialize(jsonPeople, opt);
                        jsonPeople.employeeList.RemoveAt(index);
                        json = JsonSerializer.Serialize(jsonPeople, opt);
                        Json.WriteJson("Accounts", json);
                    }
                    else
                    {
                        adminMenu.AM();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{antwoord} is geen valide antwoord.");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Wilt u het nog een keer proberen? (J/N)");
                string vraag = loginscherm.newwayoftyping().ToLower();
                if (vraag == "j" || vraag == "ja" || vraag == "yes" || vraag == "y") { AddAdminOrWorker(); }
                else if (vraag == "nee" || vraag == "no" || vraag == "n") { adminMenu.AM(); }
                else { adminMenu.AM(); }
            }
        }

        public dynamic AddWorker()
        {
            CPeople.Person worker = new CPeople.Person();
            string ingelogd = Json.ReadJson("MainMenu");
            string accountsBefore = Json.ReadJson("Accounts");
            Registerscreen.CreateAccount();
            Json.WriteJson("MainMenu", ingelogd);
            string accounts = Json.ReadJson("Accounts");
            CPeople.People list = CPeople.People.FromJson(accounts);
            if (list.peopleList != null && accountsBefore != accounts)
            {
                int length = 0;
                foreach (CPeople.Person person in list.peopleList) { length++; }
                int length2 = 0;
                foreach (CPeople.Person person in list.peopleList)
                {
                    length2++;
                    if (length2 == length)
                    {
                        worker.setPerson(person.id, person.name, person.email, person.password, person.age, person.phonenumber);
                    }
                }
            }
            return worker;
        }

        public bool isEmployee(CPeople.Employee person) { if (person.GetType().Equals(typeof(CPeople.Employee))) { return true; } return false; }
        public bool isEmployee(CPeople.Admin person) { if (person.GetType().Equals(typeof(CPeople.Employee))) { return true; } return false; }
        public bool isEmployee(CPeople.Person person) { if (person.GetType().Equals(typeof(CPeople.Employee))) { return true; } return false; }

        public void CoronaFilter(bool isCoronaFilter)
        {
            string jsonZalen = Json.ReadJson("Zalen");
            Zalen zalen = Zalen.FromJson(jsonZalen);
            if (!isCoronaFilter)
            {
                foreach (Zaal zaal in zalen.zalenList)
                {
                    int count = 0;
                    int length = 0;
                    List<Stoel> stoel = zaal.stoelen;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied)
                        {
                            count += 1;
                        }
                        length++;
                    }
                    Tuple<bool, int, Stoel.price, int>[] occupiedStoelen = new Tuple<bool, int, Stoel.price, int>[count];
                    int tempIndex = 0;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied && tempIndex < count)
                        {
                            occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel2.Price, stoel.FindIndex(a => a == stoel2));
                        }
                    }

                    zaal.setZaal(zaal.date, zaal.time, length, zaal.film);
                    for (int j = 0, i = 0; j < length; j++)
                    {
                        if (j % 3 == 0)
                        {
                            zaal.stoelen[j].isOccupied = false;
                            zaal.stoelen[j].isOccupiedBy = 1;
                            zaal.stoelen[j].Price = zaal.stoelen[j].stoolworth(j, length);
                        }
                        else
                        {
                            zaal.stoelen[j].isOccupied = true;
                            zaal.stoelen[j].isOccupiedBy = 0;
                            zaal.stoelen[j].Price = 0;
                        }
                        int index = zaal.stoelen.FindIndex(st => st == zaal.stoelen[j]);
                        if (i < occupiedStoelen.Length && occupiedStoelen[i].Item4 == index)
                        {
                            zaal.stoelen[j].isOccupied = occupiedStoelen[i].Item1;
                            zaal.stoelen[j].isOccupiedBy = occupiedStoelen[i].Item2;
                            zaal.stoelen[j].Price = occupiedStoelen[i].Item3;
                            i++;
                        }
                    }
                    Json.WriteJson("Zalen", zalen.ToJson());
                }
            }
            else
            {
                foreach (Zaal zaal in zalen.zalenList)
                {
                    int count = 0;
                    int length = 0;
                    List<Stoel> stoel = zaal.stoelen;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied && stoel2.isOccupiedBy != 0)
                        {
                            count += 1;
                        }
                        length++;
                    }
                    Tuple<bool, int, Stoel.price, int>[] occupiedStoelen = new Tuple<bool, int, Stoel.price, int>[count];
                    int tempIndex = 0;
                    foreach (Stoel stoel2 in stoel)
                    {
                        if (stoel2.isOccupied && stoel2.isOccupiedBy != 0 && tempIndex < count)
                        {
                            occupiedStoelen[tempIndex++] = Tuple.Create(stoel2.isOccupied, stoel2.isOccupiedBy, stoel2.Price, stoel.FindIndex(a => a == stoel2));
                        }
                    }
                    int minus = 18;
                    if (length > 499) { minus = 130; }
                    else if (length < 500 && length > 299) { minus = 42; }
                    zaal.setZaal(zaal.date, zaal.time, length - minus, zaal.film);
                    for (int j = 0, i = 0; j < length - minus; j++)
                    {
                        int index = zaal.stoelen.FindIndex(st => st == zaal.stoelen[j]);
                        if (i < occupiedStoelen.Length && occupiedStoelen[i].Item4 == index)
                        {
                            zaal.stoelen[j].isOccupied = occupiedStoelen[i].Item1;
                            zaal.stoelen[j].isOccupiedBy = occupiedStoelen[i].Item2;
                            zaal.stoelen[j].Price = occupiedStoelen[i].Item3;
                            i++;
                        }
                    }
                    Json.WriteJson("Zalen", zalen.ToJson());
                }
            }
        }
    }
}
