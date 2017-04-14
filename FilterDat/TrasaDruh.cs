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
    }
}
