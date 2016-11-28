using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Novacode;
using Service_Konektor.poseidon;

namespace ZapisovacDocX
{
    public static class Zapisovac
    {
        public static bool GenerujVyvesku(string cesta, VSVlak[] vlaky)
        {
            if (cesta == null)
            {
                cesta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            using (DocX document = DocX.Create(cesta +"\\Vyveska.docx"))
            {
                Paragraph p = document.InsertParagraph();
                var arial = new FontFamily("Arial");
                try
                {
                    p.Append("Vyveska vlakov\n\n").Bold().FontSize(24).Font(arial);
                    foreach (var vlak in vlaky)
                    {
                        var nazov = vlak.Nazev == null ? "": ", s názvom: " +vlak.Nazev;
                        p.Append(
                                $"císlo vlaku: {vlak.Cislo}{nazov}, a platnostou od: {vlak.PlatnostOd.ToShortDateString()} " +
                                $"do: {vlak.PlatnostDo.ToShortDateString()} \n")
                            .Font(arial)
                            .Spacing(1.3)
                            .FontSize(12);
                    }
                    document.Save();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }

        public static void ZapisUdajeDoSuboru(VSVlak[] vlaky)
        {
            string json = JsonConvert.SerializeObject(vlaky);
            ZapisDoSuboru(null,json);
        }

        private static void ZapisDoSuboru(string cesta, string json)
        {
            if (cesta == null)
            {
                cesta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            using (var sw = new StreamWriter(Path.Combine(cesta, "PomocneData.log")))
            {
                sw.WriteLine(json);
            }
        }

        public static VSVlak[] NacitajZoSuboru(string cesta)
        {

            if (cesta == null)
            {
                cesta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            string json;
            try
            {
                using (var sr = new StreamReader(Path.Combine(cesta, "PomocneData.log")))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSVlak[]>(json);
        }
    }
}
