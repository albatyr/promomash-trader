using System.Net;
using FluentAssertions;
using Promomash.Trader.Tests.API.Abstractions;

namespace Promomash.Trader.Tests.API;

public class CountriesApiTests(ApiTestWebAppFactory factory)
    : BaseApiTest(factory)
{
    [Fact]
    public async Task Get_Countries_Returns_Success()
    {
        var response = await HttpClient.GetAsync("/api/cuntries");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_Provinces_By_CountryCode_Returns_Success()
    {
        const string countryCode = "USA";

        var response = await HttpClient.GetAsync($"/api/cuntries/{countryCode}/provinces");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}