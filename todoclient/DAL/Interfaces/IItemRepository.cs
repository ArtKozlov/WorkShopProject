using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAll();
        Item GetById(int key);
        void Create(Item e);
        void Delete(int key);
        void Update(Item e);
    }
}
