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
    public class CondicionPagoController : Controller
    {
        // GET: CondicionPago
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerCondicionPagos()
        {
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            List<CondicionPagoDTO> lstCondicionPagoDTO = oCondicionPagoDAO.ObtenerCentroCostos();
            if (lstCondicionPagoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCondicionPagoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertCondicionPago(CondicionPagoDTO CondicionPagoDTO)
        {

            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int resultado = oCondicionPagoDAO.UpdateInsertCondicionPago(CondicionPagoDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdCondicionPago)
        {
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            List<CondicionPagoDTO> lstCondicionPagoDTO = oCondicionPagoDAO.ObtenerDatosxID(IdCondicionPago);

            if (lstCondicionPagoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCondicionPagoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarCondicionPago(int IdCondicionPago)
        {
            CondicionPagoDAO oCondicionPagoDAO = new CondicionPagoDAO();
            int resultado = oCondicionPagoDAO.Delete(IdCondicionPago);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
