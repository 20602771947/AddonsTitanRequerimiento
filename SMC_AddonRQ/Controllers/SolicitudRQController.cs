using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAO;
using DTO;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace SMC_AddonRQ.Controllers
{
    public class SolicitudRQController : Controller
    {
        // GET: SolicitudRQ
        public ActionResult Listado()
        {
            var items = GetFiles();
            return View(items);
        }

        public ActionResult Upload()
        {
            var items = GetFiles();
            return View(items);
        }


        public string ObtenerSolicitudesRQ()
        {
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            List<SolicitudRQDTO> lstSolicitudRQDTO = oSolicitudRQDAO.ObtenerSolicitudesRQ(base.Session["IdSociedad"].ToString());
            if (lstSolicitudRQDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudRQDTO);
            }
            else
            {
                return "error";
            }
        }


        public string ObtenerDatosxID(int IdSolicitudRQ)
        {
            SolicitudRQDAO oSerieDAO = new SolicitudRQDAO();
            List<SolicitudRQDTO> lstSolicitudRQDTO = oSerieDAO.ObtenerDatosxID(IdSolicitudRQ);

            if (lstSolicitudRQDTO.Count > 0)
            {
                return JsonConvert.SerializeObject(lstSolicitudRQDTO);
            }
            else
            {
                return "error";
            }

        }


        public int EliminarDetalleSolicitud(int IdSolicitudRQDetalle)
        {
            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();
            int resultado = oSolicitudRQDAO.Delete(IdSolicitudRQDetalle);
            if (resultado == 0)
            {
                resultado = 1;
            }

            return resultado;
        }


        public int UpdateInsertSolicitud(SolicitudRQDTO solicitudRQDTO, SolicitudRQDetalleDTO solicitudRQDetalleDTO)
        {

            ModeloAutorizacionDAO oModeloAutorizacionDAO = new ModeloAutorizacionDAO();
            var Datos = oModeloAutorizacionDAO.VerificarExisteModeloSolicitud();

            if (Datos.Count > 0)
            {
                for (int i = 0; i < Datos.Count; i++)
                {
                    var Resultado = oModeloAutorizacionDAO.ObtenerDatosxID(Datos[i].IdModeloAutorizacion);
                    UsuarioDAO oUsuarioDAO = new UsuarioDAO();
                    EmpleadoDAO oEmpleadoDAO = new EmpleadoDAO();
                    for (int j = 0; j < Resultado[0].DetallesAutor.Count; j++)
                    {
                        var User = oUsuarioDAO.ObtenerDatosxID(Resultado[0].DetallesAutor[j].IdAutor);
                        var Solicitante = oEmpleadoDAO.ObtenerDatosxID(solicitudRQDTO.IdSolicitante);
                        EnviarCorreo(User[0].Correo,Solicitante[0].RazonSocial, solicitudRQDTO.Serie, solicitudRQDTO.Numero);
                    }
                    
                }
            }
            

            SolicitudRQDAO oSolicitudRQDAO = new SolicitudRQDAO();

            int resultado = oSolicitudRQDAO.UpdateInsertSolicitud(solicitudRQDTO, solicitudRQDetalleDTO, base.Session["IdSociedad"].ToString());
            if (resultado != 0)
            {
                resultado = 1;
            }

            return resultado;

        }


        public void EnviarCorreo(string Correo,string Solicitante,string Serie,int Numero)
        {
  
            string body;

      
            body = "<body>" +
                "<h1>Se creo una nueva Solicitud</h1>" +
                "<h4>Detalles de Solicitud:</h4>" +
                "<span>Serie Solicitud: "+Serie+"-"+Numero+"</span>" +
                "<br/><br/><span></span>" +
                "</body>";

            string msge = "";
            string from = "mail.mineratitan@gmail.com";
            string correo = from;
            string password = "itjgiwuyjxuvdzfb";
            string displayName = "SMC - Proceso de Autorizacion";
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(from, displayName);
            mail.To.Add(Correo); 

            mail.Subject = "Autorizacion";
            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Aquí debes sustituir tu servidor SMTP y el puerto
            client.Credentials = new NetworkCredential(from, password);
            client.EnableSsl = true;//En caso de que tu servidor de correo no utilice cifrado SSL,poner en false
            client.Send(mail);



        }



        [HttpPost]
        public ActionResult Listado(HttpPostedFileBase file)
        {
            if (file!=null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Anexos"),Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "Anexo guardado correctamente";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Error:" + ex.Message.ToString();
                    throw;
                }
            }
            else
            {
                ViewBag.Message = "Debe especificar el archivo";
            }
            var items = GetFiles();
            return View(items);
        }

        public FileResult Download(string ImageName)
        {
            var FileVirtualPath = "~/Anexos/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        private List<string> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Anexos"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*");

            List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            return items;
        }


    }
}
