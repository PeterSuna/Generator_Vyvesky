using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;

namespace Data_Kontroler
{
    public class PoseidonData
    {
        public VSProject[] Projekty { get; set; }
        public VSVlak[] Vlaky { get; set; }
        public gvd Poseidon { get; set; }

        public PoseidonData(string meno, string heslo)
        {
            Poseidon = new gvd();
            Poseidon.Login(meno, heslo);
            Projekty = Poseidon.GetProjects();
        }

        public PoseidonData()
        {
            Poseidon = new gvd();
            Poseidon.Login("suna", "peter");
            Projekty = Poseidon.GetProjects();
        }

        /// <summary>
        /// Vyber projektu, potrebné vykonať pred akoukolvek inou metodou
        /// </summary>
        /// <param name="faza"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool SelektProjektu(eVSVlakFaza faza, VSProject project )
        {
            var filter = new VSVlakFilter {Faza = faza};
            if (Poseidon.SelectProject(project, filter)) {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Pred zavolaním metody je potrebne vykonať metodu SelektProjektu()
        /// </summary>
        /// <returns></returns>
        public VSVlak[] GetVlaky()
        {
            var vlaky = Poseidon.GetVlaky();
            return vlaky;
        }
    }
}