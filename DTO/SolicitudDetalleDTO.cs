using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudDetalleDTO
    {

        public int IdSolicitudRQDetalle { get; set; }
        public int IdSolicitudCabecera { get; set; }
        public int IdArticulo { get; set; }
        public int IdUnidadMedida { get; set; }
        public DateTime FechaNecesaria { get; set; }
        public decimal CantidadNecesaria { get; set; }
        public decimal PrecioInfo { get; set; }
        public int IdIndicadorImpuesto { get; set; }
        public decimal ItemTotal { get; set; }
        public int IdAlmacen { get; set; }
        public int IdProveedor { get; set; }
        public string NumeroFabricacion { get; set; }
        public string NumeroSerie { get; set; }
        public int IdLineaNegocio { get; set; }
        public int IdCentroCostos { get; set; }
        public int IdProyecto { get; set; }
        public int IdItemMoneda { get; set; }
        public decimal ItemTipoCambio { get; set; }
        public string Referencia { get; set; }
        public string DescripcionItem { get; set; }
    }
}
