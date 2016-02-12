using System.Net.Http;
using System.Web.Http;
using EsAws.WebApi.Handlers;

namespace EsAws.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            //config.Routes.IgnoreRoute("StreamUpload1", "StreamUpload/{*pathInfo}");

            //config.Routes.IgnoreRoute("StreamUpload2", "api/StreamUpload");

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "StreamUploadx",
                routeTemplate: "api/StreamUploadX",
                defaults: null,
                constraints: null,
                handler: new StreamUploadMessageHandler() // per-route message handler
                );


            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
