using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace task_01
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Controller1",
                url: "controller/{action}/{id}",
                defaults: new { controller = "A", action = "View1", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Controller2",
                url: "controller/{action}/{id}",
                defaults: new { controller = "B", action = "View2", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Controller3",
                url: "controller/{action}/{id}",
                defaults: new { controller = "C", action = "View3", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Controller4",
                url: "controller/{action}/{id}",
                defaults: new { controller = "D", action = "View4", id = UrlParameter.Optional }
            );
        }
    }
}
