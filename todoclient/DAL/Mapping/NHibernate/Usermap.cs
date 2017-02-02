using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.NHibernate;
using FluentNHibernate.Mapping;

namespace DAL.Mapping.NHibernate
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("User");
            Id(x => x.Id).Column("Id").Not.Nullable().GeneratedBy.Increment();
            Map(x => x.Name).Column("Name").Nullable();
            Map(x => x.BirthDay).Column("BirthDay").Nullable();
        }

    }
}
