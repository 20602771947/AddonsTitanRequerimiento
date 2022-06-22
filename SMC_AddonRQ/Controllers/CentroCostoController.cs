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
    public class CentroCostoController : Controller
    {
        // GET: CentroCosto
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerCentroCostos()
        {
            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            List<CentroCostoDTO> lstCentroCostoDTO = oCentroCostoDAO.ObtenerCentroCostos();
            if (lstCentroCostoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCentroCostoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertCentroCosto(CentroCostoDTO centroCostoDTO)
        {

            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            int resultado = oCentroCostoDAO.UpdateInsertCentroCosto(centroCostoDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdCentroCosto)
        {
            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            List<CentroCostoDTO> lstCentroCostoDTO = oCentroCostoDAO.ObtenerDatosxID(IdCentroCosto);

            if (lstCentroCostoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCentroCostoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarCentroCosto(int IdCentroCosto)
        {
            CentroCostoDAO oCentroCostoDAO = new CentroCostoDAO();
            int resultado = oCentroCostoDAO.Delete(IdCentroCosto);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
