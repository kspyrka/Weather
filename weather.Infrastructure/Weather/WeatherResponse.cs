namespace weather.Infrastructure.Weather;

public class WeatherResponse
{
    public Data Data { get; set; }
}

public class Data
{
    public List<Weather> Weather { get; set; }
}

public class Weather
{
    private string date { get; set; }
    private List<Hourly> hourly { get; set; }
}

public class Hourly
{
    private string time { get; set; }
    private string tempC { get; set; }
}