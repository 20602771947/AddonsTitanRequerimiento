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
    public class MonedaController : Controller
    {
        // GET: Moneda
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerMonedas()
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ObtenerMonedas();
            if (lstMonedaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMonedaDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertMoneda(MonedaDTO MonedaDTO)
        {

            MonedaDAO oMonedaDAO = new MonedaDAO();
            int resultado = oMonedaDAO.UpdateInsertMoneda(MonedaDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdMoneda)
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            List<MonedaDTO> lstMonedaDTO = oMonedaDAO.ObtenerDatosxID(IdMoneda);

            if (lstMonedaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMonedaDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarCentroCosto(int IdMoneda)
        {
            MonedaDAO oMonedaDAO = new MonedaDAO();
            int resultado = oMonedaDAO.Delete(IdMoneda);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }



    }
}
