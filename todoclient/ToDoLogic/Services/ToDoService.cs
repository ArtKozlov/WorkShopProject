using System;
using System.Collections.Generic;
using System.Linq;
using ToDoDataAccess.Interfaces.ElasticSearch;
using ToDoDataAccess.Interfaces.NHibernate;
using ToDoLogic.DTO;
using ToDoLogic.Interfaces;
using Task = ToDoDataAccess.Entities.ElasticSearch.Task;
using User = ToDoDataAccess.Entities.NHibernate.User;

namespace ToDoLogic.Services
{
    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class ToDoService : IToDoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUnitOfWorkElastic _unitOfWorkElastic;
        private readonly IDomainMapper _mapper;
        
        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService(IUnitOfWork unitOfWork, IUnitOfWorkElastic unitOfWorkElastic, IDomainMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _unitOfWorkElastic = unitOfWorkElastic;
            _mapper = mapper;

        }

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <returns>The list of todos.</returns>
        public IEnumerable<TaskDto> GetTasks()
        {
            List<TaskDto> listOfTasks = _unitOfWork.Tasks.GetTasks().Select(task=> _mapper.Map<TaskDto>(task)).ToList();
            return listOfTasks;
        }

        public IEnumerable<TaskDto> GetTaskByName(string name)
        {
            
            List<TaskDto> result = _unitOfWorkElastic.Tasks.GetByName(name).Select(task => _mapper.Map<TaskDto>(task)).ToList();
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

                //User firstUser = new User { Name = "Zheldak", BirthDay = new DateTime(1995, 03, 05) };
                //User firstElasticUser = new User { Name = "Zheldak", BirthDay = new DateTime(1995, 03, 05) };

                //_userRepository.Create(firstUser);

                //firstElasticUser.Id = 1;
                //_userElasticRepository.Create(firstElasticUser);
                User user = _unitOfWork.Users.GetById(1);
                task.CreatedDate = DateTime.Now;
                ToDoDataAccess.Entities.NHibernate.Task newTask = _mapper.Map<ToDoDataAccess.Entities.NHibernate.Task>(task);
                newTask.User = user;
                _unitOfWork.Tasks.Create(newTask);
                Task elasticTask = _mapper.Map<Task>(newTask);
                _unitOfWorkElastic.Tasks.Create(elasticTask);
            }

        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="task">The todo to update.</param>
        public void UpdateTask(TaskDto task)
        {
            _unitOfWork.Tasks.Update(_mapper.Map<ToDoDataAccess.Entities.NHibernate.Task>(task));
            _unitOfWorkElastic.Tasks.Update(_mapper.Map<Task>(task));
        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public void DeleteTask(int id)
        {
            _unitOfWork.Tasks.Delete(id);
            _unitOfWorkElastic.Tasks.Delete(id);

        }
    }
}