using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using DAO;
using Newtonsoft.Json;

namespace SMC_AddonRQ.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerAccesos()
        {
            AccesoDAO oAccesoDAO = new AccesoDAO();
            List<AccesoDTO> lstAccesoDTO = oAccesoDAO.ObtenerAccesos(base.Session["IdPerfil"].ToString());
            if (lstAccesoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAccesoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerAccesosPerfil(string IdPerfil)
        {
            AccesoDAO oAccesoDAO = new AccesoDAO();
            List<AccesoDTO> lstAccesoDTO = oAccesoDAO.ObtenerAccesos(IdPerfil);
            if (lstAccesoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAccesoDTO);
            }
            else
            {
                return "error";
            }
        }

        public string ObtenerMenus()
        {
            MenuDAO oMenuDAO = new MenuDAO();
            List<MenuDTO> lstMenuDTO = oMenuDAO.ObtenerMenus();
            if (lstMenuDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstMenuDTO);
            }
            else
            {
                return "error";
            }
        }

        public int GuardarAcceso(int IdPerfil,List<int> ArrayAccesos)
        {
            AccesoDAO oAccesoDAO = new AccesoDAO();
            int resultado = oAccesoDAO.UpdateInsertAcceso(IdPerfil, ArrayAccesos);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;
        }



    }
}
