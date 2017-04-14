using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class Vlak
    {
        /// <summary>
        /// Z vybraného pola vlakov vráti práve jeden vlak z daným identifikárom.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="vlaky"></param>
        /// <returns></returns>
        public static VSVlak NajdiVlakPodlaId(int id, VSVlak[] vlaky)
        {
            return vlaky.SingleOrDefault(c => c.ID == id);
        }


        public static VSVlak[] NajdiVlakyVTrasaBody(VSTrasaBod[] body, VSVlak[] vlaky)
        {
            int[] idVlakov = body.Select(c => c.VlakID).ToArray();
            VSVlak[] v = vlaky.Where(c => idVlakov.Contains(c.ID)).Select(c => c).ToArray();
            return v;
        }

        public static int ZisiteCisloVlaku(int idVlaku, VSVlak[] vlaky)
        {
            VSVlak vlak = vlaky.FirstOrDefault(c => c.ID == idVlaku);
            int cislovlaku = vlak?.Cislo ?? -1;
            return cislovlaku;
        }
    }
}
