using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using weather.Domain.Weather.Repository;

namespace weather.Infrastructure.Weather;

public class WeatherRepository : IWeatherRepository
{
    private readonly IConnectionMultiplexer _redis;

    private const string GeolookupAndCurrentConditionsUri =
        "http://api.worldweatheronline.com/premium/v1/weather.ashx?key=34acb43c84eb46349a4162059222108 &q=Warsaw&format=json&num_of_days=2&tp=1";

    public WeatherRepository(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }

    public async Task<ICollection<string>> GetWeatherInfoAsync(CancellationToken cancellationToken)
    {
        var redisDb = _redis.GetDatabase();
        var warsaw = await redisDb.StringGetAsync("Warsaw");
        if (warsaw.HasValue)
        {
            return null;
        }

        // var dbWeatherForecasts = GetForecast();
        GetForecasts(cancellationToken);
        // var serializedDbWeatherForecasts = JsonConvert.SerializeObject(dbWeatherForecasts);

        //
        // await redisDb.StringSetAsync("Warsaw", serializedDbWeatherForecasts, GetExpirationTime());
        //
        // return dbWeatherForecasts;
        return null;
    }

    private TimeSpan GetExpirationTime()
    {
        var nextCachingDate = DateTime.Today.AddHours(6);
        if (DateTime.Now > nextCachingDate)
        {
            nextCachingDate.AddDays(1);
        }

        return nextCachingDate.Subtract(DateTime.Now);
    }

    private async void GetForecasts(CancellationToken cancellationToken)
    {
        using (var client = new HttpClient())
        {
            HttpResponseMessage response =  await client.GetAsync(GeolookupAndCurrentConditionsUri, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(content);

                // if (weatherResponse.results != null && weatherResponse.results.error != null)
                //     throw new WeatherServiceException(weatherResponse.results.error.message);
                //
                // return weatherResponse;
            }
        }
        // return null;
    }
}