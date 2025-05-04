using RecomandationSystem.Application.Models;

namespace RecomandationSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<Guid> CreateAsync(User user, CancellationToken cancellationToken);
    }
}
