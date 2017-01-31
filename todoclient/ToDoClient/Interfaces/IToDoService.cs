using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoClient.Models;

namespace todoclient.Interfaces
{
    public interface IToDoService
    {
        IList<ToDoItemViewModel> GetItems(int userId);
        IList<ToDoItemViewModel> GetByName(string name);
        void CreateItem(ToDoItemViewModel item);
        void UpdateItem(ToDoItemViewModel item);
        void DeleteItem(int id);
    }
}
