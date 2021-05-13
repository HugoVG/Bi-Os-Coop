using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;

namespace Bi_Os_Coop
{
    class loginscherm
    {
        /// <summary>
        /// ik gebruik Dynamic hier want we willen admin//employee//User
        /// </summary>
        public static dynamic mailwachtvragen(string username, string password)
        {
            string account = Json.ReadJson("Accounts");
            CPeople.People accounts = new CPeople.People();
            accounts = accounts.FromJson(account);
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
        public static string newwayoftyping()
        {
            string newstring = "";
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
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
            while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);
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
        public static dynamic login()
        {
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
                    catch { }
                    return inlog;
                }
            }
            return "1go2to3main4menu5";
        }
        public static SecureString maskInputString()
        {
            SecureString pass = new SecureString();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
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
            while (keyInfo.Key != ConsoleKey.Enter && keyInfo.Key != ConsoleKey.Escape);
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