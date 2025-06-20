using NetTopologySuite.IO;

namespace RecomandationSystem.Application.Interfaces.UseCases
{
    public interface IQueryHandler<TRequest, TResponse>
        where TRequest : IQuery<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest query, CancellationToken cancellationToken);
    }

    public interface ICachedQueryHandler<TRequest, TResponse>
        where TRequest : ICachedQuery<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest query, CancellationToken cancellationToken);
    }
}
