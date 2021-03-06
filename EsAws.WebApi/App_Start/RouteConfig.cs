﻿using System.Web.Mvc;
using System.Web.Routing;

namespace EsAws.WebApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("api/StreamUpload/{*pathInfo}");
            routes.IgnoreRoute("api/StreamUpload");
            routes.IgnoreRoute("a/StreamUpload/{*pathInfo}");
            routes.IgnoreRoute("StreamUpload/{*pathInfo}");

            

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
