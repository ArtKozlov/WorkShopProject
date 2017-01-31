using DAL.Entities;
using ToDoClient.Models;

namespace todoclient.Mapping
{
    public static class Mapper
    {

        public static Item ToItem(this ToDoItemViewModel toDoItemViewModel)
        {
            if (ReferenceEquals(toDoItemViewModel, null))
                return null;

            return new Item()
            {
                Id = toDoItemViewModel.Id,
                Name = toDoItemViewModel.Name,
                IsCompleted = toDoItemViewModel.IsCompleted,
                UserId = toDoItemViewModel.UserId
            };

        }

        public static ToDoItemViewModel ToViewModel(this Item toDoItem)
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