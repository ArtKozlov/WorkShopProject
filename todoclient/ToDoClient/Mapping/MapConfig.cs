using AutoMapper;
using ToDoClient.Models;
using ToDoLogic.DTO;

namespace ToDoClient.Mapping
{
    public class MapConfig
    {

        public static void CreateMapTaskDtoToTaskViewModel()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TaskDto, TaskViewModel>()
                .IgnoreAllPropertiesWithAnInaccessibleSetter()
                .ForSourceMember(x => x.Id, y => y.Ignore())
                .ForSourceMember(x => x.Name, y => y.Ignore())
                .ForSourceMember(x => x.IsCompleted, y => y.Ignore())
                .ForSourceMember(x => x.UserId, y => y.Ignore()));
        }
        public static void CreateMapTaskViewModelToTaskDto()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<TaskViewModel, TaskDto>()
                .ForMember(x => x.CreatedDate, y => y.Ignore())
                .ForSourceMember(x => x.Id, y => y.Ignore())
                .ForSourceMember(x => x.Name, y => y.Ignore())
                .ForSourceMember(x => x.IsCompleted, y => y.Ignore())
                .ForSourceMember(x => x.UserId, y => y.Ignore()));
        }

    }
}