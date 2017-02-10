using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoDataAccess.Entities.ElasticSearch;

namespace ToDoDataAccess.Interfaces.ElasticSearch
{
    public interface IUserElasticRepository
    {
        void Create(User item);
        void Update(User item);
        IEnumerable<User> GetByName(string name);
        void Delete(int key);
        IEnumerable<User> GetItems();
    }
}
