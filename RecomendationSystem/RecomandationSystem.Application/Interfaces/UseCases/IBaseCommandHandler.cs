namespace RecomandationSystem.Application.Interfaces.UseCases
{
    public interface IBaseCommandHandler<TRequest>
        where TRequest : IBaseCommand
    {
        Task HandleAsync(TRequest command, CancellationToken cancellationToken);
    }
}
