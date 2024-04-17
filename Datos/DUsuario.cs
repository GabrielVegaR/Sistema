using Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DUsuario
    {
        public DataTable Listar()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_listar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }
        public DataTable ListarRoles()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("rol_listar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                sqlcon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }

        public DataTable Buscar(string valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_buscar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                sqlcon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }

        public string Existe(string valor)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_existe", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;
                SqlParameter parExiste = new SqlParameter();
                parExiste.ParameterName = "@existe";
                parExiste.SqlDbType = SqlDbType.Int;
                parExiste.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(parExiste);
                sqlcon.Open();
                Comando.ExecuteNonQuery();
                Respuesta = Convert.ToString(parExiste.Value);
            }
            catch (Exception ex)
            {

                Respuesta = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
            return Respuesta;
        }

        public string Insertar(Usuario obj)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_insertar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("idrol", SqlDbType.VarChar).Value = obj.IdRol;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = obj.TipoDocumento;
                Comando.Parameters.Add("num_documento", SqlDbType.VarChar).Value = obj.NumDocumento;
                Comando.Parameters.Add("direccion", SqlDbType.VarChar).Value = obj.Direccion;
                Comando.Parameters.Add("telefono", SqlDbType.VarChar).Value = obj.Telefono;
                Comando.Parameters.Add("email", SqlDbType.VarChar).Value = obj.Email;
                Comando.Parameters.Add("clave", SqlDbType.VarChar).Value = obj.Clave;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el usuario";
            }
            catch (Exception ex)
            {

                Respuesta = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
            return Respuesta;
        }

        public string Actualizar(Usuario obj)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_actualizar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("idrol", SqlDbType.VarChar).Value = obj.IdRol;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = obj.IdUsuario;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@tipo_documento", SqlDbType.VarChar).Value = obj.TipoDocumento;
                Comando.Parameters.Add("num_documento", SqlDbType.VarChar).Value = obj.NumDocumento;
                Comando.Parameters.Add("direccion", SqlDbType.VarChar).Value = obj.Direccion;
                Comando.Parameters.Add("telefono", SqlDbType.VarChar).Value = obj.Telefono;
                Comando.Parameters.Add("email", SqlDbType.VarChar).Value = obj.Email;
                Comando.Parameters.Add("clave", SqlDbType.VarChar).Value = obj.Clave;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el usuario";
            }
            catch (Exception ex)
            {

                Respuesta = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
            return Respuesta;
        }

        public string Eliminar(int IdUsuario)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_eliminar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = IdUsuario;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el usuario";
            }
            catch (Exception ex)
            {

                Respuesta = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
            return Respuesta;
        }

        public string Desactivar(int IdUsuario)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_desactivar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = IdUsuario;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el usuario";
            }
            catch (Exception ex)
            {

                Respuesta = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
            return Respuesta;
        }

        public string Activar(int IdUsuario)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_activar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idusuario", SqlDbType.Int).Value = IdUsuario;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el usuario";
            }
            catch (Exception ex)
            {

                Respuesta = ex.Message;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
            return Respuesta;
        }

        public DataTable Login(string email, string clave)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("usuario_login", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
                Comando.Parameters.Add("@clave", SqlDbType.VarChar).Value = clave;
                sqlcon.Open();
                Resultado = Comando.ExecuteReader();
                Tabla.Load(Resultado);
                return Tabla;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (sqlcon.State == ConnectionState.Open)
                {
                    sqlcon.Close();
                }
            }
        }

    }
}
