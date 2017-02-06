using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.ElasticSearch;

namespace DAL.Interfaces.ElasticSearch
{
    public interface IUserElasticSearchRepository
    {
        void Create(ElasticSearchUser item);
        void Update(ElasticSearchUser item);
        IEnumerable<ElasticSearchUser> GetByName(string name);
        void Delete(int key);
        IEnumerable<ElasticSearchUser> GetItems();
    }
}
