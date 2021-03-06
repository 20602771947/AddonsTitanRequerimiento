using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQDTO
    {
        public int IdSolicitudRQ { get; set; }
        public int IdSerie { get; set; }
        public string Serie { get; set; }
        public int Numero { get; set; }
        public int IdSolicitante { get; set; }
        public string Solicitante { get; set; }
        public int IdSucursal { get; set; }
        public int IdDepartamento { get; set; }
        public int IdClaseArticulo { get; set; }
        public int IdMoneda { get; set; }
        public decimal TipoCambio { get; set; }
        public int IdTitular { get; set; }
        public decimal TotalAntesDescuento { get; set; }
        public int IdIndicadorImpuesto { get; set; }
        public decimal Impuesto { get; set; }
        public decimal Total { get; set; }
        public DateTime FechaContabilizacion { get; set; }
        public DateTime FechaValidoHasta { get; set; }
        public DateTime FechaDocumento { get; set; }
        public string Comentarios { get; set; }
        public int Estado { get; set; }
        public int Prioridad { get; set; }
        public string DetalleEstado { get; set; }

        public IList<SolicitudDetalleDTO> Detalle;

        public List<SolicitudRQAnexos> DetalleAnexo { get; set; }
        public IList<SolicitudRQAnexos> DetallesAnexo;
    }


    public class SolicitudRQAnexos
    {
        public int IdSolicitudRQAnexos { get; set; }
        public int IdSolicitud { get; set; }
        public string Nombre { get; set; }
        public int IdSociedad { get; set; }
    }
}
