﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ToDoClient.Models;
using ToDoLogic.Interfaces;
using AutoMapper;
using ToDoClient.Mapping.Interface;
using ToDoLogic.DTO;

namespace ToDoClient.Controllers
{
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class ToDosController : ApiController
    {
        private readonly IToDoService _toDoService;
        private readonly IMapper _mapper;
       // private readonly IUserService _userService;

        public ToDosController(IToDoService toDoService, IClientMapper mapper)
        {
            _mapper = mapper;
            _toDoService = toDoService;
        }

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<TaskViewModel> Get()
        {
            return _toDoService.GetTasks().Select(task => _mapper.Map<TaskViewModel>(task)).ToList();
        }
        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<TaskViewModel> Get(string name)
        {
            
            List<TaskViewModel> result = _toDoService.GetTaskByName(name).Select(task => _mapper.Map<TaskViewModel>(task)).ToList();
            return result;
        }
        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="taskViewModel">The todo-item to update.</param>
        public void Put(TaskViewModel taskViewModel)
        {
            _toDoService.UpdateTask(_mapper.Map<TaskDto>(taskViewModel));
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
            _toDoService.CreateTask(_mapper.Map<TaskDto>(taskViewModel));
        }
    }
}
