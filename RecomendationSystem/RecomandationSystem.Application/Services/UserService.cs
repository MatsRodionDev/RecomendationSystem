using RecomandationSystem.Application.Interfaces;
using RecomandationSystem.Application.Models;

namespace RecomandationSystem.Application.Services
{
    public sealed class UserService(ApplicationDbContext context) : IUserService
    {
        public async Task<Guid> CreateAsync(User user, CancellationToken cancellationToken) 
        {
            await context.Users.AddAsync(user, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
