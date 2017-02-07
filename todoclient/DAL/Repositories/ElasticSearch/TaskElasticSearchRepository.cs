using DAL.Entities.ElasticSearch;
using DAL.Interfaces.ElasticSearch;
using Nest;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using DAL.ElasticSearch;

namespace DAL.Repositories.ElasticSearch
{
    public class TaskElasticSearchRepository : ITaskElasticSearchRepository
    {
        /// <summary>
        /// The service URL.
        /// </summary>
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
            
            var result = _uow.Tasks.Search<ElasticSearchTask>(
            //s => s
            //    .Query(q => q
            //        .Bool(b => b
            //            .Should(
            //                bs => bs.Term(p => p.UserId, userId)
            //)))
            ).Documents;

            return result;
        }


        public ISearchResponse<ElasticSearchTask> GetByName(string name)
        {
            var result = _uow.Tasks.Search<ElasticSearchTask>(
                s => s
                .Query(q => q
                    .Match(m => m
                    .Field(f => f.Name.Suffix("standard")).Query("23")))
                    //.Highlight(h => h
                    //.PreTags("<b>")
                    //.PostTags("</b>").Fields(f => f.Field("Name"))
                    //)
                    );
            return result;
        }


    }
}
