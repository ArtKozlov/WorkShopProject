using ToDoDataAccess.Interfaces.NHibernate;

namespace ToDoDataAccess.Interfaces.NHibernate
{
    public interface IUnitOfWork
    {
        ITaskRepository Tasks { get; }
        IUserRepository Users { get; }
    }
}
