using RecomandationSystem.Application.Models;
using System.Threading.Tasks;

namespace RecomandationSystem.Application.Interfaces
{
    public interface IProductService
    {
        Task CreateOneAsync(Product product, CancellationToken cancellationToken);
        Task CreateManyAsync(List<Product> products, CancellationToken cancellationToken);
        Task<List<ProductDto>> GetAllAsync(string filter, int limit, CancellationToken cancellationToken);
        Task<List<ProductDto>> GetRecomendationsAsync(Guid userId, int limit, CancellationToken cancellationToken);
        Task BuyProductAsync(Guid userId, Guid productId, CancellationToken cancellationToken);
    }
}
