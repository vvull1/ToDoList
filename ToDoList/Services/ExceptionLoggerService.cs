using ToDoList.EfCore;
using ToDoList.Models;

namespace ToDoList.Services
{
    public class ExceptionLoggerService : IExceptionLoggerService
    {
        private readonly ToDoContext _context;

        public ExceptionLoggerService(ToDoContext context)
        {
            _context = context;
        }

        public async Task<string> ExpectionLogger(string? serviceName, string? Controller, string? exception)
        {
            var log = new LoggerTable();

            log.Service = serviceName;
            log.Controller = Controller;
            log.Exception = exception;
            log.CreatedTime= DateTime.UtcNow;  

            await _context.Loggers.AddAsync(log);
            await _context.SaveChangesAsync(); //getting error while saving changes

            return log.LoggerId.ToString();
        }
    }
}
