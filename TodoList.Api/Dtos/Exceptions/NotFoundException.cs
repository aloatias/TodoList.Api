namespace TodoList.Api.Dtos.Exceptions
{
    public class NotFoundException : Error
    {
        public NotFoundException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
