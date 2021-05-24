using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    public class MovieInterpreter
    {
        public int movieid { get; set; }
        public string name { get; set; }
        public string releasedate { get; set; }
        public List<string> genres { get; set; }
        public int leeftijd { get; set; }
        public double beoordeling { get; set; }
        public List<string> acteurs { get; set; }
        public string taal { get; set; }
        public string beschrijving { get; set; }
        public int MovieTime { get; set; }
        public void setFilm(int movieid, string name, string releasedate, List<string> genres, int leeftijd, double beoordeling, List<string> acteurs, int movieTime, string taal = null, string beschrijving = null)
        {
            this.movieid = movieid;
            this.name = name;
            this.releasedate = releasedate;
            this.genres = genres.ToList();
            this.leeftijd = leeftijd;
            this.beoordeling = beoordeling;
            this.acteurs = acteurs.ToList();
            this.taal = taal;
            this.beschrijving = beschrijving;
            this.MovieTime = movieTime;
        }
    }
    public class Films
    {
        public List<MovieInterpreter> movieList { get; set; }
        public void addFilm(MovieInterpreter newMovie)
        {
            if (movieList == null)
            {
                List<MovieInterpreter> newMovieList = new List<MovieInterpreter>();
                newMovieList.Add(newMovie);
                movieList = newMovieList;
            }
            else
            {
                movieList.Add(newMovie);
            }
        }
    }
}
