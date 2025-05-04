using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using RecomandationSystem.Application;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RecomandationSystem.BLL.Services
{
    public class ProductService(
        IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator,
        ApplicationDbContext context) : IProductService
    {
        public async Task CreateOneAsync(Product product, CancellationToken cancellationToken)
        {
            var textToEmbed = $"{product.Name}.{product.Description}.";

            var generatedEmbeddings = await embeddingGenerator.GenerateAsync([textToEmbed]);

            var embedding = generatedEmbeddings.Single().Vector.ToArray();

            //var norm = (float)Math.Sqrt(embedding.Sum(x => x * x));
            //var normalizedEmbedding = embedding.Select(x => x / norm).ToArray();
            
            product.Embedding1024 = new(embedding);

            await context.Products.AddAsync(product, cancellationToken);
            await context.SaveChangesAsync();
        }

        public async Task CreateManyAsync(List<Product> products, CancellationToken cancellationToken)
        {
            foreach(var product in products)
            {
                var textToEmbed = $"{product.Name}.{product.Description}.";

                var generatedEmbeddings = await embeddingGenerator.GenerateAsync([textToEmbed]);

                var embedding = generatedEmbeddings.Single().Vector.ToArray();

                //var norm = (float)Math.Sqrt(embedding.Sum(x => x * x));
                //var normalizedEmbedding = embedding.Select(x => x / norm).ToArray();

                product.Embedding1024 = new(embedding);

                await context.Products.AddAsync(product, cancellationToken);
            }

            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<ProductDto>> GetAllAsync(string filter, int limit, CancellationToken cancellationToken)
        {
            var generatedEmbeddings = await embeddingGenerator.GenerateAsync([filter]);

            var embedding = generatedEmbeddings.Single().Vector.ToArray();

            return await context.Products
                .Where(p => p.Embedding1024.CosineDistance(new Vector(embedding)) <= 0.4)
                .OrderBy(p => p.Embedding1024.CosineDistance(new Vector(embedding)))
                .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description, SimilarityScore = p.Embedding1024.CosineDistance(new Vector(embedding)) })
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<List<ProductDto>> GetRecomendationsAsync(Guid userId, int limit, CancellationToken cancellationToken)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null)
            {
                return [];
            }

            if(user.CombinedEmbedding1024 is null)
            {
                return await context.Products
                    .AsNoTracking()
                    .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description })
                    .Take(limit)
                    .ToListAsync(cancellationToken);
            }

            return await context.Products
                .AsNoTracking()
                .OrderBy(p => p.Embedding1024.CosineDistance(user.CombinedEmbedding1024))
                .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description, SimilarityScore = p.Embedding1024.CosineDistance(user.CombinedEmbedding1024) })
                .Take(limit)
                .ToListAsync(cancellationToken);
        }


        public async Task BuyProductAsync(Guid userId, Guid productId, CancellationToken cancellationToken)
        {
            var user = await context.Users.Include(u => u.BuyedProducts).FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

            if (user is null)
            {
                return;
            }

            if (user.BuyedProducts.Select(p => p.Id).Contains(productId))
            {
                return;
            }

            var product = await context.Products.FirstOrDefaultAsync(u => u.Id == productId, cancellationToken);

            if (product is null)
            {
                return;
            }

            user.BuyedProducts.Add(product);

            var userEmbeddings = user.BuyedProducts
                .Where(p => p.Embedding1024 != null)
                .Select(p => p.Embedding1024.ToArray())
                .ToList();

            float[] userEmbedding = new float[1024]; 
            foreach (var embedding in userEmbeddings)
            {
                for (int i = 0; i < embedding.Length; i++)
                {
                    userEmbedding[i] += embedding[i];
                }
            }

            if (userEmbeddings.Count > 0)
            {
                for (int i = 0; i < userEmbedding.Length; i++)
                {
                    userEmbedding[i] /= userEmbeddings.Count;
                }
            }

            user.CombinedEmbedding1024 = new(userEmbedding);

            await context.SaveChangesAsync(cancellationToken);
        }
    }
}
