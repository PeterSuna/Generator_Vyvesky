using System;
using System.Collections;
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
        /// Podla id stanice najde na akých trasách sa nachádza stanica
        /// </summary>
        /// <param name="bodId"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static MapTrasaBod[] NajdiPodlaDopravnýchBodov(int[] bodId, MapTrasaBod[] trasaBody)
        {
            MapTrasaBod[] body = trasaBody.Where(c => bodId.Contains(c.BodID)).Select(c => c).ToArray();
            return body;
        }


        /// <summary>
        /// porovná všetky id vlakov a všetky idVlakov ktoré obsauje pole trasaBody. Tie trasabody ktoré majú zhodné idVlakou z nejakým vlakom
        /// tak tie vráti.
        /// </summary>
        /// <param name="vlaky"></param>
        /// <param name="trasaBody"></param>
        /// <returns></returns>
        public static MapTrasaBod[] NajdiTrasyPoldaVlaku(MapVlak[] vlaky, MapTrasaBod[] trasaBody)
        {
            int[] idVlakov = vlaky.Select(c => c.ID).ToArray();
            return trasaBody.Where(c => c!=null && idVlakov.Contains(c.VlakID)).Select(c => c).ToArray();
        }

        public static MapTrasaBod[] NajdiDopravnéUzly(MapDopravnyUsek[] useky, MapTrasaBod[] body)
        {
            var dictionary = new Dictionary<int, int>();
            for (int i = 0; i < useky.Length; i++)
            {
                int id1 = useky[i].DopravnyBod1ID;
                if (dictionary.ContainsKey(id1))
                {
                    int pocetnost = dictionary[id1];
                    dictionary[id1] = pocetnost + 1;
                }
                else
                {
                    dictionary.Add(id1, 1);
                }

                int id2 = useky[i].DopravnyBod2ID;
                if (dictionary.ContainsKey(id2))
                {
                    int pocetnost = dictionary[id2];
                    dictionary[id2] = pocetnost+1;
                }
                else
                {
                    dictionary.Add(id2, 1);
                }
            }

            int[] uzly = dictionary.Where(c => c.Value > 2).Select(c => c.Key).ToArray();
            return NajdiPodlaDopravnýchBodov(uzly,body);
        }
    }
}
