using System;
using System.Collections.Generic;
using ToDoDataAccess.Entities.ElasticSearch;
using ToDoDataAccess.Interfaces.ElasticSearch;
using Nest;

namespace ToDoDataAccess.Repositories.ElasticSearch
{
    public class UserElasticRepository : IUserElasticRepository
    {
        private readonly IElasticClient _elasticClient;

        public UserElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;

        }
        public void Create(User item)
        {
            _elasticClient.Create(item);
        }

        public void Delete(int key)
        {
            _elasticClient.Delete(new DeleteRequest<User>(key.ToString()));
        }

        public IEnumerable<User> GetByName(string name)
        {
            IEnumerable<User> result = _elasticClient.Search<User>(s => s
                .Query(q => q.Bool(b => b
                   .Must(
                         bs => bs.Term(p => p.Name, name.ToLower()))))).Documents;
            return result;
        }

        public IEnumerable<User> GetItems()
        {
            var result = _elasticClient.Search<User>(
            ).Documents;

            return result;
        }

        public void Update(User item)
        {
            _elasticClient.Update(DocumentPath<User>
                .Id(item.Id),
                    u => u.Doc(item).DocAsUpsert());
        }
    }
}
