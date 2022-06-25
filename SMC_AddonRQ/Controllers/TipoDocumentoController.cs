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
    public class TipoDocumentoController : Controller
    {
        // GET: TipoDocumento
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerTipoDocumentos()
        {
            TipoDocumentoDAO oTipoDocumentoDAO = new TipoDocumentoDAO();
            List<TipoDocumentoDTO> lstTipoDocumentoDTO = oTipoDocumentoDAO.ObtenerTipoDocumentos();
            if (lstTipoDocumentoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstTipoDocumentoDTO);
            }
            else
            {
                return "error";
            }
        }


    }
}
