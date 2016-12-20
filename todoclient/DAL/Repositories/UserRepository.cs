using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Context;

namespace DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ToDoListContext _context;

        public UserRepository()
        {
            _context = new ToDoListContext();
        }
        public void Create(User user)
        {
            if (ReferenceEquals(user, null))
                throw new ArgumentNullException();

            _context.Users.Add(user);
            _context.SaveChanges();
            _context.Dispose();
        }
        
    }
}
