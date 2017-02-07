using System;
using System.Configuration;
using DAL.Interfaces.ElasticSearch;
using Nest;

namespace DAL.ElasticSearch
{
    public class UnitOfWorkElasticSearch : IUnitOfWorkElasticSearch
    {

        public ElasticClient Tasks
        {
            get
            {
                return new ElasticClient(new ConnectionSettings(
                    new Uri(ConfigurationManager.AppSettings["ElasticSearchUrl"]))
                        .DefaultIndex("todorepository").DefaultTypeNameInferrer(t => "task"));
            }
        }

        public ElasticClient Users
        {
            get
            {
                return new ElasticClient(new ConnectionSettings(
                    new Uri(ConfigurationManager.AppSettings["ElasticSearchUrl"]))
                        .DefaultIndex("todorepository").DefaultTypeNameInferrer(t => "user"));
            }
        }
    }
}
