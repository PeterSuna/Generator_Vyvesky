using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class Poznamka
    {
        public static string ZistiPoznamku(int vlakId, VSTrasaObecPozn[] top, VSObecnaPoznamka[] op)
        {
            VSTrasaObecPozn trasobp = top.FirstOrDefault(c => c.VlakID == vlakId);
            VSObecnaPoznamka obecnaPozn = op.FirstOrDefault(c => c.ID == trasobp?.ObecnaPoznamkaID);
            return obecnaPozn?.Poznamka;
        }
    }
}
