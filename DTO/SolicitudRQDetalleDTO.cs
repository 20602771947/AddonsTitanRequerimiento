﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQDetalleDTO
    {
        public List<int> IdSolicitudRQDetalle { get; set; }
        public List<int> IdSolicitudCabecera { get; set; }
        public List<int> IdArticulo { get; set; }
        public List<int> IdUnidadMedida { get; set; }
        public List<DateTime> FechaNecesaria { get; set; }
        public List<decimal> CantidadNecesaria { get; set; }
        public List<decimal> PrecioInfo { get; set; }
        public List<int> IdIndicadorImpuesto { get; set; }
        public List<decimal> ItemTotal { get; set; }
        public List<int> IdAlmacen { get; set; }
        public List<int> IdProveedor { get; set; }
        public List<string> NumeroFabricacion { get; set; }
        public List<string> NumeroSerie { get; set; }
        public List<int> IdLineaNegocio { get; set; }
        public List<int> IdCentroCostos { get; set; }
        public List<int> IdProyecto { get; set; }
        public List<int> IdItemMoneda { get; set; }
        public List<decimal> ItemTipoCambio { get; set; }
        public List<string> Referencia { get; set; }
    }
}
