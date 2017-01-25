using FluentNHibernate.Mapping;

namespace DAL.Entities.Mapping
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Table("Item");
            Id(x => x.Id).Column("Id").Not.Nullable().GeneratedBy.Increment(); 
            Map(x => x.Name).Column("Name").Nullable();
            Map(x => x.ToDoId).Column("ToDoId").Nullable();
            Map(x => x.IsCompleted).Column("IsCompleted").Nullable();
            Map(x => x.UserId).Column("UserId").Nullable();
        }
    }
}
