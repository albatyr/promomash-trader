using Promomash.Trader.UserAccess.Domain.BuildingBlocks;

namespace Promomash.Trader.UserAccess.Domain.Countries;

public class Province : Entity
{
    private Province()
    {
        // Only for EF.
    }

    private Province(string code, string name, string countryCode)
    {
        Id = ProvinceId.Create(code, countryCode);
        Name = name;
        CountryCode = countryCode;
    }
    
    public ProvinceId Id { get; private set; }
    public string Name { get; private set; }
    public string CountryCode { get; private set; }

    public static Province Create(string code, string name, string countryCode)
    {
        return new Province(code, name, countryCode);
    }
}