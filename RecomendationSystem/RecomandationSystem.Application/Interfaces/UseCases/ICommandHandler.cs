namespace RecomandationSystem.Application.Interfaces.UseCases
{
    public interface ICommandHandler<TRequest, TResponse>
        where TRequest : ICommand<TResponse>
    {
        Task<TResponse> HandleAsync(TRequest command, CancellationToken cancellationToken);
    }
}
