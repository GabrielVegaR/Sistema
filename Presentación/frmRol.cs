using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentación
{
    public partial class frmRol : Form
    {
        public frmRol()
        {
            InitializeComponent();
        }

        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NRol.Listar();
                this.Formato();
                lblTotal.Text = $"Total registros:  + {dgvListado.RowCount}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmRol_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void Formato()
        {
            dgvListado.Columns[0].Width = 100;
            dgvListado.Columns[0].HeaderText = "ID";
            dgvListado.Columns[1].Width = 200;
            dgvListado.Columns[1].HeaderText = "Nombre";

        }
    }
}
