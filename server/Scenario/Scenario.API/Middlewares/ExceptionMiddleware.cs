using Scenario.Application.Exceptions;

namespace Scenario.API.Middlewares.ExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                var errors = new Dictionary<string, string>();
                httpContext.Response.StatusCode = 500;
                if (ex is CustomException customException)
                {
                    message = customException.Message;
                    errors = customException.Errors;
                    httpContext.Response.StatusCode = customException.Code;
                }
                await httpContext.Response.WriteAsJsonAsync(new { message, errors });
            }
        }
    }
}
