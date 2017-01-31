using ElasticSearch.Interfaces;
using System;
using System.Collections.Generic;
using ElasticSearch.Indices;
using Nest;

namespace ElasticSearch.Queries
{
    public class ItemQueries : IRestQueries
    {
        private readonly Uri local;
        private readonly ConnectionSettings settings;
        private readonly ElasticClient client;

        public ItemQueries()
        {

            local = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(local)
            .DefaultIndex("todolist").DefaultTypeNameInferrer(t => "item");
            client = new ElasticClient(settings);

        }
        

        public void Create(ItemIdx item)
        {
            client.Create(item);

        }

        public void Delete(int key)
        {
            client.Delete(new DeleteRequest<ItemIdx>(key.ToString()));
        }

        public void Update(ItemIdx item)
        {
            client.Update(DocumentPath<ItemIdx>
                .Id(item.Id),
                u => u.Doc(item).DocAsUpsert(true));
        }

        public IEnumerable<ItemIdx> GetItems(int userId)
        {
            var result = client.Search<ItemIdx>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(
                            bs => bs.Term(p => p.UserId, userId)
            )))).Documents;

            return result;
        }

        public IEnumerable<ItemIdx> GetByName(string name)
        {
            var result = client.Search<ItemIdx>(s => s
                .Query(q => q
                    .Bool(b => b
                        .Should(
                             bs => bs.Term(p => p.Name, name.ToLower())
            )))).Documents;

            return result;
        }

     }

}
