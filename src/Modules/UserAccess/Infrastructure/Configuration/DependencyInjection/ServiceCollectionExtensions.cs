using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Promomash.Trader.UserAccess.Application.Users.RegisterUser;
using Promomash.Trader.UserAccess.Domain.Users;

namespace Promomash.Trader.UserAccess.Infrastructure.Configuration.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserAccessModule(this IServiceCollection services)
    {
        services.AddValidators();
        services.AddRepositories();
        services.AddMediator();

        return services;
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assemblies.Application));
        services.Scan(
            scan => scan
                .FromAssemblies(Assemblies.Infrastructure)
                .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.Scan(
            scan => scan
                .FromAssemblies(Assemblies.Infrastructure)
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
    }

    private static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<RegisterUserCommandValidator>();
    }
}