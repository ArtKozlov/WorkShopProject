﻿using System;
using NHibernate;
using ToDoDataAccess.Interfaces.NHibernate;

namespace ToDoDataAccess.Repositories.NHibernate
{
    public class UnitOfWork : IUnitOfWork
    {
        
        private ITaskRepository _taskRepository;
        private IUserRepository _userRepository;

        public ITaskRepository Tasks
        {
            get
            {
                if (ReferenceEquals(_taskRepository, null))
                    _taskRepository = new TaskRepository();
                return _taskRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (ReferenceEquals(_userRepository, null))
                    _userRepository = new UserRepository();
                return _userRepository;
            }
        }
    }
}
