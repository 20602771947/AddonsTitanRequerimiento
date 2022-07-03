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
    public class IndicadorImpuestoController : Controller
    {
        // GET: IndicadorImpuesto
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerIndicadorImpuestos()
        {
            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            List<IndicadorImpuestoDTO> lstIndicadorImpuestoDTO = oIndicadorImpuestoDAO.ObtenerIndicadorImpuestos(base.Session["IdSociedad"].ToString());
            if (lstIndicadorImpuestoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstIndicadorImpuestoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertIndicadorImpuesto(IndicadorImpuestoDTO IndicadorImpuestoDTO)
        {

            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            int resultado = oIndicadorImpuestoDAO.UpdateInsertIndicadorImpuesto(IndicadorImpuestoDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdIndicadorImpuesto)
        {
            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            List<IndicadorImpuestoDTO> lstIndicadorImpuestoDTO = oIndicadorImpuestoDAO.ObtenerDatosxID(IdIndicadorImpuesto);

            if (lstIndicadorImpuestoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstIndicadorImpuestoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarIndicadorImpuesto(int IdIndicadorImpuesto)
        {
            IndicadorImpuestoDAO oIndicadorImpuestoDAO = new IndicadorImpuestoDAO();
            int resultado = oIndicadorImpuestoDAO.Delete(IdIndicadorImpuesto);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
