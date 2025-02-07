namespace Promomash.Trader.UserAccess.Domain.Users;

public interface IUserRepository
{
    Task AddAsync(User user);

    Task<bool> IsEmailUniqueAsync(string email);
}