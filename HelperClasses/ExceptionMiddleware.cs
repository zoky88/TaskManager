using System.Net;
using System.Text.Json;

namespace TaskManager.HelperClasses
{
    /// <summary>
    /// Middleware to handle exceptions globally in the application.
    /// </summary>
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware to handle HTTP requests and catch exceptions.
        /// </summary>
        /// <param name="httpContext">The current HTTP context.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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

        /// <summary>
        /// Handles exceptions by setting the response status code and returning a JSON error response.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <param name="exception">The exception that occurred.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
