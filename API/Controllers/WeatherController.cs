using Microsoft.AspNetCore.Mvc;
using weather.Application.Mediatr;
using weather.Application.Weather;
using weather.Infrastructure.Mediatr;

namespace API.Controllers;

[Route($"{RoutePattern}/test")]
[ApiController]
public class WeatherController : ControllerM
{
    public WeatherController(ICommandBus commandBus, IQueryBus queryBus)
        : base(commandBus, queryBus)
    {
    }

    [HttpGet]
    public async Task<ActionResult<string>> GetWeather(
        CancellationToken cancellationToken)
    {
        var result = await QueryBus.QueryAsync(new GetWeatherInfoQuery(), cancellationToken);
        return Ok(result);
    }
}