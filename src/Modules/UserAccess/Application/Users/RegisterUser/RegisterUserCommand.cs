using MediatR;

namespace Promomash.Trader.UserAccess.Application.Users.RegisterUser;

public class RegisterUserCommand : IRequest<Guid>
{
    public string Email { get; set; }

    public string Password { get; set; }

    public bool IsAgreedToWorkForFood { get; set; }

    public string ProvinceId { get; set; }
}