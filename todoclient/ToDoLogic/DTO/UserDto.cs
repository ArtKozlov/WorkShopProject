

using System;
using System.Collections.Generic;

namespace ToDoLogic.DTO
{
    public class UserDto
    {
        public UserDto()
        {
            Tasks = new List<TaskDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public IList<TaskDto> Tasks { get; set; }
    }
}
