using System.Linq;
using Service_Konektor.Entity;

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
        public static MapVlak NajdiVlakPodlaId(int id, MapVlak[] vlaky)
        {
            return vlaky.SingleOrDefault(c => c.ID == id);
        }


        /// <summary>
        /// prejde všetky vybradné trasi a všetky vybrané vlaky a porovná ktoré vlaky prechádzajú danú trasu
        /// a tie vlaky vráti
        /// </summary>
        /// <param name="body"></param>
        /// <param name="vlaky"></param>
        /// <returns></returns>
        public static MapVlak[] NajdiVlakyVTrasaBody(MapTrasaBod[] body, MapVlak[] vlaky)
        {
            int[] idVlakov = body.Select(c => c.VlakID).ToArray();
            MapVlak[] v = vlaky.Where(c => idVlakov.Contains(c.ID)).Select(c => c).ToArray();
            return v;
        }

        /// <summary>
        /// najde vlak z vybraného pola ktorému patrí zadané idVlaku
        /// </summary>
        /// <param name="idVlaku"></param>
        /// <param name="vlaky"></param>
        /// <returns></returns>
        public static int ZisiteCisloVlaku(int idVlaku, MapVlak[] vlaky)
        {
            MapVlak vlak = vlaky.FirstOrDefault(c => c.ID == idVlaku);
            int cislovlaku = vlak?.Cislo ?? -1;
            return cislovlaku;
        }
    }
}
