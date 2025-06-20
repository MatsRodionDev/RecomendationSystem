using NpgsqlTypes;
using Pgvector;
using RecomandationSystem.Application.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecomandationSystem.Application.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public Categories Category { get; set; }

        [Column(TypeName = "vector(1024)")]
        public Vector? Embedding1024 { get; set; }
    }

    public class ProductDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Brand { get; set; } = string.Empty;

        public double Similiraty { get; set; }

        public Categories Category { get; set; }
    }
}


