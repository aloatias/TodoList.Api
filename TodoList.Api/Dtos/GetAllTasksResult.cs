using System.Collections.Generic;
using TodoList.Api.DataAccess;
using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Dtos
{
    public class GetAllTasksResult : ResultBase
    {
        public List<TodoTask> Tasks { get; private set; }

        public GetAllTasksResult(ErrorBase error, StatusEnum status) : base(error, status)
        {
        }

        public GetAllTasksResult(List<TodoTask> tasks, StatusEnum status) : base(status)
        {
            Tasks = tasks;
        }
    }
}