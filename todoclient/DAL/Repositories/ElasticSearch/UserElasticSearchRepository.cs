using System;
using System.Collections.Generic;
using DAL.Entities.ElasticSearch;
using DAL.Interfaces.ElasticSearch;
using Nest;

namespace DAL.Repositories.ElasticSearch
{
    public class UserElasticSearchRepository : IUserElasticSearchRepository
    {
        private readonly IUnitOfWorkElasticSearch _uow;

        public UserElasticSearchRepository(IUnitOfWorkElasticSearch uow)
        {
            _uow = uow;

        }
        public void Create(ElasticSearchUser item)
        {
            _uow.Users.Create(item);
        }

        public void Delete(int key)
        {
            _uow.Users.Delete(new DeleteRequest<ElasticSearchUser>(key.ToString()));
        }

        public IEnumerable<ElasticSearchUser> GetByName(string name)
        {
            IEnumerable<ElasticSearchUser> result = _uow.Users.Search<ElasticSearchUser>(s => s
                .Query(q => q.Bool(b => b
                   .Must(
                         bs => bs.Term(p => p.Name, name.ToLower()))))).Documents;
            return result;
        }

        public IEnumerable<ElasticSearchUser> GetItems()
        {
            var result = _uow.Users.Search<ElasticSearchUser>(
            ).Documents;

            return result;
        }

        public void Update(ElasticSearchUser item)
        {
            _uow.Users.Update(DocumentPath<ElasticSearchUser>
                .Id(item.Id),
                    u => u.Doc(item).DocAsUpsert());
        }
    }
}
