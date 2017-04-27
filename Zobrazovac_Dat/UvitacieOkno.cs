using System;
using System.Configuration;
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

        /// <summary>
        /// Vytvorenie uvítacieho okna
        /// </summary>
        /// <param name="projekt">Ak je rôzne od null okno sa bere ako nastavovacie</param>
        /// <param name="faza"></param>
        public UvitacieOkno(VSProject projekt, eVSVlakFaza faza)
        {
            InitializeComponent();
            _projekty = DataZoSuboru.Nacitaj.Projekty(@"Data\Projekty.json");
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

        /// <summary>
        /// Vybranie načítanie údajov zo súbora
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubor_Click(object sender, EventArgs e)
        {
            VybranyProjekt = _projekty.SingleOrDefault(c => c.Nazov == (string) cbxSelektProjektu.SelectedItem);
            _dopravneBody =
                DataZoSuboru.Nacitaj.MapDopravneBody(@"Data\" + VybranyProjekt.Nazov +
                                                     "\\MapDopravneBody.json");
            InitCmbox();
        }

        /// <summary>
        /// aktualizovanie vybraných tried
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            
            var meno = ConfigurationManager.AppSettings["Meno"];
            var heslo = ConfigurationManager.AppSettings["Heslo"];
            kontrolerPoseidon = PoseidonData.PoseidonConstruc(meno,heslo);
            if (kontrolerPoseidon == null)
            {
                Mwbox("Aplikácii sa nepodarilo prihlásiť, zrejme ste zadali nesprávne prihlasovacie údaje","upoztornenie");
                return;
            }
            kontrolerPoseidon.SelektProjektu(VybranaFaza, VybranyProjekt);
            



            lblFilter.Text = "Vybraná fáza: " + VybranaFaza;
            lblSelekt.Text = "Vybraný projekt: " + VybranyProjekt.Nazov;

            _projekty = kontrolerPoseidon.Projekty;
            cbxSelektProjektu.DataSource = _projekty.Select(c => c.Nazov).ToList();
            //Aktualizcácia Dát
            foreach (object itemChecked in chbxAktData.CheckedItems)
            {
                Aktualizuj(itemChecked.ToString(), kontrolerPoseidon);
            }
            kontrolerPoseidon.Logout();
            Mwbox("Data sú aktualizované", "info");
        }

        /// <summary>
        /// pred ukončením poukladá potrebné atributy
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                if (cbxMesto.SelectedItem != null)
                {
                    //VybranyDopravnyBod = _dopravneBody.SingleOrDefault(c => c.Nazov == (string) cbxMesto.SelectedItem);
                    VybranyDopravnyBod = _dopravneBody[cbxMesto.SelectedIndex];
                    VybranaFaza = cbxSelektFiltra.SelectedItem is eVSVlakFaza
                        ? (eVSVlakFaza) cbxSelektFiltra.SelectedItem
                        : eVSVlakFaza.Pozadavek_zkonstruovano;
                    VybranyProjekt = _projekty.SingleOrDefault(c => c.Nazov == (string) cbxSelektProjektu.SelectedItem);
                    base.OnFormClosing(e);
                }
                else
                {
                    Mwbox("Pre pokračovanie je potrebné načítať dáta a zvoliť mesto", "Upozornenie");
                    e.Cancel = true;
                }
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

        /// <summary>
        /// Aktualizuje vybrané dáta a uloží ich do súboru
        /// </summary>
        /// <param name="text"></param>
        /// <param name="poseidon"></param>
        private void Aktualizuj(string text, PoseidonData poseidon)
        {
            string cesta = @"Data\" + VybranyProjekt.Nazov;
            VSEntitaBase[] data;
            try
            {
                switch (text)
                {
                    case "Dopravné body":
                        cesta += @"\MapDopravneBody.json";
                        data = poseidon.GetMapDopravneBody();
                        break;
                    case "Dopravné druhy":
                        cesta += "\\" + VybranaFaza + @"\MapDopravneDruhy.json";
                        data = poseidon.GetMapTrasaDopravneDruhy();
                        break;
                    case "Dopravné úseky":
                        cesta += @"\MapDopravneUseky.json";
                        data = poseidon.GetMapDopravneUseky();
                        break;
                    case "Poznámky":
                        DataZoSuboru.Zapis.DoSuboru(cesta + @"\ObecnaPoznamka.json", poseidon.GetObecnePoznamky());
                        cesta += "\\" + VybranaFaza + @"\MapTrasaObPoznamky.json";
                        data = poseidon.GetMapTrasaObecPozn();
                        break;
                    case "Trasa body":
                        cesta += "\\" + VybranaFaza + @"\MapTrasaBody.json";
                        data = poseidon.GetMapTrasaBody();
                        break;
                    case "Vlaky":
                        cesta += "\\" + VybranaFaza + @"\MapVlaky.json";
                        data = poseidon.GetMapVlaky();
                        break;
                    default:
                        return;
                }
            }
            catch (System.Net.WebException)
            {
                Mwbox("Nepodarilo sa stiahnúť dáta "+ text + " kvlôli timeout na servery","upozornenie");
                return;
            }

            DataZoSuboru.Zapis.DoSuboru(cesta,data);
        }
    }
}
