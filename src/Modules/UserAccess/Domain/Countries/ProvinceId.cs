namespace Promomash.Trader.UserAccess.Domain.Countries;

public class ProvinceId : IEquatable<ProvinceId>
{
    private ProvinceId()
    {
        // Only for EF.
    }

    private ProvinceId(string provinceCode, string countryCode)
    {
        ProvinceCode = provinceCode;
        CountryCode = countryCode;
    }

    public string ProvinceCode { get; }

    public string CountryCode { get; }

    public bool Equals(ProvinceId other)
    {
        if (other == null)
        {
            return false;
        }

        return ProvinceCode == other.ProvinceCode && CountryCode == other.CountryCode;
    }

    public override string ToString()
    {
        return $"{CountryCode}:{ProvinceCode}";
    }

    public static ProvinceId FromString(string str)
    {
        var parts = str.Split(':');

        if (parts.Length != 2)
        {
            throw new ArgumentException("Province id must consist of exactly two parts.");
        }

        return Create(parts[1], parts[0]);
    }

    public static ProvinceId Create(string provinceCode, string countryCode)
    {
        return new ProvinceId(provinceCode, countryCode);
    }

    public override bool Equals(object obj)
    {
        if (obj is ProvinceId other)
        {
            return Equals(other);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(ProvinceCode, CountryCode);
    }
}