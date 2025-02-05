using MediatR;
using Microsoft.AspNetCore.Mvc;
using Promomash.Trader.UserAccess.Application.Countries.GetAllCountries;
using Promomash.Trader.UserAccess.Application.Countries.GetAllProvincesByCountry;

namespace Promomash.Trader.API.Controllers;

[ApiController]
[Route("api/cuntries")]
public class CountriesController(IMediator mediator) : ControllerBase
{
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCountries()
    {
        var countries = await mediator.Send(new GetAllCountriesQuery());
        return Ok(countries);
    }

    [HttpGet("{countryCode}/provinces")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProvincesByCountry(string countryCode)
    {
        var query = new GetAllProvincesByCountryQuery(countryCode);
        var provinces = await mediator.Send(query);
        return Ok(provinces);
    }
}