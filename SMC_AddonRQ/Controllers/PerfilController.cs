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
    public class PerfilController : Controller
    {
        // GET: Perfil
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerPerfiles()
        {
            PerfilDAO oPerfilDAO = new PerfilDAO();
            List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerPerfiles(base.Session["IdSociedad"].ToString());
            if (lstPerfilDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPerfilDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertPerfil(PerfilDTO perfilDTO)
        {

            PerfilDAO oPerfilDAO = new PerfilDAO();
            int resultado = oPerfilDAO.UpdateInsertPerfil(perfilDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdPerfil)
        {
            PerfilDAO oPerfilDAO = new PerfilDAO();
            List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerDatosxID(IdPerfil);

            if (lstPerfilDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPerfilDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarPerfil(int IdPerfil)
        {
            PerfilDAO oPerfilDAO = new PerfilDAO();
            int resultado = oPerfilDAO.Delete(IdPerfil);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
