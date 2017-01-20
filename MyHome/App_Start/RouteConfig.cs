using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MyHome
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Addresses", action = "Index", id = UrlParameter.Optional }
            );

            /*
            //routes.MapRoute(
            //    "DefaultWithId", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Search", action = "Index", },
            //    new { id = @"\d+" }
            //);
            routes.MapRoute(
               "unitOverView", // Route name
               "UnitOverView/{id}", // URL with parameters
               new { controller = "UnitOverView", action = "UnitOverView", id = "" },
               new { id = @"\d+" }
               );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Search", action = "Index", id = "" }
            );
            */

            //routes.MapRoute("NotFound", "{*url}", new { controller = "Error", action = "Http404" });
        }
    }
}
