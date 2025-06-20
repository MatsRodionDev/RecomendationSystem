using RecomandationSystem.Application.Models;

namespace RecomandationSystem.Application.Interfaces
{
    public interface IRagService
    {
        Task<List<ProductDto>> FilterProductsAsync(string query, List<ProductDto> products, CancellationToken cancellationToken);
        Task<string> GetQueryAsync(string query, CancellationToken cancellationToken = default);
    }
}
