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
        public static VSVlak NajdiVlakPodlaID(int id, VSVlak[] vlaky)
        {
            return vlaky.SingleOrDefault(c => c.ID == id);
        }
    }
}
