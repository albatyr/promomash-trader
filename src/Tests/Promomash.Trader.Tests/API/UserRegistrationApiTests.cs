using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Promomash.Trader.Tests.API.Abstractions;
using Promomash.Trader.UserAccess.Application.Users.RegisterUser;

namespace Promomash.Trader.Tests.API;

public class UserRegistrationApiTests(ApiTestWebAppFactory factory) : BaseApiTest(factory)
{
    private readonly RegisterUserCommand _request = new()
    {
        Email = "testuser@example.com",
        Password = "Password123!",
        IsAgreedToWorkForFood = true,
        ProvinceId = "USA:CA"
    };

    [Fact]
    public async Task Post_User_Registration_Returns_Success()
    {
        _request.Email = $"user_{Guid.NewGuid()}@example.com";

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", _request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_Email_Is_Invalid()
    {
        // Arrange
        _request.Email = "not a valid email";

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", _request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_Password_Has_No_Number()
    {
        // Arrange
        _request.Password = "1234";

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", _request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_Password_Has_No_Letter()
    {
        // Arrange
        _request.Password = "asd";

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", _request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_ProvinceId_Is_Invalid()
    {
        // Arrange
        _request.ProvinceId = "USA---CA";

        // Act
        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", _request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Post_User_Registration_Returns_Error_If_User_Already_Exists()
    {
        // Arrange
        await HttpClient.PostAsJsonAsync("/api/user/registration", _request);

        // Act (trying to add same user again)
        var response = await HttpClient.PostAsJsonAsync("/api/user/registration", _request);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }
}