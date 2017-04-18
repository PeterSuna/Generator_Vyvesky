using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Data_Kontroler;
using Service_Konektor.poseidon;
using GeneratorVyvesky;
using Service_Konektor.Entity;

namespace Zobrazovac_Dat
{
    public partial class Form1 : Form
    {
        private PoseidonData _kontrolerPoseidon;
        private VSProject[] _projekty;
        private bool _online;
        private VSProject _projekt;
        private eVSVlakFaza _faza;
        private VSDopravnyBod _vybranyDopBod;
        private VSTrasaBod[] _trasBody;
        private VSVlak[] _vlaky;
        private VSDopravnyBod[] _dopravneBody;


        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            _kontrolerPoseidon = NastavData(true);

        }

        /// <summary>
        /// Metóda zavolé uvítacie okno v ktorom sa nastavujú potrebné data
        /// </summary>
        /// <param name="uvitanie">true pri štarte ak je okno vypnuté vypne sa aj program | false ak sa menia len nastavenia</param>
        /// <returns></returns>
        private PoseidonData NastavData(bool uvitanie)
        {
            PoseidonData poseidon;
            if (uvitanie)
            {
                poseidon = new PoseidonData();
            }
            else
            {
                poseidon = _kontrolerPoseidon;
            }
            using (var uvitacieOkno = new UvitacieOkno(_projekt, _faza))
            {

                uvitacieOkno.ShowDialog(this);
                if (uvitacieOkno.DialogResult == DialogResult.OK)
                {
                    _vybranyDopBod = uvitacieOkno.VybranyDopravnyBod;
                    _faza = uvitacieOkno.VybranaFaza;
                    _projekt = uvitacieOkno.VybranyProjekt;
                    _online = uvitacieOkno.Online;
                    poseidon = uvitacieOkno.KontrolerPoseidon;

                }
                else
                {
                    if (uvitanie)
                    {
                        Close();
                    }
                }
                if (!_online)
                {
                    btnGenVlaky.Visible = false;
                    btnGenTrasaBody.Visible = false;
                    btnGenerujDopBod.Visible = false;
                }
            }
            return poseidon;
        }


        private void btnGenVlaky_Click(object sender, EventArgs e)
        {
            if (!_online)
            {
                Mwbox("Uzivatel nieje pripojený na server", "chyba");
            }
            else
            {

                dgvVlaky.DataSource = _kontrolerPoseidon.GetVlaky();
            }
        }

        private void btnGenTrasaBody_Click(object sender, EventArgs e)
        {
            if (!_online)
            {
                Mwbox("Uzivatel nieje pripojený na server", "chyba");
            }
            else
            {

                dgvVlaky.DataSource = _kontrolerPoseidon.GetTrasy();


            }
        }

        private void btnGenerujDopBod_Click(object sender, EventArgs e)
        {
            if (!_online)
            {
                Mwbox("Uzivatel nieje pripojený na server", "chyba");
            }
            else
            {
                //dgvVlaky.DataSource = _kontrolerPoseidon.GetDopravneBody();
                dgvVlaky.DataSource = _kontrolerPoseidon.Poseidon.GetProjects();
            }
        }


        private void btnZapisDoSuboru_Click(object sender, EventArgs e)
        {
            if (dgvVlaky.DataSource == null)
            {
                Mwbox("Data neboli načítane", "chyba");
            }
            else
            {
                string cesta = @"..\..\..\Projekt\";
                string cestaProj = @"..\..\..\Projekt\" + _projekt.Nazov;
                if (dgvVlaky.DataSource is VSTrasaBod[])
                {
                    cestaProj += @"\TrasaBody";
                    DataZoSuboru.Zapis.TrasaBodyDoSuboruCasti(cestaProj, dgvVlaky.DataSource as VSTrasaBod[]);
                }
                if (dgvVlaky.DataSource is VSVlak[])
                {
                    cestaProj += @"\Vlaky\Vlaky.json";
                    DataZoSuboru.Zapis.DoSuboru(cestaProj, dgvVlaky.DataSource as VSEntitaBase[]);
                }
                if (dgvVlaky.DataSource is VSDopravnyBod[])
                {
                    cesta += @"\DopravneBody.json";
                    DataZoSuboru.Zapis.DoSuboru(cesta, dgvVlaky.DataSource as VSDopravnyBod[]);
                }
                if (dgvVlaky.DataSource is VSTrasaSpecifikace[])
                {
                    cestaProj += @"\Specifikacie\Specifikacie.json";
                    DataZoSuboru.Zapis.DoSuboru(cestaProj, dgvVlaky.DataSource as VSTrasaSpecifikace[]);
                }
                if (dgvVlaky.DataSource is VSTrasaDruh[])
                {
                    cestaProj += @"\TrasaDruh\DopravneDruhy.json";
                    DataZoSuboru.Zapis.DoSuboru(cestaProj, dgvVlaky.DataSource as VSTrasaDruh[]);
                }
                if (dgvVlaky.DataSource is VSObecnaPoznamka[])
                {
                    cestaProj += @"\Poznamky\ObecnaPoznamka.json";
                    DataZoSuboru.Zapis.DoSuboru(cestaProj, dgvVlaky.DataSource as VSObecnaPoznamka[]);
                }
                if (dgvVlaky.DataSource is VSTrasaObecPozn[])
                {
                    cestaProj += @"\Poznamky\TrasaObPoznamky.json";
                    DataZoSuboru.Zapis.DoSuboru(cestaProj, dgvVlaky.DataSource as VSTrasaObecPozn[]);
                }
                if (dgvVlaky.DataSource is VSProject[])
                {
                    cesta += @"\Projekty.json";
                    DataZoSuboru.Zapis.DoSuboru(cesta, dgvVlaky.DataSource as VSProject[]);
                }
                if (dgvVlaky.DataSource is MapTrasaBod[])
                {
                    cestaProj += @"\MapTrasaBody.json";
                    DataZoSuboru.Zapis.DoSuboru(cestaProj, dgvVlaky.DataSource as MapTrasaBod[]);
                }

                MessageBox.Show("Data Boli uložené", "Oznam", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void btnNacitaj_Click(object sender, EventArgs e)
        {
            string cesta = @"..\..\..\Projekt\" + _projekt.Nazov + @"\Vlaky";

            VSVlak[] vlaky = DataZoSuboru.Nacitaj.VlakyZoSuboru(cesta);
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
            string cesta = @"..\..\..\Projekt\";

            var dopravneBody = DataZoSuboru.Nacitaj.DopravneBodyZoSuboru(cesta);
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
            var data = DataZoSuboru.Nacitaj.ZoSuboruMapTrasBody(@"..\..\..\Projekt\" + _projekt.Nazov);
            dgvVlaky.DataSource = data;
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
            catch (IOException ex)
            {
                Mwbox("Vyvesku sa nepodarilo uložiť, je potrebné zavrieť Prichody.docx dokument", "upozornenie");
            }

        }

        private Generator VytvorGenerator()
        {
            string cesta = @"..\..\..\Projekt\" + _projekt.Nazov + @"\Vlaky";
            var trasaBody = DataZoSuboru.Nacitaj.ZoSuboruMapTrasBody(@"..\..\..\Projekt\" + _projekt.Nazov);
            var trasaBodyVybStanice = FilterDat.TrasaBod.NajdiPodlaDopravnehoBodu(_vybranyDopBod.ID, trasaBody);
            var vlaky = FilterDat.Vlak.NajdiVlakyVTrasaBody(trasaBodyVybStanice,
                DataZoSuboru.Nacitaj.VlakyZoSuboru(cesta));
            var trasaBodyVlakov = FilterDat.TrasaBod.NajdiTrasyPoldaVlaku(vlaky, trasaBody);

            Generator gen = new Generator(trasaBodyVlakov, vlaky, trasaBodyVybStanice, _vybranyDopBod, @"..\..\..\Projekt\" + _projekt.Nazov, _projekt);
            return gen;
        }

        /// <summary>
        /// Nacíta vsetky TrasaBody ktoré boli uložené do viacerých súborou podla vybraného projektu
        /// </summary>
        /// <returns></returns>
        private VSTrasaBod[] NacitajVsetkyTrsaBody()
        {
            string cesta = @"..\..\..\Projekt\" + _projekt.Nazov + @"\TrasaBody";
            DirectoryInfo d = new DirectoryInfo(cesta);
            FileInfo[] files = d.GetFiles("*.json");
            List<VSTrasaBod> trasaBodyList = new List<VSTrasaBod>();

            if (_trasBody != null)
            {
                trasaBodyList = _trasBody.OfType<VSTrasaBod>().ToList();
            }
            foreach (FileInfo file in files)
            {
                if (_trasBody == null)
                {
                    _trasBody = DataZoSuboru.Nacitaj.TrasaBodyZoSuboru(cesta, file.Name);
                    trasaBodyList = _trasBody.OfType<VSTrasaBod>().ToList();
                }
                trasaBodyList.AddRange(DataZoSuboru.Nacitaj.TrasaBodyZoSuboru(cesta, file.Name));
            }
            return trasaBodyList.ToArray();
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
            catch (IOException ex)
            {
                Mwbox("Vyvesku sa nepodarilo uložiť, je potrebné zavrieť Odchody.docx dokument", "upozornenie");
            }
        }
    }
}
