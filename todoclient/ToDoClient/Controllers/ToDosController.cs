using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ToDoClient.Mapping;
using ToDoClient.Models;
using ToDoLogic.Interfaces;
using AutoMapper;
using ToDoLogic.DTO;

namespace ToDoClient.Controllers
{
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class ToDosController : ApiController
    {
        private readonly IToDoService _toDoService;
       // private readonly IUserService _userService;

        public ToDosController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<TaskViewModel> Get()
        {
           // var userId = _userService.GetOrCreateUser();
           MapConfig.CreateMapTaskDtoToTaskViewModel();
            return _toDoService.GetTasks().Select(t => Mapper.Map<TaskViewModel>(t)).ToList();
        }
        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<TaskViewModel> Get(string name)
        {
            // var userId = _userService.GetOrCreateUser();
            MapConfig.CreateMapTaskDtoToTaskViewModel();
            return _toDoService.GetTaskByName(name, 327).Select(t => Mapper.Map<TaskDto, TaskViewModel>(t)).ToList();
        }
        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="taskViewModel">The todo-item to update.</param>
        public void Put(TaskViewModel taskViewModel)
        {
           // todo.UserId = _userService.GetOrCreateUser();
           MapConfig.CreateMapTaskViewModelToTaskDto();
            _toDoService.UpdateTask(Mapper.Map<TaskViewModel, TaskDto>(taskViewModel));
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        public void Delete(int id)
        {
            _toDoService.DeleteTask(id);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="taskViewModel">The todo-item to create.</param>
        public void Post(TaskViewModel taskViewModel)
        {
            //todo.UserId = _userService.GetOrCreateUser();
            MapConfig.CreateMapTaskViewModelToTaskDto();
            _toDoService.CreateTask(Mapper.Map<TaskViewModel, TaskDto>(taskViewModel));
        }
    }
}
