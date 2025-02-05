using Promomash.Trader.UserAccess.Domain.BuildingBlocks;

namespace Promomash.Trader.UserAccess.Domain.Countries;

public class Country : Entity
{
    private Country()
    {
        // Only for EF.
    }

    private Country(string code, string name)
    {
        Code = code;
        Name = name;
    }
    
    public string Code { get; private set; }
    public string Name { get; private set; }

    public static Country Create(string code, string name)
    {
        return new Country(code, name);
    }
}