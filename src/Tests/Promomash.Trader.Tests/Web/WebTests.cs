using System.Net;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.DependencyInjection;
using Projects;

namespace Promomash.Trader.Tests.Web;

public class WebTests
{
    [Fact]
    public async Task Get_Web_Resource_Root_Returns_Ok()
    {
        // Arrange
        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Promomash_Trader_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(
            clientBuilder => { clientBuilder.AddStandardResilienceHandler(); });

        await using var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        var httpClient = app.CreateHttpClient("trader-app");
        await resourceNotificationService.WaitForResourceAsync("trader-app", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));
        var response = await httpClient.GetAsync("/");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}