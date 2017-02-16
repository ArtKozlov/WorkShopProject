using System;
using System.Configuration;
using Nest;
using ToDoDataAccess.Interfaces.ElasticSearch;
using ToDoDataAccess.Repositories.ElasticSearch;

namespace ToDoDataAccess.Repositories.ElasticSearch
{
    public class UnitOfWorkElastic : IUnitOfWorkElastic
    {
        private readonly ElasticClient _elasticClient = new ElasticClient(new ConnectionSettings(
                new Uri(ConfigurationManager.AppSettings["ElasticSearchUrl"]))
            //.DefaultIndex("todo")
            );

        private ITaskElasticRepository _taskElasticRepository;
        private IUserElasticRepository _userElasticRepository;

        public ITaskElasticRepository Tasks
        {
            get
            {
                if (ReferenceEquals(_taskElasticRepository, null))
                    _taskElasticRepository = new TaskElasticRepository(_elasticClient);
                return _taskElasticRepository;
            }
        }

        public IUserElasticRepository Users
        {
            get
            {
                if (ReferenceEquals(_userElasticRepository, null))
                    _userElasticRepository = new UserElasticRepository(_elasticClient);
                return _userElasticRepository;
            }
        }
    }
}
