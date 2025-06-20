namespace RecomandationSystem.Application.Interfaces
{
    public interface IEmbeddingService
    {
        Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken = default);
        Task<List<float[]>> GetEmbeddingsAsync(string[] texts, CancellationToken cancellationToken = default);
    }
}
