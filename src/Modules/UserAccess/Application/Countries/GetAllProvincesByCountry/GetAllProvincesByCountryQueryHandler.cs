using Dapper;
using MediatR;
using Npgsql;

namespace Promomash.Trader.UserAccess.Application.Countries.GetAllProvincesByCountry;

public class GetAllProvincesByCountryQueryHandler(NpgsqlDataSource dataSource)
    : IRequestHandler<GetAllProvincesByCountryQuery, List<ProvinceDto>>
{
    public async Task<List<ProvinceDto>> Handle(
        GetAllProvincesByCountryQuery request,
        CancellationToken cancellationToken)
    {
        await using var connection = await dataSource.OpenConnectionAsync(cancellationToken);

        const string sql = """
                           SELECT "Id", "Name" FROM "users"."Provinces" WHERE "CountryCode" = @CountryCode
                           """;

        var provinces = await connection.QueryAsync<ProvinceDto>(sql, new { request.CountryCode });

        return provinces.AsList();
    }
}