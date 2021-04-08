using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Bi_Os_Coop
{
    class Filmaddtojsonfile
    {
        public static void test()
        {
            ////1e keer films toevoegen
            //MovieInterpreter Movie = new MovieInterpreter();
            //string[] genres = new string[4] { "Romance", "Supernatural", "School", "Drama" };
            //string[] Acteurs = new string[2] { "Mone Kamishiraishi", "Ryunosuke Kamiki" };
            ////Ik zou voor genres en acteurs het omwisselen naar lists, aangezien je perse bij een array moet aangeven hoeveel items er in een array moeten zitten.
            //Movie.setFilm(1, "Kimi no Na wa.", "2016", genres, 13, 8.95, Acteurs, "https://www.youtube.com/watch?v=3KR8_igDs1Y");
            //// voor movieid had ik in gedachten om voor het toevoegen zoiets als dit te doen. int listlength = lengte list; movieid = listlength+1;
            //Films MovieLibrary = new Films();
            //MovieLibrary.addFilm(Movie);
            //string json = JsonSerializer.Serialize(MovieLibrary);

            //File.WriteAllText("../../../json1.json", json);

            ////wanneer er al dingen in de json staan
            //Films Filmslist = JsonSerializer.Deserialize<Films>(json);
            //MovieInterpreter newMovie = new MovieInterpreter();
            //newMovie.setFilm(2, "Kimi no Na wa.", "2016", genres, 13, 8.95, Acteurs, "https://www.youtube.com/watch?v=3KR8_igDs1Y");
            //Filmslist.addFilm(newMovie);

            //json = JsonSerializer.Serialize(Filmslist);

            //File.WriteAllText("../../../json1.json", json);
        }
    }
}
