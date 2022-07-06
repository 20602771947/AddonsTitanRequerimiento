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
    public class CodigoUbsoController : Controller
    {
        // GET: CodigoUbso
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerCodigoUbso()
        {
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            List<CodigoUbsoDTO> lstCodigoUbsoDTO = oCodigoUbsoDAO.ObtenerCodigoUbso(base.Session["IdSociedad"].ToString());
            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return "error";
            }
        }


        public int UpdateInsertCodigoUbso(CodigoUbsoDTO oCodigoUbsoDTO)
        {

            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            int resultado = oCodigoUbsoDAO.UpdateInsertCodigoUbso(oCodigoUbsoDTO,base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }


        public string ObtenerDatosxID(int IdCodigoUbso)
        {
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            List<CodigoUbsoDTO> lstCodigoUbsoDTO = oCodigoUbsoDAO.ObtenerDatosxID(IdCodigoUbso);

            if (lstCodigoUbsoDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstCodigoUbsoDTO);
            }
            else
            {
                return "error";
            }

        }


        public int EliminarCodigoUbso(int IdCodigoUbso)
        {
            CodigoUbsoDAO oCodigoUbsoDAO = new CodigoUbsoDAO();
            int resultado = oCodigoUbsoDAO.Delete(IdCodigoUbso);
            if (resultado == 0)
            {
                resultado = 1;
            }
            return resultado;
        }


    }
}
