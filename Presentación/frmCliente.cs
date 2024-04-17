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
    public partial class frmCliente : Form
    {
        public frmCliente()
        {
            InitializeComponent();
        }
        public void Formato()
        {
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Width = 50;
            dgvListado.Columns[2].Width = 100;
            dgvListado.Columns[2].HeaderText = "Tipo Persona";
            dgvListado.Columns[3].Width = 150;
            dgvListado.Columns[4].Width = 100;
            dgvListado.Columns[4].HeaderText = "Documento";
            dgvListado.Columns[5].Width = 100;
            dgvListado.Columns[5].HeaderText = "Número Documento";
            dgvListado.Columns[6].Width = 100;
            dgvListado.Columns[6].HeaderText = "Dirección";
            dgvListado.Columns[7].Width = 100;
            dgvListado.Columns[7].HeaderText = "Teléfono";
            dgvListado.Columns[8].Width = 100;
        }

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
            txtDireccion.Clear();
            txtEmail.Clear();
            txtNombre.Clear();
            txtNumDocumento.Clear();
            txtTelefono.Clear();
            cmbTipoDoc.Text = string.Empty;
        }

        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NPersona.Listar();
                lblTotal.Text = $"Total de registro: {dgvListado.RowCount}";
                this.Limpiar();
                this.Formato();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Buscar()
        {
            dgvListado.DataSource = NPersona.Buscar(txtBuscar.Text);
            this.Formato();
            lblTotal.Text = $"Total de registro: {dgvListado.RowCount}";
        }

        private void btnInsertar_Click(object sender, EventArgs e)
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
                    respuesta = NPersona.Insertar("Cliente", txtNombre.Text.Trim(), cmbTipoDoc.Text, txtNumDocumento.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text);
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
            this.Limpiar();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (txtId.Text == string.Empty || txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Debe ingresar todos los campos requeridos");
                    errorProvider1.SetError(txtNombre, "Ingrese el nombre");
                }
                else
                {
                    respuesta = NPersona.Actualizar(int.Parse(txtId.Text), "Cliente", txtNombreAnt.Text, txtNombre.Text.Trim(), cmbTipoDoc.Text, txtNumDocumento.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text);
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

            btnActualizar.Visible = false;
            btnInsertar.Visible = true;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("Desea eliminar este Proveedor?", "Sistema de ventas-eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NPersona.Eliminar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se elimino el usuario: {row.Cells[2].Value}");
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
                btnEliminar.Visible = true;
            }
            else
            {
                dgvListado.Columns[0].Visible = false;
                btnEliminar.Visible = false;
            }
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                try
                {
                    this.Limpiar();
                    btnActualizar.Visible = true;
                    btnInsertar.Visible = false;

                    int idUsuario = Convert.ToInt32(dgvListado.CurrentRow.Cells["ID"].Value);
                    string email = dgvListado.CurrentRow.Cells["Email"].Value.ToString();
                    txtId.Text = idUsuario.ToString();
                    txtNombre.Text = dgvListado.CurrentRow.Cells["nombre"].Value.ToString();
                    cmbTipoDoc.Text = dgvListado.CurrentRow.Cells["tipo_documento"].Value.ToString();
                    txtNumDocumento.Text = dgvListado.CurrentRow.Cells["num_documento"].Value.ToString();
                    txtDireccion.Text = dgvListado.CurrentRow.Cells["direccion"].Value.ToString();
                    txtTelefono.Text = dgvListado.CurrentRow.Cells["telefono"].Value.ToString();
                    txtEmail.Text = email;

                    tabGeneral.SelectedIndex = 1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ocurrió un error al intentar cargar los datos: " + ex.Message);
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            this.Listar();
            cmbTipoDoc.Items.Add("Cedula De IDentidad");
            cmbTipoDoc.Items.Add("Pasaporte");
            cmbTipoDoc.Items.Add("Acta De Nacimiento");
        }
    }
}
