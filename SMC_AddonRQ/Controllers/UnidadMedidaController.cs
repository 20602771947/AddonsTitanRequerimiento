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
    public class UnidadMedidaController : Controller
    {
        // GET: UnidadMedida
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerUnidadMedidas()
        {
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = oUnidadMedidaDAO.ObtenerUnidadMedidas(base.Session["IdSociedad"].ToString());
            if (lstUnidadMedidaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUnidadMedidaDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertUnidadMedida(UnidadMedidaDTO UnidadMedidaDTO)
        {

            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            int resultado = oUnidadMedidaDAO.UpdateInsertUnidadMedida(UnidadMedidaDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdUnidadMedida)
        {
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            List<UnidadMedidaDTO> lstUnidadMedidaDTO = oUnidadMedidaDAO.ObtenerDatosxID(IdUnidadMedida);

            if (lstUnidadMedidaDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstUnidadMedidaDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarUnidadMedida(int IdUnidadMedida)
        {
            UnidadMedidaDAO oUnidadMedidaDAO = new UnidadMedidaDAO();
            int resultado = oUnidadMedidaDAO.Delete(IdUnidadMedida);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

    }
}
