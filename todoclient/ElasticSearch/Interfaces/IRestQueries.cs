using ElasticSearch.Indices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Interfaces
{
    public interface IRestQueries
    {
        void Create(Item e);
        void Update(Item e);
        Item GetById(int key);
        void Delete(int key);
        IEnumerable<Item> GetItems(int userId);
    }
}
