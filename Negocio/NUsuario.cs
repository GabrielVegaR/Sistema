using Datos;
using Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NUsuario
    {
        public static DataTable Listar()
        {
            DUsuario Datos = new DUsuario();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Buscar(valor);
        }

        public static string Insertar(int idRol, string nombre, string tipoDocumento, string numDocumento, string direccion, string telefono, string email, string clave)
        {
            DUsuario Datos = new DUsuario();

            string existe = Datos.Existe(nombre);
            if (existe == "1")
            {
                return "El articulo ya no existe";
            }
            else
            {
                Usuario obj = new Usuario();
                obj.IdRol = idRol;
                obj.Nombre = nombre;
                obj.TipoDocumento = tipoDocumento;
                obj.NumDocumento = numDocumento;
                obj.Direccion = direccion;
                obj.Telefono = telefono;
                obj.Email = email;
                obj.Clave = clave;
                return Datos.Insertar(obj);
            }
        }

        public static string Actualizar(int idUsuario, int idRol, string nombre, string tipoDocumento, string numDocumento, string direccion, string telefono, string email, string clave)
        {
            DUsuario Datos = new DUsuario();

            Usuario obj = new Usuario();
            obj.IdUsuario = idUsuario;
            obj.IdRol = idRol;
            obj.Nombre = nombre;
            obj.TipoDocumento = tipoDocumento;
            obj.NumDocumento = numDocumento;
            obj.Direccion = direccion;
            obj.Telefono = telefono;
            obj.Email = email;
            obj.Clave = clave;
            return Datos.Actualizar(obj);
        }

        public static string Eliminar(int idUsuario)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Eliminar(idUsuario);
        }

        public static string Activar(int idUsuario)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Activar(idUsuario);
        }

        public static string Desactivar(int idUsuario)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Desactivar(idUsuario);
        }

        public static DataTable ListarRoles()
        {
            DUsuario Datos = new DUsuario();
            return Datos.ListarRoles();
        }

        public static DataTable Login(string email, string clave)
        {
            DUsuario Datos = new DUsuario();
            return Datos.Login(email, clave);
        }
    }
}
