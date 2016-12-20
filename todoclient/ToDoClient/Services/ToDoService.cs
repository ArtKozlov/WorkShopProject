using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ToDoClient.Models;
using DAL.Interfaces;
using DAL.Repositories;
using todoclient.Mapping;
using System.Linq;
using System.Threading;

namespace ToDoClient.Services
{
    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class ToDoService
    {
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        /// <summary>
        /// The url for getting all todos.
        /// </summary>
        private const string GetAllUrl = "ToDos?userId={0}";

        /// <summary>
        /// The url for updating a todo.
        /// </summary>
        private const string UpdateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's creation.
        /// </summary>
        private const string CreateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's deletion.
        /// </summary>
        private const string DeleteUrl = "ToDos/{0}";

        private readonly HttpClient httpClient;
        private IItemRepository _itemRepository;
        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService()
        {
            httpClient = new HttpClient();
            _itemRepository = new ItemRepository();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public IList<ToDoItemViewModel> GetItems(int userId)
        {
            var itemResult = _itemRepository.GetItems(userId).Select(i => i.ToViewModel()).ToList();

            if (itemResult.Count != 0)
            {
                return itemResult;
            }
            else
            {
                var dataAsString = httpClient.GetStringAsync(string.Format(serviceApiUrl + GetAllUrl, userId)).Result;
                var userViewItems = JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
                var items = userViewItems.Select(i => i.ToItem()).ToList();
                foreach (var elem in items)
                {
                    _itemRepository.Create(elem);
                }
                _itemRepository.Save();
                return userViewItems;
            }

        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
        /// </summary>
        /// <param name="item">The todo to create.</param>
        public void CreateItem(ToDoItemViewModel item)
        {
            item.ToDoId = _itemRepository.GetItems(item.UserId).Last().ToDoId+1;
            _itemRepository.Create(item.ToItem());
            _itemRepository.Save();
            ThreadPool.QueueUserWorkItem(t => httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, item)
                .Result.EnsureSuccessStatusCode());

        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="item">The todo to update.</param>
        public void UpdateItem(ToDoItemViewModel item)
        {
            _itemRepository.Update(item.ToItem());
            _itemRepository.Save();
            ThreadPool.QueueUserWorkItem(t => httpClient.PutAsJsonAsync(serviceApiUrl + UpdateUrl, item)
            .Result.EnsureSuccessStatusCode());

        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public void DeleteItem(int id)
        {
            _itemRepository.Delete(id);
            _itemRepository.Save();
            ThreadPool.QueueUserWorkItem(t => httpClient.DeleteAsync(string.Format(serviceApiUrl + DeleteUrl, id))
                .Result.EnsureSuccessStatusCode());


        }
    }
}