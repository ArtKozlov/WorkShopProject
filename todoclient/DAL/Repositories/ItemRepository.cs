using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Context;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ToDoListContext _context;

        public ItemRepository()
        {
            _context = new ToDoListContext();
        }
        public void Create(Item item)
        {
            if (ReferenceEquals(item, null))
                throw new ArgumentNullException();

            _context.Items.Add(item);
            _context.SaveChanges();
            _context.Dispose();


        }

        public void Delete(int key)
        {
            var item = _context.Items.FirstOrDefault(i => i.Id == key);
            if (!ReferenceEquals(item, null))
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
                _context.Dispose();
            }
        }

        public IEnumerable<Item> GetAll()
        {
            var result = _context.Items.Select(i => i);

            return result;
        }

        public Item GetById(int key)
        {
            var result = _context.Items.FirstOrDefault(i => i.Id == key);

            return result;
        }

        public void Update(Item item)
        {
            var entity = _context.Items.Find(item.Id);
            entity.Name = item.Name;
            entity.IsCompleted = item.IsCompleted;
            entity.User = item.User;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Dispose();
        }
    }
}
