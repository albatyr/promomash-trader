using Promomash.Trader.UserAccess.Domain.BuildingBlocks;
using Promomash.Trader.UserAccess.Domain.Countries;
using Promomash.Trader.UserAccess.Domain.Users.Rules;

namespace Promomash.Trader.UserAccess.Domain.Users;

public class User : Entity, IAggregateRoot
{
    public UserId Id { get; }
    public string Login { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public bool IsAgreedToWorkForFood { get; private set; }
    public ProvinceId ProvinceId { get; private set; }

    private User()
    {
        // Only for EF.
    }

    private User(
        string email,
        string password,
        bool isAgreedToWorkForFood,
        ProvinceId provinceId,
        IUsersCounter usersCounter)
    {
        CheckRule(new UserEmailMustBeUniqueRule(usersCounter, email));
        
        Id = new UserId(Guid.NewGuid());
        
        Login = email;
        Email = email;
        Password = password;
        IsAgreedToWorkForFood = isAgreedToWorkForFood;
        ProvinceId = provinceId;
    }

    public static User CreateUser(
        string email,
        string password,
        bool isAgreedToWorkForFood,
        ProvinceId provinceId,
        IUsersCounter usersCounter)
    {
        return new User(
            email,
            password,
            isAgreedToWorkForFood,
            provinceId,
            usersCounter);
    }
}