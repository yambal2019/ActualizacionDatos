using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace UniqueActualizacionDatos
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "ConfirmacionDatos",
            url: "ConfirmacionDatos/{codigo}",
            defaults: new { controller = "ConfirmacionDatos", action = "Detalle" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ActualizaTusDatos", action = "Inicio", id = UrlParameter.Optional }
            );


        }
    }
}
