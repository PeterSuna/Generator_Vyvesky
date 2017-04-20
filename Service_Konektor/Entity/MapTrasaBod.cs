using Service_Konektor.poseidon;

namespace Service_Konektor.Entity
{
    public class MapTrasaBod : VSEntitaBase
    {
        public int AktCisloVlaku { get; set; }
        public int BodID { get; set; }
        public int CasOdjazdu { get; set; }
        public int CasPrijazdu { get; set; }
        public int Poradi { get; set; }
        public int VlakID { get; set; }
    }
}
