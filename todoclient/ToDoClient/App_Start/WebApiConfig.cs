using System.Web.Http;
using Microsoft.Practices.Unity;
using todoclient.App_Start;
using todoclient.DependencyResolver;

namespace todoclient
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
            config.DependencyResolver = new UnityResolver(UnityConfig.BuildUnityContainer());

        }
    }
}
