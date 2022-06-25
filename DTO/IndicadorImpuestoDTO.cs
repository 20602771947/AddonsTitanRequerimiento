using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class IndicadorImpuestoDTO
    {

        public int IdIndicadorImpuesto { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Porcentaje { get; set; }
        public bool Estado { get; set; }

    }
}
