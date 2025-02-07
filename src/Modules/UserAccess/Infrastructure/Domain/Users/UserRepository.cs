using Microsoft.EntityFrameworkCore;
using Promomash.Trader.UserAccess.Domain.Users;

namespace Promomash.Trader.UserAccess.Infrastructure.Domain.Users;

public class UserRepository(UserAccessContext dbContext) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await dbContext.Users.AnyAsync(u => u.Email == email);
    }
}