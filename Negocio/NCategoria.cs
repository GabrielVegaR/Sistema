using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;
using System.Data;

namespace Negocio
{
    public class NCategoria
    {
        public static DataTable Listar()
        {
            DCategoria Datos = new DCategoria();
            return Datos.Listar();
        }

        public static DataTable Buscar(string valor)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Buscar(valor);
        }

        public static string Insertar(string nombre, string descripcion)
        {
            DCategoria Datos = new DCategoria();

            string existe = Datos.Existe(nombre);
            if (existe == "1")
            {
                return "la categoria ya no existe";
            }
            else
            {
                Categoria obj = new Categoria();
                obj.Nombre = nombre;
                obj.Descripcion = descripcion;
                return Datos.Insertar(obj);
            }
        }

        public static string Actualizar(int idCategoria, string nombre, string descripcion)
        {
            DCategoria Datos = new DCategoria();

            Categoria obj = new Categoria();
            obj.IdCategoria = idCategoria;
            obj.Nombre = nombre;
            obj.Descripcion = descripcion;

            return Datos.Actualizar(obj);
        }

        public static string Eliminar(int idCategoria)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Eliminar(idCategoria);
        }

        public static string Activar(int idCategoria)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Activar(idCategoria);
        }

        public static string Desactivar(int idCategoria)
        {
            DCategoria Datos = new DCategoria();
            return Datos.Desactivar(idCategoria);
        }
    }
}
