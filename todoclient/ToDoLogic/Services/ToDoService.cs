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
        private readonly IUserRepository _userRepository;
        private readonly IUserElasticSearchRepository _userElasticSearchRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly ITaskElasticSearchRepository _taskElasticSearchRepository;

        private readonly IMapper _mapper;

        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService(ITaskRepository itemRepository, ITaskElasticSearchRepository taskElasticSearchRepository,
            IUserRepository userRepository, IUserElasticSearchRepository userElasticSearchRepository)
        {
            _userElasticSearchRepository = userElasticSearchRepository;
            _userRepository = userRepository;
            _mapper = DtoMapperConfiguration.GetConfiguration().CreateMapper();
            _taskRepository = itemRepository;
            _taskElasticSearchRepository = taskElasticSearchRepository;

        }

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public IList<TaskDto> GetTasks()
        {
            var listOfTasks = _taskRepository.GetTasks().Select(i => _mapper.Map<TaskDto>(i)).ToList();
            return listOfTasks;
        }

        public IList<TaskDto> GetTaskByName(string name)
        {
            
            List<TaskDto> result = _taskElasticSearchRepository.GetByName(name).Documents.Select(i => _mapper.Map<TaskDto>(i)).ToList();
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
                
               // User firstUser = new User { Name = "Zheldak", BirthDay = new DateTime(1995, 03, 05)};
               // ElasticSearchUser firstElasticUser = new ElasticSearchUser { Name = "Zheldak", BirthDay = new DateTime(1995, 03, 05) };

               // _userRepository.Create(firstUser);

               // firstElasticUser.Id = 1;
              //  _userElasticSearchRepository.Create(firstElasticUser);
                User user = _userRepository.GetById(1);
                task.CreatedDate = DateTime.Now;
                Task newTask = _mapper.Map<Task>(task);
                newTask.User = user;
                _taskRepository.Create(newTask);
                ElasticSearchTask elasticTask = _mapper.Map<ElasticSearchTask>(newTask);
                 _taskElasticSearchRepository.Create(elasticTask);
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