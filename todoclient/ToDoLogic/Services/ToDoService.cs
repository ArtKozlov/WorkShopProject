﻿using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using DAL.Entities.ElasticSearch;
using DAL.Entities.NHibernate;
using DAL.Interfaces.ElasticSearch;
using DAL.Interfaces.NHibernate;
using ToDoLogic.DTO;
using ToDoLogic.Interfaces;
using ToDoLogic.Mapping;

namespace ToDoLogic.Services
{
    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class ToDoService : IToDoService
    {
        /// <summary>
        /// The service URL.
        /// </summary>
        //private readonly string _serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        private readonly ITaskRepository _taskRepository;
        
        private ITaskElasticSearchRepository _taskElasticSearchRepository;

       // private ProxyService _proxyService;
        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService(ITaskRepository itemRepository, ITaskElasticSearchRepository itemQueries)
        {
            _taskRepository = itemRepository;
            _taskElasticSearchRepository = itemQueries;
            
        }

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public IList<TaskDto> GetTasks()
        {
            MapConfig.CreateMapTaskToTaskDto();
            var listOfTasks = _taskRepository.GetTasks().Select(i => Mapper.Map<TaskDto>(i)).ToList();
            return listOfTasks;

            // so fast. 
            // var itemResult = _itemQueries.GetItems(userId).Select(i => i.ToViewModel()).ToList();

        }

        public IList<TaskDto> GetTaskByName(string name, int userId)
        {

            MapConfig.CreateMapElasticSearchTaskToTaskDto();
            List<TaskDto> result = _taskElasticSearchRepository.GetByName(name, userId).Select(i => Mapper.Map<TaskDto>(i)).ToList();
            return result;

        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
        /// </summary>
        /// <param name="task">The todo to create.</param>
        public void CreateTask(TaskDto task)
        {
            if(!ReferenceEquals(task.Name, null))
            { 
                task.CreatedDate = DateTime.Now;
                          
                MapConfig.CreateMapTaskDtoToTask();
                _taskRepository.Create(Mapper.Map<TaskDto, Task>(task));

                MapConfig.CreateMapTaskDtoToElasticSearchTask();
                task.Id = _taskRepository.GetTasks().Last()?.Id ?? 1;
                _taskElasticSearchRepository.Create(Mapper.Map<ElasticSearchTask>(task));
            }

        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="task">The todo to update.</param>
        public void UpdateTask(TaskDto task)
        {
            MapConfig.CreateMapTaskDtoToTask();
            _taskRepository.Update(Mapper.Map<TaskDto, Task>(task));
            MapConfig.CreateMapTaskDtoToElasticSearchTask();
            _taskElasticSearchRepository.Update(Mapper.Map<ElasticSearchTask>(task));
        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public void DeleteTask(int id)
        {
            _taskRepository.Delete(id);
            _taskElasticSearchRepository.Delete(id);

        }
    }
}