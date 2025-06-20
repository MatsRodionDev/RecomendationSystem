using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Pgvector;
using Pgvector.EntityFrameworkCore;
using RecomandationSystem.Application;
using RecomandationSystem.Application.Enums;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.Encodings.Web;

namespace RecomandationSystem.BLL.Services
{
    public class ProductService(
        ApplicationDbContext context,
        IRagService ragService) : IProductService
    {
       

        public async Task CreateOneAsync(Product product, CancellationToken cancellationToken)
        {
            //var text = $"""
            //    product: {product.Name.ToLower()}
            //    technical specs: {product.Description}
            //    brand: {product.Brand}
            //    category: {product.Category}
            //    """
            //    .ToLowerInvariant();

            //var tokens = text.Split(new[] { ',', '.', '!', '?', ';', ':', ' ', '-', '_', '(', ')', '[', ']', '{', '}' },
            //                    StringSplitOptions.RemoveEmptyEntries)
            //    .Where(token => !string.IsNullOrWhiteSpace(token))
            //    .Select(token => token.Trim());

            //var filteredTokens = tokens
            //    .Where(token => !StopWordsHelper.GetStopWords().Contains(token))
            //    .Where(token => token.Length > 2)
            //    .ToArray();

            //var filteredDescription = string.Join(' ', filteredTokens);

            //var generatedEmbeddings = await embeddingGenerator.GenerateAsync([filteredDescription]);

            //var embedding = generatedEmbeddings.Single().Vector.ToArray();

            ////var norm = (float)Math.Sqrt(embedding.Sum(x => x * x));
            ////var normalizedEmbedding = embedding.Select(x => x / norm).ToArray();
            
            //product.Embedding1024 = new(embedding);

            //await context.Products.AddAsync(product, cancellationToken);
            //await context.SaveChangesAsync();
        }

        public async Task CreateManyAsync(List<Product> products, CancellationToken cancellationToken)
        {
            //foreach(var product in products)
            //{
            //    var text = $"""
            //        product: {product.Name.ToLower()}
            //        technical specs: {product.Description}
            //        brand: {product.Brand}
            //        category: {product.Category}
            //        """
            //        .ToLowerInvariant();

            //    var tokens = text.Split(new[] { ',', '.', '!', '?', ';', ':', ' ', '-', '_', '(', ')', '[', ']', '{', '}' },
            //                        StringSplitOptions.RemoveEmptyEntries)
            //        .Where(token => !string.IsNullOrWhiteSpace(token))
            //        .Select(token => token.Trim());

            //    var filteredTokens = tokens
            //        .Where(token => !StopWordsHelper.GetStopWords().Contains(token))
            //        .Where(token => token.Length > 2)
            //        .ToArray();

            //    var filteredDescription = string.Join(' ', filteredTokens);

            //    var generatedEmbeddings = await embeddingGenerator.GenerateAsync([filteredDescription]);

            //    var embedding = generatedEmbeddings.Single().Vector.ToArray();

            //    var norm = (float)Math.Sqrt(embedding.Sum(x => x * x));
            //    var normalizedEmbedding = embedding.Select(x => x / norm).ToArray();

            //    product.Embedding1024 = new(embedding);

            //    await context.Products.AddAsync(product, cancellationToken);
            //}

            //await context.SaveChangesAsync(cancellationToken);

            return;
        }

        public async Task<List<ProductDto>> GetAllAsync(string filter, int limit, CancellationToken cancellationToken)
        {
            //var tokens = filter.Split(new[] { ',', '.', '!', '?', ';', ':', ' ', '-', '_', '(', ')', '[', ']', '{', '}' },
            //        StringSplitOptions.RemoveEmptyEntries)
            //    .Where(token => !StopWordsHelper.GetStopWords().Contains(token.ToLower()))
            //    .ToArray();
            //var cleanedFilter = string.Join(' ', tokens);

            //var generatedEmbeddings = await embeddingGenerator.GenerateAsync([cleanedFilter]);
            //var queryEmbedding = generatedEmbeddings.Single().Vector.ToArray();

            //var candidateProducts = await context.Products
            //    .Where(p => p.Embedding1024.CosineDistance(new Vector(queryEmbedding)) < 5)
            //    .OrderBy(p => p.Embedding1024.CosineDistance(new Vector(queryEmbedding)))
            //    .Take(limit * 2)
            //    .Select(p => new ProductDto
            //    {
            //        Id = p.Id,
            //        Name = p.Name,
            //        Description = p.Description,
            //        Brand = p.Brand,
            //        Category = p.Category,
            //        Similiraty = p.Embedding1024.CosineDistance(new Vector(queryEmbedding))
            //    })
            //    .AsNoTracking()
            //    .ToListAsync(cancellationToken);

            //return await ragService.FilterProductsAsync(filter, candidateProducts, cancellationToken);

            return [];
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
                .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description })
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
