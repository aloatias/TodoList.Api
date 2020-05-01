namespace TodoList.Api.Dtos.Exceptions
{
    public abstract class Error
    {
        public string ErrorMessage { get; protected set; }
    }
}
