using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace RedirectionService.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "token",
                routeTemplate: "api/redirection/{token}",
                defaults: new { controller = "Redirection", token = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }

    public static class RedirectionServiceConfig
    {
        public static IRedirectionService RedirectionService;

        static RedirectionServiceConfig()
        {
            RedirectionService = new RedirectionServiceFactory().Build();
        }
    }
}
