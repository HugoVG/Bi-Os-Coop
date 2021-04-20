using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                CPeople.Person persoon = accounts.peopleList.Single(henk => henk.email == username && henk.password == password);
                return persoon;
            }
            catch (Exception)
            {
                try
                {
                    CPeople.Admin admin = accounts.adminList.Single(henk => henk.email == username && henk.password == password);
                    return admin;
                }
                catch (Exception)
                {
                    try
                    {
                        CPeople.Employee employee = accounts.employeeList.Single(henk => henk.email == username && henk.password == password);
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
        public static dynamic login()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("E-mail:");
            string username = Console.ReadLine();
            Console.WriteLine("Wachtwoord:");
            string password = Console.ReadLine();

            var inlog = loginscherm.mailwachtvragen(username, password);

            return inlog;
        }
        public static void check()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("E-mail of wachtwoord onjuist!");
            Console.ForegroundColor = ConsoleColor.Gray;
            login();

        }
    }
}