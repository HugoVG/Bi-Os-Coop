using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop
{
    public class ZAALTESTERNIETGEBRUIKEN
    {
        public void Test()
        {
            Zalen testzaal = new Zalen();
            Zaal tijdelijkeZaal = new Zaal();
            //tijdelijkeZaal.setZaal(10, "30-01-2021", "13:00", 100);
            tijdelijkeZaal.showStool();
            CPeople.Person Henk = new CPeople.Person();
            Henk.setPerson(69, "Henk", "Henkerino@HahaHenk.com", "0nlyWams", "30-01-2021", "06111111");
            //tijdelijkeZaal.Reser(-5, Henk);
            Console.ReadKey();
            testzaal.AddZaal(tijdelijkeZaal);
            testzaal.writeZalen();
            string json = testzaal.ToJson();
            Json.WriteJson("Zalen", json);
            Console.ReadKey();
            Zalen testzaal2 = new Zalen();
            string json2 = Json.ReadJson("Zalen");
            testzaal2 = testzaal2.FromJson(json2);
            //int[] gfdjhfskd = new int[] { 31, 32, 33, 34 };
            int gfdjhfskd = 30;
            testzaal2.Reserveseats(gfdjhfskd, Henk, "30-01-2021", "13:00");
            foreach (Zaal zaal in testzaal2.zalenList)
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

    internal class Zalen
    {
        public List<Zaal> zalenList { get; set; }

        public void AddZaal(Zaal zaal)
        {
            if (zalenList == null)
            {
                List<Zaal> newZaal = new List<Zaal> { zaal };
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
                Console.Write($"\ndate:{zaal.date} \t");
                Console.Write($"time:{zaal.time} \t");
                Console.Write($"movie:{zaal.film.name} \n");
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
            gekozenzaal.occupyStool(indexes, orderer);
        }

        public void Reserveseats(int index, CPeople.Person orderer, string date, string time)
        {
            Zaal gekozenzaal = zalenList.Single(movie => movie.date == date && movie.time == time);
            int[] indexs = new int[1];
            indexs[0] = index;
            gekozenzaal.occupyStool(indexs, orderer);
        }
    }

    internal class Zaal
    {
        public enum Size : int // https://imgur.com/gallery/FmTnf7e Size chart
        {
            MegaChonker = 500,
            heftyChonk = 300,
            chonk = 150
        }

        private int stoelWidth { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public MovieInterpreter film { get; set; } //Film film {get; set;}
        public List<Stoel> stoelen { get; set; }

        public void setZaal(string date, string time, int totalStools, MovieInterpreter film = null)
        {
            this.date = date;
            this.time = time;
            this.film = film;
            List<Stoel> nstoelen = new List<Stoel>();
            /*
            for (int i = 0; i < totalStools; i++)
            {
                Stoel stoel = new Stoel();
                nstoelen.Add(stoel);
                stoelen = nstoelen;
            }*/
            if (totalStools == (int)Size.MegaChonker)
            {
                totalStools = totalStools +130;
                this.stoelWidth = 30;
                for (int i = 0; i < totalStools; i++)
                {
                    Stoel stoel = new Stoel();
                    stoel.setStoolDefault(stoel.stoolworth(i, totalStools));
                    nstoelen.Add(stoel);
                }
                stoelen = nstoelen;
            }
            else if (totalStools == (int)Size.heftyChonk)
            {
                totalStools = totalStools + 42;
                this.stoelWidth = 18;
                for (int i = 0; i < totalStools; i++)
                {
                    Stoel stoel = new Stoel();
                    stoel.setStoolDefault(stoel.stoolworth(i, totalStools));
                    nstoelen.Add(stoel);
                }
                stoelen = nstoelen;
            }
            else if (totalStools == (int)Size.chonk)
            {
                totalStools = totalStools + 18;
                this.stoelWidth = 12;
                for (int i = 0; i < totalStools; i++)
                {
                    Stoel stoel = new Stoel();
                    stoel.setStoolDefault(stoel.stoolworth(i, totalStools));
                    nstoelen.Add(stoel);
                }
                stoelen = nstoelen;
            }
            //showStool();
        }

        public void applyzaal()
        {
        }

        /// <summary>
        /// Occupies a stool
        /// </summary>
        /// <param name="index"></param>
        /// <param name="orderer"></param>
        public int occupyStool(int[] indexs, CPeople.Person orderer)
        {
            //orderer is the 'Owner' of that chair
            //index -= 1; // we have to count the seats from 1 but in code from 0 so if someone wants to order Seat 1 in code it will be seat 0

            /* 20 seats Rows of seats are divided by one or more aisles so that there are seldom more than 20 seats in a row.
             * This allows easier access to seating, as the space between rows is very narrow.
             * Depending on the angle of rake of the seats, the aisles have steps.
             * 1 2 3 4 5 6 7 8 9 10;
             * 11 12 13 14 15 16 17 18 19 20;
             * I'm going to prompt it the numbers 001, 002, 003, 004, 005, 006, 007, 008, 009, 010... in return
             */
            try
            {
                foreach (int index in indexs)
                {
                    if (stoelen.ElementAt(index).isOccupied)
                    {
                        Console.WriteLine($"{index - 1} stool is already ");
                        return -1;
                    }
                    stoelen.ElementAt(index - 1).isOccupied = true;
                    stoelen.ElementAt(index - 1).isOccupiedBy = orderer.id;
                }
                return 1;
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"\n\none of the chairs is not valid"); // Textbox.Hint
                return 0;
            }
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
                    if (stoel.Price == Stoel.price.NONE)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                    }
                    else if (stoel.Price == Stoel.price.LOW)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    else if (stoel.Price == Stoel.price.MEDIUM)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                    }
                    else if (stoel.Price == Stoel.price.HIGH)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkYellow;
                    }
                }
                if (counter <= 8)
                {
                    Console.Write($"00{counter + 1}");
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

    internal class Stoel
    {
        public enum price : int
        {
            HIGH = 20,
            MEDIUM = 15,
            LOW = 10,
            NONE = 0
        }
        public bool isOccupied { get; set; } = false;
        public int isOccupiedBy { get; set; } = 1; //dan kan je het ID van de persoon uit lezen
        public price Price { get; set; }

        public void setStoolDefault(price prijs = price.LOW)
        {
            if (prijs == price.NONE)
            {
                this.isOccupied = true;
            }
            else
            {
                this.isOccupiedBy = 1;
            }
            this.isOccupied = false;
            this.Price = prijs;
        }

        public price stoolworth(int index, int size)
        {
            int[] stoelen500 = new int[] {
              //1 2 3 4 5 6 7 8 9 10 11  12  13  14  15  16  17  18  19  20  21  22  23  24  25  26  27  28  29  30
                0,0,0,0,1,1,1,1,1,1 ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,0  ,0  ,0  ,0, //20
                0,0,0,1,1,1,1,1,1,2 ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,0  ,0  ,0, //19
                0,0,0,1,1,1,1,1,2,2 ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,0  ,0  ,0, //18
                0,0,0,1,1,1,1,1,2,2 ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,0  ,0  ,0, //17
                0,0,0,1,1,1,1,2,2,2 ,2  ,2  ,2  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,0  ,0  ,0, //16
                0,0,1,1,1,1,1,2,2,2 ,2  ,2  ,3  ,3  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,0  ,0, //15
                0,1,1,1,1,1,2,2,2,2 ,2  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,0, //14
                1,1,1,1,1,1,2,2,2,2 ,2  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1, //13
                1,1,1,1,1,2,2,2,2,2 ,2  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1, //12
                1,1,1,1,1,2,2,2,2,2 ,2  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1, //11
                1,1,1,1,1,1,2,2,2,2 ,2  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1, //10
                1,1,1,1,1,1,1,2,2,2 ,2  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,1, //9
                0,1,1,1,1,1,1,1,2,2 ,2  ,2  ,2  ,3  ,3  ,3  ,3  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,0, //8
                0,0,1,1,1,1,1,1,2,2 ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,0  ,0, //7
                0,0,1,1,1,1,1,1,1,2 ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,0  ,0, //6
                0,0,0,1,1,1,1,1,1,1 ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,0  ,0  ,0, //5
                0,0,0,1,1,1,1,1,1,1 ,1  ,1  ,2  ,2  ,2  ,2  ,2  ,2  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,1  ,0  ,0  ,0, //4
                0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,
                0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,0,0,0,0,0,
                0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
            };
            int[] stoelen300 = new int[] {
                0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,
                0,1,1,1,1,1,2,2,2,2,2,2,1,1,1,1,1,0,
                0,1,1,1,1,2,2,2,2,2,2,2,2,1,1,1,1,0,
                0,1,1,1,1,2,2,2,2,2,2,2,2,1,1,1,1,0,
                0,1,1,1,2,2,2,2,2,2,2,2,2,2,1,1,1,0,
                0,1,1,1,2,2,2,2,3,3,2,2,2,2,1,1,1,0,
                1,1,1,2,2,2,2,3,3,3,3,2,2,2,2,1,1,1,
                1,1,1,2,2,2,3,3,3,3,3,3,2,2,2,1,1,1,
                1,1,2,2,2,2,3,3,3,3,3,3,2,2,2,2,1,1,
                1,1,2,2,2,2,3,3,3,3,3,3,2,2,2,2,1,1,
                1,1,2,2,2,2,3,3,3,3,3,3,2,2,2,2,1,1,
                0,1,1,2,2,2,2,3,3,3,3,2,2,2,2,1,1,0,
                0,1,1,1,2,2,2,2,3,3,2,2,2,2,1,1,1,0,
                0,1,1,1,1,2,2,2,2,2,2,2,2,1,1,1,1,0,
                0,0,1,1,1,1,2,2,2,2,2,2,1,1,1,1,0,0,
                0,0,1,1,1,1,2,2,2,2,2,2,1,1,1,1,0,0,
                0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,
                0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,
                0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,0,0,0

            };
            int[] stoelen150 = new int[]
            {
                0,0,1,1,1,1,1,1,1,1,0,0,
                0,1,1,1,1,1,1,1,1,1,1,0,
                0,1,1,1,1,1,1,1,1,1,1,0,
                1,1,1,1,1,2,2,1,1,1,1,1,
                1,1,1,1,2,2,2,2,1,1,1,1,
                1,1,1,2,2,3,3,2,2,1,1,1,
                1,1,1,2,2,3,3,2,2,1,1,1,
                1,1,1,2,2,3,3,2,2,1,1,1,
                1,1,1,2,2,3,3,2,2,1,1,1,
                1,1,1,1,2,2,2,2,1,1,1,1,
                1,1,1,1,1,2,2,1,1,1,1,1,
                0,1,1,1,1,1,1,1,1,1,1,0,
                0,0,1,1,1,1,1,1,1,1,0,0,
                0,0,1,1,1,1,1,1,1,1,0,0

            };
            int returner;
            if (size == 630)
            {
                returner = stoelen500[index];
                //Console.WriteLine(stoelen500.Length);
            }
            else if (size == 342)
            {
                returner = stoelen300[index];
                //Console.WriteLine(stoelen300.Length);
            }
            else if (size == 168)
            {
                returner = stoelen150[index];
                //Console.WriteLine(stoelen150.Length);
            }
            else
            {
                throw new IdiotException();
            }
            if (returner == 0) { return price.NONE; }
            else if (returner == 1) { return price.LOW; }
            else if (returner == 2) { return price.MEDIUM; }
            else if (returner == 3) { return price.HIGH; }
            //Console.ReadKey();
            return price.LOW; // incase of idiot exception ;-;
        }
    }
}