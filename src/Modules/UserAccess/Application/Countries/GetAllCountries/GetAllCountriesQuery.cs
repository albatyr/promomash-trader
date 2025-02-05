using MediatR;

namespace Promomash.Trader.UserAccess.Application.Countries.GetAllCountries;

public class GetAllCountriesQuery : IRequest<List<CountryDto>>
{
}