using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecomandationSystem.Application.BackgroundServices;
using RecomandationSystem.Application.Decorators;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Interfaces.UseCases;
using RecomandationSystem.Application.Models;
using RecomandationSystem.Application.Services;
using RecomandationSystem.Application.UseCases;
using RecomandationSystem.Application.UseCases.Commands;
using RecomandationSystem.BLL.Services;

namespace RecomandationSystem.Application.DI
{
    public static class DependencyInjection
    {
        public static void AddBLL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });


            //services.AddEmbeddingGenerator(
            //    new OllamaEmbeddingGenerator(configuration["Ollama:Url"]!, "mxbai-embed-large"));

            services
                .AddScoped<IProductService, ProductService>()
                .AddScoped<IUserService, UserService>()
                .AddScoped<IRagService, RagService>();

            services
                .AddSingleton<IDispatcher, Dispatcher>()
                .AddScoped<ICachedQueryHandler<GetProductsQuery, List<ProductDto>>, GetProductQueryHandler>()
                .AddScoped<ICacheService, CacheService>()
                .AddScoped<IEmbeddingService, EmbeddingService>();

            services.Decorate(typeof(ICachedQueryHandler<,>), typeof(CacheDecorator.QueryHandler<,>));
            services.Decorate(typeof(ICachedQueryHandler<,>), typeof(LogginDecorator.CachedQueryHandler<,>));

            services.AddHostedService<CreateProductsVectorJob>();
        }
    }
}
