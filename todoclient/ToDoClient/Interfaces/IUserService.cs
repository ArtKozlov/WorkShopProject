

namespace todoclient.Interfaces
{
    public interface IUserService
    {
        int CreateUser(string userName);
        int GetOrCreateUser();
    }
}
