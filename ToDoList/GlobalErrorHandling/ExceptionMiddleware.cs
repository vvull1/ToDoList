using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using ToDoList.Models.DTO;
using ToDoList.Models.Utility;
using ToDoList.Services;

namespace ToDoList.GlobalErrorHandling
{
    public class ExceptionMiddleware
    {
        RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext context, IExceptionLoggerService exceptionLoggerService)
        {
            var controllerName = context.GetRouteData()?.Values["controller"]?.ToString() ?? string.Empty;
            var actionName = context.GetRouteData()?.Values["action"]?.ToString() ?? string.Empty;

            try
            {

                //await exceptionLoggerService.ActivityLogger(actionName, controllerName);

                await _requestDelegate(context);
            }
            catch (Exception ex)
            {

                //context
                string requestInfo = string.Empty;
                if (context.Request.Body?.CanRead ?? false && (!context.Request.Path.ToString().Contains("/api/AuthenticationAPI/UserLogin", StringComparison.OrdinalIgnoreCase) && !context.Request.Path.ToString().Contains("/api/AuthenticationAPI/UserLogin", StringComparison.OrdinalIgnoreCase)))
                {
                    using StreamReader streamReader = new StreamReader(context.Request.Body);
                    requestInfo = await streamReader.ReadToEndAsync();
                }

                await exceptionLoggerService.ExpectionLogger(actionName, controllerName, ex.Message);

            }
        }
    }

    public static class LogExceptionExtension
    {
        public static IApplicationBuilder UseLogException(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
