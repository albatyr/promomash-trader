using Microsoft.EntityFrameworkCore;
using Npgsql;
using Promomash.Trader.UserAccess.Infrastructure;

namespace Promomash.Trader.API.Configuration.HostedServices
{
    public class DatabaseInitializationBackgroundService : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DatabaseInitializationBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializationBackgroundService(
            IConfiguration configuration,
            ILogger<DatabaseInitializationBackgroundService> logger,
            IServiceProvider serviceProvider)
        {
            _configuration = configuration;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                _logger.LogInformation("Database initialization...");

                await EnsureDatabaseCreated();
                await ApplyMigrationsAsync();

                _logger.LogInformation("Database initialization completed successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initializing the database.");
            }
        }
        
        private async Task EnsureDatabaseCreated()
        {
            var connectionString = _configuration.GetConnectionString("traderDb");
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var dbName = builder.Database;
            builder.Database = "postgres";

            await using var connection = new NpgsqlConnection(builder.ToString());
            await connection.OpenAsync();

            var dbExistsQuery = $"SELECT EXISTS (SELECT 1 FROM pg_database WHERE datname = '{dbName}')";
            await using var command = new NpgsqlCommand(dbExistsQuery, connection);
            var dbExists = (bool)await command.ExecuteScalarAsync();

            if (!dbExists)
            {
                _logger.LogInformation("Creating database '{dbName}'.", dbName);
                var createDbCommand = $"CREATE DATABASE \"{dbName}\"";
                await using var createCommand = new NpgsqlCommand(createDbCommand, connection);
                await createCommand.ExecuteNonQueryAsync();
            }
        }

        private async Task ApplyMigrationsAsync()
        {
            _logger.LogInformation("Applying EF Core migrations...");

            using var scope = _serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<UserAccessContext>();

            await dbContext.Database.MigrateAsync();
            
            _logger.LogInformation("Applying EF Core migrations completed successfully.");
        }
    }
}
