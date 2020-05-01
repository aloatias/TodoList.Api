namespace TodoList.Api.Dtos.Exceptions
{
    public class DuplicatedArgumentException : Error
    {
        public DuplicatedArgumentException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}