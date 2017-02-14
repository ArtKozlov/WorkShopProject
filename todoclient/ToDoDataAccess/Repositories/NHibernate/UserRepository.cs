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
        private readonly ISession _session = NHibernateHelper.OpenSession();

        public void Create(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            using (_session)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    _session.Save(user);
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
                    User item = _session.Query<User>().FirstOrDefault(i => i.Id == key);
                    if (!ReferenceEquals(item, null))
                    {
                        _session.Delete(item);
                        transaction.Commit();
                    }
                }
            }
        }

        public User GetById(int key)
        {
            using (_session)
            {
                User item = _session.Query<User>().FirstOrDefault(i => i.Id == key);

                if (!ReferenceEquals(item, null))
                {
                    return item;
                }
            }
            return null;
        }

        public IEnumerable<User> GetUsers()
        {
            using (_session)
            {
                List<User> result = _session.Query<User>().ToList();
                return result;
            }
        }

        public void Update(User user)
        {
            using (_session)
            {
                using (ITransaction transaction = _session.BeginTransaction())
                {
                    User entity = _session.Query<User>().FirstOrDefault(i => i.Id == user.Id);
                    entity.Name = user.Name;
                    entity.BirthDay = user.BirthDay;
                    entity.Tasks = user.Tasks;
                    _session.Save(entity);
                    transaction.Commit();
                }
            }
        }
    }
}
