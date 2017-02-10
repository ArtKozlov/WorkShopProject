using System;
using System.Collections.Generic;
using ToDoDataAccess.Entities.NHibernate;

namespace ToDoDataAccess.Interfaces.NHibernate
{
    public interface ITaskRepository 
    {
        void Create(Task task);
        void Update(Task task);
        Task GetById(int key);
        void Delete(int key);
        IEnumerable<Task> GetTasks();
    }
}
