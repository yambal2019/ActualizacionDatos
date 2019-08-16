using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniqueActualizacionDatos.Data;
using UniqueActualizacionDatos.Models;

namespace UniqueActualizacionDatos.Controllers
{
    public class ConfirmacionDatosController : Controller
    {
       
        public ActionResult Detalle(string codigo)
        {

            //string parametro = (string)RouteData.Values["id"];
            string parametro = Convert.ToString(codigo);
            String Clave = Helper.Decrypt(parametro);
            string[] lista = Clave.Split(',');
            DatoModel objModel = new DatoModel();
            objModel.idDato =Convert.ToInt32( lista[0]);
             
            objModel.vchCodConsultora = Convert.ToString(lista[2]);
            objModel.idPromocion = Convert.ToInt32(lista[3]);

            if (lista[1] == "1") //SMS
            {
                objModel.TipoEnvio = 1;
                objModel.bitConfirmadoSMS = true;
                objModel.dtmFechaConfirmadoSMS = DateTime.Now;
                ViewBag.Texto = Helper.TextoBD("MENSAJE_CONFIRMACION_SMS");
            }
            if (lista[1] == "2")//Email
            {
                objModel.TipoEnvio = 2;
                objModel.bitConfirmadoEmail = true;
                objModel.dtmFechaConfirmadoEmail = DateTime.Now;
                ViewBag.Texto = Helper.TextoBD("MENSAJE_CONFIRMACION_EMAIL");
            }

            DAODato.Update(objModel);
            
            return View();
        }
    }
}