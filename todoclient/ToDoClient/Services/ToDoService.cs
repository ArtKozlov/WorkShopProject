using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using ToDoClient.Models;
using DAL.Interfaces;
using DAL.Repositories;
using todoclient.Mapping;
using System.Linq;
using System.Reflection;
using System.Threading;
using DAL.Entities;
using todoclient.Services;

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
        private readonly string _serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        /// <summary>
        /// The url for getting all todos.
        /// </summary>
        private const string GetAllUrl = "ToDos?userId={0}";


        private readonly HttpClient _httpClient;

        private readonly IItemRepository _itemRepository;

        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService()
        {
            _httpClient = new HttpClient();
            _itemRepository = new ItemRepository();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        }

        static ToDoService()
        {
            StartProxy();
        }

        public static void StartProxy()
        {

            ProxyService proxy = new ProxyService();
            new Thread(() => proxy.UpdateAzureService()).Start();
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

            string dataAsString = 
                _httpClient.GetStringAsync(string.Format(_serviceApiUrl + GetAllUrl, userId)).Result;

            IList<ToDoItemViewModel> userViewItems =
                JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);

            List<Item> items = userViewItems.Select(i => i.ToItem()).ToList();

            foreach (Item elem in items)
            {
                _itemRepository.Create(elem);
            }

            return userViewItems;

        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
        /// </summary>
        /// <param name="item">The todo to create.</param>
        public void CreateItem(ToDoItemViewModel item)
        {

            _itemRepository.Create(item.ToItem());

        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="item">The todo to update.</param>
        public void UpdateItem(ToDoItemViewModel item)
        {
            _itemRepository.Update(item.ToItem());

        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public void DeleteItem(int id)
        {
            _itemRepository.Delete(id);

        }
    }
}