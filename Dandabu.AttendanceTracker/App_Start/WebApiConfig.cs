using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace Dandabu.AttendanceTracker
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.Routes.MapHttpRoute(
                name: "DefaultApiWithExtension",
                routeTemplate: "xml/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, ext = "xml" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.AddUriPathExtensionMapping("xml", "application/xml");
            config.Formatters.JsonFormatter.AddUriPathExtensionMapping("json", "application/json");
        }
    }
}
