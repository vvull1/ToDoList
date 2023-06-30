using ToDoList.EfCore;
using ToDoList.Models;
using ToDoList.Models.Utility;

namespace ToDoList.Services
{
    public class ExceptionLoggerService : IExceptionLoggerService
    {
        private readonly ToDoContext _context;
        private readonly CliamService _cliamService;

        public ExceptionLoggerService(ToDoContext context, CliamService cliamService)
        {
            _context = context;
            _cliamService = cliamService;
        }

        public async Task<string> ExpectionLogger(string? serviceName, string? Controller, string? exception)
        {
            var log = new LoggerTable();

            log.Service = serviceName;
            log.Controller = Controller;
            log.Exception = exception;
            log.CreatedTime= DateTime.UtcNow;  

            await _context.Loggers.AddAsync(log);
            await _context.SaveChangesAsync(); 

            return log.LoggerId.ToString();
        }

        public async Task<string> ActivityLogger(string? serviceName,string? Controller)
        {
            var Activitylog = new ActivityLogger();

            Activitylog.Service = serviceName;
            Activitylog.ActivityPerformed = Controller;
            Activitylog.UserId = _cliamService.GetCurrentUserId();
            Activitylog.CreatedDateTime = DateTime.UtcNow;

             _context.ActivityLoggers.Add(Activitylog);
            await _context.SaveChangesAsync();

            return Activitylog.ActivityLoggerId.ToString();
        }
    }
}
