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
    public class SociedadController : Controller
    {
        // GET: Sociedad
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerSociedades()
        {
            SociedadDAO oSociedadDAO = new SociedadDAO();
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerSociedades();
            if (lstSociedadDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSociedadDTO);
            }
            else
            {
                return "error";
            }

        }

        public int UpdateInserSociedad(SociedadDTO sociedadDTO)
        {

            SociedadDAO SociedadDAO = new SociedadDAO();
            int resultado = SociedadDAO.UpdateInsertSociedad(sociedadDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdSociedad)
        {
            SociedadDAO oSociedadDAO = new SociedadDAO();
            List<SociedadDTO> lstSociedadDTO = oSociedadDAO.ObtenerDatosxID(IdSociedad);

            if (lstSociedadDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSociedadDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarSociedad(int IdSociedad)
        {
            SociedadDAO oSociedadDAO = new SociedadDAO();
            int resultado = oSociedadDAO.Delete(IdSociedad);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
