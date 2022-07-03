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
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        public ActionResult Listado()
        {
            return View();
        }


        public string ObtenerDepartamentos()
        {
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            List<DepartamentoDTO> lstDepartamentoDTO = oDepartamentoDAO.ObtenerDepartamentos(base.Session["IdSociedad"].ToString());
            if (lstDepartamentoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDepartamentoDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertDepartamento(DepartamentoDTO departamentoDTO)
        {

            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            int resultado = oDepartamentoDAO.UpdateInsertDepartamento(departamentoDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdDepartamento)
        {
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            List<DepartamentoDTO> lstDepartamentoDTO = oDepartamentoDAO.ObtenerDatosxID(IdDepartamento);

            if (lstDepartamentoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstDepartamentoDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarDepartamento(int IdDepartamento)
        {
            DepartamentoDAO oDepartamentoDAO = new DepartamentoDAO();
            int resultado = oDepartamentoDAO.Delete(IdDepartamento);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
