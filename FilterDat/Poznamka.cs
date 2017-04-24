using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class Poznamka
    {
        /// <summary>
        /// zistí poznámku pre daný vlak
        /// </summary>
        /// <param name="vlakId"></param>
        /// <param name="top"></param>
        /// <param name="op"></param>
        /// <returns></returns>
        public static string ZistiPoznamku(int vlakId, MapTrasaObecPozn[] top, VSObecnaPoznamka[] op)
        {
            MapTrasaObecPozn trasobp = top.FirstOrDefault(c => c.VlakID == vlakId);
            VSObecnaPoznamka obecnaPozn = op.FirstOrDefault(c => c.ID == trasobp?.ObecnaPoznamkaID);
            return obecnaPozn?.Poznamka;
        }
    }
}
