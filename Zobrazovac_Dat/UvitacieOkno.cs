using System;
using System.Linq;
using System.Windows.Forms;
using Data_Kontroler;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;

namespace Zobrazovac_Dat
{
    public partial class UvitacieOkno : Form
    {
        private MapDopravnyBod[] _dopravneBody;
        private VSProject[] _projekty;

        public VSProject VybranyProjekt { get; set; }
        public MapDopravnyBod VybranyDopravnyBod { get; set; }
        public eVSVlakFaza VybranaFaza { get; set; }


        public UvitacieOkno(VSProject projekt, eVSVlakFaza faza)
        {
            InitializeComponent();

            _projekty = DataZoSuboru.Nacitaj.Projekty(@"..\..\..\Projekt\Projekty.json");
            cbxSelektProjektu.DataSource = _projekty.Select(c => c.Nazov).ToList();
            cbxSelektFiltra.DataSource = Enum.GetValues(typeof(eVSVlakFaza));

            if (projekt != null)
            {
                lblFilter.Text = "Vybraná fáza: " + faza;
                lblSelekt.Text = "Vybraný projekt: " + projekt.Nazov;
                cbxSelektProjektu.SelectedText = projekt.Nazov;
                cbxSelektFiltra.SelectedText = faza.ToString();
            }
        }


        private void btnSubor_Click(object sender, EventArgs e)
        {
            VybranyProjekt = _projekty.SingleOrDefault(c => c.Nazov == (string) cbxSelektProjektu.SelectedItem);
            _dopravneBody =
                DataZoSuboru.Nacitaj.MapDopravneBody(@"..\..\..\Projekt\" + VybranyProjekt.Nazov +
                                                          "\\MapDopravneBody.json");
            InitCmbox();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            PoseidonData kontrolerPoseidon;
            VybranaFaza = cbxSelektFiltra.SelectedItem is eVSVlakFaza
                ? (eVSVlakFaza) cbxSelektFiltra.SelectedItem
                : eVSVlakFaza.Pozadavek_zkonstruovano;
            VybranyProjekt = _projekty.SingleOrDefault(c => c.Nazov == (string) cbxSelektProjektu.SelectedItem);
            if (VybranyProjekt == null)
            {
                Mwbox("Je potrebný vybrať projekt podla ktorého bude prebiehať aktualizácia", "Upozornenie");
                return;
            }
            try
            {
                kontrolerPoseidon = new PoseidonData();
                kontrolerPoseidon.SelektProjektu(VybranaFaza, VybranyProjekt);
            }
            catch (Exception)
            {
                Mwbox("Nepodarilo sa pripojiť na server","Chyba");
                return;
            }
           

            lblFilter.Text = "Vybraná fáza: " + VybranaFaza;
            lblSelekt.Text = "Vybraný projekt: " + VybranyProjekt.Nazov;
            
            _projekty = kontrolerPoseidon.Projekty;
            cbxSelektProjektu.DataSource = _projekty.Select(c => c.Nazov).ToList();
            //Aktualizcácia Dát
            foreach (object itemChecked in chbxAktData.CheckedItems)
            {
                Aktualizuj(itemChecked.ToString(),kontrolerPoseidon);
            }
            kontrolerPoseidon.Logout();
            Mwbox("Data sú aktualizované","info");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (cbxMesto.SelectedItem != null)
                {
                    VybranyDopravnyBod = _dopravneBody.SingleOrDefault(c => c.Nazov == (string) cbxMesto.SelectedItem);
                    VybranaFaza = cbxSelektFiltra.SelectedItem is eVSVlakFaza
                       ? (eVSVlakFaza)cbxSelektFiltra.SelectedItem
                       : eVSVlakFaza.Pozadavek_zkonstruovano;
                    VybranyProjekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);

                }
                else
                {
                    Mwbox("Pre pokračovanie je potrebné zvoliť mesto", "Upozornenie");
                    return;
                }
                base.OnFormClosing(e);
            }
        }



        private void Mwbox(string telo, string hlavicka)
        {
            MessageBox.Show(telo, hlavicka, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Naplní combobox mestami ktoré boli načítané
        /// </summary>
        private void InitCmbox()
        {
            cbxMesto.DataSource = _dopravneBody.Select(c => c.Nazov).ToArray();
        }

        private void Aktualizuj(string text, PoseidonData poseidon)
        {
            string cesta = @"..\..\..\Projekt\" + VybranyProjekt.Nazov;
            VSEntitaBase[] data;
            switch (text)
            {
                case "Dopravné body":
                    cesta += @"\MapDopravneBody.json";
                    data = poseidon.GetMapDopravneBody();
                    break;
                case "Dopravné druhy":
                    cesta += @"\MapDopravneDruhy.json";
                    data = poseidon.GetMapTrasaDopravneDruhy();
                    break;
                case "Dopravné úseky":
                    cesta += @"\MapDopravneUseky.json";
                    data = poseidon.GetMapDopravneUseky();
                    break;
                case "Poznámky":
                    DataZoSuboru.Zapis.DoSuboru(cesta+ @"\TrasaObPoznamky.json", poseidon.GetTrasaObPoznamky());
                    cesta += @"\ObecnaPoznamka.json";
                    data = poseidon.GetObecnePoznamky();
                    break;
                case "Trasa body":
                    cesta += @"\MapTrasaBody.json";
                    data = poseidon.GetMapTrasaBody();
                    break;
                case "Vlaky":
                    cesta += @"\MapVlaky.json";
                    data = poseidon.GetMapVlaky();
                    break;
                default:
                    return;
            }
            DataZoSuboru.Zapis.DoSuboru(cesta,data);
        }
    }
}
