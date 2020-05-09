namespace TodoList.Api.Dtos.Exceptions
{
    public abstract class ErrorBase
    {
        public string ErrorMessage { get; private set; }

        protected ErrorBase(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
