
using Nest;

namespace ToDoDataAccess.Interfaces.ElasticSearch
{
    public interface IUnitOfWorkElastic
    {
        ITaskElasticRepository Tasks { get; }
        IUserElasticRepository Users { get; }

    }
}
