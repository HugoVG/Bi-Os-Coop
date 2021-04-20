using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Bi_Os_Coop
{
    public class ZAALTESTERNIETGEBRUIKEN
    {
        public void Test()
        {
            Zalen testzaal = new Zalen();
            Zaal tijdelijkeZaal = new Zaal();
            tijdelijkeZaal.setZaal(10, "30-01-2021", "13:00", 100, "Miauwer");
            tijdelijkeZaal.showStool();
            CPeople.Person Henk = new CPeople.Person();
            Henk.setPerson(69, "Henk", "Henkerino@HahaHenk.com", "0nlyWams", "30-01-2021", "06111111");
            tijdelijkeZaal.occupyStool(10, Henk);
            Console.ReadKey();
            testzaal.AddZaal(tijdelijkeZaal);
            testzaal.writeZalen();
            string json = testzaal.ToJson();
            Json.WriteJson("Zalen", json);
            Console.ReadKey();
            Zalen testzaal2 = new Zalen();
            string json2 = Json.ReadJson("Zalen");
            testzaal2 = testzaal2.FromJson(json2);
            int[] gfdjhfskd = new int[] { 31, 32, 33, 34 };
            testzaal2.Reserveseats(gfdjhfskd, Henk, "30-01-2021", "13:00");
            foreach(Zaal zaal in testzaal2.zalenList)
            {
                zaal.showStool();
            }
            Console.ReadKey();
            json2 = testzaal2.ToJson();
            Json.WriteJson("Zalen", json2);
            testzaal2 = testzaal2.FromJson(json2);
            foreach (Zaal zaal in testzaal2.zalenList)
            {
                zaal.showStool();
            }

        }
    }
    class Zalen
    {
        public List<Zaal> zalenList { get; set; }
        public void AddZaal(Zaal zaal)
        {
            if (zalenList == null)
            {
                List<Zaal> newZaal = new List<Zaal>();
                newZaal.Add(zaal);
                zalenList = newZaal;
            }
            else
            {
                zalenList.Add(zaal);
            }
        }
        public void writeZalen()
        {
            foreach (Zaal zaal in this.zalenList)
            {
                Console.Write($"date:{zaal.date} \t");
                Console.Write($"time:{zaal.time} \t");
                Console.Write($"movie:{zaal.film} \n");
            }
        }

        public string ToJson()
        {
            JsonSerializerOptions opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }

        public Zalen FromJson(string json)
        {
            return JsonSerializer.Deserialize<Zalen>(json);
        }

        public void Reserveseats(int[] indexes, CPeople.Person orderer, string date, string time)
        {
            Zaal gekozenzaal = zalenList.Single(movie => movie.date == date && movie.time == time);
            foreach(int index in indexes)
            {
                gekozenzaal.occupyStool(index, orderer);
            }
        }

        public void Reserveseats(int index, CPeople.Person orderer, string date, string time)
        {
            Zaal gekozenzaal = zalenList.Single(movie => movie.date == date && movie.time == time);
            gekozenzaal.occupyStool(index, orderer);
        }

    }
    class Zaal
    {
        public int stoelWidth { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public List<Stoel> stoelen { get; set; }
        public string film { get; set; } //Film film {get; set;}
        public void setZaal(int stoelwidth, string date, string time, int totalStools, string film)
        {
            this.stoelWidth = stoelwidth;
            this.date = date;
            this.time = time;
            this.film = film;
            List<Stoel> nstoelen = new List<Stoel>();
            for (int i = 0; i < totalStools; i++)
            {
                Stoel stoel = new Stoel();
                nstoelen.Add(stoel);
                stoelen = nstoelen;
            }
        }
        public void occupyStool(int index, CPeople.Person orderer)
        {
            //orderer is the 'Owner' of that chair
            index -= 1; // we have to count the seats from 1 but in code from 0 so if someone wants to order Seat 1 in code it will be seat 0

            /* 20 seats Rows of seats are divided by one or more aisles so that there are seldom more than 20 seats in a row.
             * This allows easier access to seating, as the space between rows is very narrow.
             * Depending on the angle of rake of the seats, the aisles have steps.
             * 1 2 3 4 5 6 7 8 9 10;
             * 11 12 13 14 15 16 17 18 19 20;
             * I'm going to prompt it the numbers 001, 002, 003, 004, 005, 006, 007, 008, 009, 010... in return
             */
            if (stoelen.ElementAt(index).isOccupied)
            {
                Console.WriteLine("this stool is already reserved");
                return;
            }
            stoelen.ElementAt(index).isOccupied = true;
            stoelen.ElementAt(index).isOccupiedBy = orderer.id;

        }
        /// <summary>
        /// DIT IS CONOLSE
        /// </summary>
        public void showStool()
        {
            Console.Clear();
            int counter = 0;
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Stoel stoel in this.stoelen)
            {

                if (counter % stoelWidth == 0)
                {
                    if (counter < 10) { }
                    else { Console.WriteLine("\n"); }
                }

                if (stoelen.ElementAt(counter).isOccupied)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                if (counter <= 8)
                {
                    Console.Write($"00{counter+1}");
                }
                else if (counter <= 98)
                {
                    Console.Write($"0{counter + 1}");
                }
                else
                {
                    Console.Write($"{counter + 1}");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" ");
                counter += 1;
            }
        }
    }

    class Stoel
    {
        public bool isOccupied { get; set; } = false;
        public int isOccupiedBy { get; set; } = 1;//dan kan je het ID van de persoon uit lezen
        public Double Price { get; set; } = 10.0;
        public void setStoolDefault()
        {
            this.isOccupied = false;
            this.isOccupiedBy = 1;
            this.Price = 10.0;
        }
    }
}
