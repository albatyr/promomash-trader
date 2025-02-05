using System.Net;
using FluentAssertions;
using Promomash.Trader.Tests.API.Abstractions;

namespace Promomash.Trader.Tests.API;

public class CountriesApiTests(ApiTestWebAppFactory factory)
    : BaseApiTest(factory)
{
    // Test GET /api/countries
    [Fact]
    public async Task Get_Countries_Returns_Success()
    {
        // Act
        var response = await HttpClient.GetAsync("/api/cuntries");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_Provinces_By_CountryCode_Returns_Success()
    {
        // Arrange
        var countryCode = "USA";

        // Act
        var response = await HttpClient.GetAsync($"/api/cuntries/{countryCode}/provinces");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}