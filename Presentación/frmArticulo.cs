using Negocio;
using System;
using System.Windows.Forms;
using BarcodeLib;
using System.Drawing;
using System.Data;
using System.Runtime.Remoting.Contexts;
using Entidades;

namespace Presentación
{
    public partial class frmArticulo : Form
    {
        private object barcodelib;

        public frmArticulo()
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
            dgvListado.Columns[5].Width = 100;
            dgvListado.Columns[6].Width = 100;
            dgvListado.Columns[8].Width = 250;
            dgvListado.Columns[8].HeaderText = "Descripción";
            dgvListado.Columns[7].Width = 100;
            dgvListado.Columns[9].Width = 100;

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
            txtDescripcion.Clear();
            errorProvider1.Clear();
            txtNombre.Clear();
            txtCodigoBarras.Clear();
            txtImagen.Clear();

            btnActivar.Visible = false;
            btnDesactivar.Visible = false;
            chkSeleccionar.Checked = false;
            btnEliminar.Visible = false;
        }

        private void Listar()
        {
            try
            {
                dgvListado.DataSource = NArticulos.Listar();
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
            dgvListado.DataSource = NArticulos.Buscar(txtBuscar.Text);
            this.Formato();
            lblTotal.Text = $"Total de registro: {dgvListado.RowCount}";
        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            this.Listar();
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
                    respuesta = NArticulos.Insertar(int.Parse(txtId.Text), txtCodigoBarras.Text, txtNombre.Text, Decimal.Parse(txtPrecioVenta.Text), int.Parse(txtStock.Text), txtDescripcion.Text, txtImagen.Text);
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
        }

        private void dgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Limpiar();
                btnActualizar.Visible = true;
                btnInsertar.Visible = false;
                txtId.Text = dgvListado.CurrentRow.Cells["ID"].Value.ToString();
                txtCodigoBarras.Text = dgvListado.CurrentRow.Cells["CodigoBarras"].Value.ToString();
                txtNombre.Text = dgvListado.CurrentRow.Cells["Nombre"].Value.ToString();
                txtPrecioVenta.Text = dgvListado.CurrentRow.Cells["Precio Venta"].Value.ToString();
                txtStock.Text = dgvListado.CurrentRow.Cells["Stock"].Value.ToString();
                txtDescripcion.Text = dgvListado.CurrentRow.Cells["Descripcion"].Value.ToString();
                txtImagen.Text = dgvListado.CurrentRow.Cells["Imagen"].Value.ToString();
                tabGeneral.SelectedIndex = 1;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Seleccione una celta a partir del nombre");
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
                    errorProvider1.SetError(txtNombre, "ingrese un nombre");
                }
                else
                {
                    respuesta = NArticulos.Actualizar(Convert.ToInt32(txtId.Text), Convert.ToInt32(txtIdArticulo.Text), txtCodigoBarras.Text, txtNombre.Text, Convert.ToDecimal(txtPrecioVenta.Text), Convert.ToInt32(txtStock.Text), txtDescripcion.Text, txtImagen.Text);
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

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            try
            {
                Barcode CodigoBarras = new Barcode();

                panel1.BackgroundImage = CodigoBarras.Encode(BarcodeLib.TYPE.CODE128, txtCodigoBarras.Text, Color.Black, Color.White);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }

        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {           
            try
            {
                Image ImgBarras = (Image)panel1.BackgroundImage.Clone();
                SaveFileDialog sfd = new SaveFileDialog();

                sfd.AddExtension = true;
                sfd.Filter = "png image (.png) | *.png*";
                sfd.ShowDialog();

                if (!string.IsNullOrEmpty(sfd.FileName))
                {
                    ImgBarras.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
                ImgBarras.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnPuntos_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();

            ofd.Filter = "image files (.jpg, .jpge, .png) | *.jpg; *.jpge; *.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Image.FromFile(ofd.FileName);
                txtImagen.Text = ofd.FileName;
                txtImagen.Text = ofd.FileName.Substring(ofd.FileName.LastIndexOf("\\") + 1);
            }
        }

        private void cbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtId.Clear();

            try
            {
                DataTable categorias = NArticulos.CategoriaListar();

                foreach (DataRow fila in categorias.Rows)
                {
                    var name = fila["nombre"].ToString();
                    var id = fila["idcategoria"].ToString();

                    if (name == cbCategoria.SelectedItem.ToString())
                    {
                        txtId.Text = id.ToString();
                    }
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void cbCategoria_Click(object sender, EventArgs e)
        {
            try
            {
                cbCategoria.Items.Clear();

                DataTable categorias = NArticulos.CategoriaListar();

                foreach (DataRow fila in categorias.Rows)
                {
                    var name = fila["nombre"].ToString();
                    cbCategoria.Items.Add(name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar las categorías: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }       

        private void btnActivar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion = MessageBox.Show("¿Desea activar este articulo?", "Sistema de ventas-activar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NArticulos.Activar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se activo el articulo: {row.Cells[2].Value}");
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
                DialogResult opcion = MessageBox.Show("Desea Desactivar este articulo?", "Sistema de ventas-Desactivar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NArticulos.Desactivar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se Desactivo el articulo: {row.Cells[2].Value}");
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
                DialogResult opcion = MessageBox.Show("Desea eliminar este articulo?", "Sistema de ventas-eliminar", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    int codigo;
                    string respuesta = "";

                    foreach (DataGridViewRow row in dgvListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            codigo = Convert.ToInt32(row.Cells[1].Value);
                            respuesta = NArticulos.Eliminar(codigo);

                            if (respuesta == "OK")
                            {
                                this.MensajeOK($"se elimino el articulo: {row.Cells[2].Value}");
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
