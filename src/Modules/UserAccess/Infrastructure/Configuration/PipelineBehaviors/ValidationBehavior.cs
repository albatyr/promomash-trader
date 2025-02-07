using FluentValidation;
using MediatR;
using Promomash.Trader.UserAccess.Infrastructure.Configuration.Exceptions;

namespace Promomash.Trader.UserAccess.Infrastructure.Configuration.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validationTasks = _validators
            .Select(v => v.ValidateAsync(request, cancellationToken));

        var validationResults = await Task.WhenAll(validationTasks);

        var errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();

        if (errors.Any())
        {
            throw new InvalidCommandException(errors.Select(x => x.ErrorMessage).ToList());
        }

        return await next();
    }
}