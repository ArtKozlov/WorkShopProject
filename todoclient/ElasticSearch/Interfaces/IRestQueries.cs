using ElasticSearch.Indices;
using System;
using System.Collections.Generic;

namespace ElasticSearch.Interfaces
{
    public interface IRestQueries
    {
        void Create(ItemIdx item);
        void Update(ItemIdx item);
        IEnumerable<ItemIdx> GetByName(string name);
        void Delete(int key);
        IEnumerable<ItemIdx> GetItems(int userId);
    }
}
