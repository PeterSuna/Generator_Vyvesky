using System.IO;
using Newtonsoft.Json;
using Service_Konektor.poseidon;

namespace DataZoSuboru
{
    public static class Zapis
    {
        /// <summary>
        /// Metóda uloží načítané dáta vlakov
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="vlaky"></param>
        public static void VlakyDoSuboru(string cesta, VSVlak[] vlaky)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "PomocneData.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, vlaky);
            }
        }

        /// <summary>
        /// Metóda uloží načítané dáta trsaSpecifikacie
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="specifikaces"></param>
        public static void SpecifikacieDoSuboru(string cesta, VSTrasaSpecifikace[] specifikaces)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "Specifikacie.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, specifikaces);
            }
        }

        /// <summary>
        /// metóda uloží načítané dáta Bodov
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="trasaBody"></param>
        public static void TrasaBodyDoSuboru(string cesta, VSTrasaBod[] trasaBody)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "PomocneData.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, trasaBody);
            }
        }

        /// <summary>
        /// Metóda uloží načítané dáta TrsaDruhy
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="druhy"></param>
        public static void TrasaDopravneDruhyDoSuboru(string cesta, VSTrasaDruh[] druhy)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "DopravneDruhy.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, druhy);
            }
        }

        /// <summary>
        /// Metóda uloží načítané dáta Trsa Obecnej Poznámky
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="top"></param>
        public static void TrasaObecnePoznamky(string cesta, VSTrasaObecPozn[] top)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "TrasaObPoznamky.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, top);
            }
        }

        /// <summary>
        /// Metóda uloží načítané dáta Obecnej poznámky
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="op"></param>
        public static void ObecnePoznamky(string cesta, VSObecnaPoznamka[] op)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "ObecnaPoznamka.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, op);
            }
        }

        /// <summary>
        /// Metóda uloží načítané dáta Trsabodob do viacerých súborov
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="trasaBody"></param>
        public static void TrasaBodyDoSuboryCasti(string cesta, VSTrasaBod[] trasaBody)
        {
            int pocet = trasaBody.Length;
            int part = pocet / 10 + 1;
            int j = 0;
            while (j < trasaBody.Length)
            {
                VSTrasaBod[] bodpart = new VSTrasaBod[part];
                for (int i = 0; i < part; i++)
                {
                    if (j >= trasaBody.Length)
                        break;
                    bodpart[i] = trasaBody[j++];
                }

                using (TextWriter writer = File.CreateText(Path.Combine(cesta, "PomocneData" + j + ".json")))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, bodpart);
                }
            }
        }

        /// <summary>
        /// Metóda uloží načítané dáta Dopravné body
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="dopravneBody"></param>
        public static void DoSuboruDopravneBody(string cesta, VSDopravnyBod[] dopravneBody)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "DopravneBody.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, dopravneBody);
            }
        }
    }
}