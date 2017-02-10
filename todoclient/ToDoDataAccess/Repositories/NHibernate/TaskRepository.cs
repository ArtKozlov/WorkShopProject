using System;
using System.Collections.Generic;
using System.Linq;
using ToDoDataAccess.Entities.NHibernate;
using ToDoDataAccess.Interfaces.NHibernate;
using NHibernate;
using NHibernate.Linq;

namespace ToDoDataAccess.Repositories.NHibernate
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ISession _session;
        public TaskRepository(ISession session)
        {
            _session = session;
        }
        public void Create(Task task)
        {
            if (ReferenceEquals(task, null))
                throw new ArgumentNullException();

            using (_session)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {

                    _session.Save(task);
                    transaction.Commit();
                }
            }

        }
        
        public void Delete(int key)
        {

            using (_session)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    Task task = _session.Query<Task>().FirstOrDefault(i => i.Id == key);
                    if (!ReferenceEquals(task, null))
                    {
                        _session.Delete(task);
                        transaction.Commit();
                    }
                }
            }

        }
        
        public Task GetById(int key)
        {
            using (_session)
            {
                    Task task = _session.Query<Task>().FirstOrDefault(i => i.Id == key);

                    if (!ReferenceEquals(task, null))
                    {
                        return task;
                    }
            }
            return null;
        }
        
        public IEnumerable<Task> GetTasks()
        {
            using (_session)
            {
                var listOfTasks = _session.Query<Task>().ToList();
                return listOfTasks;
            }
        }
        
        public void Update(Task task)
        {
            using (_session)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    var entity = _session.Query<Task>().FirstOrDefault(i => i.Id == task.Id);
                    entity.Name = task.Name;
                    entity.IsCompleted = task.IsCompleted;
                    entity.User = task.User;
                    _session.Save(entity);
                    transaction.Commit();
                }
            }
        }

    }
}
