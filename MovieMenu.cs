using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Bi_Os_Coop
{
    class MovieMenu
    {
        public static void mainPagina(string sort = "name", int index = 1)
        {
            Console.Clear();
            MainMenu.Logo();
            Console.WriteLine("Film Menu\n");
            Console.WriteLine("Type S om een film te zoeken");
            Console.WriteLine("Beschikbare films:\n");
            if (sort == "name")
            {
                MainMenu.printlist(MainMenu.sortbyname(), index);
            }
            else if (sort == "release")
            {
                MovieMenu.printlist(MainMenu.sortbyrelease(), index);
            };
            Console.WriteLine("\nOf type '0' om terug te gaan naar de main menu");
            Console.Write("Kies de pagina waar u naar toe wilt scrollen: ");
            string indexstring = Console.ReadLine();
            if(indexstring == "s" || indexstring == "S") 
            {
                Console.WriteLine("Type hier de film die u wilt zoeken:");
                string movsearch = Console.ReadLine();
                MovieMenu.search(movsearch);
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
