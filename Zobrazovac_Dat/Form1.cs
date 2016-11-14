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
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            try
            {
                _projekty = _kontroler.Projekty;
            }
            catch (Exception)
            {

            }
            cbxSelektProjektu.DataSource = _projekty.Select(c => c.Nazov).ToList();
            cbxSelektFiltra.DataSource = Enum.GetValues(typeof(eVSVlakFaza));

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            var faza = cbxSelektFiltra.SelectedItem is eVSVlakFaza ? (eVSVlakFaza) cbxSelektFiltra.SelectedItem : eVSVlakFaza.Pozadavek_zkonstruovano;
            var projekt = _projekty.SingleOrDefault(c => c.Nazov == (string)cbxSelektProjektu.SelectedItem);
            _kontroler.SelektProjektu(faza, projekt);
            dgvVlaky.DataSource = _kontroler.GetVlaky();
        }
    }
}
