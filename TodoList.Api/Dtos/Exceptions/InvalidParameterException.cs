using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Services
{
    public class InvalidParameterException : Error
    {
        public InvalidParameterException(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}