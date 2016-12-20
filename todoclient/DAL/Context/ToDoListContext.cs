using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Entities;
using System.Data.Entity;

namespace DAL.Context
{
    public class ToDoListContext : DbContext
    {
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<User> Users { get; set; }

        static ToDoListContext()
        {
            Database.SetInitializer(new ToDoListDBInitializer());
        }

        public ToDoListContext()
            : base("name=ToDoListDB")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(i => i.Items)
                .WithRequired(t => t.User);
        }

        private class ToDoListDBInitializer : DropCreateDatabaseIfModelChanges<ToDoListContext>
        {
            protected override void Seed(ToDoListContext db)
            {
                db.Items.Add(new Item { Name = "George", IsCompleted = true });
                db.Items.Add(new Item { Name = "Dmitriy", IsCompleted = true });
                db.Items.Add(new Item { Name = "Genya", IsCompleted = true });
                db.SaveChanges();
            }
        }
    }
}