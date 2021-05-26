using System;

namespace Bi_Os_Coop.Class
{
    /// <summary>
    /// Deze class berekent hoeveel veranderingen er nodig zijn om een string gelijk te stellen aan een andere, des te lager de uitkomst des te meer het op de andere string lijkt.
    /// Wat er gereturned wordt is het aantal benodigde veranderingen, zet in je eigen code wat je wilt dat het maximale aantal verandereingen is.
    /// als je het algoritm wilt begrijpen: https://www.youtube.com/watch?v=Xxx0b7djCrs
    /// </summary>
    class LevenshteinDistance
    {
        //call de functie compute voor het berekenen.
        public static int Compute(string searchmov, string movielistitem)
        {
            if (string.IsNullOrEmpty(searchmov))
            {
                if (string.IsNullOrEmpty(movielistitem))
                    return 0;
                return movielistitem.Length;
            }

            if (string.IsNullOrEmpty(movielistitem))
            {
                if (movielistitem != null) return movielistitem.Length;
            }

            int searchmovlength = searchmov.Length;
            int movielistitemlength = movielistitem.Length;
            int[,] distance = new int[searchmovlength + 1, movielistitemlength + 1];

            for (int i = 0; i <= searchmovlength; distance[i, 0] = i++) ;
            for (int j = 1; j <= movielistitemlength; distance[0, j] = j++) ;

            for (int i = 1; i <= searchmovlength; i++)
            {
                for (int j = 1; j <= movielistitemlength; j++)
                {
                    int substitutioncost = (movielistitem[j - 1] == searchmov[i - 1]) ? 0 : 1;
                    int delete = distance[i - 1, j] + 1;
                    int insert = distance[i, j - 1] + 1;
                    int substitution = distance[i - 1, j - 1] + substitutioncost;
                    distance[i, j] = Math.Min(Math.Min(delete, insert), substitution);
                }
            }
            return distance[searchmovlength, movielistitemlength];
        }
    }
}
