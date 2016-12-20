using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class User
    {
        public User()
        {
            Items = new List<Item>();
        }
        public int Id { get; set; }

        public virtual IList<Item> Items { get; set; }
    }
}
