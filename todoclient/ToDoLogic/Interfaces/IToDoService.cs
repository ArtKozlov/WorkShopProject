using System;
using System.Collections.Generic;
using Nest;
using ToDoLogic.DTO;

namespace ToDoLogic.Interfaces
{
    public interface IToDoService
    {
        IEnumerable<TaskDto> GetTasks();
        IEnumerable<TaskDto> GetTaskByName(string name);
        void CreateTask(TaskDto task);
        void UpdateTask(TaskDto task);
        void DeleteTask(int id);
    }
}
