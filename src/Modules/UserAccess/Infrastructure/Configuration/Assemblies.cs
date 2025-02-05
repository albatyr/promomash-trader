using System.Reflection;
using Promomash.Trader.UserAccess.Application.Users.RegisterUser;

namespace Promomash.Trader.UserAccess.Infrastructure.Configuration;

internal static class Assemblies
{
    public static readonly Assembly Application = typeof(RegisterUserCommand).Assembly;
    public static readonly Assembly Infrastructure = typeof(UserAccessContext).Assembly;
}