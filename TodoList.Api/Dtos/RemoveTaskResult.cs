using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Dtos
{
    public class RemoveTaskResult : ResultBase
    {
        public RemoveTaskResult(StatusEnum status) : base(status)
        {
        }

        public RemoveTaskResult(Error error, StatusEnum status) : base(error, status)
        {
        }
    }
}