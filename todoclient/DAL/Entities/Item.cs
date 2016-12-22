using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public int ToDoId { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

    }
}
