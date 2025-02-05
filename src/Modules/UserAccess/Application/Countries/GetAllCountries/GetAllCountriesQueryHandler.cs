using Dapper;
using MediatR;
using Npgsql;

namespace Promomash.Trader.UserAccess.Application.Countries.GetAllCountries;

public class GetAllCountriesQueryHandler(NpgsqlDataSource dataSource)
    : IRequestHandler<GetAllCountriesQuery, List<CountryDto>>
{
    public async Task<List<CountryDto>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
    {
        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);

        const string sql = """
                           SELECT "Code", "Name" FROM "users"."Countries"
                           """;

        var countries = await connection.QueryAsync<CountryDto>(sql);

        return countries.AsList();
    }
}