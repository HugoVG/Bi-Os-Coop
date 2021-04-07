using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Bi_Os_Coop
{
    class admin
    {
        public int aantalZalen { get; set; }
        public string[] HomeAlone { get; set; }
        public string[] BlackWidow { get; set; }
    }
    
    public class adminMenu
    {
        public static void AM()
        {
            string path = "../../Json/admin.json";
            string tekstUitJson = System.IO.File.ReadAllText(path);
            admin objectTest = new admin();
            objectTest = JsonSerializer.Deserialize<admin>(tekstUitJson);

            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };
            tekstUitJson = JsonSerializer.Serialize(objectTest, options);
            System.IO.File.WriteAllText(path, tekstUitJson);

            
            // deze twee zijn er alleen zodat de rest niet 20483 errors geeft 
            Tuple<string, string, string>[] filmsArray = new Tuple<string, string, string>[15];
            int aantalFilms = 0;

            // while loop die hoofdPagina loopt tot je "0" in tikt
            //while (true)
            //{
                string keuze = hoofdPagina();
                if (keuze == "0")
                {
                    Environment.Exit(0);
                }
                else if (keuze == "1")
                {
                    Console.Clear();
                    Console.WriteLine("Hoeveel zalen wilt u?");
                    objectTest.aantalZalen = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine(objectTest.aantalZalen);
                }
                else if (keuze == "2")
                {
                    filmsArray[aantalFilms] = FilmsToevoegen();
                    aantalFilms += 1;
                }
                
                else if (keuze == "3")
                {
                    Console.WriteLine(FilmsAanpassen());
                }
                else
                {
                    hoofdPagina();
                }
                /* dit werkt nog niet helemaal met de while loop dus heb het ff in een comment gezet zodat ik het kon commiten 
                if (keuze == "4")
                {
                    Console.Clear();
                    Console.WriteLine(filmsArray);
                }
                else if (keuze == "5")
                {
                    Console.Clear();
                    Console.WriteLine(zalen);
                }
                */
            //}
        }

        public static string hoofdPagina()
        {
            Console.Clear();
            Console.WriteLine("Admin Menu\n");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1) Aantal zalen selecteren");
            Console.WriteLine("2) Film toevoegen");
            Console.WriteLine("3) Film aanpassen");
            //Console.WriteLine("4) Alle Films");
            //Console.WriteLine("5) Aantal Zalen");
            Console.WriteLine("Of type '0' om te stoppen");
            Console.Write("\nKies een pagina: ");
            string keuze = Console.ReadLine();
            return keuze;
        }

        //public static string AantalZalen()
        //{
        //    Console.Clear();
        //    Console.WriteLine("Hoeveel zalen wilt u?");
        //    string zalen = Console.ReadLine();
        //    return zalen;
        //}

        public static Tuple<string, string, string> FilmsToevoegen()
        {
            Console.Clear();
            Console.WriteLine("Welke film wilt u toevoegen?");
            string film = Console.ReadLine();
            Console.WriteLine("In welk jaar kwam de film uit?");
            string jaar = Console.ReadLine();
            Console.WriteLine("Tot welke genre behoort deze film?");
            string genre = Console.ReadLine();
            return Tuple.Create(film, jaar, genre);
        }

        public static Tuple<string, string, string> FilmsAanpassen()
        {
            Console.Clear();
            Console.WriteLine("Welke film wilt u aanpassen?");
            string film = Console.ReadLine();
            Console.WriteLine("In welk jaar kwam de film uit?");
            string jaar = Console.ReadLine();
            Console.WriteLine("Tot welke genre behoort deze film?");
            string genre = Console.ReadLine();
            return Tuple.Create(film, jaar, genre);
        }
    }
}
