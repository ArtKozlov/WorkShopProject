﻿using System;
using AutoMapper;
using ToDoClient.Mapping.Interface;
using ToDoClient.Models;
using ToDoLogic.DTO;

namespace ToDoClient.Mapping
{
    public class ClientMapper : IClientMapper
    {
        private IMapper _mapper;
        private MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg => 
        {

            cfg.CreateMap<TaskViewModel, TaskDto>().ReverseMap();

        });

        public ClientMapper()
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