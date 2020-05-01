using System;

namespace TodoList.Api.DataAccess
{
    public class TodoTask
    {
        public Guid Id { get; set; }

        public string Description { get; set; }
    }
}