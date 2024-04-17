using Negocio;
using System;
using System.Windows.Forms;

namespace Presentación
{
    public partial class frmCategoria : Form
    {
        public frmCategoria()
        {
            InitializeComponent();
        }

        public void Formato()
        {
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Visible = false;
            dgvListado.Columns[2].Width = 150;
            dgvListado.Columns[3].Width = 300;
            dgvListado.Columns[3].HeaderText = "Descripción";
            dgvListado.Columns[4].Width = 100;
        }

        //Mensaje de error
        private void MensajeError(string msg)
        {
            MessageBox.Show(msg, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //Mensaje de error
        private void MensajeOK(string msg)
        {
            MessageBox.Show(msg, "sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Limpiar()
        {
            txtBuscar.Clear();
            txtDescripcion.Clear();
            txtId.Clear();
            btnActualizar.Visible = false;
            btnInsertar.Visible = true;

            errorProvider1.Clear();
            txtNombre.Clear();

            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            chkSeleccionar.Checked = false;


        }

        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NCategoria.Listar();
                lblTotal.Text = $"Total de registro: {dgvListado.RowCount}";
                this.Limpiar();
                this.Formato();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        //Metodo para buscar una categoria
        private void Buscar()
        {
            dgvListado.DataSource = NUsuario.Buscar(txtBuscar.Text);
            this.Formato();
            lblTotal.Text = $"Total de registro: {dgvListado.RowCount}";
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void btninsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Debe ingresar todos los campos requeridos");
                    errorProvider1.SetError(txtNombre, "Ingrese el nombre");
                }
                else
                {
                    respuesta = NCategoria.Insertar(txtNombre.Text.Trim(), txtDescripcion.Text.Trim());
                    if (respuesta == "OK")
                    {
                        this.MensajeOK("El registro se inserto de manera correcta");
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Listar();
            tabGeneral.SelectedIndex = 0;
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtId.Text = dgvListado.CurrentRow.Cells["ID"].Value.ToString();
                txtNombre.Text = dgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                txtDescripcion.Text = dgvListado.CurrentRow.Cells["Descripcion"].Value.ToString();
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Seleccione una celda a partir del nombre");
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta;
                if (txtNombre.Text == string.Empty || txtId.Text == string.Empty)
                {
                    this.MensajeError("Ingresar los datos indicados");
                    errorProvider1.SetError(txtNombre, "ingrese un Nombre");
                }
                else
                {
                    respuesta = NCategoria.Actualizar(Convert.ToInt32(txtId.Text), txtNombre.Text, txtDescripcion.Text);
                    if (respuesta == "OK")
                    {
                        this.MensajeOK("Se actualizo el registro de manera correcta");
                        this.Limpiar();
                        this.Listar();
                    }
                    else
                    {
                        this.MensajeError(respuesta);
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("¿Desea activar esta categoria?", "Sistema de ventas-activar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NCategoria.Activar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se activo la categoria: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnDesactivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("Desea Desactivar esta categoria?", "Sistema de ventas-Desactivar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NCategoria.Desactivar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se Desactivo la categoria: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("Desea eliminar esta categoria?", "Sistema de ventas-eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NCategoria.Eliminar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se elimino la categoria: {row.Cells[2].Value}");
                            }
                            else
                            {
                                this.MensajeError(respuesta);
                            }
                        }

                    }
                    this.Listar();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void chkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSeleccionar.Checked)
            {
                dgvListado.Columns[0].Visible = true;
                btnActivar.Visible = true;
                btnDesactivar.Visible = true;
                btnEliminar.Visible = true;
            }
            else
            {
                dgvListado.Columns[0].Visible = false;
                btnActivar.Visible = false;
                btnDesactivar.Visible = false;
                btnEliminar.Visible = false;
            }

        }

        private void dgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListado.Columns["seleccionar"].Index)
            {
                DataGridViewCheckBoxCell chkeliminar = (DataGridViewCheckBoxCell)dgvListado.Rows[e.RowIndex].Cells["seleccionar"];
                chkeliminar.Value = !Convert.ToBoolean(chkeliminar.Value);
            }
        }
    }
}
