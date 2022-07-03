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
    public class ProyectoController : Controller
    {
        // GET: Proyecto
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerProyectos()
        {
            ProyectoDAO oProyectoDAO = new ProyectoDAO();
            List<ProyectoDTO> lstProyectoDTO = oProyectoDAO.ObtenerProyectos(base.Session["IdSociedad"].ToString());
            if (lstProyectoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProyectoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertProyecto(ProyectoDTO ProyectoDTO)
        {

            ProyectoDAO oProyectoDAO = new ProyectoDAO();
            int resultado = oProyectoDAO.UpdateInsertProyecto(ProyectoDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdProyecto)
        {
            ProyectoDAO oProyectoDAO = new ProyectoDAO();
            List<ProyectoDTO> lstProyectoDTO = oProyectoDAO.ObtenerDatosxID(IdProyecto);

            if (lstProyectoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProyectoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarProyecto(int IdProyecto)
        {
            ProyectoDAO oProyectoDAO = new ProyectoDAO();
            int resultado = oProyectoDAO.Delete(IdProyecto);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
