using System;
using System.Configuration;
using System.Web.Http;
using ToDoDataAccess.ElasticSearch;
using Nest;
using ToDoClient.DependencyResolver;

namespace ToDoClient
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

            ElasticSearchHelper elasticSearchHelper = new ElasticSearchHelper();
            elasticSearchHelper.CreateIndex();
        }
    }
}
