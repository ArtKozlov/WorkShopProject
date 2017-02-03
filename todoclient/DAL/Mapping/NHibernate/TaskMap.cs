using DAL.Entities.NHibernate;
using FluentNHibernate.Mapping;

namespace DAL.Mapping.NHibernate
{
    public class TaskMap : ClassMap<Task>
    {
        public TaskMap()
        {
            Table("Task");
            Id(x => x.Id).Column("Id").Not.Nullable().GeneratedBy.Increment(); 
            Map(x => x.Name).Column("Name").Nullable();
            Map(x => x.IsCompleted).Column("IsCompleted").Nullable();
            Map(x => x.CreatedDate).Column("CreatedDate").Nullable();
           // Map(x => x.User).Column("User").Nullable();
            References(x => x.User).Cascade.SaveUpdate();
        }
    }
}
