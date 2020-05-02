using System;

namespace TodoList.Api.DataAccess
{
    public class TodoTask
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public bool Done { get; set; }

        public DateTime CreationDate { get; set; }
    }
}