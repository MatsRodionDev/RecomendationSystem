using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Services;
using RecomandationSystem.BLL.Services;

namespace RecomandationSystem.Application.DI
{
    public static class DependencyInjection
    {
        public static void AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddEmbeddingGenerator(
                new OllamaEmbeddingGenerator(configuration["Ollama:Url"]!, "mxbai-embed-large"));

            services
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IUserService, UserService>();
        }
    }
}
