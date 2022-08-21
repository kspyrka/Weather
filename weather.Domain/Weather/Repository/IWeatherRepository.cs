namespace weather.Domain.Weather.Repository;

public interface IWeatherRepository
{
    Task<ICollection<string>> GetWeatherInfoAsync(CancellationToken cancellationToken);
}