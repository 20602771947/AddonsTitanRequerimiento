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
    public class EmpleadoController : Controller
    {
        // GET: Empleado
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerEmpleados()
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerEmpleados();
            if (lstEmpleadoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEmpleadoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertEmpleado(EmpleadoDTO empleadoDTO)
        {

            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            int resultado = oEmpleadoDAO.UpdateInsertEmpleado(empleadoDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdEmpleado)
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            List<EmpleadoDTO> lstEmpleadoDTO = oEmpleadoDAO.ObtenerDatosxID(IdEmpleado);

            if (lstEmpleadoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstEmpleadoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarEmpleado(int IdEmpleado)
        {
            EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
            int resultado = oEmpleadoDAO.Delete(IdEmpleado);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
