using Entities.Exceptions;
using Entities.Exceptions.NotFound;
using Microsoft.AspNetCore.Diagnostics;

namespace HospitalApi.ExceptionHandling;
public class GlobalExceptionHandling : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {

        if (exception is NotFoundException)
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        if (exception is BusinessRuleViolationException)
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

        await httpContext.Response.WriteAsJsonAsync(exception.Message);

        return false;
    }
}