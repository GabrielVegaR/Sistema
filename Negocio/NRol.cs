using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Datos;

namespace Negocio
{
    public class NRol
    {
        public static DataTable Listar()
        {
            DRol Datos = new DRol();
            return Datos.Listar();
        }
    }
}
