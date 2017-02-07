using System;

namespace DAL.Entities.ElasticSearch
{
    public class ElasticSearchTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
    }
}
