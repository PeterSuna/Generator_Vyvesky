using System;
using System.IO;
using Newtonsoft.Json;
using Service_Konektor.poseidon;

namespace DataZoSuboru
{
    public static class Zapis
    {
        /// <summary>
        /// Zapíše do súboru data
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="data"></param>
        public static void DoSuboru(string cesta, VSEntitaBase[] data)
        {
            using (TextWriter writer = File.CreateText(cesta))
            {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(writer, data);
            }
        }

        /// <summary>
        /// Metóda uloží načítané dáta Trsabodob do viacerých súborov
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="trasaBody"></param>
        public static void TrasaBodyDoSuboruCasti(string cesta, VSTrasaBod[] trasaBody)
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
    }
}