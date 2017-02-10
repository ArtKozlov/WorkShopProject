
using System;

namespace ToDoDataAccess.Entities.NHibernate
{
    public class Task
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsCompleted { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User User { get; set; }

    }
}
