using DAL.Interfaces.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities.NHibernate;
using NHibernate;
using DAL.NHibernate;
using NHibernate.Linq;

namespace DAL.Repositories.NHibernate
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
                    var item = session.Query<User>().FirstOrDefault(i => i.Id == key);
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
                var item = session.Query<User>().FirstOrDefault(i => i.Id == key);

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
                var result = session.Query<User>().ToList();
                return result;
            }
        }

        public void Update(User user)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var entity = session.Query<User>().FirstOrDefault(i => i.Id == user.Id);
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
