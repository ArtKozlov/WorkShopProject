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
      //  private IndexNameResolver resolver;
       // private readonly string index;
        private readonly ElasticClient client;

        public ItemQueries()
        {

            local = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(local)
            .DefaultIndex("todolist").DefaultTypeNameInferrer(t => "item");
            //resolver = new IndexNameResolver(settings);
            //index = resolver.Resolve<Item>();
            client = new ElasticClient(settings);

        }

        

        public void Create(ItemIdx e)
        {
            //client.Index(index);
            client.Create(e);
            //client.Delete(new DeleteRequest<Item>(e.Id.ToString()));

        }

        public void Delete(int key)
        {
            //client.Index(index);
            client.Delete(new DeleteRequest<ItemIdx>(key.ToString()));
        }

        public ItemIdx GetById(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ItemIdx> GetItems(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(ItemIdx e)
        {
            client.Update(DocumentPath<ItemIdx>
                .Id(e.Id),
                u => u.Doc(e).DocAsUpsert(true)); 
        }


    }
}
