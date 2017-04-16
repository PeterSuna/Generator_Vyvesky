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

        public readonly PoseidonData KontrolerPoseidon = new PoseidonData();
        public VSProject VybranyProjekt { get; set; }
        public VSDopravnyBod VybranyDopravnyBod { get; set; }
        public eVSVlakFaza VybranaFaza { get; set; }
        public bool Online { get; set; }

        public UvitacieOkno(VSProject projekt, eVSVlakFaza faza)
        {
           
            InitializeComponent();
            try
            {
                _projekty = KontrolerPoseidon.Projekty;
                cbxSelektProjektu.DataSource = _projekty.Select(c => c.Nazov).ToList();
                cbxSelektFiltra.DataSource = Enum.GetValues(typeof(eVSVlakFaza));
            }
            catch (Exception)
            {
                string[] nazvi = {"gvd16", "gvd16_zm3", "gvd17_tsi", "gvd17_zaklad", "gvd17_zm2"};
                cbxSelektProjektu.DataSource = nazvi;
                Online = false;
                btnServer.Visible = false;
                Mwbox("Nebolo možne pripojiť sa k serveru", "chyba");
            }
            
    
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
            _dopravneBody = DataZoSuboru.Nacitaj.DopravneBodyZoSuboru(@"..\..\..\Projekt\");
            Online = false;
            InitCmbox();
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            VybranaFaza = cbxSelektFiltra.SelectedItem is eVSVlakFaza
                       ? (eVSVlakFaza)cbxSelektFiltra.SelectedItem
                       : eVSVlakFaza.Pozadavek_zkonstruovano;
            
            VybranyProjekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);
            KontrolerPoseidon.SelektProjektu(VybranaFaza, VybranyProjekt);
            _dopravneBody = KontrolerPoseidon.GetDopravneBody();
            cbxSelektFiltra.Visible = false;
            cbxSelektProjektu.Visible = false;
            lblFilter.Text = "Vybraná fáza: "+VybranaFaza;
            lblSelekt.Text = "Vybraný projekt: "+VybranyProjekt.Nazov;
            Online = true;
            InitCmbox();
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
    }
}
