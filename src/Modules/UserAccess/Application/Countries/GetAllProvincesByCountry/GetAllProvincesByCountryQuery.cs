using MediatR;

namespace Promomash.Trader.UserAccess.Application.Countries.GetAllProvincesByCountry;

public sealed record GetAllProvincesByCountryQuery(string CountryCode) : IRequest<List<ProvinceDto>>;