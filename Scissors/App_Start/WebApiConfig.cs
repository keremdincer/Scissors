using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Scissors
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            /** 
             * Xml yerine Json cevap döndürmesi için
             * Eğer web üzerinde görünlemek gerekirse açılabilir
             */
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));
            config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
