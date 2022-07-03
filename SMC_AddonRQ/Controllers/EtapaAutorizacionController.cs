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
    public class EtapaAutorizacionController : Controller
    {
        // GET: EtapaAutorizacion
        public ActionResult Listado()
        {
            return View();
        }


        public string ObtenerEtapaAutorizacion()
        {
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            List<EtapaAutorizacionDTO> lstEtapaAutorizacionDTO = oEtapaAutorizacionDAO.ObtenerEtapaAutorizacion(base.Session["IdSociedad"].ToString());
            if (lstEtapaAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEtapaAutorizacionDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertEtapaAutorizacion(EtapaAutorizacionDTO etapaAutorizacionDTO)
        {

            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            int resultado = oEtapaAutorizacionDAO.UpdateInsertEtapaAutorizacion(etapaAutorizacionDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdEtapaAutorizacion)
        {
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            List<EtapaAutorizacionDTO> lstEtapaAutorizacionDTO = oEtapaAutorizacionDAO.ObtenerDatosxID(IdEtapaAutorizacion);

            if (lstEtapaAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEtapaAutorizacionDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarEtapaAutorizacion(int IdEtapaAutorizacion)
        {
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            int resultado = oEtapaAutorizacionDAO.Delete(IdEtapaAutorizacion);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public int EliminarDetalleAutorizacion(int IdEtapaAutorizacionDetalle)
        {
            EtapaAutorizacionDAO oEtapaAutorizacionDAO = new EtapaAutorizacionDAO();
            int resultado = oEtapaAutorizacionDAO.EliminarDetalleAutorizacion(IdEtapaAutorizacionDetalle);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
