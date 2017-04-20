using System;
using System.IO;
using Data_Kontroler;
using Newtonsoft.Json;
using Service_Konektor.Entity;
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
        public static VSVlak[] Vlaky(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
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
        public static VSTrasaDruh[] DopravneDruhy(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
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
        public static VSProject[] Projekty(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
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
        ///  Načíta projekty z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static MapTrasaBod[] MapTrasBody(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<MapTrasaBod[]>(json);
        }

        /// <summary>
        /// Načíta Trsa obecnú poznámky z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSTrasaObecPozn[] TrasaObPozn(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
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
        public static VSObecnaPoznamka[] ObecnuPoznam(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
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
        public static VSTrasaSpecifikace[] Specifikacie(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
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
        /// Načíta Dopravný bod z uloženého súboru
        /// </summary>
        /// <param name="cesta"></param>
        /// <returns></returns>
        public static VSDopravnyBod[] DopravneBody(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
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

        public static VSDopravnyUsek[] DopravnyUsek(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                return null;
            }
            return JsonConvert.DeserializeObject<VSDopravnyUsek[]>(json);
        }
    }
}
