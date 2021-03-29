using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    class MainMenu
    {
        public static void Logo()
        {
            Console.ForegroundColor = ConsoleColor.White;
            int origWidth = Console.WindowWidth;
            string dashes = "";
            for (int i = 1; i < origWidth; i++) { dashes += "-"; }
            Console.Write(dashes + "\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("oooooooooo  o88");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("              ooooooo");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("                          oooooooo8\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" 888    888 oooo");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("           o888   888o  oooooooo8");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("           o888     88   ooooooo     ooooooo  ooooooooo\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" 888oooo88   888");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ooooooooo");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(" 888     888 888ooooooo");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(" ooooooooo");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(" 888         888     888 888     888 888    888\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" 888    888  888");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("           888o   o888         888");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("          888o     oo 888     888 888     888 888    888\n");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("o888ooo888  o888o");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("            88ooo88   88oooooo88");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("            888oooo88    88ooo88     88ooo88   888ooo88\n                                                                                               o888\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(dashes + "\n\n");
        }
        public static void MainMenuShow(string language = "Nederlands")
        {
            Logo();
            if (language == "Nederlands")
            {
                string loginstructions = "Druk op 'I' om in te loggen";
                int stringlength = loginstructions.Length;
                int origWidth = Console.WindowWidth - stringlength;
                string spaces = "";
                for (int j = 1; j < origWidth; j++) { spaces += " "; }
                Console.WriteLine(spaces + loginstructions);
                Console.WriteLine("ACTUELE FILMS:");
                var Movies = new List<string>()
                {
                    "Bon Bini: Judeska in da House (2020)",
                    "Monster Hunter (2020)",
                    "Honest Thief (2020)",
                    "Roald Dahl's The Witches (2020)",
                    "Cats & Dogs: Paws Unite (2020)",
                    "All My Life (2020)",
                    "Kom Hier Dat Ik U Kus (2020)",
                    "Ammonite (2020)",
                    "Freaky (2020)",
                    "The Comeback Trail (2020)"
                };
                int i = 1;
                Movies.ForEach(num => Console.WriteLine(i++ + ". " + num));
                if (Console.ReadKey(true).Key == ConsoleKey.I)
                {
                    Console.Clear();
                    loginscherm.login();
                };
            }
        }
    }
}
