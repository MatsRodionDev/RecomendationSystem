using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pgvector;

namespace RecomandationSystem.Application.BackgroundServices
{
    internal sealed class CreateProductsVectorJob(IServiceProvider serviceProvider) : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            //    while (!stoppingToken.IsCancellationRequested)
            //    {
            //        await Task.Delay(5000, stoppingToken);

            //        using var scope = serviceProvider.CreateScope();

            //        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            //        var embeddingGenerator = scope.ServiceProvider.GetRequiredService<IEmbeddingGenerator<string, Embedding<float>>>();

            //        var products = await context
            //            .Products
            //            .AsNoTracking()
            //            .Where(p => p.Embedding1024 == null)
            //            .Take(100)
            //            .ToListAsync(stoppingToken);

            //        if(!products.Any())
            //        {
            //            continue;
            //        }

            //        var embeddings = await embeddingGenerator.GenerateAsync(products.Select(p => p.Description));
            //        var queryEmbeddings = embeddings
            //            .Select(e => e.Vector.ToArray())
            //            .ToList();

            //        for (var i = 0; i < queryEmbeddings.Count; i++)
            //        {
            //            products[i].Embedding1024 = new Vector(queryEmbeddings[i]);
            //        }

            //        context.Products.UpdateRange(products);
            //        await context.SaveChangesAsync(stoppingToken);
            //    }
        }
    }
}
