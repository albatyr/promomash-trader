using Microsoft.AspNetCore.Diagnostics;
using Promomash.Trader.UserAccess.Domain.BuildingBlocks;
using Promomash.Trader.UserAccess.Infrastructure.Configuration.Exceptions;

namespace Promomash.Trader.API.Configuration.ExceptionHandlers;

public class ExceptionToProblemDetailsHandler(IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var error = new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
            {
                Title = "An error occurred",
                Detail = exception.Message,
                Type = exception.GetType().Name
            },
            Exception = exception
        };

        switch (exception)
        {
            case InvalidCommandException validationException:
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                error.ProblemDetails.Title = "Request was malformed or invalid";
                error.ProblemDetails.Extensions.Add("Errors", validationException.Errors);
                break;
            case BusinessRuleValidationException businessRuleValidationException:
                httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
                error.ProblemDetails.Title = "Business rule broken";
                error.ProblemDetails.Detail = businessRuleValidationException.Details;
                break;
        }

        return await problemDetailsService.TryWriteAsync(error);
    }
}