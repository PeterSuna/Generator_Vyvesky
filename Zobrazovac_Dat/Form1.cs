using System;
using System.IO;
using System.Windows.Forms;
using Service_Konektor.poseidon;
using GeneratorVyvesky;
using Service_Konektor.Entity;

namespace Zobrazovac_Dat
{
    public partial class Form1 : Form
    {
        private VSProject _projekt;
        private eVSVlakFaza _faza;
        private MapDopravnyBod _vybranyDopBod;

        public string Cesta { get; set; }
        public string CestaProjekt  { get; set; }


        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            NastavData(true);
            base.OnLoad(e);
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
                    Cesta = @"..\..\..\Projekt\";
                    CestaProjekt = @"..\..\..\Projekt\" + _projekt.Nazov+"\\";
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


        private void btnNacitaj_Click(object sender, EventArgs e)
        {
            MapVlak[] vlaky = DataZoSuboru.Nacitaj.MapVlaky(Cesta + "MapVlaky.json");
            if (vlaky == null)
            {
                Mwbox("Data neboli ulozene", "chyba");
            }
            else
            {
                dgvVlaky.DataSource = vlaky;
            }
        }

        private void btnNacitajDopravneBody_Click(object sender, EventArgs e)
        {
            var dopravneBody = DataZoSuboru.Nacitaj.MapDopravneBody(CestaProjekt+"MapDopravneBody.json");
            if (dopravneBody == null)
            {
                Mwbox("Data neboli ulozene", "chyba");
            }
            else
            {
                dgvVlaky.DataSource = dopravneBody;
            }
        }

        private void btnNacitajTrasyBody_Click(object sender, EventArgs e)
        {
            var data = DataZoSuboru.Nacitaj.MapTrasBody(CestaProjekt+"MapTrasaBody.json");
            if (data == null)
            {
                Mwbox("Data neboli ulozene", "chyba");
            }
            else
            {
                dgvVlaky.DataSource = data;
            }
        }

        private void btnNastav_Click(object sender, EventArgs e)
        {
            NastavData(false);
        }

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
                Mwbox("Vyvesku sa nepodarilo uložiť, je potrebné zavrieť Prichody.docx dokument", "upozornenie");
            }

        }

        private void btnGenerujOdchody_Click(object sender, EventArgs e)
        {
            try
            {
                int i = 0;
                while (true)
                {
                    var generator = VytvorGenerator();
                    i = generator.GenerujOdchodyDocxSubor(i);
                    if (i >= generator.TrasaBodyVybStanice.Length)
                    {
                        break;
                    }
                }
            }
            catch (IOException)
            {
                Mwbox("Vyvesku sa nepodarilo uložiť, je potrebné zavrieť Odchody.docx dokument", "upozornenie");
            }
        }

        private Generator VytvorGenerator()
        {
            var trasaBody = DataZoSuboru.Nacitaj.MapTrasBody(CestaProjekt + "MapTrasaBody.json");
            var trasaBodyVybStanice = FilterDat.TrasaBod.NajdiPodlaDopravnehoBodu(_vybranyDopBod.ID, trasaBody);
            var vlaky = FilterDat.Vlak.NajdiVlakyVTrasaBody(trasaBodyVybStanice,
                DataZoSuboru.Nacitaj.MapVlaky(CestaProjekt + "MapVlaky.json"));
            var trasaBodyVlakov = FilterDat.TrasaBod.NajdiTrasyPoldaVlaku(vlaky, trasaBody);
            var trasaBodyUzly =
                FilterDat.TrasaBod.NajdiDopravnéUzly(
                    DataZoSuboru.Nacitaj.MapDopravneUseky(CestaProjekt + "MapDopravneUseky.json"), trasaBodyVlakov);

            Generator gen = new Generator(trasaBodyUzly, vlaky, trasaBodyVybStanice, _vybranyDopBod, CestaProjekt,
                _projekt);
            return gen;
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
