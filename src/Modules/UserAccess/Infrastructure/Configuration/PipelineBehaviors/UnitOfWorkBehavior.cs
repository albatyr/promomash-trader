using MediatR;

namespace Promomash.Trader.UserAccess.Infrastructure.Configuration.PipelineBehaviors
{
    public class UnitOfWorkBehavior<TRequest, TResponse>(UserAccessContext dbContext)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var response = await next();

            await dbContext.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}