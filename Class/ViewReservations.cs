using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    class ViewReservations
    {
        /// <summary>
        /// returned een jagged list met in de list de eerste index de film en de rest de stoelen.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<List<int>> viewres(int id)
        {
            string json = Json.ReadJson("Zalen");
            Zalen jsonZalen = System.Text.Json.JsonSerializer.Deserialize<Zalen>(json);
            List<List<int>> reservationslist = new List<List<int>>();
            List<int> seatlist = new List<int>();
            int movieindex = 0;
            int seatindex = 0;

            for (int i = 0; i < jsonZalen.zalenList.Count(); i++)
            {
                seatlist = new List<int>();
                movieindex = i;
                seatlist.Add(movieindex);
                for (int j = 0; j < jsonZalen.zalenList[i].stoelen.Count(); j++)
                {
                    if(jsonZalen.zalenList[i].stoelen[j].isOccupiedBy == id)
                    {
                        seatindex = j + 1;
                        seatlist.Add(seatindex);
                    }
                }
                if (seatlist.Count() > 2)
                {
                    reservationslist.Add(seatlist);
                }
                else
                {
                    seatlist = new List<int>();
                }
            }
            //het eerste item in de list is de filmindex, de rest van de items zijn de stoelen
            return reservationslist;
        }
        /// <summary>
        /// laat de gereserveerde stoel(en) per film zien en de data van de film.
        /// call deze functie als je het scherm met reserveringen wilt laten zien.
        /// voer voor users bij id automatisch hun eigen id in vanuit de mainmenulist.
        /// </summary>
        /// <param name="id"></param>
        public static void showres(int id)
        {
            string json = Json.ReadJson("Zalen");
            Zalen jsonZalen = System.Text.Json.JsonSerializer.Deserialize<Zalen>(json);
            //call to the function that does all the work (viewres)
            List<List<int>> reservationlist = viewres(id);
            MainMenu.Logo();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Uw reserveringen: \n");
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < reservationlist.Count(); i++)
            {
                //prints all the movie information
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Titel: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(jsonZalen.zalenList[reservationlist[i][0]].film.name);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Datum: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(jsonZalen.zalenList[reservationlist[i][0]].date);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Tijd: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(jsonZalen.zalenList[reservationlist[i][0]].time);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Leeftijd: ");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(jsonZalen.zalenList[reservationlist[i][0]].film.leeftijd);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Stoelen: ");
                Console.ForegroundColor = ConsoleColor.Gray;

                //prints all the chairs
                for (int j = 1; j < reservationlist[i].Count(); j++)
                {
                    if (j != 1)
                    {
                        Console.Write($", ");
                    }
                    if (j % 20 == 0)
                    {
                        Console.Write("\n");
                    }
                    Console.Write($"{reservationlist[i][j]}");
                }
                Console.Write("\n\n");
            }
            //temp, make esc later to go back to main menu.
            Console.ReadKey();
        }
    }
}
