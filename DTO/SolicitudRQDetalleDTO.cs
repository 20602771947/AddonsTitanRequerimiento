using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQDetalleDTO
    {
        public int IdSolicitudRQDetalle { get; set; }
        public int IdSolicitudCabecera { get; set; }
        public int IdArticulo { get; set; }
        public int IdUnidadMedida { get; set; }
        public DateTime FechaNecesaria { get; set; }
        public decimal CantidadNecesaria { get; set; }
        public decimal PrecioInfo { get; set; }
        public int IdIndicadorImpuesto { get; set; }
        public decimal Total { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProveedor { get; set; }
        public string NumeroFabricacion { get; set; }
        public int IdLineaNegocio { get; set; }
        public int IdCentroCostos { get; set; }
        public int IdProyecto { get; set; }
        public int IdMoneda { get; set; }
        public decimal TipoCambio { get; set; }
    }
}
