using ToDoDataAccess.Entities.NHibernate;
using System;
using System.Collections.Generic;

namespace ToDoDataAccess.Interfaces.NHibernate
{
    public interface IUserRepository
    {
        void Create(User user);
        void Update(User user);
        User GetById(int key);
        void Delete(int key);
        IEnumerable<User> GetUsers();
    }
}
