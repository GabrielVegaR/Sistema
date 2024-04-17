using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class NArticulos
    {
        public static DataTable Listar()
        {
            DArticulos Datos = new DArticulos();
            return Datos.Listar();
        }

        public static DataTable CategoriaListar()
        {
            DArticulos Datos = new DArticulos();
            return Datos.CategoriaListar();
        }

        public static DataTable Buscar(string valor)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Buscar(valor);
        }
        public static DataTable BuscarCodigo(string valor)
        {
            DArticulos Datos = new DArticulos();
            return Datos.BuscarCodigo(valor);
        }

        public static string Insertar(int idCategoria, string codigo, string nombre, decimal precioVenta, int stock, string descripcion, string imagen)
        {
            DArticulos Datos = new DArticulos();

            string existe = Datos.Existe(nombre);
            if (existe == "1")
            {
                return "El articulo ya no existe";
            }
            else
            {
                Articulos obj = new Articulos();
                obj.IdCategoria = idCategoria;
                obj.Codigo = codigo;
                obj.Nombre = nombre;
                obj.PrecioVenta = precioVenta;
                obj.Stock = stock;
                obj.Descripcion = descripcion;
                obj.Imagen = imagen;
                return Datos.Insertar(obj);
            }
        }

        public static string Actualizar(int idCategoria,int idArtculo, string codigo, string nombre, decimal precioVenta, int stock, string descripcion, string imagen)        
        {
            DArticulos Datos = new DArticulos();

            Articulos obj = new Articulos();
            obj.IdArticulo = idArtculo;
            obj.IdCategoria = idCategoria;
            obj.Codigo = codigo;
            obj.Nombre = nombre;
            obj.PrecioVenta = precioVenta;
            obj.Stock = stock;
            obj.Descripcion = descripcion;
            obj.Imagen = imagen;

            return Datos.Actualizar(obj);
        }

        public static string Eliminar(int idArticulo)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Eliminar(idArticulo);
        }

        public static string Activar(int idArticulo)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Activar(idArticulo);
        }

        public static string Desactivar(int idArticulo)
        {
            DArticulos Datos = new DArticulos();
            return Datos.Desactivar(idArticulo);
        }


    }
}
