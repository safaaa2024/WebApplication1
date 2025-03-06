using System.Net;

namespace WebApplication1.EroorHandelr
{
    public class ExeptionMidelWare
    {
        
            private readonly RequestDelegate _next;
            private readonly ILogger<ExeptionMidelWare> _logger;

            public ExeptionMidelWare(RequestDelegate next, ILogger<ExeptionMidelWare> logger)
            {
                _next = next;
                _logger = logger;
            }

            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unhandled exception occurred.");
                    await HandleExceptionAsync(context, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                return context.Response.WriteAsync(new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "An internal error occurred. Please try again later."
                }.ToString());
            }
        }

    
}
