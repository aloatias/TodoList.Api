namespace TodoList.Api.Dtos.Exceptions
{
    public class DuplicatedArgumentException : ErrorBase
    {
        public DuplicatedArgumentException(string errorMessage) : base(errorMessage)
        {
        }
    }
}