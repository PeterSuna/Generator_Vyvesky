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
        private readonly PoseidonData _kontroler = new PoseidonData();
        private VSProject[] _projekty;
        private bool _online;
        private VSProject _projekt;
        private VSTrasaBod[] _trasBody;
        private VSVlak[] _vlaky;

        public Form1()
        {
            InitializeComponent();
            _online = true;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _projekty = _kontroler.Projekty;
                cbxSelektProjektu.DataSource = _projekty.Select(c => c.Nazov).ToList();
                cbxSelektFiltra.DataSource = Enum.GetValues(typeof(eVSVlakFaza));
            }
            catch (Exception)
            {
                _online = false;
                Mbox("Nebolo možne pripojiť sa k serveru","chyba",MessageBoxButtons.OK,MessageBoxIcon.Warning);
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
                var faza = cbxSelektFiltra.SelectedItem is eVSVlakFaza
                    ? (eVSVlakFaza) cbxSelektFiltra.SelectedItem
                    : eVSVlakFaza.Pozadavek_zkonstruovano;
                _projekt = _projekty.SingleOrDefault(c => c.Nazov == (string) cbxSelektProjektu.SelectedItem);
                _kontroler.SelektProjektu(faza, _projekt);
                dgvVlaky.DataSource = _kontroler.GetVlaky();
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
                //var faza = cbxSelektFiltra.SelectedItem is eVSVlakFaza
                //    ? (eVSVlakFaza)cbxSelektFiltra.SelectedItem
                //    : eVSVlakFaza.Pozadavek_zkonstruovano;
                //var projekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);
                //_kontroler.SelektProjektu(faza, projekt);
                dgvVlaky.DataSource = _kontroler.GetTrasy();


            }
        }

        private void btnZapisDoSuboru_Click(object sender, EventArgs e)
        {
            if (dgvVlaky.DataSource == null)
            {
                Mbox("Data neboli načítane", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                _projekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);
                String path = @"..\..\..\Projekt\" + _projekt.Nazov;
                if (dgvVlaky.DataSource is VSTrasaBod[])
                {
                    path += @"\TrasaBody";
                    ZapisovacDocX.Zapisovac.ZapisTrasaBodyDoSuboru(path, dgvVlaky.DataSource as VSTrasaBod[]);
                }
                if (dgvVlaky.DataSource is VSVlak[])
                {
                    path += @"\Vlaky";
                    ZapisovacDocX.Zapisovac.ZapisVlakyDoSuboru(path, dgvVlaky.DataSource as VSVlak[]);
                }
                
            }
           
        }

        private void btnNacitaj_Click(object sender, EventArgs e)
        {
            _projekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);
            String cesta = @"..\..\..\Projekt\" + _projekt.Nazov + @"\Vlaky";

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

        private void btnNacitajTrasyBody_Click(object sender, EventArgs e)
        {
            _projekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);
            String cesta = @"..\..\..\Projekt\" + _projekt.Nazov + @"\TrasaBody";
            DirectoryInfo d = new DirectoryInfo(cesta);
            FileInfo[] files = d.GetFiles("*.json");
            string str = "";
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
            dgvVlaky.DataSource = trasaBodyList.ToArray();
        }

        private void Mbox(string telo, string hlavicka, MessageBoxButtons btn, MessageBoxIcon icn)
        {
            MessageBox.Show(telo, hlavicka, btn, icn);
        }
    }
}
