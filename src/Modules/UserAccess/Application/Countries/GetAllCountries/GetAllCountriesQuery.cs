using MediatR;

namespace Promomash.Trader.UserAccess.Application.Countries.GetAllCountries;

public sealed record GetAllCountriesQuery : IRequest<List<CountryDto>>;