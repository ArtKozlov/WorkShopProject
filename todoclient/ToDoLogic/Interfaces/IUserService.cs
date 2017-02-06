using System.Collections.Generic;
using ToDoLogic.DTO;

namespace ToDoLogic.Interfaces
{
    public interface IUserService
    {
        IList<UserDto> GetUsers();
        IList<UserDto> GetUserByName(string name);
        void CreateUser(UserDto user);
        void UpdateUser(UserDto user);
        void DeleteUser(int id);
    }
}
