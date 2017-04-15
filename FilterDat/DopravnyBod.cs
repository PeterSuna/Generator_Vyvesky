using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;

namespace FilterDat
{
    public static class DopravnyBod
    {
        /// <summary>
        /// Vybere z pola stanic podla idcka vsetky stanice ktoré sú na danej trase
        /// </summary>
        /// <param name="idBodov"></param>
        /// <param name="dopravneBody"></param>
        /// <returns></returns>
        public static VSDopravnyBod[] NajdiDopravneBody(int[] idBodov, VSDopravnyBod[] dopravneBody)
        {
            VSDopravnyBod[] body = dopravneBody.Where(c => idBodov.Contains(c.ID)).Select(c => c).ToArray();
            return body;
        }

        public static string[] NajdiNazvyStanic(VSTrasaBod aktualnaStanica,VSTrasaBod[] body, VSDopravnyBod[] dopravneBody)
        {
            int[] idDopBodov = body.Where(c => c.Poradi<= aktualnaStanica.Poradi && c.AktCisloVlaku == aktualnaStanica.AktCisloVlaku)
                .OrderByDescending(c => c.Poradi).Select(c => c.BodID).ToArray();
            return dopravneBody.Where(c => idDopBodov.Contains(c.ID)).Select(c => c.Nazov).ToArray();
        }

        public static VSTrasaBod[] Stanic(VSTrasaBod aktualnaStanica, VSTrasaBod[] body, VSDopravnyBod[] dopravneBody)
        {
            return body.Where(c => c.Poradi <= aktualnaStanica.Poradi && c.AktCisloVlaku == aktualnaStanica.AktCisloVlaku)
                .OrderBy(c => c.Poradi).Select(c => c).ToArray();
        }

        public static string VytvorTextZoSmeru(VSTrasaBod aktualnaStanica, VSTrasaBod[] body, VSDopravnyBod[] dopravneBody)
        {
            VSTrasaBod[] bodyStanicPred =
                body.Where(c => c.Poradi < aktualnaStanica.Poradi && c.AktCisloVlaku == aktualnaStanica.AktCisloVlaku)
                    .OrderBy(c => c.Poradi).Select(c => c).ToArray();
            string text ="";
            for (int i = 0; i < bodyStanicPred.Length; i++)
            {
                if (i == bodyStanicPred.Length - 1)
                {
                    text += string.Format("{0}({1:%h}.{1:%m})", NajdiNazovDopBodu(bodyStanicPred[i].BodID,dopravneBody), TimeSpan.FromSeconds(bodyStanicPred[i].CasPrijazdu));
                }
                else
                {
                    text += string.Format("{0}({1:%h}.{1:%m}) - ", NajdiNazovDopBodu(bodyStanicPred[i].BodID, dopravneBody), TimeSpan.FromSeconds(bodyStanicPred[i].CasPrijazdu));
                }
            }
            return text;
        }

        private static string NajdiNazovDopBodu(int idBodu, VSDopravnyBod[] dopravneBody)
        {
            VSDopravnyBod db = dopravneBody.FirstOrDefault(c => c.ID == idBodu);
            return db?.Nazov;
        }

    }
}
