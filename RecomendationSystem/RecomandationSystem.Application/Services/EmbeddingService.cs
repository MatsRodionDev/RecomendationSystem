using Microsoft.Extensions.AI;
using RecomandationSystem.Application.Interfaces;

namespace RecomandationSystem.Application.Services
{
    internal sealed class EmbeddingService() : IEmbeddingService
    {
        public async Task<float[]> GetEmbeddingAsync(string text, CancellationToken cancellationToken)
        {
            return [];

            //var generatedEmbeddings = await embeddingGenerator.GenerateAsync([text], cancellationToken: cancellationToken);
            //var queryEmbedding = generatedEmbeddings.Single().Vector.ToArray();

            //return queryEmbedding;
        }

        public async Task<List<float[]>> GetEmbeddingsAsync(string[] texts, CancellationToken cancellationToken)
        {
            return [];

            //var generatedEmbeddings = await embeddingGenerator.GenerateAsync(texts, cancellationToken: cancellationToken);
            //var queryEmbedding = generatedEmbeddings.Select(e => e.Vector.ToArray()).ToList();

            //return queryEmbedding;
        }
    }
}
