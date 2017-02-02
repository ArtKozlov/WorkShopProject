using DAL.Entities.ElasticSearch;
using System;
using System.Collections.Generic;

namespace DAL.Interfaces.ElasticSearch
{
    public interface ITaskElasticSearchRepository
    {
        void Create(ElasticSearchTask item);
        void Update(ElasticSearchTask item);
        IEnumerable<ElasticSearchTask> GetByName(string name, int userId);
        void Delete(int key);
        IEnumerable<ElasticSearchTask> GetItems(int userId);
    }
}
