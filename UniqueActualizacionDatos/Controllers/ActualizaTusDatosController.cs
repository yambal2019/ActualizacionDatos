using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniqueActualizacionDatos.Models;
using UniqueActualizacionDatos.Data;
using UniqueActualizacionDatos.Utilities;

namespace UniqueActualizacionDatos.Controllers
{
    public class ActualizaTusDatosController : Controller
    {
        [HttpGet]
        public ActionResult Inicio()
        {
          
            ConsultoraModel obj = new ConsultoraModel();

            obj.ListaTipoDocumento = ListaTipoDocumento();

            return View(obj);
        }

      

        [HttpPost]
        public ActionResult Inicio(ConsultoraModel objModel)
        {
            objModel.ListaTipoDocumento = ListaTipoDocumento();

            try
            {


                DAOConsultora.Busqueda_Consultora(ref objModel);

                if (!String.IsNullOrEmpty(objModel.vchCodConsultora))
                {
                    Session["Model"] = objModel;

                    return RedirectToAction("Datos", "ActualizaTusDatos");
                }
                ViewBag.Message = "Error vuelva a ingresar sus datos";
            }
            catch (Exception ex)
            {
                return View();
            }

            return View(objModel);
        }


        [HttpGet]
        public ActionResult Datos()
        {
            ConsultoraModel obj = new ConsultoraModel();

            return View(obj);
        }

        [HttpPost]
        public async Task<JsonResult> ProcesarDatosAsync(ConsultoraModel objModel)
        {
            ConsultoraModel objMemoria = (ConsultoraModel)Session["Model"];

            try
            {

          
            
            if (ModelState.IsValid)

            {

                objMemoria.vchEmail = objModel.vchEmail;
                objMemoria.vchTelefono = objModel.vchTelefono;

                if (objModel.vchEmail != null)
                {
                    objMemoria.texto = Helper.TextoBD("MENSAJE_POPUP_EMAIL_SMS");
                }
                else
                {
                    objMemoria.texto = Helper.TextoBD("MENSAJE_POPUP_SMS");
                }

                //*******************************************************************

                DatoModel objDatoModel = new DatoModel();
                objDatoModel.idTipoDocumento = objMemoria.TipoDocumentoId;
                objDatoModel.vchDato = objMemoria.vchDato;
                objDatoModel.vchEmail = objModel.vchEmail;
                objDatoModel.vchTelefono = objModel.vchTelefono;
                objDatoModel.bitEnviado = true;
                objDatoModel.dtmFechaEnvio = DateTime.Now;
                objDatoModel.vchCodConsultora = objMemoria.vchCodConsultora;
                objDatoModel.vchEstado = "1";
                objDatoModel.idPromocion = 1;
                DAODato.Add(ref objDatoModel);
                //*******************************************************************

                objMemoria.intDato = objDatoModel.idDato;
                objMemoria.Exito = 1;
                objMemoria.vchEncriptadoSMS = Helper.Encrypt(objMemoria.intDato + "," + "1" + "," + objMemoria.vchCodConsultora + "," + 1);
                objMemoria.vchEncriptadoEmail = Helper.Encrypt(objMemoria.intDato + "," + "2" + "," + objMemoria.vchCodConsultora + "," + 1);

                await Helper.EnvioSMSAsync(objMemoria);
               // Helper.EnvioSMSAsync(objMemoria);
                 
                if (objModel.vchEmail != null)
                {
                    objMemoria.vchRuta = Server.MapPath("~/Views/HtmlTemplates/HPRespuestaPedido.html");
                    Helper.EnvioEmail(objMemoria);
                }


                return Json(objMemoria, JsonRequestBehavior.AllowGet);
            }
            else
            {
                objMemoria.Exito = 0;
                return Json(objMemoria, JsonRequestBehavior.AllowGet);
            }
            }
            catch (Exception ex)
            {
                Log.Error(ex.StackTrace + "-----" +  ex.Message);
                return Json(objMemoria, JsonRequestBehavior.AllowGet);
            }

        }

        [NonAction]
        private static SelectList ListaTipoDocumento()
        {
            List<ConsultoraModel.TipoDocumentoModel> objLista = new List<ConsultoraModel.TipoDocumentoModel>();
            objLista.Add(new ConsultoraModel.TipoDocumentoModel { intTipoDocumento = 1, vchDocumento = "Codigo Consultora" });
            objLista.Add(new ConsultoraModel.TipoDocumentoModel { intTipoDocumento = 2, vchDocumento = "DNI " });

            SelectList ListaTipoDocumento = new SelectList(objLista, "intTipoDocumento", "vchDocumento");

            return ListaTipoDocumento;
        }
    }
}



