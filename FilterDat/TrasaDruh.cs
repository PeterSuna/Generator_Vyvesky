using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;

namespace FilterDat
{
   public static class TrasaDruh
    {
        public static string NajdiDruhVlaku(int idVlaku, VSTrasaDruh[] druhy)
        {
            VSTrasaDruh druh = druhy.FirstOrDefault(c => c.VlakID == idVlaku);
            return druh?.Druh;
        }

        public static bool ZisitiSpravnyDruhVlaku(int idVlaku, VSTrasaDruh[] druhy)
        {
            VSTrasaDruh druh = druhy.FirstOrDefault(c => c.VlakID == idVlaku);
            switch (druh?.Druh)
            {
                case "EC":
                case "IC":
                case "EN":
                case "Ex":
                case "R":
                case "Os":
                case "Sp":
                case "Sv":
                    return true;
                default:
                    return false;
            }
            
        }

    }
}
