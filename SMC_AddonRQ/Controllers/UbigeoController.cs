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
    public class UbigeoController : Controller
    {
        // GET: Ubigeo
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerDepartamentos()
        {
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerDepartamentos();
            if (lstUbigeoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUbigeoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerProvincias(string Departamento)
        {
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerProvincias(Departamento);
            if (lstUbigeoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUbigeoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerDistritos(string Provincia)
        {
            UbigeoDAO oUbigeoDAO = new UbigeoDAO();
            List<UbigeoDTO> lstUbigeoDTO = oUbigeoDAO.ObtenerDistritos(Provincia);
            if (lstUbigeoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUbigeoDTO);
            }
            else
            {
                return "error";
            }
        }


    }
}
