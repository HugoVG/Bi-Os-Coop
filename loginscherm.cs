using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    class loginscherm
    {
        public static void login()
        {
            ///vragen naar e-mail en wachtwoord
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine("E-mail:");
                string username = Console.ReadLine();
                Console.WriteLine("Wachtwoord:");
                string password = Console.ReadLine();

                ///check uitvoeren of wachtwoord juist is bij e-mail adress 
                if (username != "valid" || password != "valid")
                {
                    Console.Clear();
                    Console.WriteLine("E-mail of wachtwoord onjuist!", Console.ForegroundColor = ConsoleColor.Red);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Succesvol ingelogd!", Console.ForegroundColor = ConsoleColor.Green);
                    break;
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
