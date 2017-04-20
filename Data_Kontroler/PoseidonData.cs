using System;
using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;


namespace Data_Kontroler
{
    public class PoseidonData
    {
        private readonly gvd _poseidon;

        public VSProject[] Projekty { get; set; }
        public VSVlak[] Vlaky { get; set; }

        public PoseidonData(string meno, string heslo)
        {
            _poseidon = new gvd
            {
                Timeout = 2000000,
                CookieContainer = new System.Net.CookieContainer()
            };
            try
            {
                _poseidon.Login(meno, heslo);
                Projekty = _poseidon.GetProjects();
            }
            catch (Exception)
            {
               // pokračuje offline
            }
        }

        public PoseidonData()
        {
            _poseidon = new gvd
            {
                Timeout = 600000,
                CookieContainer = new System.Net.CookieContainer()
            };
            try
            {
                _poseidon.Login("suna", "peter");
                Projekty = _poseidon.GetProjects();
            }
            catch (Exception)
            {
                // pokračuje offline
            }
        }

        /// <summary>
        /// Vyber projektu, potrebné vykonať pred akoukolvek inou metodou
        /// </summary>
        /// <param name="faza"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool SelektProjektu(eVSVlakFaza faza, VSProject project)
        {
            var filter = new VSVlakFilter {Faza = faza};
            
            if (_poseidon.SelectProject(project, filter))
            {
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
            var vlaky = _poseidon.GetVlaky();
            return vlaky;
        }

        /// <summary>
        /// Vrati všetky TrasaBody s poseidona
        /// </summary>
        /// <returns></returns>
        public VSTrasaBod[] GetTrasy()
        {
            var trasy = _poseidon.GetTrasaBody();
            return trasy;
        }

        public MapTrasaBod[] MapujTrasaBody(VSTrasaBod[] trasy)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VSTrasaBod, MapTrasaBod>();
            });
            IMapper mapper = config.CreateMapper();
            List<MapTrasaBod> array = new List<MapTrasaBod>();
            foreach (var trasa in trasy)
            {
                MapTrasaBod v = mapper.Map<MapTrasaBod>(trasa);
                array.Add(v);
            }
            return array.ToArray();
        }

        public MapTrasaBod[] GetMapTrasy()
        {
            var trasy = _poseidon.GetTrasaBody();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VSTrasaBod, MapTrasaBod>();
            });
            IMapper mapper = config.CreateMapper();
            List<MapTrasaBod> array = new List<MapTrasaBod>();
            foreach (var trasa in trasy)
            {
                MapTrasaBod v = mapper.Map<MapTrasaBod>(trasa);
                array.Add(v);
            }
            return array.ToArray();
        }
        /// <summary>
        /// Vrati všetky dopravne body s poseidona
        /// </summary>
        /// <returns></returns>
        public VSDopravnyBod[] GetDopravneBody()
        {
            var body = _poseidon.GetDopravneBody();
            return body;
        }

        /// <summary>
        /// Vrati všetky Specifikácie trasy s poseidona
        /// </summary>
        /// <returns></returns>
        public VSTrasaSpecifikace[] GetTrasaSpecifikacie()
        {
            return _poseidon.GetTrasaSpecifikacie();
        }

        /// <summary>
        /// Vrati všetky Trsa dopravné druhy s poseidona
        /// </summary>
        /// <returns></returns>
        public VSTrasaDruh[] GetTrasaDopravneDruhy()
        {
            return _poseidon.GetTrasaDopravneDruhy();
        }

        /// <summary>
        /// Vrati všetky Trasa obecné poznámky s poseidona
        /// </summary>
        /// <returns></returns>
        public VSTrasaObecPozn[] GetTrasaObPoznamky()
        {
            return _poseidon.GetTrasaObecPoznamky();
        }

        /// <summary>
        /// Vrati všetky Obecné poznámky s poseidona
        /// </summary>
        /// <returns></returns>
        public VSObecnaPoznamka[] GetObecnePoznamky()
        {
            return _poseidon.GetObecnePoznamky();
        }

        public VSDopravnyUsek[] GetDopravneUseky()
        {
            return _poseidon.GetDopravneUseky();
        }
    }
}