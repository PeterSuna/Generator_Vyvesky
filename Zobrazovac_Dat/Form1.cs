using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data_Kontroler;
using Service_Konektor.poseidon;

namespace Zobrazovac_Dat
{
    public partial class Form1 : Form
    {
        private readonly PoseidonData _kontrolerPoseidon = new PoseidonData();
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
            NastavData();

        }

        private void NastavData()
        {
            using (var uvitacieOkno = new UvitacieOkno())
            {

                uvitacieOkno.ShowDialog(this);
                if (uvitacieOkno.DialogResult == DialogResult.OK)
                {
                    _vybranyDopBod = uvitacieOkno.VybranyDopravnyBod;
                    _faza = uvitacieOkno.VybranaFaza;
                    _projekt = uvitacieOkno.VybranyProjekt;
                    _online = uvitacieOkno.Online;

                }
                else
                {
                   this.Close();
                }
                if (_online)
                {
                    _kontrolerPoseidon.SelektProjektu(_faza, _projekt);
                }
            }
        }

        private void btnGenerujVyvesku_Click(object sender, EventArgs e)
        {
            if (dgvVlaky.DataSource == null)
            {
                Mbox("Vlaky neboli načítane", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (ZapisovacDocX.Zapisovac.GenerujVyvesku(null, dgvVlaky.DataSource as VSVlak[]))
                {
                    Mbox("Data boli vygenerovane", "Inform", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    Mbox("Pri generovani nastala chyba", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnGenVlaky_Click(object sender, EventArgs e)
        {
            if (!_online)
            {
                Mbox("Uzivatel nieje pripojený na server", "chyba", MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                Mbox("Uzivatel nieje pripojený na server", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                
                dgvVlaky.DataSource = _kontrolerPoseidon.Poseidon.GetTrasaBody();


            }
        }

        private void btnGenerujDopBod_Click(object sender, EventArgs e)
        {
            if (!_online)
            {
                Mbox("Uzivatel nieje pripojený na server", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dgvVlaky.DataSource = _kontrolerPoseidon.Poseidon.GetDopravneBody();
            }
        }

        private void btnGenerujData_Click(object sender, EventArgs e)
        {
            if (_vybranyDopBod == null)
            {
                Mbox("Pre zobrazenie vlakou prechádzajúcich stanicou je potrebné v nastaveniach vybrať stanicu", "Upozornenie", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _trasBody = NacitajVsetkyTrsaBody();
            var body = FilterDat.TrasaBod.NajdiPodlaDopravnehoBodu(_vybranyDopBod.ID, _trasBody);
            dgvVlaky.DataSource = body;
        }

        private void btnZapisDoSuboru_Click(object sender, EventArgs e)
        {
            if (dgvVlaky.DataSource == null)
            {
                Mbox("Data neboli načítane", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string path = @"..\..\..\Projekt\" + _projekt.Nazov;
                if (dgvVlaky.DataSource is VSTrasaBod[])
                {
                    path += @"\TrasaBody";
                    ZapisovacDocX.Zapisovac.ZapisTrasaBodyDoSuboryCasti(path, dgvVlaky.DataSource as VSTrasaBod[]);
                }
                if (dgvVlaky.DataSource is VSVlak[])
                {
                    path += @"\Vlaky";
                    ZapisovacDocX.Zapisovac.ZapisVlakyDoSuboru(path, dgvVlaky.DataSource as VSVlak[]);
                }
                if (dgvVlaky.DataSource is VSDopravnyBod[])
                {
                    string cesta = @"..\..\..\Projekt\";
                    ZapisovacDocX.Zapisovac.ZapisDoSuboruDopravneBody(cesta, dgvVlaky.DataSource as VSDopravnyBod[]);
                }
                
            }
           
        }

        private void btnNacitaj_Click(object sender, EventArgs e)
        {
            string cesta = @"..\..\..\Projekt\" + _projekt.Nazov + @"\Vlaky";

            VSVlak[] vlaky = ZapisovacDocX.Zapisovac.NacitajVlakyZoSuboru(cesta);
            if (vlaky == null)
            {
                Mbox("Data neboli ulozene", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dgvVlaky.DataSource = vlaky;
            }

        }

        private void btnNacitajDopravneBody_Click(object sender, EventArgs e)
        {
            string cesta = @"..\..\..\Projekt\";

            var dopravneBody = ZapisovacDocX.Zapisovac.NacitajDopravneBodyZoSuboru(cesta);
            if (dopravneBody == null)
            {
                Mbox("Data neboli ulozene", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            NastavData();
        }

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
                    _trasBody = ZapisovacDocX.Zapisovac.NacitajTrasaBodyZoSuboru(cesta, file.Name);
                    trasaBodyList = _trasBody.OfType<VSTrasaBod>().ToList();
                }
                trasaBodyList.AddRange(ZapisovacDocX.Zapisovac.NacitajTrasaBodyZoSuboru(cesta, file.Name));
            }
            return trasaBodyList.ToArray();
        }

        private void Mbox(string telo, string hlavicka, MessageBoxButtons btn, MessageBoxIcon icn)
        {
            MessageBox.Show(telo, hlavicka, btn, icn);
        }
    }
}
