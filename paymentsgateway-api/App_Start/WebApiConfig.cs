using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WBPayments_API.Utils;

namespace WBPayments_API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //MSACCONE: Habilitacion de CORS.
            string origin = ConfigurationManager.AppSettings.Get("CorsOrigin");

            if (!String.IsNullOrEmpty(origin))
            {
                string headers = ConfigurationManager.AppSettings.Get("CorsHeaders");
                if (String.IsNullOrWhiteSpace(headers))
                {
                    headers = "*";
                }
                string methods = ConfigurationManager.AppSettings.Get("CorsMethods");
                if (String.IsNullOrWhiteSpace(methods))
                {
                    methods = "*";
                }
                EnableCorsAttribute cors = new EnableCorsAttribute(origin, headers, methods);
                config.EnableCors(cors);
            }


            // Web API routes
            config.MapHttpAttributeRoutes();

            string apiContext = ConfigurationManager.AppSettings["ApiContext"];
            //Si se especifica un context para la API, controlo si tiene una '/' para ver si la agrego.
            if (!String.IsNullOrEmpty(apiContext)) { 
                //Si no hay una barra, o no esta al final
                if (apiContext.IndexOf('/')<0 || apiContext.LastIndexOf('/') < (apiContext.Length-1))
                {
                    apiContext += "/";
                }
            }
            config.Routes.MapHttpRoute(
                name: "WBPayments_API",
                routeTemplate: apiContext+"{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            
            /*Elimino el formatter Json x default , y agrego el que sanitiza el input*/
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            config.Formatters.Remove(jsonFormatter);
            config.Formatters.Add(new SanitizeJsonMediaTypeFormatter());

            /*Elimino el formatter Xml x default, y agrego el que sanitiza el input*/
            var xmlFormatter = config.Formatters.OfType<XmlMediaTypeFormatter>().First();
            config.Formatters.Remove(jsonFormatter);
            config.Formatters.Add(new SanitizeXmlMediaTypeFormatter());

            /*MSACCONE: Mapea segun jquery parameter, que content type debe ser el response*/
            // Adding formatter for Json   
            config.Formatters.JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "json", new MediaTypeHeaderValue("application/json")));

            // Adding formatter for XML   
            config.Formatters.XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("type", "xml", new MediaTypeHeaderValue("application/xml")));

        }

    }
}
