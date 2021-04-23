using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Bi_Os_Coop
{
    class MovieMenu
    {
        public static void mainPagina(string sort = "name", int index = 1)
        {
            string json = Json.ReadJson("Films");
            Films jsonFilms = JsonSerializer.Deserialize<Films>(json);
            int highestpage;
            if ((jsonFilms.movieList.Count() - 1) % 10 > 0)
            {
                highestpage = (jsonFilms.movieList.Count() - 1) / 10 + 1;
            }
            else
            {
                highestpage = (jsonFilms.movieList.Count() - 1) / 10;
            }
            Console.Clear();
            MainMenu.Logo();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Film Menu\n");
            Console.WriteLine("Type S om een film te zoeken");
            Console.WriteLine("Of type '0' om terug te gaan naar de main menu");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Beschikbare films:\n");
            if (sort == "name")
            {
                MainMenu.printlist(MainMenu.sortbyname(), index);
            }
            else if (sort == "release")
            {
                MainMenu.printlist(MainMenu.sortbyrelease(), index);
            };
            Console.WriteLine($"\t\t\t\t\t\t\t\t\t\tBladzijde {index} van {highestpage}");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Kies de pagina waar u naar toe wilt scrollen: ");
            Console.ForegroundColor = ConsoleColor.Gray;
            string indexstring = Console.ReadLine();
            if(indexstring == "s" || indexstring == "S") 
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Type hier de film die u wilt zoeken: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                string movsearch = Console.ReadLine();
                MovieMenu.search(movsearch);
            }
            else if(indexstring == "0")
            {
                Console.Clear();
                //temp set to false, login will be linked later
                MainMenu.MainMenuShow(false);
            }
            else
            {
                try
                {
                    index = int.Parse(indexstring);
                    if (index > highestpage)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Deze bladzijde bestaat niet!");
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Thread.Sleep(1500);
                        index = highestpage;
                    }
                }
                catch (Exception)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invoer onjuist, probeer opnieuw!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(1000);
                }
            }
            MovieMenu.mainPagina(sort, index);
        }
        public static void search(string searchmov)
        {
            Console.WriteLine("This movie does not exist!");
            Console.ReadLine();
            Console.Clear();
            MovieMenu.mainPagina();
        }
    }
}
