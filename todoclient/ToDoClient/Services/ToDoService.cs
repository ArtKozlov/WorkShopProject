using System;
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
using DAL.Entities;
using todoclient.Interfaces;
using todoclient.Services;
using ElasticSearch.Interfaces;
using ElasticSearch.Queries;
using ElasticSearch.Indices;

namespace ToDoClient.Services
{
    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class ToDoService : IToDoService
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
        
        private IRestQueries _itemQueries;

       // private ProxyService _proxyService;
        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService(IItemRepository itemRepository, IRestQueries itemQueries)
        {
            _httpClient = new HttpClient();
            _itemRepository = itemRepository;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _itemQueries = itemQueries;

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
                if(!ProxyService.listOfUsersId.Contains(userId))
                    ProxyService.listOfUsersId.Enqueue(userId);
                
                return itemResult;
            }

            //string dataAsString =
            //    _httpClient.GetStringAsync(string.Format(_serviceApiUrl + GetAllUrl, userId)).Result;

            //IList<ToDoItemViewModel> userViewItems =
            //    JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);

            //List<Item> items = userViewItems.Select(i => i.ToItem()).ToList();

            //foreach (Item elem in items)
            //{
            //    _itemRepository.Create(elem);
            //}

            //return userViewItems;

            return itemResult;

        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
        /// </summary>
        /// <param name="item">The todo to create.</param>
        public void CreateItem(ToDoItemViewModel item)
        {
            _itemRepository.Create(item.ToItem());
            //ItemIdx itemIdx = item.ToItemIdx();
            //itemIdx.Id = _itemRepository.GetItems(item.UserId).Last().Id;
            _itemQueries.Create(item.ToItemIdx());
            
        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="item">The todo to update.</param>
        public void UpdateItem(ToDoItemViewModel item)
        {
            _itemRepository.Update(item.ToItem());
            _itemQueries.Update(item.ToItemIdx());
        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public void DeleteItem(int id)
        {
            _itemRepository.Delete(id);
            _itemQueries.Delete(id);

        }
    }
}