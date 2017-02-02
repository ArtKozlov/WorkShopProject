using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoClient.Models
{
    public class TaskViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

    }
}