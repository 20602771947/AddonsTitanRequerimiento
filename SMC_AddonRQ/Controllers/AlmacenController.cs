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
    public class AlmacenController : Controller
    {
        // GET: Almacen
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerAlmacenes()
        {
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerAlmacenes(base.Session["IdSociedad"].ToString());
            if (lstAlmacenDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAlmacenDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertAlmacen(AlmacenDTO almacenDTO)
        {

            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            int resultado = oAlmacenDAO.UpdateInsertAlmacen(almacenDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdAlmacen)
        {
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            List<AlmacenDTO> lstAlmacenDTO = oAlmacenDAO.ObtenerDatosxID(IdAlmacen);

            if (lstAlmacenDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstAlmacenDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarAlmacen(int IdAlmacen)
        {
            AlmacenDAO oAlmacenDAO = new AlmacenDAO();
            int resultado = oAlmacenDAO.Delete(IdAlmacen);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
