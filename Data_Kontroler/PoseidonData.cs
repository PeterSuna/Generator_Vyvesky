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

        public static PoseidonData PoseidonConstruc(string meno, string heslo)
        {
            var poseidon = new gvd
            {
                Timeout = 2000000,
                CookieContainer = new System.Net.CookieContainer()
            };
            try
            {
                poseidon.Login(meno, heslo);
                var poseidonData = new PoseidonData(poseidon);
                return poseidonData;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private PoseidonData(gvd poseidon)
        {
            _poseidon = poseidon;

            Projekty = _poseidon.GetProjects();
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

            return _poseidon.SelectProject(project, filter);
        }

        public void Logout()
        {
            _poseidon.Logout();
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

        /// <summary>
        /// Vrati všetky Dopravné uskey s poseidona
        /// </summary>
        /// <returns></returns>
        public VSDopravnyUsek[] GetDopravneUseky()
        {
            return _poseidon.GetDopravneUseky();
        }

        /// <summary>
        /// Vytvorý s VSTrasaBod skrátenú verziu
        /// </summary>
        /// <returns></returns>
        public MapTrasaBod[] GetMapTrasaBody()
        {
            VSTrasaBod[] trasy; 
            try
            {
                trasy = _poseidon.GetTrasaBody();
            }
            catch (System.Net.WebException ex)
            {
                throw ex;
            }

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
        /// Vytvorý s VSDopravnyBod skrátenú verziu
        /// </summary>
        /// <returns></returns>
        public MapDopravnyBod[] GetMapDopravneBody()
        {
            var body = _poseidon.GetDopravneBody();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VSDopravnyBod, MapDopravnyBod>();
            });
            IMapper mapper = config.CreateMapper();
            List<MapDopravnyBod> array = new List<MapDopravnyBod>();
            foreach (var bod in body)
            {
                MapDopravnyBod v = mapper.Map<MapDopravnyBod>(bod);
                array.Add(v);
            }
            return array.ToArray();
        }

        /// <summary>
        /// Vytvorý s VSDopravnyUsek skrátenú verziu
        /// </summary>
        /// <returns></returns>
        public MapDopravnyUsek[] GetMapDopravneUseky()
        {
            var useky = _poseidon.GetDopravneUseky();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VSDopravnyUsek, MapDopravnyUsek>();
            });
            IMapper mapper = config.CreateMapper();
            List<MapDopravnyUsek> array = new List<MapDopravnyUsek>();
            foreach (var usek in useky)
            {
                MapDopravnyUsek v = mapper.Map<MapDopravnyUsek>(usek);
                array.Add(v);
            }
            return array.ToArray();
        }

        /// <summary>
        /// Vytvorý s VSTrasaDruh skrátenú verziu
        /// </summary>
        /// <returns></returns>
        public MapTrasaDruh[] GetMapTrasaDopravneDruhy()
        {
            var data = _poseidon.GetTrasaDopravneDruhy();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VSTrasaDruh, MapTrasaDruh>();
            });
            IMapper mapper = config.CreateMapper();
            List<MapTrasaDruh> array = new List<MapTrasaDruh>();
            foreach (var d in data)
            {
                MapTrasaDruh v = mapper.Map<MapTrasaDruh>(d);
                array.Add(v);
            }
            return array.ToArray();
        }

        /// <summary>
        /// Vytvorý s VSVlak skrátenú verziu
        /// </summary>
        /// <returns></returns>
        public MapVlak[] GetMapVlaky()
        {
            var data = _poseidon.GetVlaky();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VSVlak, MapVlak>();
            });
            IMapper mapper = config.CreateMapper();
            List<MapVlak> array = new List<MapVlak>();
            foreach (var d in data)
            {
                MapVlak v = mapper.Map<MapVlak>(d);
                array.Add(v);
            }
            return array.ToArray();
        }

        /// <summary>
        /// Vytvorý s VSTrasaObecPozn skrátenú verziu
        /// </summary>
        /// <returns></returns>
        public MapTrasaObecPozn[] GetMapTrasaObecPozn()
        {
            var data = _poseidon.GetTrasaObecPoznamky();
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<VSTrasaObecPozn, MapTrasaObecPozn>();
            });
            IMapper mapper = config.CreateMapper();
            List<MapTrasaObecPozn> array = new List<MapTrasaObecPozn>();
            foreach (var d in data)
            {
                MapTrasaObecPozn v = mapper.Map<MapTrasaObecPozn>(d);
                array.Add(v);
            }
            return array.ToArray();
        }
    }
}