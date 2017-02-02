using ToDoClient.Models;
using ToDoLogic.DTO;

namespace ToDoClient.Mapping
{
    public static class Mapper
    {

        public static TaskDto ToTaskDto(this TaskViewModel taskViewModel)
        {
            if (ReferenceEquals(taskViewModel, null))
                return null;

            return new TaskDto()
            {
                Id = taskViewModel.Id,
                Name = taskViewModel.Name,
                IsCompleted = taskViewModel.IsCompleted,
                UserId = taskViewModel.UserId
            };

        }

        public static TaskViewModel ToViewModel(this TaskDto taskDto)
        {
            if (ReferenceEquals(taskDto, null))
                return null;

            return new TaskViewModel()
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                IsCompleted = taskDto.IsCompleted,
                UserId = taskDto.UserId
            };

        }

    }
}