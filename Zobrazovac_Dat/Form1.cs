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
                string path = @"..\..\..\Projekt\" + _projekt.Nazov;
                if (dgvVlaky.DataSource is VSTrasaBod[])
                {
                    path += @"\TrasaBody";
                    DataZoSuboru.Zapis.TrasaBodyDoSuboryCasti(path, dgvVlaky.DataSource as VSTrasaBod[]);
                }
                if (dgvVlaky.DataSource is VSVlak[])
                {
                    path += @"\Vlaky";
                    DataZoSuboru.Zapis.VlakyDoSuboru(path, dgvVlaky.DataSource as VSVlak[]);
                }
                if (dgvVlaky.DataSource is VSDopravnyBod[])
                {

                    DataZoSuboru.Zapis.DoSuboruDopravneBody(cesta, dgvVlaky.DataSource as VSDopravnyBod[]);
                }
                if (dgvVlaky.DataSource is VSTrasaSpecifikace[])
                {
                    path += @"\Specifikacie";
                    DataZoSuboru.Zapis.SpecifikacieDoSuboru(path, dgvVlaky.DataSource as VSTrasaSpecifikace[]);
                }
                if (dgvVlaky.DataSource is VSTrasaDruh[])
                {
                    path += @"\TrasaDruh";
                    DataZoSuboru.Zapis.TrasaDopravneDruhyDoSuboru(path, dgvVlaky.DataSource as VSTrasaDruh[]);
                }
                if (dgvVlaky.DataSource is VSObecnaPoznamka[])
                {
                    path += @"\Poznamky";
                    DataZoSuboru.Zapis.ObecnePoznamky(path, dgvVlaky.DataSource as VSObecnaPoznamka[]);
                }
                if (dgvVlaky.DataSource is VSTrasaObecPozn[])
                {
                    path += @"\Poznamky";
                    DataZoSuboru.Zapis.TrasaObecnePoznamky(path, dgvVlaky.DataSource as VSTrasaObecPozn[]);
                }
                if (dgvVlaky.DataSource is VSProject[])
                {
                    DataZoSuboru.Zapis.ProjektyDoSuboru(cesta, dgvVlaky.DataSource as VSProject[]);
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
            var data = NacitajVsetkyTrsaBody();
            dgvVlaky.DataSource = data;
        }

        private void btnNastav_Click(object sender, EventArgs e)
        {
            NastavData(false);
        }

        private void btnDocx_Click(object sender, EventArgs e)
        {
            string cesta = @"..\..\..\Projekt\" + _projekt.Nazov + @"\Vlaky";
            var trasaBody = NacitajVsetkyTrsaBody();
            var trasaBodyVybStanice = FilterDat.TrasaBod.NajdiPodlaDopravnehoBodu(_vybranyDopBod.ID, trasaBody);
            var vlaky = FilterDat.Vlak.NajdiVlakyVTrasaBody(trasaBodyVybStanice,
                DataZoSuboru.Nacitaj.VlakyZoSuboru(cesta));
            var trasaBodyVlakov = FilterDat.TrasaBod.NajdiTrasyPoldaVlaku(vlaky, trasaBody);
            
                Generator gen = new Generator(trasaBodyVlakov, vlaky, trasaBodyVybStanice, _vybranyDopBod, @"..\..\..\Projekt\" + _projekt.Nazov, _projekt);
            try
            {
                gen.GenerujDocxSubor();
            }
            catch (IOException ex)
            {
                Mwbox("Vyvesku sa nepodarilo uložiť, je potrebné zavrieť result.docx dokument","upozornenie");
            }
           
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

        
    }
}
