using System.Collections.Generic;
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
                cfg.CreateMap<Task, ElasticSearchTask>()
                        .ForMember(x => x.UserId,
                            x => x.MapFrom(src => src.User.Id));
                cfg.CreateMap<ElasticSearchTask, TaskDto>();
                cfg.CreateMap<ElasticSearchTask, Task>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<ElasticSearchUser, UserDto>();
                cfg.CreateMap<UserDto, ElasticSearchUser>();
                cfg.CreateMap<User, ElasticSearchUser>();

            });
        }

 

    }
}
