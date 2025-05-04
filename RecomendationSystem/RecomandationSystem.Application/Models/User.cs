using Pgvector;
using RecomandationSystem.Application.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecomandationSystem.Application.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public int Age { get; set; }

        public Gender UserGender { get; set; }

        public List<Product> BuyedProducts { get; set; } = [];

        [Column(TypeName = "vector(1024)")]
        public Vector? CombinedEmbedding1024 { get; set; }
    }
}


