using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class SolicitudRQModeloAprobacionesDTO
    {
        public int IdSolicitudRQModeloAprobaciones { get; set; }
        public int IdSolicitudModelo { get; set; }
        public int IdAutorizador { get; set; }
        public DateTime FechaAprobacion { get; set; }
        public int Accion { get; set; }
        public int IdSociedad { get; set; }
    }
}
