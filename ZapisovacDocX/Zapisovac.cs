using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using Service_Konektor.poseidon;

namespace Zapisovac
{
    public static class Zapisovac
    {
       
        /// <summary>
        /// Metóda uloží načítané dáta vlakov
        /// </summary>
        /// <param name="cesta"></param>
        /// <param name="vlaky"></param>
        public static void ZapisVlakyDoSuboru(string cesta, VSVlak[] vlaky)
        {
            //string json = JsonConvert.SerializeObject(vlaky);
            //ZapisDoSuboru(cesta,json);
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "PomocneData.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, vlaky);
            }
        }

        public static void ZapisSpecifikacieDoSuboru(string cesta, VSTrasaSpecifikace[] specifikaces)
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
        public static void ZapisTrasaBodyDoSuboru(string cesta, VSTrasaBod[] trasaBody)
        { 
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "PomocneData.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, trasaBody);
            }
        }

        public static void ZapisTrasaDopravneDruhyDoSuboru(string cesta, VSTrasaDruh[] druhy)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "DopravneDruhy.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, druhy);
            }
        }

        public static void ZapisTrasaObecnePoznamky(string cesta, VSTrasaObecPozn[] top)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "TrasaObPoznamky.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, top);
            }
        }
        public static void ZapisObecnePoznamky(string cesta, VSObecnaPoznamka[] op)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "ObecnaPoznamka.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, op);
            }
        }

        public static void ZapisTrasaBodyDoSuboryCasti(string cesta, VSTrasaBod[] trasaBody)
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


        public static void ZapisDoSuboruDopravneBody(string cesta, VSDopravnyBod[] dopravneBody)
        {
            using (TextWriter writer = File.CreateText(Path.Combine(cesta, "DopravneBody.json")))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(writer, dopravneBody);
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

        public static VSTrasaDruh[] NacitajZoSuboruDopravneDruhy(string cesta)
        {
            string json;
            try
            {
                string str = Path.Combine(cesta, "DopravneDruhy.json");
                using (var sr = new StreamReader(str))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSTrasaDruh[]>(json);
        }

        public static VSTrasaObecPozn[] NacitajZoSuboruTrasaObPozn(string cesta)
        {
            string json;
            try
            {
                string str = Path.Combine(cesta, "TrasaObPoznamky.json");
                using (var sr = new StreamReader(str))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSTrasaObecPozn[]>(json);
        }
        public static VSObecnaPoznamka[] NacitajZoSuboruObecnuPoznam(string cesta)
        {
            string json;
            try
            {
                string str = Path.Combine(cesta, "ObecnaPoznamka.json");
                using (var sr = new StreamReader(str))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSObecnaPoznamka[]>(json);
        }

        public static VSTrasaSpecifikace[] NacitajZoSuborSpecifikaces(string cesta)
        {
            string json;
            try
            {
                string str = Path.Combine(cesta, "Specifikacie.json");
                using (var sr = new StreamReader(str))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSTrasaSpecifikace[]>(json);
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

        public static VSDopravnyBod[] NacitajDopravneBodyZoSuboru(string cesta)
        {
            string json;
            try
            {
                String str = Path.Combine(cesta, "DopravneBody.json");
                using (var sr = new StreamReader(str))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSDopravnyBod[]>(json);
        }
    }
}

