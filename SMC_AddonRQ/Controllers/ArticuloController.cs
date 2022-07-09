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
    public class ArticuloController : Controller
    {
        // GET: Articulo
        public ActionResult Listado()
        {
            return View();
        }


        public string ObtenerArticulos()
        {
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticulos(base.Session["IdSociedad"].ToString());
            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return "error";
            }

        }

        public string ObtenerDatosxID(int IdArticulo)
        {
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerDatosxID(IdArticulo);

            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return "error";
            }

        }

        public int UpdateInsertArticulo(ArticuloDTO oArticuloDTO)
        {
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int resultado = oArticuloDAO.UpdateInsertArticulo(oArticuloDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }


        public int EliminarArticulo(int IdArticulo)
        {
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            int resultado = oArticuloDAO.Delete(IdArticulo);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }



        public string ObtenerArticuloxCodigo(string Codigo)
        {
            ArticuloDAO oArticuloDAO = new ArticuloDAO();
            List<ArticuloDTO> lstArticuloDTO = oArticuloDAO.ObtenerArticuloxCodigo(Codigo);

            if (lstArticuloDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstArticuloDTO);
            }
            else
            {
                return "error";
            }

        }


    }
}
