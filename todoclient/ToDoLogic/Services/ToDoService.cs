using System;
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

        private readonly ITaskRepository _taskRepository;
        
        private readonly ITaskElasticSearchRepository _taskElasticSearchRepository;

        private readonly IMapper _mapper;

        public ToDoService(ITaskRepository itemRepository, ITaskElasticSearchRepository itemQueries)
        {
            _mapper = DtoMapperConfiguration.GetConfiguration().CreateMapper();
            _taskRepository = itemRepository;
            _taskElasticSearchRepository = itemQueries;

        }
        /// <summary>
        /// Creates the service.
        /// </summary>
        //public ToDoService(ITaskRepository itemRepository, ITaskElasticSearchRepository itemQueries, IMapper mapper)
        //{
        //    _mapper = mapper;
        //    _taskRepository = itemRepository;
        //    _taskElasticSearchRepository = itemQueries;
            
        //}

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public IList<TaskDto> GetTasks()
        {
            var listOfTasks = _taskRepository.GetTasks().Select(i => _mapper.Map<TaskDto>(i)).ToList();
            return listOfTasks;

            // so fast. 
            // var itemResult = _itemQueries.GetItems(userId).Select(i => i.ToViewModel()).ToList();

        }

        public IList<TaskDto> GetTaskByName(string name, int userId)
        {
            
            List<TaskDto> result = _taskElasticSearchRepository.GetByName(name, userId).Select(i => _mapper.Map<TaskDto>(i)).ToList();
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
                          
                _taskRepository.Create(_mapper.Map<TaskDto, Task>(task));
                
                task.Id = _taskRepository.GetTasks().Last()?.Id ?? 1;
                _taskElasticSearchRepository.Create(_mapper.Map<ElasticSearchTask>(task));
            }

        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="task">The todo to update.</param>
        public void UpdateTask(TaskDto task)
        {
            _taskRepository.Update(_mapper.Map<TaskDto, Task>(task));
            _taskElasticSearchRepository.Update(_mapper.Map<ElasticSearchTask>(task));
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