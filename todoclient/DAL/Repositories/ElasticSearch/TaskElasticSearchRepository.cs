using DAL.Entities.ElasticSearch;
using DAL.Interfaces.ElasticSearch;
using Nest;
using System.Collections.Generic;

namespace DAL.Repositories.ElasticSearch
{
    public class TaskElasticSearchRepository : ITaskElasticSearchRepository
    {

        private readonly IUnitOfWorkElasticSearch _uow;

        public TaskElasticSearchRepository(IUnitOfWorkElasticSearch uow)
        {
            _uow = uow;

        }

        public void Create(ElasticSearchTask item)
        {
            _uow.Tasks.Create(item);
        }

        public void Delete(int key)
        {
            _uow.Tasks.Delete(new DeleteRequest<ElasticSearchTask>(key.ToString()));
        }

        public void Update(ElasticSearchTask item)
        {
            _uow.Tasks.Update(DocumentPath<ElasticSearchTask>
                .Id(item.Id),
                u => u.Doc(item).DocAsUpsert());
        }

        public IEnumerable<ElasticSearchTask> GetItems()
        {
            
            var result = _uow.Tasks.Search<ElasticSearchTask>().Documents;

            return result;
        }


        public IEnumerable<ElasticSearchTask> GetByName(string name)
        {

            ISearchResponse<ElasticSearchTask> searchResponse = 
                _uow.Tasks.Search<ElasticSearchTask>(s => s
                .Query(q => q
                    .Match(m => m
                        .Field(f => f.Name).Query(name)
                    )
                ).Highlight(h => h
                    .PreTags("<b  style='background:#B0C4DE'>")
                    .PostTags("</b>")
                        .Fields(
                 fs => fs
                    .Field(p => p.Name))));

            IReadOnlyCollection<ElasticSearchTask> resultTasks = searchResponse.Documents;

            List<string> listOfHits = new List<string>();
            foreach (IHit<ElasticSearchTask> hit in searchResponse.Hits)
            {
                foreach (var secondLevelHit in hit.Highlights)
                {
                    foreach (string resultHit in secondLevelHit.Value.Highlights)
                    {
                        listOfHits.Add(resultHit);
                    }
                }
            }

            int i = 0;
            foreach (ElasticSearchTask task in resultTasks)
            {
                task.Name = listOfHits[i];
                i++;
            }

            return resultTasks;
        }


    }
}
