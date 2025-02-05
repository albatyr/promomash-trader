namespace Promomash.Trader.Tests.API.Abstractions;

public class BaseApiTest(ApiTestWebAppFactory factory) : IClassFixture<ApiTestWebAppFactory>
{
    protected HttpClient HttpClient { get; set; } = factory.CreateClient();
}