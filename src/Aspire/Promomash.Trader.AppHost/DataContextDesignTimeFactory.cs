using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Promomash.Trader.UserAccess.Infrastructure;

namespace Promomash.Trader.AppHost;

public sealed class DataContextDesignTimeFactory :
    IDesignTimeDbContextFactory<UserAccessContext>
{
    public UserAccessContext CreateDbContext(string[] args)
    {
        return new UserAccessContext(new DbContextOptionsBuilder<UserAccessContext>().UseNpgsql().Options);
    }
}