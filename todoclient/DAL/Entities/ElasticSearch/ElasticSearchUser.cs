using System;
using System.Collections.Generic;

namespace DAL.Entities.ElasticSearch
{
    public class ElasticSearchUser
    {
        public ElasticSearchUser()
        {
            Tasks = new List<ElasticSearchTask>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public IList<ElasticSearchTask> Tasks { get; set; }
    }
}
