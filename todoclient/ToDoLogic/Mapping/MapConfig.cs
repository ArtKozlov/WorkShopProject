using AutoMapper;
using DAL.Entities.ElasticSearch;
using DAL.Entities.NHibernate;
using ToDoLogic.DTO;

namespace ToDoLogic.Mapping
{
    public class MapConfig
    {
        public static void CreateMapTaskToTaskDto()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<Task, TaskDto>()
                .ForSourceMember(x => x.Id, y => y.Ignore())
                .ForSourceMember(x => x.Name, y => y.Ignore())
                .ForSourceMember(x => x.IsCompleted, y => y.Ignore())
                .ForSourceMember(x => x.CreatedDate, y => y.Ignore())
                .ForSourceMember(x => x.UserId, y => y.Ignore()));
        }

        public static void CreateMapElasticSearchTaskToTaskDto()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<ElasticSearchTask, TaskDto>()
                .ForSourceMember(x => x.Id, y => y.Ignore())
                .ForSourceMember(x => x.Name, y => y.Ignore())
                .ForSourceMember(x => x.IsCompleted, y => y.Ignore())
                .ForSourceMember(x => x.CreatedDate, y => y.Ignore())
                .ForSourceMember(x => x.UserId, y => y.Ignore()));
        }
        public static void CreateMapTaskDtoToTask()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TaskDto, Task>()
                .ForSourceMember(x => x.Id, y => y.Ignore())
                .ForSourceMember(x => x.Name, y => y.Ignore())
                .ForSourceMember(x => x.IsCompleted, y => y.Ignore())
                .ForSourceMember(x => x.CreatedDate, y => y.Ignore())
                .ForSourceMember(x => x.UserId, y => y.Ignore()));
        }
        public static void CreateMapTaskDtoToElasticSearchTask()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TaskDto, ElasticSearchTask>()
                .ForSourceMember(x => x.Id, y => y.Ignore())
                .ForSourceMember(x => x.Name, y => y.Ignore())
                .ForSourceMember(x => x.IsCompleted, y => y.Ignore())
                .ForSourceMember(x => x.CreatedDate, y => y.Ignore())
                .ForSourceMember(x => x.UserId, y => y.Ignore()));
        }

    }
}
