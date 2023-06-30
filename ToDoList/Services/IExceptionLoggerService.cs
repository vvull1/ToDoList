namespace ToDoList.Services
{
    public interface IExceptionLoggerService
    {
        Task<string> ExpectionLogger(string? serviceName, string? Controller, string? exception);
        Task<string> ActivityLogger(string? serviceName, string? Controller);
    }
}
