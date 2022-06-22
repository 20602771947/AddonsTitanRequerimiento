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
    public class SucursalController : Controller
    {
        // GET: Sucursales
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerSucursales()
        {
            SucursalDAO oSucursalDAO = new SucursalDAO();
            List<SucursalDTO> lstSucursalDTO = oSucursalDAO.ObtenerSucursales();
            if (lstSucursalDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSucursalDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertSucursal(SucursalDTO sucursalDTO)
        {

            SucursalDAO oSucursalDAO = new SucursalDAO();
            int resultado = oSucursalDAO.UpdateInsertSucursal(sucursalDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdSucursal)
        {
            SucursalDAO oSucursalDAO = new SucursalDAO();
            List<SucursalDTO> lstSucursalDTO = oSucursalDAO.ObtenerDatosxID(IdSucursal);

            if (lstSucursalDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSucursalDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarSucursal(int IdSucursal)
        {
            SucursalDAO oSucursalDAO = new SucursalDAO();
            int resultado = oSucursalDAO.Delete(IdSucursal);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
