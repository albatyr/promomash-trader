using MediatR;
using Microsoft.Extensions.Logging;

namespace Promomash.Trader.UserAccess.Infrastructure.Configuration.PipelineBehaviors;

internal class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        using (BeginCommandLoggingScope(request))
        {
            try
            {
                logger.LogInformation("Executing command {RequestType}", request.GetType().Name);

                var response = await next();

                logger.LogInformation("Command {RequestType} processed successfully", request.GetType().Name);

                return response;
            }
            catch (Exception exception)
            {
                logger.LogError(exception, "Command {RequestType} processing failed", request.GetType().Name);
                throw;
            }
        }
    }
    
    private IDisposable? BeginCommandLoggingScope(TRequest request)
    {
        return logger.BeginScope(new Dictionary<string, object>
        {
            { "RequestType", request.GetType().Name }
        });
    }
}