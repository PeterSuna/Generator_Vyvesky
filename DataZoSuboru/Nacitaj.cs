using System;
using System.IO;
using Newtonsoft.Json;
using Service_Konektor.poseidon;

namespace DataZoSuboru
{
    public static class Nacitaj
    {
        /// <summary>
        /// Načíta vlaky z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSVlak[] VlakyZoSuboru(string cesta)
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
        /// Načíta Dopravné druhy z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSTrasaDruh[] ZoSuboruDopravneDruhy(string cesta)
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

        /// <summary>
        ///  Načíta projekty z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSProject[] ZoSuboruProjekty(string cesta)
        {
            string json;
            try
            {
                string str = Path.Combine(cesta, "projekty.json");
                using (var sr = new StreamReader(str))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSProject[]>(json);
        }

        /// <summary>
        /// Načíta Trsa obecnú poznámky z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSTrasaObecPozn[] ZoSuboruTrasaObPozn(string cesta)
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

        /// <summary>
        /// Načíta Obecnú pooznámky z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSObecnaPoznamka[] ZoSuboruObecnuPoznam(string cesta)
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

        /// <summary>
        /// Načíta Trasa specifikácie z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSTrasaSpecifikace[] ZoSuborSpecifikaces(string cesta)
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
        public static VSTrasaBod[] TrasaBodyZoSuboru(string cesta, string nazov)
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

        /// <summary>
        /// Načíta Dopravný bod z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSDopravnyBod[] DopravneBodyZoSuboru(string cesta)
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
