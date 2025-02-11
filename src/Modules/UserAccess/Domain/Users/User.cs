using Promomash.Trader.UserAccess.Domain.BuildingBlocks;
using Promomash.Trader.UserAccess.Domain.Countries;

namespace Promomash.Trader.UserAccess.Domain.Users;

public class User : Entity, IAggregateRoot
{
    private User()
    {
        // Only for EF.
    }

    private User(
        string email,
        string password,
        bool isAgreedToWorkForFood,
        ProvinceId provinceId)
    {
        Id = new UserId(Guid.CreateVersion7());

        Login = email;
        Email = email;
        Password = password;
        IsAgreedToWorkForFood = isAgreedToWorkForFood;
        ProvinceId = provinceId;
    }

    public UserId Id { get; }

    public string Login { get; private set; }

    public string Email { get; private set; }

    public string Password { get; private set; }

    public bool IsAgreedToWorkForFood { get; private set; }

    public ProvinceId ProvinceId { get; private set; }

    public static User CreateUser(
        string email,
        string password,
        bool isAgreedToWorkForFood,
        ProvinceId provinceId)
    {
        return new User(
            email,
            password,
            isAgreedToWorkForFood,
            provinceId);
    }
}