using System;
using FluentNHibernate.Mapping;

namespace DAL.Entities.Mapping
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.ToDoId);
            Map(x => x.IsCompleted);
            Map(x => x.UserId);
        }
    }
}
