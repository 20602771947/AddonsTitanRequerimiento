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
    public class SerieController : Controller
    {
        // GET: Serie
        public ActionResult Listado()
        {
            return View();
        }


        public string ObtenerSeries()
        {
            SerieDAO oSerieDAO = new SerieDAO();
            List<SerieDTO> lstSerieDTO = oSerieDAO.ObtenerSeries(base.Session["IdSociedad"].ToString());
            if (lstSerieDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertSerie(SerieDTO SerieDTO)
        {

            SerieDAO oSerieDAO = new SerieDAO();
            int resultado = oSerieDAO.UpdateInsertSerie(SerieDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdSerie)
        {
            SerieDAO oSerieDAO = new SerieDAO();
            List<SerieDTO> lstSerieDTO = oSerieDAO.ObtenerDatosxID(IdSerie);

            if (lstSerieDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarSerie(int IdSerie)
        {
            SerieDAO oSerieDAO = new SerieDAO();
            int resultado = oSerieDAO.Delete(IdSerie);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public string ValidarNumeracionSerieSolicitudRQ(int IdSerie)
        {
            SerieDAO oSerieDAO = new SerieDAO();
            List<SerieDTO> lstSerieDTO = oSerieDAO.ValidarNumeracionSerieSolicitudRQ(IdSerie);

            if (lstSerieDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSerieDTO);
            }
            else
            {
                return "sin datos";
            }

        }


    }
}
