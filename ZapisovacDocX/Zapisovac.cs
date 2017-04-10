using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Novacode;
using Service_Konektor.poseidon;

namespace ZapisovacDocX
{
    public static class Zapisovac
    {
        /// <summary>
        /// Vygenerejuje Docx súbor Vývesky vlakov
        /// </summary>
        /// <param name="cesta">Nieje povinný parameter/ ak je null subor je vytvorený na ploche</param>
        /// <param name="vlaky">Data s ktorými sa pracuje</param>
        /// <returns></returns>
        public static bool GenerujVyvesku(string cesta, VSVlak[] vlaky)
        {
            if (cesta == null)
            {
                cesta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            }
            using (DocX document = DocX.Create(cesta +"\\Vyveska.docx"))
            {
                Paragraph p = document.InsertParagraph();
                var b = p.Direction == Direction.LeftToRight;
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

        /// <summary>
        /// Metóda uloží načítané dáta vlakov
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="vlaky"></param>
        public static void ZapisVlakyDoSuboru(String cesta, VSVlak[] vlaky)
        {
            //string json = JsonConvert.SerializeObject(vlaky);
            //ZapisDoSuboru(cesta,json);
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "PomocneData.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, vlaky);
            }
        }

        /// <summary>
        /// metóda uloží načítané dáta Bodov
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="trasaBody"></param>
        public static void ZapisTrasaBodyDoSuboru(String cesta, VSTrasaBod[] trasaBody)
        {
            //string json = JsonConvert.SerializeObject(trasaBody);
            //ZapisDoSuboru(cesta, json);
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "PomocneData.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, trasaBody);
            }
        }


        private static void ZapisDoSuboru(string cesta, string json)
        {
            if (cesta == null)
            {
                cesta = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            using (var sw = new StreamWriter(Path.Combine(cesta, "PomocneData.json")))
            {
                sw.WriteLine(json);
            }
        }

        /// <summary>
        /// Načíta vlaky z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSVlak[] NacitajVlakyZoSuboru(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(Path.Combine(cesta, "PomocneData.json")))
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

        /// <summary>
        /// Načíta trasy Body trasy
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="nazov"></param>
        /// <returns></returns>
        public static VSTrasaBod[] NacitajTrasaBodyZoSuboru(string cesta, string nazov)
        {

            string json;
            try
            {
                String str = Path.Combine(cesta, nazov);
                using (var sr = new StreamReader(str))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSTrasaBod[]>(json);
        }
    }
}

