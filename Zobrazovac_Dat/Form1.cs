using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void btnOK_Click(object sender, EventArgs e)
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
                var projekt = _projekty.SingleOrDefault(c => c.Nazov == (string) cbxSelektProjektu.SelectedItem);
                _kontroler.SelektProjektu(faza, projekt);
                dgvVlaky.DataSource = _kontroler.GetVlaky();
            }
        }

        private void btnZapisDoSuboru_Click(object sender, EventArgs e)
        {
            if (dgvVlaky.DataSource == null)
            {
                Mbox("Vlaky neboli načítane", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                ZapisovacDocX.Zapisovac.ZapisUdajeDoSuboru(dgvVlaky.DataSource as VSVlak[]);
            }
           
        }

        private void btnNacitaj_Click(object sender, EventArgs e)
        {
            VSVlak[] vlaky = ZapisovacDocX.Zapisovac.NacitajZoSuboru(null);
            if (vlaky == null)
            {
                Mbox("Data neboli ulozene", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dgvVlaky.DataSource = vlaky;
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
                    Mbox("Pri generovani nastala chyba","Chyba",MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void Mbox(string telo, string hlavicka, MessageBoxButtons btn, MessageBoxIcon icn)
        {
            MessageBox.Show(telo, hlavicka, btn, icn);
        }

    }
}
