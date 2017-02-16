using Microsoft.Practices.Unity;
using ToDoDataAccess.Interfaces.ElasticSearch;
using ToDoDataAccess.Interfaces.NHibernate;
using ToDoDataAccess.Repositories.ElasticSearch;
using ToDoDataAccess.Repositories.NHibernate;

namespace ToDoLogic.DependencyResolver
{
    public class UnityDtoConfig
    {
        public static void BuildUnityDtoContainer(IUnityContainer container)
        {
            container.RegisterType<IUnitOfWorkElastic, UnitOfWorkElastic>(new PerResolveLifetimeManager());
            container.RegisterType<IUnitOfWork, UnitOfWork>(new PerResolveLifetimeManager());

        }
    }
}
