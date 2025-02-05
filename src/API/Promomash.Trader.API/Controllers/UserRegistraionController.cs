using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Promomash.Trader.UserAccess.Application.Users.RegisterUser;

namespace Promomash.Trader.API.Controllers;

[ApiController]
[Route("api/user/registration")]
public class UserRegistraionController(IMediator mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RegisterUser(RegisterUserCommand request)
    {
        await mediator.Send(request);

        return Ok();
    }
}