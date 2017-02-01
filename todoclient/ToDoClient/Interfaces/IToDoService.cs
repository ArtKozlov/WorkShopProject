using System;
using System.Collections.Generic;
using ToDoClient.Models;

namespace todoclient.Interfaces
{
    public interface IToDoService
    {
        IList<ToDoItemViewModel> GetItems(int userId);
        IList<ToDoItemViewModel> GetByName(string name, int userId);
        void CreateItem(ToDoItemViewModel item);
        void UpdateItem(ToDoItemViewModel item);
        void DeleteItem(int id);
    }
}
