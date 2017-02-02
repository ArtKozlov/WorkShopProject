using DAL.Entities.ElasticSearch;
using DAL.Interfaces.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;

namespace DAL.Repositories.ElasticSearch
{
    public class TaskElasticSearchRepository : ITaskElasticSearchRepository
    {
        private readonly Uri local;
        private readonly ConnectionSettings settings;
        private readonly ElasticClient client;

        public TaskElasticSearchRepository()
        {

            local = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(local)
            .DefaultIndex("todorepository").DefaultTypeNameInferrer(t => "task");
            client = new ElasticClient(settings);

        }

        public void Create(ElasticSearchTask item)
        {
            client.Create(item);
        }

        public void Delete(int key)
        {
            client.Delete(new DeleteRequest<ElasticSearchTask>(key.ToString()));
        }

        public void Update(ElasticSearchTask item)
        {
            client.Update(DocumentPath<ElasticSearchTask>
                .Id(item.Id),
                u => u.Doc(item).DocAsUpsert());
        }

        public IEnumerable<ElasticSearchTask> GetItems(int userId)
        {
            var result = client.Search<ElasticSearchTask>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            bs => bs.Term(p => p.UserId, userId)
            )))).Documents;

            return result;
        }


        public IEnumerable<ElasticSearchTask> GetByName(string name, int userId)
        {
            var result = client.Search<ElasticSearchTask>(s => s
                .Query(q => q.Bool(b => b
                    .Must(/*bs => bs.Term(p => p.UserId, userId),*/
                          bs => bs.Term(p => p.Name, name.ToLower()))))).Documents;
            return result;
        }


    }
}
