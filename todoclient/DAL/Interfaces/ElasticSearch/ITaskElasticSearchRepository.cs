﻿using DAL.Entities.ElasticSearch;
using System;
using System.Collections.Generic;
using Nest;

namespace DAL.Interfaces.ElasticSearch
{
    public interface ITaskElasticSearchRepository
    {
        void Create(ElasticSearchTask item);
        void Update(ElasticSearchTask item);
        ISearchResponse<ElasticSearchTask> GetByName(string name);
        void Delete(int key);
        IEnumerable<ElasticSearchTask> GetItems();
    }
}
