using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO;
using DAO;

namespace SMC_AddonRQ.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult CerrarSesion()
        {
            base.Session.Abandon();
            return RedirectToAction("Index", "Home");
        }


        public bool login(string usuario, string password)
        {
            bool respuesta = false;
            try
            {
                UsuarioDAO oUsuarioDAO = new UsuarioDAO();
                List<UsuarioDTO> lstUsuarioDTO = oUsuarioDAO.ValidarUsuario(usuario, password);
                UsuarioDTO oUsuarioDTO = lstUsuarioDTO[0];
                if (oUsuarioDTO.Estado == true)
                {
                    base.Session["idUsuario"] = oUsuarioDTO.IdUsuario;
                    base.Session["Usuario"] = oUsuarioDTO.Usuario;
                    base.Session["NombUsuario"] = oUsuarioDTO.NombreUsuario;
                    base.Session["IdPerfil"] = oUsuarioDTO.IdPerfil;
                    base.Session["IdSociedad"] = oUsuarioDTO.IdSociedad;
                    base.Session["Estado"] = oUsuarioDTO.Estado;
                    base.Session["NombreSociedad"] = oUsuarioDTO.NombreSociedad;

                    return true;
                }
                else
                {
                    return false;
                }


            }
            catch (Exception ex)
            {
                return false;
                throw;

            }
        }

    }
}