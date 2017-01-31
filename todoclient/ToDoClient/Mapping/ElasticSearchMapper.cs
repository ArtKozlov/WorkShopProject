using ElasticSearch.Indices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoClient.Models;

namespace todoclient.Mapping
{
    public static class ElasticSearchMapper
    {

        public static ItemIdx ToItemIdx(this ToDoItemViewModel toDoItemViewModel)
        {
            if (ReferenceEquals(toDoItemViewModel, null))
                return null;

            return new ItemIdx()
            {
                Id = toDoItemViewModel.Id,
                Name = toDoItemViewModel.Name,
                IsCompleted = toDoItemViewModel.IsCompleted,
                UserId = toDoItemViewModel.UserId
            };

        }

        public static ToDoItemViewModel ToViewModel(this ItemIdx toDoItem)
        {
            if (ReferenceEquals(toDoItem, null))
                return null;

            return new ToDoItemViewModel()
            {
                Id = toDoItem.Id,
                Name = toDoItem.Name,
                IsCompleted = toDoItem.IsCompleted,
                UserId = toDoItem.UserId
            };

        }
    }
}