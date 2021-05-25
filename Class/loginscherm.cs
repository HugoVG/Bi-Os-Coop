using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace Bi_Os_Coop.Class
{
    class loginscherm
    {
        /// <summary>
        /// ik gebruik Dynamic hier want we willen admin//employee//User
        /// </summary>
        public static dynamic mailwachtvragen(string username, string password)
        {
            string account = Json.ReadJson("Accounts");
            CPeople.People accounts = CPeople.People.FromJson(account);
            try
            {
                CPeople.Person persoon = accounts.peopleList.Single(henk => henk.email.ToLower() == username.ToLower() && henk.password == password);
                return persoon;
            }
            catch (Exception)
            {
                try
                {
                    CPeople.Admin admin = accounts.adminList.Single(henk => henk.email.ToLower() == username.ToLower() && henk.password == password);
                    return admin;
                }
                catch (Exception)
                {
                    try
                    {
                        CPeople.Employee employee = accounts.employeeList.Single(henk => henk.email.ToLower() == username.ToLower() && henk.password == password);
                        return employee;
                    }
                    catch (Exception)
                    {
                        check();
                        return false;
                    }
                }
            }
        }
        public static string getdate()
        {
            string newstring = "";
            ConsoleKeyInfo keyInfo;
            bool unlocked = false;

            do
            {
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter && newstring.Length == 10)
                {
                    if (newstring.Substring(0, 5) == "29/02")
                    {
                        if (isleapyear(Convert.ToInt32(newstring.Substring(6)))) { unlocked = true; }
                        else
                        {
                            Program.newEntry("\nNot a leap year\n", ConsoleColor.Red);
                            newstring = "";
                            Console.Write(newstring);
                        }
                    }
                    else { unlocked = true; }
                }
                else if (keyInfo.Key == ConsoleKey.Escape) { unlocked = true; }
                if ((newstring.Length == 2 || newstring.Length == 5) && keyInfo.Key != ConsoleKey.Backspace)
                {
                    newstring += "/";
                    Console.Write('/');
                }
                if (Char.IsNumber(keyInfo.KeyChar))
                {
                    if (newstring.Length == 0 && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.D3))
                    {
                        newstring += keyInfo.KeyChar;
                        Console.Write(keyInfo.KeyChar);
                    }
                    else if (newstring.Length == 1)
                    {
                        if (newstring[0] == '3' && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1))
                        {
                            newstring += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                        }
                        else if (newstring[0] == '0' && keyInfo.Key != ConsoleKey.D0)
                        {
                            newstring += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                        }
                        else if (newstring[0] == '1' || newstring[0] == '2')
                        {
                            newstring += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                        }
                    }
                    else if (newstring.Length == 3 && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1))
                    {
                        newstring += keyInfo.KeyChar;
                        Console.Write(keyInfo.KeyChar);
                    }

                    else if (newstring.Length == 4)
                    {
                        if (newstring[0] == '3' && newstring[1] == '0')
                        {
                            if (newstring[3] == '0' && (keyInfo.Key != ConsoleKey.D2 && keyInfo.Key != ConsoleKey.D0))
                            {
                                newstring += keyInfo.KeyChar;
                                Console.Write(keyInfo.KeyChar);
                            }
                            else if (newstring[3] == '1' && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D2))
                            {
                                newstring += keyInfo.KeyChar;
                                Console.Write(keyInfo.KeyChar);
                            }

                        }
                        if (newstring[0] == '3' && newstring[1] == '1')
                        {
                            if (newstring[3] == '0' &&
                                (keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.D5 || keyInfo.Key == ConsoleKey.D7 || keyInfo.Key == ConsoleKey.D8))
                            {
                                newstring += keyInfo.KeyChar;
                                Console.Write(keyInfo.KeyChar);
                            }
                            else if (newstring[3] == '1' && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D2))
                            {
                                newstring += keyInfo.KeyChar;
                                Console.Write(keyInfo.KeyChar);
                            }
                        }
                        else if ((newstring[0] == '0' || newstring[0] == '1') && newstring[3] == '0' && (keyInfo.Key != ConsoleKey.D0))
                        {
                            newstring += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                        }
                        else if ((newstring[0] == '0' || newstring[0] == '1') && newstring[3] == '1' && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D2))
                        {
                            newstring += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                        }
                        else if (newstring[0] == '2' && newstring[3] == '0' && (keyInfo.Key != ConsoleKey.D0))
                        {
                            newstring += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                        }
                        else if (newstring[0] == '2' && newstring[3] == '1' && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D2))
                        {
                            newstring += keyInfo.KeyChar;
                            Console.Write(keyInfo.KeyChar);
                        }
                    }

                    else if (newstring.Length == 6 && (keyInfo.Key == ConsoleKey.D0 || keyInfo.Key == ConsoleKey.D1 || keyInfo.Key == ConsoleKey.D2))
                    {
                        newstring += keyInfo.KeyChar;
                        Console.Write(keyInfo.KeyChar);
                    }
                    else if (newstring.Length == 7 || newstring.Length == 8 || newstring.Length == 9)
                    {
                        newstring += keyInfo.KeyChar;
                        Console.Write(keyInfo.KeyChar);
                    }
                }
                if (keyInfo.Key == ConsoleKey.Backspace && newstring.Length > 0)
                {
                    newstring = newstring.Substring(0, newstring.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (!unlocked);
            {
                Console.WriteLine();
                if (keyInfo.Key == ConsoleKey.Enter)
                { 
                    return newstring;
                }
                else
                {
                    return "1go2to3main4menu5";
                }
            }
        }

        public static bool isleapyear(int year)
        {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) { return true; }
            else
            {
                return false;
            }
        }

        public static string newwayoftyping()
        {
            string newstring = "";
            ConsoleKeyInfo keyInfo;
            bool unlocked = false;

            do
            {
                keyInfo = Console.ReadKey(true);
                if ((keyInfo.Key == ConsoleKey.Enter && newstring.Length > 1) || keyInfo.Key == ConsoleKey.Escape) { unlocked = true; }

                if (!char.IsControl(keyInfo.KeyChar))
                {
                    newstring += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
                if (keyInfo.Key == ConsoleKey.Backspace && newstring.Length > 0)
                {
                    newstring = newstring.Substring(0, newstring.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (!unlocked);
            {
                Console.WriteLine();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return newstring;
                }
                else
                {
                    return "1go2to3main4menu5";
                }
            }
        }

        public static string RemoveSpecialCharacters(string str)
        {
            StringBuilder newString = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    newString.Append(Char.ToLower(c));
                }
            }
            return newString.ToString();
        }

        public static string FirstCharToUpper(string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static dynamic login()
        {
            MainMenu.ClearAndShowLogoPlusEsc("Main Menu");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("E-mail:");
            string username = newwayoftyping();
            if (username != "1go2to3main4menu5")
            {
                Console.WriteLine("\nWachtwoord:");
                SecureString pass = maskInputString();
                string password = new System.Net.NetworkCredential(string.Empty, pass).Password;
                if (password != "1go2to3main4menu5")
                {
                    var inlog = loginscherm.mailwachtvragen(username.ToLower(), password);
                    try { if (inlog == false) { inlog = login(); } }
                    catch
                    {
                        // ignored
                    }

                    return inlog;
                }
                else
                {
                    System.Threading.Thread.Sleep(1500);
                    Console.Clear();
                    login();
                }
                
            }
            return "1go2to3main4menu5";
        }
        public static SecureString maskInputString()
        {
            SecureString pass = new SecureString();
            ConsoleKeyInfo keyInfo;
            bool unlocked = false;

            do
            {
                keyInfo = Console.ReadKey(true);
                if ((keyInfo.Key == ConsoleKey.Enter && pass.Length > 1) || keyInfo.Key == ConsoleKey.Escape) { unlocked = true; }

                if (!char.IsControl(keyInfo.KeyChar))
                {
                    pass.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    pass.RemoveAt(pass.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (!unlocked);
            {
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    return pass;
                }
                else
                {
                    SecureString pass2 = new SecureString();
                    List<char> charlist = new List<char>() { '1', 'g', 'o', '2', 't', 'o', '3', 'm', 'a', 'i', 'n', '4', 'm', 'e', 'n', 'u', '5' };
                    for (int i = 0; i < charlist.Count(); i++) { pass2.AppendChar(charlist[i]); }
                    return pass2;
                }
            }
        }
        public static void check()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("E-mail of wachtwoord onjuist!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}