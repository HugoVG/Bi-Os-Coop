using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bi_Os_Coop
{
    /// <summary>
    /// Deze class berekent hoeveel veranderingen er nodig zijn om een string gelijk te stellen aan een andere, des te lager de uitkomst des te meer het op de andere string lijkt.
    /// Wat er gereturned wordt is het aantal benodigde veranderingen, zet in je eigen code wat je wilt dat het maximale aantal verandereingen is.
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
                return movielistitem.Length;
            }

            int searchmovlength = searchmov.Length;
            int movielistitemlength = movielistitem.Length;
            int[,] d = new int[searchmovlength + 1, movielistitemlength + 1];

            for (int i = 0; i <= searchmovlength; d[i, 0] = i++) ;
            for (int j = 1; j <= movielistitemlength; d[0, j] = j++) ;

            for (int i = 1; i <= searchmovlength; i++)
            {
                for (int j = 1; j <= movielistitemlength; j++)
                {
                    int cost = (movielistitem[j - 1] == searchmov[i - 1]) ? 0 : 1;
                    int min1 = d[i - 1, j] + 1;
                    int min2 = d[i, j - 1] + 1;
                    int min3 = d[i - 1, j - 1] + cost;
                    d[i, j] = Math.Min(Math.Min(min1, min2), min3);
                }
            }
            return d[searchmovlength, movielistitemlength];
        }
    }
}
