namespace Promomash.Trader.UserAccess.Domain.Users;

public interface IUsersCounter
{
    int CountUsersWithEmail(string login);
}