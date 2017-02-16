
using Microsoft.Practices.Unity;
using ToDoClient.Mapping;
using ToDoClient.Mapping.Interface;
using ToDoLogic.DependencyResolver;
using ToDoLogic.Interfaces;
using ToDoLogic.Mapping;
using ToDoLogic.Services;

namespace ToDoClient.DependencyResolver
{
    public class UnityConfig
    {
        public static IUnityContainer BuildUnityContainer()
        {
            UnityContainer container = new UnityContainer();

            container.RegisterType<IToDoService, ToDoService>(new PerResolveLifetimeManager());
            container.RegisterType<IClientMapper, ClientMapper>(new PerResolveLifetimeManager());
            container.RegisterType<IDomainMapper, DomainMapper>(new PerResolveLifetimeManager());

            UnityDtoConfig.BuildUnityDtoContainer(container);

            return container;
        }
    }
}
