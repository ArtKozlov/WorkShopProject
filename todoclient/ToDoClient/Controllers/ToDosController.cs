using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using todoclient.Interfaces;
using ToDoClient.Models;

namespace ToDoClient.Controllers
{
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class ToDosController : ApiController
    {
        private readonly IToDoService _toDoService;
        private readonly IUserService _userService;

        public ToDosController(IToDoService toDoService, IUserService userService)
        {
            _toDoService = toDoService;
            _userService = userService;
        }

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<ToDoItemViewModel> Get()
        {
            var userId = _userService.GetOrCreateUser();
            return _toDoService.GetItems(userId);
        }
        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<ToDoItemViewModel> Get(string name)
        {
            var userId = _userService.GetOrCreateUser();
            return _toDoService.GetByName(name, userId);
        }
        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        public void Put(ToDoItemViewModel todo)
        {
            todo.UserId = _userService.GetOrCreateUser();
            _toDoService.UpdateItem(todo);
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        public void Delete(int id)
        {
            _toDoService.DeleteItem(id);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        public void Post(ToDoItemViewModel todo)
        {
            todo.UserId = _userService.GetOrCreateUser();
            _toDoService.CreateItem(todo);
        }
    }
}
