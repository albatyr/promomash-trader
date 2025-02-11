using Microsoft.EntityFrameworkCore;
using Npgsql;
using Promomash.Trader.UserAccess.Infrastructure;

namespace Promomash.Trader.API.Configuration.HostedServices;

public class DatabaseInitializationBackgroundService(
    ILogger<DatabaseInitializationBackgroundService> logger,
    IServiceProvider serviceProvider)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            logger.LogInformation("Database initialization...");

            await ApplyMigrationsAsync();

            logger.LogInformation("Database initialization completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while initializing the database.");
        }
    }

    private async Task ApplyMigrationsAsync()
    {
        logger.LogInformation("Applying EF Core migrations...");

        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserAccessContext>();

        await dbContext.Database.MigrateAsync();

        logger.LogInformation("Applying EF Core migrations completed successfully.");
    }
}