using Microsoft.EntityFrameworkCore;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Interfaces.UseCases;
using RecomandationSystem.Application.Models;

namespace RecomandationSystem.Application.UseCases.Commands
{
    public record GetProductsQuery(
        string Request,
        int Limit) : ICachedQuery<List<ProductDto>>
    {
        public string Key => Request;
    }

    public class GetProductQueryHandler(
        IEmbeddingService embeddingService,
        ApplicationDbContext context,
        IRagService ragService) : ICachedQueryHandler<GetProductsQuery, List<ProductDto>>
    {
        public async Task<List<ProductDto>> HandleAsync(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var ragQuery = await ragService.GetQueryAsync(query.Request, cancellationToken);

            var queryEmbedding = await embeddingService.GetEmbeddingAsync(ragQuery, cancellationToken);

            return await context.Products
                .AsNoTracking()
                .Where(p => p.Embedding1024 != null)
                .Where(p => p.Embedding1024!.CosineDistance(new Vector(queryEmbedding)) < 0.5)
                .OrderBy(p => p.Embedding1024!.CosineDistance(new Vector(queryEmbedding)))
                .Take(query.Limit)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Brand = p.Brand,
                    Category = p.Category,
                    Similiraty = p.Embedding1024!.CosineDistance(new Vector(queryEmbedding))
                })
                .ToListAsync(cancellationToken);
        }
    }
}
