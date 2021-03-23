using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    class loginscherm
    {
        public static bool mailwachtvragen(string username, string password)
        {
            string account = Json.ReadJson("Accounts");
            CPeople.People accounts = new CPeople.People();
            accounts = accounts.FromJson(account);
            try
            {
                CPeople.Person persoon = accounts.peopleList.Single(henk => henk.email == username && henk.password == password);
                return true;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }
        public static void login()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("E-mail:");
            string username = Console.ReadLine();
            Console.WriteLine("Wachtwoord:");
            string password = Console.ReadLine();
            loginscherm.check(username, password);
        }
        public static void check(string username, string password)
        {

            if (mailwachtvragen(username, password))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Succesvol ingelogd!");
                System.Threading.Thread.Sleep(1000);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Clear();
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("E-mail of wachtwoord onjuist!");
                Console.ForegroundColor = ConsoleColor.Gray;
                loginscherm.login();
            }
        }
    }
}
   