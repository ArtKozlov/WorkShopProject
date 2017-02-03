using DAL.Entities.ElasticSearch;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces.ElasticSearch
{
    public interface ITaskElasticSearchRepository
    {
        void Create(ElasticSearchTask item);
        void Update(ElasticSearchTask item);
        IEnumerable<ElasticSearchTask> GetByName(string name);
        void Delete(int key);
        IEnumerable<ElasticSearchTask> GetItems();
    }
}
