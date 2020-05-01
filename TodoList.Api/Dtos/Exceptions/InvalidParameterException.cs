using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Services
{
    public class NullArgumentException : Error
    {
        public NullArgumentException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}