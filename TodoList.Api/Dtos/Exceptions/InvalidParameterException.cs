using TodoList.Api.Dtos.Exceptions;

namespace TodoList.Api.Services
{
    public class InvalidParameterException : ErrorBase
    {
        public InvalidParameterException(string errorMessage) : base(errorMessage)
        {
        }
    }
}