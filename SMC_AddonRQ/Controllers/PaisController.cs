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
    public class PaisController : Controller
    {
        // GET: Pais
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerPaises()
        {
            PaisDAO oPaisDAO = new PaisDAO();
            List<PaisDTO> lstPaisDTO = oPaisDAO.ObtenerPaises();
            if (lstPaisDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPaisDTO);
            }
            else
            {
                return "error";
            }
        }



    }
}
