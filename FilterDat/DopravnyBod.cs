using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class DopravnyBod
    {
        /// <summary>
        /// Vybere z pola stanic podla idcka vsetky stanice ktoré sú na danej trase
        /// </summary>
        /// <param name="idBodov"></param>
        /// <param name="dopravneBody"></param>
        /// <returns></returns>
        public static VSDopravnyBod[] NajdiDopravneBody(int[] idBodov, VSDopravnyBod[] dopravneBody)
        {
            VSDopravnyBod[] body = dopravneBody.Where(c => idBodov.Contains(c.ID)).Select(c => c).ToArray();
            return body;
        }
    }
}
