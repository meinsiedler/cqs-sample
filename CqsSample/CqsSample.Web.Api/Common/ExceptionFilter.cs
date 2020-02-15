using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CqsSample.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CqsSample.Web.Api.Common
{
    public class ExceptionFilter : IAsyncExceptionFilter
    {
        public async Task OnExceptionAsync(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "text/plain";
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var message = "Internal Server Error.";

            var ex = context.Exception;

            if (ex != null)
            {
                if (ex is UnauthorizedAccessException)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    message = "Access denied.";
                }

                if (ex is ForbiddenException forbiddenException)
                {
                    context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                    message = "Forbidden. " + forbiddenException.Message;
                }


                context.ExceptionHandled = true;
                await context.HttpContext.Response.WriteAsync(message).ConfigureAwait(false);
            }
        }
    }
}
