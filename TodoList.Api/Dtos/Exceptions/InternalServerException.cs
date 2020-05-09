namespace TodoList.Api.Dtos.Exceptions
{
    public class InternalServerException : ErrorBase
    {
        public InternalServerException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
