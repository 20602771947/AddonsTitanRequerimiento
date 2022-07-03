using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAO;
using DTO;
using Newtonsoft.Json;

namespace SMC_AddonRQ.Controllers
{
    public class SolicitudRQController : Controller
    {
        // GET: SolicitudRQ
        public ActionResult Listado()
        {
            return View();
        }


        public string ObtenerSolicitudesRQ()
        {
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudRQDAO.ObtenerSolicitudesRQ(base.Session["IdSociedad"].ToString());
            if (lstSolicitudRQDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudRQDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertSolicitud(SolicitudRQDTO solicitudRQDTO, SolicitudRQDetalleDTO solicitudRQDetalleDTO)
        {

            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.UpdateInsertSolicitud(solicitudRQDTO, solicitudRQDetalleDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdSolicitudRQ)
        {
            SolicitudRQDAO oSerieDAO = new SolicitudRQDAO();
            List<SolicitudRQDTO> lstSolicitudRQDTO = oSerieDAO.ObtenerDatosxID(IdSolicitudRQ);

            if (lstSolicitudRQDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudRQDTO);
            }
            else
            {
                return "error";
            }

        }


        public int EliminarDetalleSolicitud(int IdSolicitudRQDetalle)
        {
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.Delete(IdSolicitudRQDetalle);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
