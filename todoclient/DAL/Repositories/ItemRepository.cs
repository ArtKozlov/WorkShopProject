using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using DAL.NHibernate;
using NHibernate;
using NHibernate.Linq;

namespace DAL.Repositories
{
    public class ItemRepository : IItemRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Create(Item item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {                   
                    session.Save(item);
                    transaction.Commit();
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Delete(int key)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var item = session.Query<Item>().FirstOrDefault(i => i.Id == key);
                    if (!ReferenceEquals(item, null))
                    {
                        session.Delete(item);
                        transaction.Commit();
                    }
                }
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Item GetById(int key)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                    var item = session.Query<Item>().FirstOrDefault(i => i.Id == key);

                    if (!ReferenceEquals(item, null))
                    {
                        return item;
                    }
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<Item> GetItems(int userId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result =  session.Query<Item>().Where(i => i.UserId == userId).ToList<Item>();
                return result;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Update(Item item)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var entity = session.Query<Item>().FirstOrDefault(i => i.Id == item.Id);
                    entity.Name = item.Name;
                    entity.IsCompleted = item.IsCompleted;
                    entity.UserId = item.UserId;
                    session.Save(entity);
                    transaction.Commit();
                }
            }
        }

    }
}
