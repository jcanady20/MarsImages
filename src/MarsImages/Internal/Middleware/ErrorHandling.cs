using System;
using System.Net;
using System.Threading.Tasks;
using MarsImages.Internal.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;


namespace MarsImages.Internal.Middleware
{
     public class ErrorHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public ErrorHandling(ILogger<ErrorHandling> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                _logger?.LogError(e, e.Message);
                await ExceptionHandler(context, e);
            }
        }

        private Task ExceptionHandler(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            if (exception is UnauthorizedAccessException)
            {
                code = HttpStatusCode.Forbidden;
            }
            else if (exception is System.IO.FileNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }
            else if (exception is NotImplementedException)
            {
                code = HttpStatusCode.NotImplemented;
            }

            var result = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                status = code,
                messages = exception.Traverse(p => p.InnerException, m => m.Message)
            }, Newtonsoft.Json.Formatting.None);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}