using Newtonsoft.Json;
using Swashbuckle.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace prabhuEticket
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SerializerSettings =
                 new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
            config.Routes.MapHttpRoute(
            name: "Swagger UI",
            routeTemplate: "api/docs",
            defaults: null,
            constraints: null,
            handler: new RedirectHandler(SwaggerDocsConfig.DefaultRootUrlResolver, "api/docs/index"));
        }
    }
}
