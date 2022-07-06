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
    public class ModeloAutorizacionController : Controller
    {
        // GET: ModeloAutorizacion
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerModeloAutorizacion()
        {
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = oModeloAutorizacionDAO.ObtenerModeloAutorizacion(base.Session["IdSociedad"].ToString());
            if (lstModeloAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstModeloAutorizacionDTO);
            }
            else
            {
                return "error";
            }
        }


        public int UpdateInsertModeloAutorizacion(ModeloAutorizacionDTO modeloAutorizacionDTO)
        {

            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.UpdateInsertModeloAutorizacion(modeloAutorizacionDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }


        public string ObtenerDatosxID(int IdModeloAutorizacion)
        {
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            List<ModeloAutorizacionDTO> lstModeloAutorizacionDTO = oModeloAutorizacionDAO.ObtenerDatosxID(IdModeloAutorizacion);

            if (lstModeloAutorizacionDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstModeloAutorizacionDTO);
            }
            else
            {
                return "error";
            }

        }


        public int EliminarModeloAutorizacionDetalleEtapa(int IdModeloAutorizacionEtapa)
        {
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.EliminarModeloAutorizacionDetalleEtapa(IdModeloAutorizacionEtapa);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public int EliminarModeloAutorizacionDetalleAutor(int IdModeloAutorizacionAutor)
        {
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.EliminarModeloAutorizacionDetalleAutor(IdModeloAutorizacionAutor);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public int EliminarModeloAutorizacionDetalleCondicion(int IdModeloAutorizacionCondicion)
        {
            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            int resultado = oModeloAutorizacionDAO.EliminarModeloAutorizacionDetalleCondicion(IdModeloAutorizacionCondicion);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


    }
}
