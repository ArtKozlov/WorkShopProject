using System;
using Nest;

namespace ToDoDataAccess.Entities.ElasticSearch
{
    [ElasticsearchType(IdProperty = "Id", Name = "task")]
    public class Task
    {
        public virtual int Id { get; set; }
        [String(Name = "name", Analyzer = "customIndexNgramAnalyzer", SearchAnalyzer = "customSearchNgramAnalyzer", IndexOptions = IndexOptions.Offsets)]
        public virtual string Name { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UserId { get; set; }
    }
}
