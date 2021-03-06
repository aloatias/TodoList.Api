﻿using TodoList.Api.DataAccess;
using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Dtos
{
    public class AddTaskResult : ResultBase
    {
        public TodoTask Task { get; private set; }

        public AddTaskResult(TodoTask task, StatusEnum status) : base(status)
        {
            Task = task;
        }

        public AddTaskResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}
