namespace Promomash.Trader.UserAccess.Domain.BuildingBlocks;

public interface IBusinessRule
{
    string Message { get; }

    bool IsBroken();
}