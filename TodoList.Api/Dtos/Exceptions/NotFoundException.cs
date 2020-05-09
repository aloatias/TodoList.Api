namespace TodoList.Api.Dtos.Exceptions
{
    public class NotFoundException : ErrorBase
    {
        public NotFoundException(string errorMessage) : base(errorMessage)
        {
        }
    }
}
