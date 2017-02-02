using System;
using System.Collections.Generic;
using ToDoLogic.DTO;

namespace ToDoLogic.Interfaces
{
    public interface IToDoService
    {
        IList<TaskDto> GetTasks();
        IList<TaskDto> GetTaskByName(string name, int userId);
        void CreateTask(TaskDto task);
        void UpdateTask(TaskDto task);
        void DeleteTask(int id);
    }
}
