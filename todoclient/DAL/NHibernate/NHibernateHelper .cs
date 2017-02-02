using DAL.Entities.NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DAL.NHibernate
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(cs => cs.FromConnectionStringWithKey("WorkShopConnectionString"))
            .ShowSql())             
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Task>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
            .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
