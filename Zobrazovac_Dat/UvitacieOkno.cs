using System;
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
    public partial class UvitacieOkno : Form
    {
        private VSDopravnyBod[] _dopravneBody;
        private VSProject[] _projekty;
        private readonly PoseidonData _kontrolerPoseidon = new PoseidonData();

        public VSProject VybranyProjekt { get; set; }
        public VSDopravnyBod VybranyDopravnyBod { get; set; }
        public eVSVlakFaza VybranaFaza { get; set; }
        public bool Online { get; set; }

        public UvitacieOkno()
        {
            InitializeComponent();
            try
            {
                _projekty = _kontrolerPoseidon.Projekty;
                cbxSelektProjektu.DataSource = _projekty.Select(c => c.Nazov).ToList();
                cbxSelektFiltra.DataSource = Enum.GetValues(typeof(eVSVlakFaza));
            }
            catch (Exception)
            {
                string[] nazvi = {"gvd16","gvd16_zm3","gvd17_tsi","gvd17_zaklad","gvd17_zm2"};
                cbxSelektProjektu.DataSource = nazvi;
                Online = false;
                Mbox("Nebolo možne pripojiť sa k serveru", "chyba", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnSubor_Click(object sender, EventArgs e)
        {
            _dopravneBody = Zapisovac.Zapisovac.NacitajDopravneBodyZoSuboru(@"..\..\..\Projekt\");
            InitCmbox();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            _dopravneBody = _kontrolerPoseidon.GetDopravneBody();
            InitCmbox();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (DialogResult == DialogResult.OK)
            {
                VybranyProjekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);
                if (Online)
                {
                    VybranaFaza = cbxSelektFiltra.SelectedItem is eVSVlakFaza
                        ? (eVSVlakFaza) cbxSelektFiltra.SelectedItem
                        : eVSVlakFaza.Pozadavek_zkonstruovano;
                }
                if (cbxMesto.SelectedItem != null)
                {
                    VybranyDopravnyBod = _dopravneBody.SingleOrDefault(c => c.Nazov == (string)cbxMesto.SelectedItem);
                }
                base.OnFormClosing(e);
            }
        }



        private void Mbox(string telo, string hlavicka, MessageBoxButtons btn, MessageBoxIcon icn)
        {
            MessageBox.Show(telo, hlavicka, btn, icn);
        }

        private void InitCmbox()
        {
            cbxMesto.DataSource = _dopravneBody.Select(c => c.Nazov).ToArray();
        }
    }
}
