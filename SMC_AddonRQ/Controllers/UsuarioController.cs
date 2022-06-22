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
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Listado()
        {
            return View();
        }


        public string ObtenerUsuarios()
        {
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerUsuarios();
            if (lstUsuarioDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUsuarioDTO);
            }
            else
            {
                return "error";
            }
            
        }

        public string ObtenerPerfiles()
        {
            PerfilDAO oPerfilDAO = new PerfilDAO();
            List<PerfilDTO> lstPerfilDTO = oPerfilDAO.ObtenerPerfiles();
            if (lstPerfilDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstPerfilDTO);
            }
            else
            {
                return "error";
            }

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

        public int UpdateInsertUsuario(UsuarioDTO usuarioDTO)
        {

            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.UpdateInsertUsuario(usuarioDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdUsuario)
        {
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ObtenerDatosxID(IdUsuario);

            if (lstUsuarioDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUsuarioDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarUsuario(int IdUsuario)
        {
            UsuarioDAO oUsuarioDAO = new UsuarioDAO();
            int resultado = oUsuarioDAO.Delete(IdUsuario);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
