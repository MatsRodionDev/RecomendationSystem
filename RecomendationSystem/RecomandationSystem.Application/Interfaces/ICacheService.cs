namespace RecomandationSystem.Application.Interfaces
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);
        Task SetAsync<T>(string key, T obj, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    }
}
