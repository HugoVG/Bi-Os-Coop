﻿using System;
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
                List<Zaal> newZaal = new List<Zaal>{zaal};
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
        /// <summary>
        /// Occupies a stool
        /// </summary>
        /// <param name="index"></param>
        /// <param name="orderer"></param>
        public int occupyStool(int[] indexs, CPeople.Person orderer)
        {
            try
            {
                foreach (int index in indexs)
                {
                    if (stoelen.ElementAt(index).isOccupied)
                    {
                        Console.WriteLine($"{index} stool is already ");
                        return -1;
                    }
                    stoelen.ElementAt(index).isOccupied = true;
                    stoelen.ElementAt(index).isOccupiedBy = orderer.id;
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
