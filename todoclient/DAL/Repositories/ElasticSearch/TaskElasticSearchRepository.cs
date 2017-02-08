using DAL.Entities.ElasticSearch;
using DAL.Interfaces.ElasticSearch;
using Nest;
using System.Collections.Generic;
using System.Linq;

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
            
            IReadOnlyCollection<ElasticSearchTask> result = _uow.Tasks.Search<ElasticSearchTask>().Documents;

            return result;
        }


        public IEnumerable<ElasticSearchTask> GetByName(string name)
        {

            ISearchResponse<ElasticSearchTask> searchResponse = 
                _uow.Tasks.Search<ElasticSearchTask>(
                                   ss => ss
                    .Query(q => q
                        .Bool(b => b
                            .Should(sh => sh
                                .Wildcard(w => w
                                    .Field(f => f.Name)
                                    .Value($"*{name}*")
                                ))))
                 .Highlight(h => h
                    .PreTags("<b  style='background:#B0C4DE'>")
                    .PostTags("</b>")
                        .Fields(
                 fs => fs
                    .Field(p => p.Name))));

            List<ElasticSearchTask> resultTasks = searchResponse.Documents.ToList();

            string[] listOfHits = searchResponse.HitsMetaData.Hits.Select(t => t.Highlights["name"].Highlights.FirstOrDefault()).ToArray();


            //List<string> listOfHits = new List<string>();
            //foreach (IHit<ElasticSearchTask> hit in searchResponse.Hits)
            //{
            //    foreach (var secondLevelHit in hit.Highlights)
            //    {
            //        foreach (string resultHit in secondLevelHit.Value.Highlights)
            //        {
            //            listOfHits.Add(resultHit);
            //        }
            //    }
            //}
            for (int i = 0; i < listOfHits.Length; i++)
            {
                resultTasks[i].Name = listOfHits[i];
            }
            return resultTasks;
        }


    }
}
