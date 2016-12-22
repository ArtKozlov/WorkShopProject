using System;
using System.Collections.Generic;
using System.Linq;
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
            var item = _context.Items.FirstOrDefault(i => i.ToDoId == key);
            if (!ReferenceEquals(item, null))
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
                _context.Dispose();
            }
        }


        public Item GetById(int key)
        {
            var result = _context.Items.FirstOrDefault(i => i.UserId == key);

            return result;
        }

        public IEnumerable<Item> GetItems(int userId)
        {
            var result = _context.Items.Where(i => i.UserId == userId);

            return result;
        }

        public void Update(Item item)
        {
            var entity = _context.Items.FirstOrDefault(i => i.ToDoId == item.ToDoId);
            entity.Name = item.Name;
            entity.IsCompleted = item.IsCompleted;           
            entity.UserId = item.UserId;
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
            _context.Dispose();
        }

    }
}
