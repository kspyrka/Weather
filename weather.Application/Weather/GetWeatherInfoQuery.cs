using weather.Application.Mediatr;

namespace weather.Application.Weather;

public record GetWeatherInfoQuery() : IQuery<ICollection<string>>;