using Microsoft.AspNetCore.Mvc;
using weather.Application.Mediatr;

namespace weather.Infrastructure.Mediatr;

//controller with mediatr buses
public abstract class ControllerM : ControllerBase
{
    protected const string RoutePattern = "api/v1";
    protected ControllerM(ICommandBus commandBus, IQueryBus queryBus)
    {
        CommandBus = commandBus;
        QueryBus = queryBus;
    }

    protected ICommandBus CommandBus { get; }

    protected IQueryBus QueryBus { get; }
}