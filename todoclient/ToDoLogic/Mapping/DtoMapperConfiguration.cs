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

                cfg.CreateMap<Task, TaskDto>()
                    .ForMember(x => x.User,
                        x => x.Ignore());
                cfg.CreateMap<TaskDto, Task>();
                cfg.CreateMap<TaskDto, ElasticSearchTask>()
                    .ForMember(x => x.User,
                        x => x.Ignore());
                cfg.CreateMap<ElasticSearchTask, TaskDto>();
                cfg.CreateMap<UserDto, User>();
                cfg.CreateMap<User, UserDto>();

            });
        }

 

    }
}
