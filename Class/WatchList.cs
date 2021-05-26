using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace Bi_Os_Coop.Class
{
    public class WatchLists
    {
        public List<WatchList> allWatchLists { get; set; }
        
        public void AddList()
        {
            
            
            Console.WriteLine("Welke film toevoegen aan je lijst?");
            string naam = Console.ReadLine();
            
        }

        public void AddList(string name)
        {
            MainMenuThings mmt = JsonSerializer.Deserialize<MainMenuThings>(Json.ReadJson(Json.MainMenu)); //mmt.user; // is the current user
            MovieInterpreter filmbijnaam = Films.FromJson().movieList.Single(movie => name != null && movie.name.ToLower() == name.ToLower());
            if (mmt != null)
            {
                WatchList wl = new WatchList() {UserID = mmt.user.id, filmID = filmbijnaam.movieid};
                AddList(wl);
            }
        }
        public void AddList(WatchList wl)
        {
            if (allWatchLists == null)
            {
                List<WatchList> nlist = new List<WatchList> { wl };
                allWatchLists = nlist;
            }
            else
            {
                allWatchLists.Add(wl);
            }
        }
        /// <summary>
        /// If write is true it will write object to json already
        /// </summary>
        /// <param name="write"></param>
        /// <returns>null if write is true else it will return the json string</returns>
        public string ToJson(bool write = false)
        {
            if (write)
            {
                JsonSerializerOptions opt = new JsonSerializerOptions() { WriteIndented = true };
                string json = JsonSerializer.Serialize(this, opt);
                Json.WriteJson(Json.WatchList, json);
                return null;
            }
            else
            {
                JsonSerializerOptions opt = new JsonSerializerOptions() { WriteIndented = true };
                return JsonSerializer.Serialize(this, opt);
            }
        }

        public static WatchLists FromJson(string json) => JsonSerializer.Deserialize<WatchLists>(json);
        public static WatchLists FromJson()
        {
            string json = Json.ReadJson(Json.WatchList);
            WatchLists temp = JsonSerializer.Deserialize<WatchLists>(json);
            return temp;
        }
    }
    public class WatchList
    {
        public int UserID { get; set; }
        public int filmID { get; set; }
    }
}