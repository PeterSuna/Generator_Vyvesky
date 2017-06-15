using System;
using System.IO;
using Newtonsoft.Json;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;

namespace DataZoSuboru
{
    public static class Nacitanie
    {
        /// <summary>
        /// Nacíta vybrané dáta
        /// </summary>
        /// <typeparam name="T"> očakávaný typ</typeparam>
        /// <param name="cesta">očakávaná adresa súboru</param>
        /// <returns></returns>
        public static T ZoSuboru<T>(string cesta)
        {
            string json;
            try
            {
                using (var sr = new StreamReader(cesta))
                {
                    json = sr.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
