using MediatR;
using Promomash.Trader.UserAccess.Domain.Countries;
using Promomash.Trader.UserAccess.Domain.Users;

namespace Promomash.Trader.UserAccess.Application.Users.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository userRepository, IUsersCounter usersCounter) 
    : IRequestHandler<RegisterUserCommand, Guid>
{
    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var password = PasswordManager.HashPassword(request.Password);
        
        var user = User.CreateUser(
            request.Email,
            password,
            request.IsAgreedToWorkForFood,
            ProvinceId.FromString(request.ProvinceId),
            usersCounter);

        await userRepository.AddAsync(user);

        return user.Id.Value;
    }
}