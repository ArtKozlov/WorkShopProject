using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;

namespace DAL.Interfaces.ElasticSearch
{
    public interface IUnitOfWorkElasticSearch
    {
        ElasticClient Tasks { get; }

    }
}
