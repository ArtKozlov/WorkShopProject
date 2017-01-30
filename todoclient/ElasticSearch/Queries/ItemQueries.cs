using ElasticSearch.Interfaces;
using System;
using System.Collections.Generic;
using ElasticSearch.Indices;
using Nest;

namespace ElasticSearch.Queries
{
    public class ItemQueries : IRestQueries
    {
        public ItemQueries(IMapper mapper)
        {
          //  mapper.Mapping();
        }

        public void Create(Item e)
        {
            var local = new Uri("http://localhost:9200");

            var settings = new ConnectionSettings(local)
                .DefaultIndex("toDoList");
            var resolver = new IndexNameResolver(settings);
            var index = resolver.Resolve<Item>();
            var client = new ElasticClient(settings);
            client.Index(index);
            client.Create(e);
        }

        public void Delete(int key)
        {
            throw new NotImplementedException();
        }

        public Item GetById(int key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetItems(int userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Item e)
        {
            throw new NotImplementedException();
        }


    }
}
