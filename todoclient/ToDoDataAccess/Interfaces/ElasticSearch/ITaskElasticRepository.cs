using ToDoDataAccess.Entities.ElasticSearch;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Nest;

namespace ToDoDataAccess.Interfaces.ElasticSearch
{
 
    public interface ITaskElasticRepository
    {
        void Create(Task item);
        void Update(Task item);
        IEnumerable<Task> GetByName(string name);
        void Delete(int key);
        IEnumerable<Task> GetItems();
    }
}
