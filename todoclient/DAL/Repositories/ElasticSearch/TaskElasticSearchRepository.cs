using DAL.Entities.ElasticSearch;
using DAL.Interfaces.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using DAL.ElasticSearch;

namespace DAL.Repositories.ElasticSearch
{
    public class TaskElasticSearchRepository : ITaskElasticSearchRepository
    {
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly ElasticClient _client;

        public TaskElasticSearchRepository(ElasticClient client)
        {
            _client = client;

        }

        public void Create(ElasticSearchTask item)
        {
            _client.Create(item);
        }

        public void Delete(int key)
        {
            _client.Delete(new DeleteRequest<ElasticSearchTask>(key.ToString()));
        }

        public void Update(ElasticSearchTask item)
        {
            _client.Update(DocumentPath<ElasticSearchTask>
                .Id(item.Id),
                u => u.Doc(item).DocAsUpsert());
        }

        public IEnumerable<ElasticSearchTask> GetItems(int userId)
        {
            var result = _client.Search<ElasticSearchTask>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            bs => bs.Term(p => p.UserId, userId)
            )))).Documents;

            return result;
        }


        public IEnumerable<ElasticSearchTask> GetByName(string name, int userId)
        {
            var result = _client.Search<ElasticSearchTask>(s => s
                .Query(q => q.Bool(b => b
                    .Must(/*bs => bs.Term(p => p.UserId, userId),*/
                          bs => bs.Term(p => p.Name, name.ToLower()))))).Documents;
            return result;
        }


    }
}
