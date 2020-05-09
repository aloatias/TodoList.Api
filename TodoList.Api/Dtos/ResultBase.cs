using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Dtos
{
    public abstract class ResultBase
    {
        public StatusEnum Status { get; private set; }

        public ErrorBase Error { get; private set; }

        protected ResultBase(StatusEnum status)
        {
            Status = status;
        }

        protected ResultBase(ErrorBase error, StatusEnum status)
        {
            Error = error;
            Status = status;
        }
    }
}
