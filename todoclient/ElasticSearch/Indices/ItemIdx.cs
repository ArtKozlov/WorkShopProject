using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch.Indices
{
    public class ItemIdx
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}
