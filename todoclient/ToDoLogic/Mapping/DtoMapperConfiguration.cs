using AutoMapper;
using DAL.Entities.ElasticSearch;
using DAL.Entities.NHibernate;
using ToDoLogic.DTO;

namespace ToDoLogic.Mapping
{
    public class DtoMapperConfiguration
    {
        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(cfg => {

                cfg.CreateMap<Task, TaskDto>();
                cfg.CreateMap<TaskDto, Task>();
                cfg.CreateMap<TaskDto, ElasticSearchTask>();
                cfg.CreateMap<ElasticSearchTask, TaskDto>();
                cfg.CreateMap<UserDto, User>();

            });
        }

 

    }
}
