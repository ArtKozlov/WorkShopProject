using System;
using System.Configuration;
using AutoMapper;
using ToDoDataAccess.Repositories.ElasticSearch;
using Microsoft.Practices.Unity;
using Nest;
using todoclient.Mapping;
using todoclient.Mapping.Interface;
using ToDoDataAccess.Interfaces.ElasticSearch;
using ToDoDataAccess.Interfaces.NHibernate;
using ToDoDataAccess.Repositories.NHibernate;
using ToDoLogic.Interfaces;
using ToDoLogic.Mapping;
using ToDoLogic.Services;

namespace ToDoClient.DependencyResolver
{
    public class UnityConfig
    {
        public static IUnityContainer BuildUnityContainer()
        {
            UnityContainer container = new UnityContainer();
            container.RegisterType<IToDoService, ToDoService>();
            //  container.RegisterType<ITaskRepository, TaskRepository>();
            // container.RegisterType<ITaskElasticRepository, TaskElasticRepository>();
            // container.RegisterType<IUserRepository, UserRepository>();
            // container.RegisterType<IUserElasticRepository, UserElasticRepository>();
            container.RegisterType<IUnitOfWorkElastic, UnitOfWorkElastic>();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IClientMapper, ClientMapper>();
            container.RegisterType<IDomainMapper, DomainMapper>();

            return container;
        }
    }
}
