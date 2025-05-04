using NpgsqlTypes;
using Pgvector;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecomandationSystem.Application.Models
{
    public class Product
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "vector(1024)")]
        public Vector? Embedding1024 { get; set; }
    }

    public class ProductDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public double SimilarityScore { get; set; }
    }
}


