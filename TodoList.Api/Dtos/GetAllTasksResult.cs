using System.Collections.Generic;
using TodoList.Api.DataAccess;

namespace TodoList.Api.Dtos
{
    public class GetAllTasksResult : ResultBase
    {
        public List<TodoTask> Tasks { get; private set; }

        public GetAllTasksResult(StatusEnum status) : base(status)
        {
        }

        public GetAllTasksResult(List<TodoTask> tasks, StatusEnum status) : base(status)
        {
            Tasks = tasks;
        }
    }
}