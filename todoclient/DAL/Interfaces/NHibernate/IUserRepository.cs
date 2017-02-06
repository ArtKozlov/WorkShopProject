using DAL.Entities.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces.NHibernate
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
