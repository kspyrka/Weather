using System.Reflection;
using MediatR;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using weather.Application.Mediatr;
using weather.Domain.Weather.Repository;
using weather.Infrastructure.Mediatr;
using weather.Infrastructure.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMvc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Weather Api"
            });
        }
    )
    .AddMediatR(AppDomain.CurrentDomain.GetAssemblies())
    .AddScoped<ICommandBus, CommandBus>()
    .AddScoped<IQueryBus, QueryBus>()
    .AddScoped<IWeatherRepository, WeatherRepository>();

builder.Services.AddSingleton<IConnectionMultiplexer>(_ =>
    ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));

var app = builder.Build();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = String.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather Api");
});


app.Run();