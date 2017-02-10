using ToDoDataAccess.Interfaces.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoDataAccess.Entities.NHibernate;
using NHibernate;
using ToDoDataAccess.NHibernate;
using NHibernate.Linq;

namespace ToDoDataAccess.Repositories.NHibernate
{
    public class UserRepository : IUserRepository
    {
        public void Create(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(user);
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
                    User item = session.Query<User>().FirstOrDefault(i => i.Id == key);
                    if (!ReferenceEquals(item, null))
                    {
                        session.Delete(item);
                        transaction.Commit();
                    }
                }
            }
        }

        public User GetById(int key)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                User item = session.Query<User>().FirstOrDefault(i => i.Id == key);

                if (!ReferenceEquals(item, null))
                {
                    return item;
                }
            }
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<User> result = session.Query<User>().ToList();
                return result;
            }
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    User entity = session.Query<User>().FirstOrDefault(i => i.Id == user.Id);
                    entity.Name = user.Name;
                    entity.BirthDay = user.BirthDay;
                    entity.Tasks = user.Tasks;
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }
    }
}
