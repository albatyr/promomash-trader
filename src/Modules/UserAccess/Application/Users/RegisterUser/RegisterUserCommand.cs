using MediatR;

namespace Promomash.Trader.UserAccess.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string Password,
    bool IsAgreedToWorkForFood,
    string ProvinceId) : IRequest<Guid>;