using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace RenderTheMatrix.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "MatrixData",
                url: "{controller}/{action}/{columnCount}",
                defaults: new { controller = "Matrix", action = "GetColumns", columnCount = 1 });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Matrix", action = "Render", id = UrlParameter.Optional }
            );
            
        }
    }
}