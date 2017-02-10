using System;
using System.Collections.Generic;

namespace ToDoDataAccess.Entities.ElasticSearch
{
    public class User
    {
        public User()
        {
            Tasks = new List<Task>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public IList<Task> Tasks { get; set; }
    }
}
