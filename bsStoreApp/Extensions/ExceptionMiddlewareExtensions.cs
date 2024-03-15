using bsStoreApp.Entity.ErrorModel;
using bsStoreApp.Entity.Exceptions;
using bsStoreApp.Services.Contract;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace bsStoreApp.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication webApplication, ILoggerService loggerService)
        {
            webApplication.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null) 
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundExceptions => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };


                        loggerService.LogError($"Something went wrong: {contextFeature.Error}");
                        
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
