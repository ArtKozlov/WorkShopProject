
using DAL.Entities.ElasticSearch;
using DAL.Entities.NHibernate;
using ToDoLogic.DTO;

namespace ToDoLogic.Mapping
{
    public static class Mapper
    {
        #region BLL to DB entities mapping
        public static Task ToTask(this TaskDto taskDto)
        {
            if (ReferenceEquals(taskDto, null))
                return null;

            return new Task()
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                IsCompleted = taskDto.IsCompleted,
                CreatedDate = taskDto.CreatedDate,
                UserId = taskDto.UserId
            };

        }

        public static TaskDto ToTaskDto(this Task task)
        {
            if (ReferenceEquals(task, null))
                return null;

            return new TaskDto()
            {
                Id = task.Id,
                Name = task.Name,
                IsCompleted = task.IsCompleted,
                CreatedDate = task.CreatedDate,
                UserId = task.UserId
            };

        }
        #endregion

        #region BLL to ElastiSearch entities mapping
        public static ElasticSearchTask ToElsticSearchTask(this TaskDto taskDto)
        {
            if (ReferenceEquals(taskDto, null))
                return null;

            return new ElasticSearchTask()
            {
                Id = taskDto.Id,
                Name = taskDto.Name,
                IsCompleted = taskDto.IsCompleted,
                CreatedDate = taskDto.CreatedDate,
                UserId = taskDto.UserId
            };

        }

        public static TaskDto ToTaskDto(this ElasticSearchTask task)
        {
            if (ReferenceEquals(task, null))
                return null;

            return new TaskDto()
            {
                Id = task.Id,
                Name = task.Name,
                IsCompleted = task.IsCompleted,
                CreatedDate = task.CreatedDate,
                UserId = task.UserId
            };

        }
        #endregion
    }
}
