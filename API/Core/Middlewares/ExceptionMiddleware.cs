using API.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;

namespace API.Core.Middlewares;

internal static class ExceptionMiddleware
{
    internal static IApplicationBuilder UseNativeGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                var errorFeature = context.Features.Get<IExceptionHandlerFeature>();

                var exception = errorFeature.Error;

                var errorResponse = exception switch
                {
                    Exception apiException => new ErrorResponse(apiException),
                    _ => new ErrorResponse(exception)
                };
                context.Response.StatusCode = (int)errorResponse.StatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(errorResponse.ToJsonString());
            });
        });

        return app;
    }
}