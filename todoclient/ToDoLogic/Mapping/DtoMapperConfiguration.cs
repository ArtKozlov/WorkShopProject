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

                cfg.CreateMap<TaskDto, ElasticSearchTask>().ReverseMap();
                cfg.CreateMap<UserDto, User>().ReverseMap();
                cfg.CreateMap<UserDto, ElasticSearchUser>().ReverseMap();
                cfg.CreateMap<User, ElasticSearchUser>();
                cfg.CreateMap<ElasticSearchTask, Task>();
                cfg.CreateMap<Task, TaskDto>().ReverseMap();
                cfg.CreateMap<Task, ElasticSearchTask>()
                        .ForMember(x => x.UserId,
                            x => x.MapFrom(src => src.User.Id));

            });
        }

 

    }
}
