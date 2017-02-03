using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities.NHibernate;
using DAL.Interfaces.NHibernate;
using DAL.NHibernate;
using NHibernate;
using NHibernate.Linq;

namespace DAL.Repositories.NHibernate
{
    public class TaskRepository : ITaskRepository
    {
        public void Create(Task task)
        {
            if (ReferenceEquals(task, null))
                throw new ArgumentNullException();

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {                   
                    session.Save(task);
                    transaction.Commit();
                }
            }

        }
        
        public void Delete(int key)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var item = session.Query<Task>().FirstOrDefault(i => i.Id == key);
                    if (!ReferenceEquals(item, null))
                    {
                        session.Delete(item);
                        transaction.Commit();
                    }
                }
            }

        }
        
        public Task GetById(int key)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                    var item = session.Query<Task>().FirstOrDefault(i => i.Id == key);

                    if (!ReferenceEquals(item, null))
                    {
                        return item;
                    }
            }
            return null;
        }
        
        public IEnumerable<Task> GetTasks()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result =  session.Query<Task>().ToList();
                return result;
            }
        }
        
        public void Update(Task task)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var entity = session.Query<Task>().FirstOrDefault(i => i.Id == task.Id);
                    entity.Name = task.Name;
                    entity.IsCompleted = task.IsCompleted;
                    entity.User = task.User;
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

    }
}
