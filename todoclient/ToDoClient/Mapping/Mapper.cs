using DAL.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoClient.Models;

namespace todoclient.Mapping
{
    public static class Mapper
    {
        #region to ViewModel mapping
        //public static User ToTaskDto(this ToDoItemViewModel itemEntity)
        //{
        //    if (ReferenceEquals(itemEntity, null))
        //        return null;
        //    return new User()
        //    {
        //        Id = itemEntity.ToDoId,
        //        Name = itemEntity.
        //    };
        //}

        public static ToDoItemViewModel ToViewModel(this Item toDoItemViewModel)
        {
            if (ReferenceEquals(toDoItemViewModel, null))
                return null;
            
                return new ToDoItemViewModel()
                {
                    ToDoId = toDoItemViewModel.Id,
                    Name = toDoItemViewModel.Name,
                    IsCompleted = toDoItemViewModel.IsCompleted,
                    UserId = toDoItemViewModel.Id
                };

        }
    }
}