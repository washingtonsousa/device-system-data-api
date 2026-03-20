using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace DeviceSystemDataAPI.Filters
{
    public class RequestsExceptionsHandlerFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            var errorMessage = new ErrorMessageResult(context.Exception.Message);
            IStatusCodeActionResult result = new ObjectResult(errorMessage) { StatusCode = 500 };

            if (context.Exception is KeyNotFoundException)
                result = new ObjectResult(errorMessage) { StatusCode = 404 };

            if (context.Exception is InvalidOperationException)
                result = new ObjectResult(errorMessage) { StatusCode = 422 };

            if (context.Exception is ArgumentException)
                result = new ObjectResult(errorMessage) { StatusCode = 400 };

            context.Result = result;

        }
    }

    public class ErrorMessageResult
    {
        public ErrorMessageResult(string message, object? data = null)
        {
            Data = data;
            Message = message;
        }

        public object? Data { get; }
        public string Message { get; }
    }
}
