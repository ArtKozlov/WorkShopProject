﻿using ToDoDataAccess.Entities.ElasticSearch;
using ToDoDataAccess.Interfaces.ElasticSearch;
using Nest;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace ToDoDataAccess.Repositories.ElasticSearch
{

    public class TaskElasticRepository : ITaskElasticRepository
    {

        private readonly IElasticClient _elasticClient;

        public TaskElasticRepository(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;

        }

        public void Create(Task item)
        {
            _elasticClient.Create(item);
        }

        public void Delete(int key)
        {
            _elasticClient.Delete(new DeleteRequest<Task>(key.ToString()));
        }

        public void Update(Task item)
        {
            _elasticClient.Update(DocumentPath<Task>
                .Id(item.Id),
                u => u.Doc(item).DocAsUpsert());
        }

        public IEnumerable<Task> GetItems()
        {
            
            IReadOnlyCollection<Task> result = _elasticClient.Search<Task>().Documents;

            return result;
        }

     
        public IEnumerable<Task> GetByName(string name)
        {

            ISearchResponse<Task> searchResponse =
                _elasticClient.Search<Task>(
                                   ss => ss
                                   .Index("todorepository")
                 //               .Query(q => q
                 //    .Match(m => m
                 //        .Field(f => f.Name)
                 //        .Query(name)
                 //    )
                 //)
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
                                            .Fields(fs => fs
                            .Field(fl => fl.Name)
                 ))
                    );

            List<Task> resultTasks = searchResponse.Documents.ToList();

           // string[] listOfHits = searchResponse.HitsMetaData.Hits.Select(t => t.Highlights["name"].Highlights.FirstOrDefault()).ToArray();

            IReadOnlyCollection<IHit<Task>> hits = searchResponse.Hits;

            for (int i = 0; i < hits.Count; i++)
            {
                var highlights = hits.ElementAt(i).Highlights;
                var highlightedTextTitle = highlights["name"].Highlights.ElementAt(0);
                resultTasks.ElementAt(i).Name = highlightedTextTitle;
            }
            
            //List<string> listOfHits = new List<string>();
            //foreach (IHit<Task> hit in searchResponse.Hits)
            //{
            //    foreach (var secondLevelHit in hit.Highlights)
            //    {
            //        foreach (string resultHit in secondLevelHit.Value.Highlights)
            //        {
            //            listOfHits.Add(resultHit);
            //        }
            //    }
            //}

            //for (int i = 0; i < listOfHits.Length; i++)
            //{
            //    resultTasks[i].Name = listOfHits[i];
            //}
            return resultTasks;
        }


    }
}
