using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class AlmacenDTO
    {
        public int IdAlmacen { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public int IdSucursal { get; set; }
        public string Sucursal { get; set; }
    }
}
