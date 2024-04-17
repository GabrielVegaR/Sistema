using Negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentación
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }

        public void Formato()
        {
            dgvListado.Columns[0].Visible = false;
            dgvListado.Columns[1].Visible = false;
            dgvListado.Columns[2].Width = 100;
            dgvListado.Columns[3].Width = 100;
            dgvListado.Columns[4].Width = 100;
            dgvListado.Columns[5].Width = 200;
            dgvListado.Columns[6].Width = 100;
            dgvListado.Columns[8].Width = 100;
            dgvListado.Columns[7].Width = 200;
            dgvListado.Columns[7].HeaderText = "Dirección";
            dgvListado.Columns[9].Width = 100;
            dgvListado.Columns[10].Width = 100;

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
            txtBuscar.Clear();
            txtDireccion.Clear();
            errorProvider1.Clear();
            txtNombre.Clear();
            txtNumDocumento.Clear();
            txtTelefono.Clear();
            txtEmail.Clear();
            txtClave.Clear();
            cbRol.Items.Clear();
            txtIdrol.Clear();
            cbTipoDocumento.SelectedIndex = -1;
            cbRol.SelectedIndex = -1;

            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            chkSeleccionar.Checked = false;
            btnEliminar.Visible = false;
        }

        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NUsuario.Listar();
                lblTotal.Text = $"Total de registro: {dgvListado.RowCount}";
                this.Limpiar();
                this.Formato();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            txtIdUser.Visible = false;
            txtIdrol.Visible = false;
            this.Listar();

            cbTipoDocumento.Items.Add("Cedula De IDentidad");
            cbTipoDocumento.Items.Add("Pasaporte");
            cbTipoDocumento.Items.Add("Acta De Nacimiento");

            dgvListado.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvListado_CellDoubleClick);
        }

        private void Buscar()
        {
            dgvListado.DataSource = NUsuario.Buscar(txtBuscar.Text);
            this.Formato();
            lblTotal.Text = $"Total de registro: {dgvListado.RowCount}";
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
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
                    respuesta = NUsuario.Insertar(int.Parse(txtIdrol.Text), txtNombre.Text, cbTipoDocumento.Text, txtNumDocumento.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, txtClave.Text);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Listar();
            tabGeneral.SelectedIndex = 0;

            btnActualizar.Visible = false;
            btnInsertar.Visible = true;
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta;
                if (txtEmail.Text == string.Empty || txtClave.Text == string.Empty)
                {
                    this.MensajeError("Ingresar los datos indicados");
                    errorProvider1.SetError(txtEmail, "ingrese un email");
                    errorProvider1.SetError(txtClave, "ingrese una clave");
                }
                else
                {
                    respuesta = NUsuario.Actualizar(int.Parse(txtIdUser.Text), int.Parse(txtIdrol.Text), txtNombre.Text, cbTipoDocumento.Text, txtNumDocumento.Text, txtDireccion.Text, txtTelefono.Text, txtEmail.Text, txtClave.Text);
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
                DialogResult opcion = MessageBox.Show("¿Desea activar este usuario?", "Sistema de ventas-activar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuario.Activar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se activo el usuario: {row.Cells[2].Value}");
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
                DialogResult opcion = MessageBox.Show("Desea Desactivar este usuario?", "Sistema de ventas-Desactivar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuario.Desactivar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se Desactivo el usuario: {row.Cells[2].Value}");
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
                DialogResult opcion = MessageBox.Show("Desea eliminar este usuario?", "Sistema de ventas-eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NUsuario.Eliminar(codigo);

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

        private void cbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtIdrol.Clear();

            try
            {
                DataTable roles = NUsuario.ListarRoles();

                foreach (DataRow fila in roles.Rows)
                {
                    var name = fila["nombre"].ToString();
                    var id = fila["idrol"].ToString();

                    if (name == cbRol.SelectedItem.ToString())
                    {
                        txtIdrol.Text = id.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void cbRol_Click(object sender, EventArgs e)
        {
            try
            {
                cbRol.Items.Clear();

                DataTable roles = NUsuario.ListarRoles();

                foreach (DataRow fila in roles.Rows)
                {
                    var name = fila["nombre"].ToString();
                    cbRol.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los roles: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex != -1 && e.ColumnIndex != -1)
            {
                try
                {
                    this.Limpiar();
                    label10.Visible = true;
                    btnActualizar.Visible = true;
                    btnInsertar.Visible = false;

                    int idUsuario = Convert.ToInt32(dgvListado.CurrentRow.Cells["ID"].Value);
                    string email = dgvListado.CurrentRow.Cells["Email"].Value.ToString();

                    txtIdUser.Text = idUsuario.ToString();
                    txtNombre.Text = dgvListado.CurrentRow.Cells["nombre"].Value.ToString();
                    cbTipoDocumento.Text = dgvListado.CurrentRow.Cells["tipo_documento"].Value.ToString();
                    cbRol.Text = dgvListado.CurrentRow.Cells["Rol"].Value.ToString();
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
    }
}
