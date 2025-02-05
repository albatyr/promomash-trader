using MediatR;

namespace Promomash.Trader.UserAccess.Application.Countries.GetAllProvincesByCountry;

public class GetAllProvincesByCountryQuery : IRequest<List<ProvinceDto>>
{
    public string CountryCode { get; set; } = string.Empty;
}