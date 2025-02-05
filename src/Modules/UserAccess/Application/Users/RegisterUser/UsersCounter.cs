using Dapper;
using Npgsql;
using Promomash.Trader.UserAccess.Domain.Users;

namespace Promomash.Trader.UserAccess.Application.Users.RegisterUser;

public class UsersCounter(NpgsqlDataSource dataSource)
    : IUsersCounter
{
    public int CountUsersWithEmail(string email)
    {
        using var connection = dataSource.OpenConnection();

        const string sql = """
                           SELECT COUNT(*) 
                           FROM "users"."Users"
                           WHERE "Email" = @email
                           """;

        return connection.QuerySingle<int>(sql, new { email });
    }
}