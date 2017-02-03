using AutoMapper;
using ToDoClient.Models;
using ToDoLogic.DTO;

namespace ToDoClient.Mapping
{
    public class ClientMapperConfiguration
    {

        public static MapperConfiguration GetConfiguration()
        {
            return new MapperConfiguration(cfg => {

                cfg.CreateMap<TaskViewModel, TaskDto>();
                cfg.CreateMap<TaskDto, TaskViewModel>();

            });
        }

    }
}