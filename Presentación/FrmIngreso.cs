using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Negocio;
using Presentación;

namespace Sistema.Presentacion
{
    public partial class FrmIngreso : Form
    {

        private DataTable DtDetalle = new DataTable();
        public FrmIngreso()
        {
            InitializeComponent();
        }
        private void Listar()
        {
            try
            {
                DgvListado.DataSource = NIngreso.Listar();
                this.Formato();
                this.Limpiar();
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Buscar()
        {
            try
            {
                DgvListado.DataSource = NIngreso.Buscar(TxtBuscar.Text);
                this.Formato();
                LblTotal.Text = "Total registros: " + Convert.ToString(DgvListado.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void Formato()
        {
            DgvListado.Columns[0].Visible = false;
            DgvListado.Columns[1].Visible = false;
            DgvListado.Columns[2].Visible = false;
            DgvListado.Columns[0].Width = 100;
            DgvListado.Columns[3].Width = 150;
            DgvListado.Columns[4].Width = 150;
            DgvListado.Columns[5].Width = 100;
            DgvListado.Columns[5].HeaderText = "Documento";
            DgvListado.Columns[6].Width = 70;
            DgvListado.Columns[6].HeaderText = "Serie";
            DgvListado.Columns[7].Width = 70;
            DgvListado.Columns[7].HeaderText = "Número";
            DgvListado.Columns[8].Width = 60;
            DgvListado.Columns[9].Width = 100;
            DgvListado.Columns[10].Width = 100;
            DgvListado.Columns[11].Width = 100;
        }
        private void Limpiar()
        {
            TxtBuscar.Clear();
            TxtId.Clear();
            TxtCodigo.Clear();
            TxtIdProveedor.Clear();
            TxtNombreProveedor.Clear();
            TxtSerieComprobante.Clear();
            TxtNumComprobante.Clear();
            DtDetalle.Clear();
            TxtSubTotal.Text = "0.00";
            TxtTotalImpuesto.Text = "0.00";
            TxtTotal.Text = "0.00";

            DgvListado.Columns[0].Visible = false;
            BtnAnular.Visible = false;
            ChkSeleccionar.Checked = false;
        }
        private void MensajeError(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MensajeOk(string Mensaje)
        {
            MessageBox.Show(Mensaje, "Sistema de ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CrearTabla()
        {
            this.DtDetalle.Columns.Add("idarticulo", System.Type.GetType("System.Int32"));
            this.DtDetalle.Columns.Add("codigo", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("articulo", System.Type.GetType("System.String"));
            this.DtDetalle.Columns.Add("cantidad", System.Type.GetType("System.Int32"));
            this.DtDetalle.Columns.Add("precio", System.Type.GetType("System.Decimal"));
            this.DtDetalle.Columns.Add("importe", System.Type.GetType("System.Decimal"));

            DgvDetalle.DataSource = this.DtDetalle;

            DgvDetalle.Columns[0].Visible = false;
            DgvDetalle.Columns[1].HeaderText = "CODIGO";
            DgvDetalle.Columns[1].Width = 100;
            DgvDetalle.Columns[2].HeaderText = "ARTICULO";
            DgvDetalle.Columns[2].Width = 200;
            DgvDetalle.Columns[3].HeaderText = "CANTIDAD";
            DgvDetalle.Columns[3].Width = 70;
            DgvDetalle.Columns[4].HeaderText = "PRECIO";
            DgvDetalle.Columns[4].Width = 70;
            DgvDetalle.Columns[5].HeaderText = "IMPORTE";
            DgvDetalle.Columns[5].Width = 80;

            DgvDetalle.Columns[1].ReadOnly = true;
            DgvDetalle.Columns[2].ReadOnly = true;
            DgvDetalle.Columns[5].ReadOnly = true;
        }

        private void FormatoArticulos()
        {
            DgvArticulos.Columns[1].Visible = false;
            DgvArticulos.Columns[2].Width = 100;
            DgvArticulos.Columns[2].HeaderText = "Categoría";
            DgvArticulos.Columns[3].Width = 100;
            DgvArticulos.Columns[3].HeaderText = "Código";
            DgvArticulos.Columns[4].Width = 150;
            DgvArticulos.Columns[5].Width = 100;
            DgvArticulos.Columns[5].HeaderText = "Precio Venta";
            DgvArticulos.Columns[6].Width = 60;
            DgvArticulos.Columns[7].Width = 200;
            DgvArticulos.Columns[7].HeaderText = "Descripción";
            DgvArticulos.Columns[8].Width = 100;
        }
        private void FrmIngreso_Load(object sender, EventArgs e)
        {
            this.Listar();
            this.CrearTabla();
        }
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            this.Buscar();
        }

        private void BtnBuscarProveedor_Click(object sender, EventArgs e)
        {
            FrmVista_ProveedorIngreso frm = new FrmVista_ProveedorIngreso();
            frm.ShowDialog();

            TxtIdProveedor.Text = Convert.ToString(Variable.IdProveedor);
            TxtNombreProveedor.Text = Variable.NombreProveedor;

        }

        private void TxtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    DataTable Tabla = new DataTable();
                    Tabla = NArticulos.BuscarCodigo(TxtCodigo.Text);
                    if (Tabla.Rows.Count == 0)
                    {
                        this.MensajeError("No existe artículo con ese codigo de barras");
                    }
                    else
                    {
                        this.AgregarDetalle(Convert.ToInt32(Tabla.Rows[0][0]),
                            Convert.ToString(Tabla.Rows[0][1]),
                            Convert.ToString(Tabla.Rows[0][2]),
                            Convert.ToDecimal(Tabla.Rows[0][3])
                            );
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void AgregarDetalle(int IdArticulo, string Codigo, string Nombre, decimal Precio)
        {
            bool agregar = true;

            foreach (DataRow row in DtDetalle.Rows)
            {
                if (Convert.ToInt32(row["idarticulo"]) == IdArticulo)
                {
                    agregar = false;
                    this.MensajeError("El articulo ya ha sido agregado");
                }
            }

            if (agregar)
            {
                DataRow fila = DtDetalle.NewRow();
                fila["idarticulo"] = IdArticulo;
                fila["codigo"] = Codigo;
                fila["nombre"] = Nombre;
                fila["cantidad"] = 1;
                fila["precio"] = Precio;
                this.DtDetalle.Rows.Add(fila);
                this.CalcularTotales();
            }

        }

        private void CalcularTotales()
        {
            decimal total = 0;
            decimal subTotal = 0;

            if (DgvDetalle.Rows.Count == 0)
            {
                total = 0;
            }
            else
            {
                foreach (DataRow item in DtDetalle.Rows)
                {
                    total = total + Convert.ToDecimal(item["importe"]);
                }

                subTotal = total / (1 + Convert.ToDecimal(TxtImpuesto.Text));
                TxtTotal.Text = total.ToString("#0.00#");
                TxtSubTotal.Text = subTotal.ToString("#0.00#");
                TxtTotalImpuesto.Text = (total - subTotal).ToString("#0.00#");

            }
        }

        private void BtnVerArticulos_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = true;

        }

        private void BtnCerrarArticulos_Click(object sender, EventArgs e)
        {
            PanelArticulos.Visible = false;
        }

        private void BtnFiltrarArticulos_Click(object sender, EventArgs e)
        {
            try
            {
                DgvArticulos.DataSource = NArticulos.Buscar(TxtBuscarArticulo.Text);
                this.FormatoArticulos();
                LblTotalArticulos.Text = "total Registro: " + Convert.ToString(DgvArticulos.Rows.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DgvArticulos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int IdArticulo;
            string Codigo;
            string Nombre;
            decimal Precio;

            IdArticulo = Convert.ToInt32(DgvArticulos.CurrentRow.Cells["ID"].Value);
            Codigo = Convert.ToString(DgvArticulos.CurrentRow.Cells["Codigo"].Value);
            Nombre = Convert.ToString(DgvArticulos.CurrentRow.Cells["Nombre"].Value);
            Precio = Convert.ToDecimal(DgvArticulos.CurrentRow.Cells["Precio_Venta"].Value);
            this.AgregarDetalle(IdArticulo, Codigo, Nombre, Precio);
        }

        private void DgvDetalle_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataRow Fila = (DataRow)DtDetalle.Rows[e.RowIndex];
            decimal precio = Convert.ToDecimal(Fila["precio"]);
            int Cantidad = Convert.ToInt32(Fila["dantidad"]);
            Fila["importe"] = precio * Cantidad;
            this.CalcularTotales();
        }   

        private void DgvDetalle_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.DtDetalle.Rows.RemoveAt(e.RowIndex);
            this.CalcularTotales();
        }

        private void BtnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                string respuesta = "";
                if (TxtNumComprobante.Text == string.Empty || TxtIdProveedor.Text == string.Empty || DtDetalle.Rows.Count == 0 || TxtTotal.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados");
                    ErrorIcono.SetError(TxtIdProveedor, "Ingrese un proveedor");
                    ErrorIcono.SetError(DgvDetalle, "Ingrese al menos un artículo");
                    ErrorIcono.SetError(TxtTotal, "Ingrese el total de la compra");
                }
                else
                {
                    respuesta = NIngreso.Insertar(Convert.ToInt32(TxtIdProveedor.Text), Variable.IdUsuario, 
                        CboComprobante.Text, 
                        TxtSerieComprobante.Text.Trim(),
                        TxtNumComprobante.Text.Trim(), 
                        Convert.ToDecimal(TxtImpuesto.Text),
                        Convert.ToDecimal(TxtTotal.Text), 
                        DtDetalle);
                    if (respuesta.Equals("OK"))
                    {
                        this.MensajeOk("Se insertó de forma correcta el registro");
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

        private void DgvListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnCerrarDetalle_Click(object sender, EventArgs e)
        {

        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {

        }

        private void DgvListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ChkSeleccionar_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BtnAnular_Click(object sender, EventArgs e)
        {

        }
    }
}
