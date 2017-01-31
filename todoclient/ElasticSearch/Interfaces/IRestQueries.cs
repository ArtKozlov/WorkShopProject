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
        void Create(ItemIdx e);
        void Update(ItemIdx e);
        ItemIdx GetById(int key);
        void Delete(int key);
        IEnumerable<ItemIdx> GetItems(int userId);
    }
}
