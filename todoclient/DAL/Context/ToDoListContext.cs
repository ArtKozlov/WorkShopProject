using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL.Entities;
using System.Data.Entity;
using System.Configuration;
using System.Net.Http;
using Newtonsoft.Json;

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
            /// <summary>
            /// The service URL.
            /// </summary>
            private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

            /// <summary>
            /// The url for getting all todos.
            /// </summary>
            private const string GetAllUrl = "ToDos?userId={0}";

            /// <summary>
            /// The url for updating a todo.
            /// </summary>
            private const string UpdateUrl = "ToDos";

            /// <summary>
            /// The url for a todo's creation.
            /// </summary>
            private const string CreateUrl = "ToDos";

            /// <summary>
            /// The url for a todo's deletion.
            /// </summary>
            private const string DeleteUrl = "ToDos/{0}";

            private readonly HttpClient httpClient;

            protected override void Seed(ToDoListContext db)
            {
                var dataAsString = httpClient.GetStringAsync(string.Format(serviceApiUrl + GetAllUrl, 0)).Result;
                var result = JsonConvert.DeserializeObject<IList<Item>>(dataAsString);
                foreach(var item in result)
                {
                    db.Items.Add(item);
                }
                
                db.SaveChanges();
            }
        }
    }
}