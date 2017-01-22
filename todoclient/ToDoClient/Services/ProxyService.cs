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
        public static Queue<int> listOfUsersId = new Queue<int>();
        private Thread ProxyThread { get; set; }

        private readonly HttpClient _httpClient;
        private static ProxyService instance;

        private static object syncRoot = new Object();

        public ProxyService()
        {
            _httpClient = new HttpClient();
            _itemRepository = new ItemRepository();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
         
        }

        public static ProxyService GetInstance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    { 
                        instance = new ProxyService();
                        instance.StartProxy();
                    }
                }
            }
            return instance;
        }
        public void StartProxy()
        {
            ProxyThread = new Thread(UpdateAzureService) { IsBackground = true };
            ProxyThread.Start();
        }
        private void UpdateAzureService()
        {
            while(true)
            {
                if(listOfUsersId.Count != 0)
                { 
                    int userId = listOfUsersId.Dequeue();
                    var dataAsString = _httpClient.GetStringAsync(string.Format(_serviceApiUrl + GetAllUrl, userId)).Result;
                    var userViewItems = JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
                    var itemsIdsFromAzureService = userViewItems.Select(i => i.ToDoId.ToString());

                    foreach (var id in itemsIdsFromAzureService)
                    {
                        _httpClient.DeleteAsync(string.Format(_serviceApiUrl + DeleteUrl, id))
                            .Result.EnsureSuccessStatusCode();
                    }

                    IEnumerable<ToDoItemViewModel> listOfItemsFromDb =
                        _itemRepository.GetItems(userId).Where(i => i.UserId == userId).Select(i => i.ToViewModel());

                    foreach (var item in listOfItemsFromDb)
                    {
                        _httpClient.PostAsJsonAsync(_serviceApiUrl + CreateUrl, item)
                            .Result.EnsureSuccessStatusCode();
                    }
                }
            }
        }
    }
}