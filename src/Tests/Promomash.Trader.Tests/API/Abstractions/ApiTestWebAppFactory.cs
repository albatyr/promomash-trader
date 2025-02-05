using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Promomash.Trader.UserAccess.Infrastructure;
using Testcontainers.PostgreSql;

namespace Promomash.Trader.Tests.API.Abstractions;

public class ApiTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("traderDb")
        .Build();

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var connectionString = _dbContainer.GetConnectionString();

        builder.ConfigureHostConfiguration(config =>
        {
            config.AddInMemoryCollection(new Dictionary<string, string?>
            {
                { "ConnectionStrings:traderDb", connectionString }
            });
        });
        
        return base.CreateHost(builder);
    }
    
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();

        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserAccessContext>();
        
        await dbContext.Database.MigrateAsync();
    }

    public new Task DisposeAsync()
    {
        return _dbContainer.StopAsync();
    }
}