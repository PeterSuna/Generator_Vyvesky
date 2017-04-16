using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class TrasaBod
    {
        /// <summary>
        /// vyberie s pola trás podla id vlaku trasy ktoré chodí daný vlak
        /// </summary>
        /// <param name="vlakId"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static VSTrasaBod[] NajdiPodlaIdVlaku(int vlakId, VSTrasaBod[] trasaBody)
        {
            VSTrasaBod[] body = trasaBody.Where(c => c != null && c.VlakID == vlakId ).Select(c => c).ToArray();
            return body;
        }

        /// <summary>
        /// vyberie s pola bodov trás id stanic cez ktoré prechadza daný vlak
        /// </summary>
        /// <param name="vlakId"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static int[] NajdiPodlaVlakuIdTras(int vlakId, VSTrasaBod[] trasaBody)
        {
            var body = NajdiPodlaIdVlaku(vlakId, trasaBody);
            int[] idcka = body.Select(c => c.BodID).ToArray();
            return idcka;
        }

        /// <summary>
        /// Podla id stanice najde na akých trasách sa nachádza stanica
        /// </summary>
        /// <param name="bodId"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static VSTrasaBod[] NajdiPodlaDopravnehoBodu(int bodId, VSTrasaBod[] trasaBody)
        {
            VSTrasaBod[] body = trasaBody.Where(c => c!=null && c.BodID == bodId).Select(c => c).ToArray();
            return body;
        }

        /// <summary>
        /// vyberie tie trasy ktoré prechádza vlak s daným id;
        /// </summary>
        /// <param name="vlakId"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static VSTrasaBod[] NajdiTrasyPoldaIdVlaku(int vlakId, VSTrasaBod[] trasaBody)
        {
            return trasaBody.Where(c => c.VlakID == vlakId).Select(c => c).ToArray();
        }

        /// <summary>
        /// porovná všetky id vlakov a všetky idVlakov ktoré obsauje pole trasaBody. Tie trasabody ktoré majú zhodné idVlakou z nejakým vlakom
        /// tak tie vráti.
        /// </summary>
        /// <param name="vlaky"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static VSTrasaBod[] NajdiTrasyPoldaVlaku(VSVlak[] vlaky, VSTrasaBod[] trasaBody)
        {
            int[] idVlakov = vlaky.Select(c => c.ID).ToArray();
            return trasaBody.Where(c => c!=null && idVlakov.Contains(c.VlakID)).Select(c => c).ToArray();
        }
    }
}
