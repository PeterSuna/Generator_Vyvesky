using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Kontroler;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class TrasaBod
    {
        /// <summary>
        /// Podla id stanice najde na akých trasách sa nachádza stanica
        /// </summary>
        /// <param name="bodId"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static MapTrasaBod[] NajdiPodlaDopravnehoBodu(int bodId, MapTrasaBod[] trasaBody)
        {
            MapTrasaBod[] body = trasaBody.Where(c => c!=null && c.BodID == bodId).Select(c => c).ToArray();
            return body;
        }


        /// <summary>
        /// porovná všetky id vlakov a všetky idVlakov ktoré obsauje pole trasaBody. Tie trasabody ktoré majú zhodné idVlakou z nejakým vlakom
        /// tak tie vráti.
        /// </summary>
        /// <param name="vlaky"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static MapTrasaBod[] NajdiTrasyPoldaVlaku(VSVlak[] vlaky, MapTrasaBod[] trasaBody)
        {
            int[] idVlakov = vlaky.Select(c => c.ID).ToArray();
            return trasaBody.Where(c => c!=null && idVlakov.Contains(c.VlakID)).Select(c => c).ToArray();
        }
    }
}
