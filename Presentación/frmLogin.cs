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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnAcceder_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable Tabla = new DataTable();
                Tabla = NUsuario.Login(textBox1.Text.Trim(), textBox2.Text.Trim());
                if (Tabla.Rows.Count <= 0)
                {
                    MessageBox.Show("El Correo o la contraseña son incorrectos", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (Convert.ToBoolean(Tabla.Rows[0][4]) == false)
                    {
                        MessageBox.Show("Este usuario esta inactivo", "Acceso al sistema", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        frmPrincipal frm = new frmPrincipal();
                        frm.IdUsuario = Convert.ToInt32(Tabla.Rows[0][0]);
                        Variable.IdUsuario = frm.IdUsuario = Convert.ToInt32(Tabla.Rows[0][0]);
                        frm.IdRol = Convert.ToInt32(Tabla.Rows[0][1]);
                        frm.Rol = Convert.ToString(Tabla.Rows[0][2]);
                        frm.Nombre = Convert.ToString(Tabla.Rows[0][3]);
                        frm.Estado = Convert.ToBoolean(Tabla.Rows[0][4]);
                        frm.Show();
                        this.Hide();
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
