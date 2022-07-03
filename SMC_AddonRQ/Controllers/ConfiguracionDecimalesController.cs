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
    public class ConfiguracionDecimalesController : Controller
    {
        // GET: ConfiguracionDecimales
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerConfiguracionDecimales()
        {
            ConfiguracionDecimalesDAO oConfiguracionDecimalesDAO = new ConfiguracionDecimalesDAO();
            List<ConfiguracionDecimalesDTO> lstConfiguracionDecimalesDTO = oConfiguracionDecimalesDAO.ObtenerConfiguracionDecimales(base.Session["IdSociedad"].ToString());
            if (lstConfiguracionDecimalesDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstConfiguracionDecimalesDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertConfiguracionDecimales(ConfiguracionDecimalesDTO configuracionDecimalesDTO)
        {

            ConfiguracionDecimalesDAO oConfiguracionDecimalesDAO = new ConfiguracionDecimalesDAO();
            int resultado = oConfiguracionDecimalesDAO.UpdateInsertConfiguracionDecimales(configuracionDecimalesDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

    }
}
