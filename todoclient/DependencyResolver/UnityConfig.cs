using DAL.ElasticSearch;
using DAL.Interfaces.ElasticSearch;
using DAL.Interfaces.NHibernate;
using DAL.Repositories.ElasticSearch;
using DAL.Repositories.NHibernate;
using Microsoft.Practices.Unity;
using Nest;
using ToDoLogic.Interfaces;
using ToDoLogic.Mapping;
using ToDoLogic.Services;

namespace DependencyResolver
{
    public class UnityConfig
    {
        public static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IToDoService, ToDoService>();
            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<ITaskElasticSearchRepository, TaskElasticSearchRepository>();
            container.RegisterType<IUnitOfWorkElasticSearch, UnitOfWorkElasticSearch>();

            return container;
        }
    }
}
