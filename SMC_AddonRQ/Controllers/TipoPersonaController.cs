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
    public class TipoPersonaController : Controller
    {
        // GET: TipoPersona
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerTipoPersonas()
        {
            TipoPersonaDAO oTipoPersonaDAO = new TipoPersonaDAO();
            List<TipoPersonaDTO> lstTipoPersonaDTO = oTipoPersonaDAO.ObtenerTipoPersonas();
            if (lstTipoPersonaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoPersonaDTO);
            }
            else
            {
                return "error";
            }
        }


    }
}
