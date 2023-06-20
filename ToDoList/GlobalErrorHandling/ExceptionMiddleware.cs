using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using ToDoList.Models.DTO;

namespace ToDoList.GlobalErrorHandling
{
    public static class ExceptionMiddleware
    {
        //public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger<ExceptionHandlerMiddleware> logger)
        //{
        //    logger.LogInformation("Configuring Exception Handler middleware");
        //    app.UseExceptionHandler(errorApp =>
        //    {
        //        errorApp.Run(async context =>
        //        {
        //            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        //            context.Response.ContentType = "application/json";

        //            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if (contextFeature != null)
        //            {
        //                logger.LogError($"Something went wrong: {contextFeature.Error}");

        //                await context.Response.WriteAsync(new GlobalErrorDTO()
        //                {
        //                    StatusCode = context.Response.StatusCode,
        //                    Message = "Internal Server Error."
        //                }.ToString());
        //            }
        //        });
        //    });

        //}

        
    }
}
