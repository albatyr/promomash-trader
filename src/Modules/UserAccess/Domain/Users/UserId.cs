using Promomash.Trader.UserAccess.Domain.BuildingBlocks;

namespace Promomash.Trader.UserAccess.Domain.Users;

public class UserId(Guid value) : TypedIdValueBase(value);