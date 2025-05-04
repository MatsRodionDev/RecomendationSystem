using Microsoft.AspNetCore.Mvc;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Models;
using RecomandationSystem.Application.Services;

namespace RecomendationSystem.Presentation.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class ProductController(IProductService productService) : ControllerBase
    {
        [HttpGet("recomendation/{userId}")]
        public async Task<IActionResult> Get(Guid userId, CancellationToken cancellationToken)
        {
            var products = await productService.GetRecomendationsAsync(userId, 10, cancellationToken);

            return Ok(products);
        }

        [HttpPost("by-term")]
        public async Task<IActionResult> Get([FromBody] string term, CancellationToken cancellationToken)
        {
            var products = await productService.GetAllAsync(term, 10, cancellationToken);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([FromBody] Product product, CancellationToken cancellationToken)
        {
            await productService.CreateOneAsync(product, cancellationToken);

            return Created();
        }

        [HttpPost("batch")]
        public async Task<IActionResult> CreateMany([FromBody] List<Product> products, CancellationToken cancellationToken)
        {
            await productService.CreateManyAsync(products, cancellationToken);

            return Created();
        }

        [HttpPost("{productId}/buy")]
        public async Task<IActionResult> CreateMany(Guid productId, [FromBody] Guid userId, CancellationToken cancellationToken)
        {
            await productService.BuyProductAsync(userId, productId, cancellationToken);

            return Created();
        }
    }
}
