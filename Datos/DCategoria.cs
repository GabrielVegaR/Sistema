using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class DCategoria
    {
        //listar las categorias
        public DataTable Listar()
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_listar", sqlcon);
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

        //buscar una categoria
        public DataTable Buscar(string valor)
        {
            SqlDataReader Resultado;
            DataTable Tabla = new DataTable();
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_buscar", sqlcon);
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
                SqlCommand Comando = new SqlCommand("categoria_existe", sqlcon);
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

        public string Insertar(Categoria obj)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_insertar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@Nombre", SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo ingresar el registro";
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

        public string Actualizar(Categoria obj)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_actualizar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idCategoria", SqlDbType.Int).Value = obj.IdCategoria;
                Comando.Parameters.Add("@nombre", SqlDbType.VarChar).Value = obj.Nombre;
                Comando.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = obj.Descripcion;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo actualizar el registro";
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

        public string Eliminar(int IdCategoria)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_eliminar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idCategoria", SqlDbType.Int).Value = IdCategoria;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo eliminar el registro";
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

        public string Desactivar(int IdCategoria)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_desactivar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idCategoria", SqlDbType.Int).Value = IdCategoria;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo desactivar el registro";
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

        public string Activar(int IdCategoria)
        {
            string Respuesta = "";
            SqlConnection sqlcon = new SqlConnection();
            try
            {
                sqlcon = Conexion.getInstancia().CrearConexion();
                SqlCommand Comando = new SqlCommand("categoria_activar", sqlcon);
                Comando.CommandType = CommandType.StoredProcedure;
                Comando.Parameters.Add("@idCategoria", SqlDbType.Int).Value = IdCategoria;
                sqlcon.Open();
                Respuesta = Comando.ExecuteNonQuery() == 1 ? "OK" : "No se pudo activar el registro";
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
    }
}
