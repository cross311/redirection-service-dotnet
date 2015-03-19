using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RedirectionService.Auditing;
using RedirectionService.WebApi.Models;

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
            var auditService                     = new AuditServiceFactory().Build();
            var coreRedirectionService           = new RedirectionServiceFactory().Build();
            var httpUserRequestRepository        = new HttpUserRequestRepository();
            var webApiAuditingRedirectionService = new WebApiAuditingRedirectionService(
                coreRedirectionService,
                auditService,
                httpUserRequestRepository);

            RedirectionService = webApiAuditingRedirectionService;
        }
    }
}
