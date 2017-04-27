using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Service_Konektor.poseidon;
using GeneratorVyvesky;
using Service_Konektor.Entity;

namespace Zobrazovac_Dat
{
    public partial class HlavneOkno : Form
    {
        private VSProject _projekt;
        private eVSVlakFaza _faza;
        private MapDopravnyBod _vybranyDopBod;

        public string CestaProj { get; set; }
        public string CestaProjFaz { get; set; }


        public HlavneOkno()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            NastavData(true);
            base.OnLoad(e);
            //MapVlak[] vlaky = DataZoSuboru.Nacitaj.MapVlaky(CestaProjFaz + "MapVlaky.json");
            //if (vlaky == null)
            //{
            //    Mwbox("Data neboli ulozene", "chyba");
            //}
            //else
            //{
            //    dgvVlaky.DataSource = vlaky;
            //}

            //var buttonCol = new DataGridViewCheckBoxColumn();
            //buttonCol.Name = "cbxVymazať";
            //buttonCol.HeaderText = "Vymazať vlak";

            //dgvVlaky.Columns.Add(buttonCol);

            //foreach (DataGridViewRow row in dgvVlaky.Rows)
            //{
            //    //var button = (Button)row.Cells["ButtonColumnName"].Value;
            //    row.Cells["cbxVymazať"].Value = "ButtonText";
            //    // button is null here!
            //}
        }

        /// <summary>
        /// Metóda zavolé uvítacie okno v ktorom sa nastavujú potrebné data
        /// </summary>
        /// <param name="uvitanie">true pri štarte ak je okno vypnuté vypne sa aj program | false ak sa menia len nastavenia</param>
        /// <returns></returns>
        private void NastavData(bool uvitanie)
        {
            using (var uvitacieOkno = new UvitacieOkno(_projekt, _faza))
            {
                uvitacieOkno.ShowDialog(this);
                if (uvitacieOkno.DialogResult == DialogResult.OK)
                {
                    _vybranyDopBod = uvitacieOkno.VybranyDopravnyBod;
                    _faza = uvitacieOkno.VybranaFaza;
                    _projekt = uvitacieOkno.VybranyProjekt;
                    CestaProj = @"Data\" + _projekt.Nazov;
                    CestaProjFaz = @"Data\" + _projekt.Nazov + "\\" + _faza;
                }
                else
                {
                    if (uvitanie)
                    {
                        Close();
                    }
                }
            }
        }


        /// <summary>
        /// nacíta a zobrazí vlaky
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNacitaj_Click(object sender, EventArgs e)
        {
            MapVlak[] vlaky = DataZoSuboru.Nacitaj.MapVlaky(Path.Combine(CestaProjFaz, "MapVlaky.json"));
            if (vlaky == null)
            {
                Mwbox("Data neboli nájdené", "chyba");
            }
            else
            {
                dgvVlaky.DataSource = vlaky;
            }
        }

        /// <summary>
        /// nacíta dopravné body a zobrazí ich
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNacitajDopravneBody_Click(object sender, EventArgs e)
        {
            var dopravneBody = DataZoSuboru.Nacitaj.MapDopravneBody(Path.Combine(CestaProj, "MapDopravneBody.json"));
            if (dopravneBody == null)
            {
                Mwbox("Data neboli nájdené", "chyba");
            }
            else
            {
                dgvVlaky.DataSource = dopravneBody;
            }
        }

        /// <summary>
        /// nacíta a zobrazí všetky trasa body
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNacitajTrasyBody_Click(object sender, EventArgs e)
        {
            var data = DataZoSuboru.Nacitaj.MapTrasBody(Path.Combine(CestaProjFaz, "MapTrasaBody.json"));
            if (data == null)
            {
                Mwbox("Data neboli nájdené", "chyba");
            }
            else
            {
                dgvVlaky.DataSource = data;
            }
        }


        private void btnPoznamky_Click(object sender, EventArgs e)
        {
            var obpozn = DataZoSuboru.Nacitaj.ObecnuPoznam(Path.Combine(CestaProj, "ObecnaPoznamka.json"));
            var trasaobpozn = DataZoSuboru.Nacitaj.TrasaObPozn(Path.Combine(CestaProjFaz, "MapTrasaObPoznamky.json"));
            if (obpozn == null && trasaobpozn==null)
            {
                Mwbox("Data neboli nájdené", "chyba");
            }

        }

        /// <summary>
        /// zavolanie nastavovacieho okna pre zmenu údajov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNastav_Click(object sender, EventArgs e)
        {
            NastavData(false);
        }

        /// <summary>
        /// vytvorenie vývesky príchodov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDocx_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                while (true)
                {
                    var generator = VytvorGenerator();
                    i = generator.GenerujPrichodyDocxSubor(i);
                    if (i >= generator.TrasaBodyVybStanice.Length)
                    {
                        break;
                    }
                }
            }
            catch (IOException)
            {
                Mwbox("Vyvesku sa nepodarilo uložiť, je potrebné zavrieť Prichody.docx dokument", "Upozornenie");
            }

        }

        /// <summary>
        /// vytvorenie vývesky odchodov
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerujOdchody_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                while (true)
                {
                    var generator = VytvorGenerator();
                    if (generator == null)
                    {
                        return;
                    }
                    i = generator.GenerujOdchodyDocxSubor(i);
                    if (i >= generator.TrasaBodyVybStanice.Length)
                    {
                        break;
                    }
                }
            }
            catch (IOException)
            {
                Mwbox("Vyvesku sa nepodarilo uložiť, je potrebné zavrieť Odchody.docx dokument", "Upozornenie");
            }
        }

        /// <summary>
        /// Vytvorenie inštancie generátora
        /// </summary>
        /// <returns></returns>
        private Generator VytvorGenerator()
        {
            try
            {
                Generator gen = new Generator(_vybranyDopBod, CestaProjFaz, _projekt);
                return gen;
            }
            catch (ApplicationException e)
            {
                Mwbox("Nacítane dáta niesu kompletné skúste nanovo aktualizovať všetky dáta","Upozornenie");
                return null;
            }  
            
        }


        /// <summary>
        /// Upozornenie užívateľa
        /// </summary>
        /// <param name="telo"></param>
        /// <param name="hlavicka"></param>
        private void Mwbox(string telo, string hlavicka)
        {
            MessageBox.Show(telo, hlavicka, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

    }
}
