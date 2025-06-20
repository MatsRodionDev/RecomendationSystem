using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Interfaces.UseCases;
using RecomandationSystem.Application.Models;
using RecomandationSystem.Application.Services;
using RecomandationSystem.Application.UseCases;
using RecomandationSystem.Application.UseCases.Commands;

namespace RecomendationSystem.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController(
        IProductService productService,
        IDispatcher dispatcher) : ControllerBase
    {
        [HttpGet("recomendation/{userId}")]
        public async Task<IActionResult> Get(Guid userId, CancellationToken cancellationToken)
        {
            var products = await productService.GetRecomendationsAsync(userId, 10, cancellationToken);

            return Ok(products);
        }

        [Authorize]
        [HttpPost("by-term")]
        public async Task<IActionResult> Get([FromBody] string term, CancellationToken cancellationToken)
        {
            //var products = await productService.GetAllAsync(term, 10, cancellationToken);

            //return Ok(products);

            return Ok(await dispatcher.Dispatch(new GetProductsQuery(term, 10), cancellationToken));
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
