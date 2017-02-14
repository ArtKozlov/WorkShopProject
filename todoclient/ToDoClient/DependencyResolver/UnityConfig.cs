using ToDoDataAccess.Repositories.ElasticSearch;
using Microsoft.Practices.Unity;
using NHibernate;
using ToDoClient.Mapping;
using ToDoClient.Mapping.Interface;
using ToDoDataAccess.Interfaces.ElasticSearch;
using ToDoDataAccess.Interfaces.NHibernate;
using ToDoDataAccess.Repositories.NHibernate;
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
            container.RegisterType<IUnitOfWorkElastic, UnitOfWorkElastic>(new PerResolveLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerResolveLifetimeManager());
            container.RegisterType<IClientMapper, ClientMapper>(new PerResolveLifetimeManager());
            container.RegisterType<IDomainMapper, DomainMapper>(new PerResolveLifetimeManager());

            return container;
        }
    }
}
