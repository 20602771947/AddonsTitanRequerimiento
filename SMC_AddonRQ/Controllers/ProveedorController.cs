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
    public class ProveedorController : Controller
    {
        // GET: Proveedor
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerProveedores()
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerProveedores(base.Session["IdSociedad"].ToString());
            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertProveedor(ProveedorDTO proveedorDTO)
        {

            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.UpdateInsertProveedor(proveedorDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdProveedor)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            List<ProveedorDTO> lstProveedorDTO = oProveedorDAO.ObtenerDatosxID(IdProveedor);

            if (lstProveedorDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstProveedorDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarProveedor(int IdProveedor)
        {
            ProveedorDAO oProveedorDAO = new ProveedorDAO();
            int resultado = oProveedorDAO.Delete(IdProveedor);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }



    }
}
