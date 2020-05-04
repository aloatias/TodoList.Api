using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Dtos
{
    public class DeleteTaskResult : ResultBase
    {
        public DeleteTaskResult(StatusEnum status) : base(status)
        {
        }

        public DeleteTaskResult(Error error, StatusEnum status) : base(error, status)
        {
        }
    }
}