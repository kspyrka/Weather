using weather.Application.Mediatr;
using weather.Domain.Weather.Repository;

namespace weather.Application.Weather;

public class GetWeatherInfoQueryHandler : IQueryHandler<GetWeatherInfoQuery, ICollection<string>>
{
    private readonly IWeatherRepository _weatherRepository;

    public GetWeatherInfoQueryHandler(IWeatherRepository weatherRepository)
    {
        _weatherRepository = weatherRepository;
    }

    public async Task<ICollection<string>> Handle(GetWeatherInfoQuery request, CancellationToken cancellationToken)
    {
        var xd = 0;
        return await _weatherRepository.GetWeatherInfoAsync(cancellationToken);
    }
}