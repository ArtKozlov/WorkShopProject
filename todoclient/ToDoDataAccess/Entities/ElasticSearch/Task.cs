using System;
using Nest;

namespace ToDoDataAccess.Entities.ElasticSearch
{
    [ElasticsearchType(IdProperty = "Id", Name = "task")]
    public class Task
    {
        [Number(Name = "Id")]
        public virtual int Id { get; set; }
        [String(Name = "Name", Analyzer = "customIndexNgramAnalyzer", SearchAnalyzer = "customSearchNgramAnalyzer", IndexOptions = IndexOptions.Offsets)]
        public virtual string Name { get; set; }
        [Boolean(Name = "IsCompleted", NullValue = false, Store = true)]
        public virtual bool IsCompleted { get; set; }
        [Date(Name = "PublishDate")]
        public virtual DateTime CreatedDate { get; set; }
        [Number(Name = "UserId")]
        public virtual int UserId { get; set; }
    }
}
