using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    internal class Zalen
    {
        public List<Zaal> zalenList { get; set; }

        public void menu(List<Zaal> selected)
        {
            writeZalen(selected);
            if (selected.Count != 0)
            {
                return;
            }
            Console.WriteLine("\nselect the number of the timeframe and date you want to order");
            string temp = Console.ReadLine();
            try
            {
                int index = Convert.ToInt32(temp);
                Zaal choosenone = selected.ElementAt(index-1);
                choosenone.showStool();
                Console.WriteLine("\nSelect the seats you want to reserve add an ',' between the stools ");
                string henk = Console.ReadLine();
                if (henk != null)
                {
                    henk = henk.Trim();
                    string[] henkerino = henk.Split(',');
                    List<int> allIndexes = new List<int>();
                    foreach (string i in henkerino)
                    {
                        if (Convert.ToInt32(i) == 0)
                        {
                            Console.WriteLine("input was not a number");
                        }

                        allIndexes.Add(Convert.ToInt32(i) - 1);
                    }

                    MainMenuThings mmt = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson("MainMenu"));
                    int[] indexes = allIndexes.ToArray();
                    if (mmt != null) choosenone.occupyStool(indexes, mmt.user);
                }
                else
                {
                    return;
                }

                string json1 = this.ToJson();
                Json.WriteJson("Zalen", json1);
            }
            catch (Exception ex) { Console.WriteLine("Invalid Number"); menu(selected); }


        }


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
            int counter = 1;
            foreach (Zaal zaal in this.zalenList)
            {
                Console.Write($"\n{counter} \t");
                Console.Write($"date:{zaal.date} \t");
                Console.Write($"time:{zaal.time} \t");
                Console.Write($"movie:{zaal.film.name} \t");
                Console.Write($"release Date:{zaal.film.releasedate} \t");
                Console.Write($"Score:{zaal.film.beoordeling}\t");
                if (zaal.stoelen.Count == 630)
                {
                    Console.Write("Zaal: 1\t");
                }
                else if (zaal.stoelen.Count == 342)
                {
                    Console.Write("Zaal: 2\t");
                }
                else
                {
                    Console.Write("Zaal: 3\t");
                }
                counter++;
            }
        }
        public void writeZalen(List<Zaal> selected)
        {
            int counter = 1;
            //List<Zaal> showingzaal = this.zalenList;
            foreach (Zaal zaal in selected)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"\n{counter} \t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"date:{zaal.date} \t");
                Console.Write($"time:{zaal.time} \t");
                Console.Write($"movie:{zaal.film.name} \t");
                Console.Write($"release Date:{zaal.film.releasedate} \t");
                Console.Write($"Score:{zaal.film.beoordeling}\t");
                if (zaal.stoelen.Count == 630)
                {
                    Console.Write("Zaal: 1\t");
                }
                else if (zaal.stoelen.Count == 342)
                {
                    Console.Write("Zaal: 2\t");
                }
                else
                {
                    Console.Write("Zaal: 3\t");
                }
                counter++;
            }
        }

        public string ToJson()
        {
            JsonSerializerOptions opt = new JsonSerializerOptions() { WriteIndented = true };
            return JsonSerializer.Serialize(this, opt);
        }
        public static Zalen FromJson()
        {
            string json = Json.ReadJson(Json.Zalen);
            Zalen temp = JsonSerializer.Deserialize<Zalen>(json);
            return temp;
        }
        public static Zalen FromJson(string json) => JsonSerializer.Deserialize<Zalen>(json);
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
        public void Reserveseats(int index, CPeople.Person orderer, Zaal zaal)
        {
            int[] indexs = new int[1];
            indexs[0] = index;
            zaal.occupyStool(indexs, orderer);
        }
        
        public void Reserveseats(int[] index, CPeople.Person orderer, Zaal zaal)
        {
            zaal.occupyStool(index, orderer);
        }
        /// <summary>
        /// returned tuple met True en de zalen waarbij de film draait met x naam
        /// </summary>
        /// <param name="naam"></param>
        /// <returns></returns>
        public Tuple<bool, List<Zaal>> selectZalen(string naam)
        {
            IEnumerable<Zaal> selectedzalen = zalenList.Where(movie => movie.film.name.ToLower() == naam.ToLower() && DateTime.Parse(movie.date) >= DateTime.Today); //fixt ook de out dated films
            if (selectedzalen.Count() != 0)
                return Tuple.Create(true, selectedzalen.ToList());
            else
            {
                Console.WriteLine($"Couldn't find any movie with name {naam}");
                return Tuple.Create(false, selectedzalen.ToList());
            }
        }
    }
    internal class Zaal
    {
        public enum Size // https://imgur.com/gallery/FmTnf7e Size chart
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
            // De +130 // +42 // +18 is voor
            // de lege stoelen in de excel sheet
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

        public void writeZalen(List<Zaal> selected)
        {
            //List<Zaal> showingzaal = this.zalenList;
            foreach (Zaal zaal in selected)
            {
                Console.Write($"\ndate:{zaal.date} \t");
                Console.Write($"time:{zaal.time} \t");
                Console.Write($"movie:{zaal.film.name} \t");
                Console.Write($"release Date:{zaal.film.releasedate} \t");
                Console.Write($"Score:{zaal.film.beoordeling} ");
            }
        }

        public void applyzaal()
        {
        }

        /// <summary>
        /// Occupies a stool
        /// </summary>
        /// <param name="indexs"></param>
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
                    if (stoelen.ElementAt(index).isOccupied || stoelen.ElementAt(index).Price == Stoel.price.NONE)
                    {
                        Console.WriteLine($"{index + 1} stool is already Occupied");
                        return -1;
                    }
                }
                foreach (int index in indexs)
                {
                    stoelen.ElementAt(index).isOccupied = true;
                    //Console.WriteLine($"{index} is now occupied by {orderer.id}");
                    stoelen.ElementAt(index).isOccupiedBy = orderer.id;
                }
                return 1;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    Console.WriteLine("\n\none of the chairs is not valid"); // Textbox.Hint
                    return 0;
                }
                else throw;
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
            int stoelenPerRij;
            if (this.stoelen.Count == 630) { stoelenPerRij = 30; }
            else if (this.stoelen.Count == 342){ stoelenPerRij = 18; }
            else if (this.stoelen.Count == 168) { stoelenPerRij = 12; }
            else { throw new IdiotException(); }
            foreach (Stoel stoel in this.stoelen)
            {
                if (counter % stoelenPerRij == 0)
                {
                    if (counter < 10) { }
                    else { Console.WriteLine(""); }
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
        public enum price
        {
            HIGH = 20,
            MEDIUM = 15,
            LOW = 10,
            NONE = 0
        }
        public bool isOccupied { get; set; }
        public int isOccupiedBy { get; set; } = 1; //dan kan je het ID van de persoon uit lezen
        public price Price { get; set; }

        public void setStoolDefault(price prijs = price.LOW)
        {
            if (prijs == price.NONE)
            {
                this.isOccupied = true;
                this.isOccupiedBy = 1;
                this.Price = prijs;
            }
            else
            {
                this.isOccupied = false;
                this.Price = prijs;
            }
            
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