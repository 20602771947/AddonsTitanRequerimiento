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
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Listado()
        {
            return View();
        }

        public string ObtenerClientes()
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            List<ClienteDTO> lstClienteDTO = oClienteDAO.ObtenerClientes(base.Session["IdSociedad"].ToString());
            if (lstClienteDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstClienteDTO);
            }
            else
            {
                return "error";
            }
        }

        public int UpdateInsertCliente(ClienteDTO clienteDTO)
        {

            ClienteDAO oClienteDAO = new ClienteDAO();
            int resultado = oClienteDAO.UpdateInsertCliente(clienteDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }

        public string ObtenerDatosxID(int IdCliente)
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            List<ClienteDTO> lstClienteDTO = oClienteDAO.ObtenerDatosxID(IdCliente);

            if (lstClienteDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstClienteDTO);
            }
            else
            {
                return "error";
            }

        }

        public int EliminarCliente(int IdCliente)
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            int resultado = oClienteDAO.Delete(IdCliente);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }

        public string ConsultarDocumento(int Tipo,string Documento)
        {
            ClienteDAO oClienteDAO = new ClienteDAO();
            string datos = oClienteDAO.ConsultarDocumento(Tipo, Documento);
            if (datos != "Error")
            {
                return datos;
            }
            else
            {
                return "error";
            }
        }

    }
}
