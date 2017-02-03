using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace DAL.ElasticSearch
{
    public class TaskElasticClient : ElasticClient
    {
        public TaskElasticClient() : base(new ConnectionSettings(
            new Uri(ConfigurationManager.AppSettings["ElasticSearchUrl"]))
            .DefaultIndex("todorepository").DefaultTypeNameInferrer(t => "task"))
        {
            
        }
    }
}
