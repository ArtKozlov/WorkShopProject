using System;
using System.Collections.Generic;

namespace ToDoDataAccess.Entities.NHibernate
{
    public class User
    {
        public User()
        {
            Tasks = new List<Task>();
        }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTime BirthDay { get; set; }
        public virtual IList<Task> Tasks { get; set; }

    }
}
