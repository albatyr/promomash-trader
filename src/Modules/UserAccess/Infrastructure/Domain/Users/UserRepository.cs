﻿using Promomash.Trader.UserAccess.Domain.Users;

namespace Promomash.Trader.UserAccess.Infrastructure.Domain.Users;

public class UserRepository(UserAccessContext dbContext) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        await dbContext.Users.AddAsync(user);
    }
}