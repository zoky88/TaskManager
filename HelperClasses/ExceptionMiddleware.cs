﻿using System.Net;
using System.Text.Json;

namespace TaskManager.HelperClasses
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var response = new
            {
                message = exception.Message,
                stackTrace = exception.StackTrace // only for debugging purposes
            };
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
