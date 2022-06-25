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
    public class LineaNegocioController : Controller
    {
        // GET: LineaNegocio
        public ActionResult Listado()
        {
            return View();
        }


        public string ObtenerLineaNegocios()
        {
            LineaNegocioDAO oLineaNegocioDAO = new LineaNegocioDAO();
            List<LineaNegocioDTO> lstLineaNegocioDTO = oLineaNegocioDAO.ObtenerLineaNegocios();
            if (lstLineaNegocioDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstLineaNegocioDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertLineaNegocio(LineaNegocioDTO LineaNegocioDTO)
        {

            LineaNegocioDAO oLineaNegocioDAO = new LineaNegocioDAO();
            int resultado = oLineaNegocioDAO.UpdateInsertLineaNegocio(LineaNegocioDTO);
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdLineaNegocio)
        {
            LineaNegocioDAO oLineaNegocioDAO = new LineaNegocioDAO();
            List<LineaNegocioDTO> lstLineaNegocioDTO = oLineaNegocioDAO.ObtenerDatosxID(IdLineaNegocio);

            if (lstLineaNegocioDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstLineaNegocioDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarLineaNegocio(int IdLineaNegocio)
        {
            LineaNegocioDAO oLineaNegocioDAO = new LineaNegocioDAO();
            int resultado = oLineaNegocioDAO.Delete(IdLineaNegocio);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
