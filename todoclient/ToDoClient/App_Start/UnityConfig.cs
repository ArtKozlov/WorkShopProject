using DAL.Interfaces;
using DAL.Repositories;
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

            return container;
        }
    }
}