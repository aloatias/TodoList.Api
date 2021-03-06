﻿using TodoList.Api.DataAccess;
using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Dtos
{
    public class UpdateTaskStatusResult : ResultBase
    {
        public TodoTask Task { get; private set; }

        public UpdateTaskStatusResult(TodoTask task, StatusEnum status) : base(status)
        {
            Task = task;
        }

        public UpdateTaskStatusResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }
    }
}