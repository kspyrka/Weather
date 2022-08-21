using MediatR;

namespace weather.Application.Mediatr;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}