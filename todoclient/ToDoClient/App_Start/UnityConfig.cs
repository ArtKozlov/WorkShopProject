using DAL.Interfaces;
using DAL.Repositories;
using ElasticSearch.Interfaces;
using ElasticSearch.Queries;
using Microsoft.Practices.Unity;
using todoclient.Interfaces;
using ToDoClient.Services;

namespace todoclient.App_Start
{
    public static class UnityConfig
    {
        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IUserService, UserService>();
            container.RegisterType<IToDoService, ToDoService>();
            container.RegisterType<IItemRepository, ItemRepository>();
            container.RegisterType<IRestQueries, ItemQueries>();

            return container;
        }
    }
}