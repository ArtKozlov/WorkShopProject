using DAL.Entities;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace DAL.NHibernate
{
    public class NHibernateHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString(@"Data Source=(LocalDB)\v11.0;
                    AttachDbFilename=|DataDirectory|\WorkShopDB.mdf;Integrated Security=True")
            .ShowSql())             
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Item>())
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
            .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
