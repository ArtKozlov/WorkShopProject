using ElasticSearch.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var settings = new ConnectionSettings(local).DefaultIndex("Items"); ;
            var client = new ElasticClient(settings);

            var descriptor = new CreateIndexDescriptor("Item")
                .Mappings(ms => ms
                    .Map<Item>(m => m.AutoMap()));

            var expected = new
            {
                mappings = new
                {
                    item = new
                    {
                        properties = new
                        {
                            id = new
                            {
                                type = "integer"
                            },
                            toDoId = new
                            {
                                type = "integer"
                            },
                            name = new
                            {
                                type = "text"
                            },
                            isCompleted = new
                            {
                                type = "boolean"
                            },
                            userId = new
                            {
                                type = "integer"
                            }
                        }
                    }
                }

            };


            client.Index(expected);
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
