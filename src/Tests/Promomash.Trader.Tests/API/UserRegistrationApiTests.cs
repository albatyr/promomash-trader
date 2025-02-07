using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Promomash.Trader.Tests.API.Abstractions;
using Promomash.Trader.UserAccess.Application.Users.RegisterUser;

namespace Promomash.Trader.Tests.API;

public class UserRegistrationApiTests(ApiTestWebAppFactory factory) : BaseApiTest(factory)
{
    private readonly RegisterUserCommand _validRequest = new(
        "testuser@example.com",
        "Password123!",
        true,
        "USA:CA");

    [Fact]
    public async Task Post_User_Registration_Returns_Success()
    {
        var request = _validRequest with { Email = $"user_{Guid.NewGuid()}@example.com" };

        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", request);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_Email_Is_Invalid()
    {
        var request = _validRequest with { Email = "not a valid email" };

        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_Password_Has_No_Number()
    {
        var request = _validRequest with { Password = "1234" };

        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_Password_Has_No_Letter()
    {
        var request = _validRequest with { Password = "asd" };

        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_ProvinceId_Is_Invalid()
    {
        var request = _validRequest with { ProvinceId = "USA---CA" };

        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_User_Already_Exists()
    {
        var request = _validRequest with { Email = $"user_{Guid.NewGuid()}@example.com" };

        await HttpClient.PostAsJsonAsync("/api/user/registration", request);

        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}