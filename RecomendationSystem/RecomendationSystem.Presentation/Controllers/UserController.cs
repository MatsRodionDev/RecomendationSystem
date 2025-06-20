using Microsoft.AspNetCore.Mvc;
using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Models;

namespace RecomendationSystem.Presentation.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user, CancellationToken cancellationToken)
        {
            var id = await userService.CreateAsync(user, cancellationToken);

            return Ok(id);
        }
    }
}
