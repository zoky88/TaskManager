using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace TaskManager.HelperClasses
{
    /// <summary>
    /// Global middleware for handling unhandled exceptions.
    /// Logs the error and returns an appropriate JSON response.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<ExceptionMiddleware> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="env">The hosting environment.</param>
        /// <param name="logger">Logger instance.</param>
        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _env = env;
            _logger = logger;
        }

        /// <summary>
        /// Invokes the middleware and catches exceptions from the downstream pipeline.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>A task that completes when processing is complete.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(httpContext, ex, _env);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception, IWebHostEnvironment env)
        {
            context.Response.ContentType = "application/json";

            // Set the status code to 500 (Internal Server Error)
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            object response;
            if (env.IsDevelopment())
            {
                // In development, include detailed error information.
                response = new
                {
                    message = exception.Message,
                    stackTrace = exception.StackTrace
                };
            }
            else
            {
                // In production, do not expose detailed errors.
                response = new
                {
                    message = "An unexpected error occurred. Please try again later."
                };
            }

            // Serialize the response to JSON and return it.
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string jsonResponse = JsonSerializer.Serialize(response, options);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
