using System;
using AutoMapper;
using ToDoLogic.DTO;
using ToDoLogic.Interfaces;
using Task = ToDoDataAccess.Entities.ElasticSearch.Task;
using User = ToDoDataAccess.Entities.ElasticSearch.User;

namespace ToDoLogic.Mapping
{
    public class DomainMapper : IDomainMapper
    {
        private IMapper _mapper;

        private MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
        {

            cfg.CreateMap<TaskDto, Task>().ReverseMap();
            cfg.CreateMap<UserDto, ToDoDataAccess.Entities.NHibernate.User>().ReverseMap();
            cfg.CreateMap<UserDto, User>().ReverseMap();
            cfg.CreateMap<ToDoDataAccess.Entities.NHibernate.User, User>();
            cfg.CreateMap<Task, ToDoDataAccess.Entities.NHibernate.Task>();
            cfg.CreateMap<ToDoDataAccess.Entities.NHibernate.Task, TaskDto>().ReverseMap();
            cfg.CreateMap<ToDoDataAccess.Entities.NHibernate.Task, Task>()
                    .ForMember(x => x.UserId,
                        x => x.MapFrom(src => src.User.Id));

        });

        public DomainMapper()
        {
            _mapper = new Mapper(mapperConfiguration);
        }

        public IConfigurationProvider ConfigurationProvider
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Func<Type, object> ServiceCtor
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public object Map(object source, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public object Map(object source, object destination, Type sourceType, Type destinationType, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public TDestination Map<TDestination>(object source, Action<IMappingOperationOptions> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination, Action<IMappingOperationOptions<TSource, TDestination>> opts)
        {
            throw new NotImplementedException();
        }
    }
}
