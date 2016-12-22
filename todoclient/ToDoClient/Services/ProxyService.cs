using DAL.Context;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using Newtonsoft.Json;
using todoclient.Mapping;
using ToDoClient.Models;

namespace todoclient.Services
{
    public class ProxyService : MarshalByRefObject
    {
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string _serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        private readonly IItemRepository _itemRepository;

        /// <summary>
        /// The url for getting all todos.
        /// </summary>
        private const string GetAllUrl = "ToDos?userId={0}";
        

        /// <summary>
        /// The url for a todo's creation.
        /// </summary>
        private const string CreateUrl = "ToDos";

        /// <summary>
        /// The url for a todo's deletion.
        /// </summary>
        private const string DeleteUrl = "ToDos/{0}";
        public static int UserId { get; set; }
        
        private readonly HttpClient _httpClient;
        public ProxyService()
        {
            _httpClient = new HttpClient();
            _itemRepository = new ItemRepository();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UpdateAzureService()
        {
            while (true)
            {
                var dataAsString = _httpClient.GetStringAsync(string.Format(_serviceApiUrl + GetAllUrl, UserId)).Result;
                var userViewItems = JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
                var itemsIdsFromAzureService = userViewItems.Select(i => i.ToDoId.ToString());
                foreach (var id in itemsIdsFromAzureService)
                {
                    _httpClient.DeleteAsync(string.Format(_serviceApiUrl + DeleteUrl, id))
                            .Result.EnsureSuccessStatusCode();
                }
                var listOfItemsFromDb = _itemRepository.GetItems(UserId).Where(i => i.UserId == UserId).Select(i => i.ToViewModel());
                foreach (var item in listOfItemsFromDb)
                {
                    ThreadPool.QueueUserWorkItem(t => _httpClient.PostAsJsonAsync(_serviceApiUrl + CreateUrl, item)
                        .Result.EnsureSuccessStatusCode());
                }

            }
        }
    }
}