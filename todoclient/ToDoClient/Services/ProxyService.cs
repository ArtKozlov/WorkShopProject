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
using System.Web;

namespace todoclient.Services
{
    public class ProxyService
    {
        private readonly ToDoListContext _context;
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        private IItemRepository _itemRepository;

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
        public ProxyService()
        {
            _context = new ToDoListContext();
            httpClient = new HttpClient();
            _itemRepository = new ItemRepository();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void UploadDB()
        {
            var listUsers = _context.Items.GroupBy(i => i.UserId).Select(i => i).ToList();
            var 
            ThreadPool.QueueUserWorkItem(t => httpClient.DeleteAsync(string.Format(serviceApiUrl + DeleteUrl, id))
                .Result.EnsureSuccessStatusCode());
        }
    }
}