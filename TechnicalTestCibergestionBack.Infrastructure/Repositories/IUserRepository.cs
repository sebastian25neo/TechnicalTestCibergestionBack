using TechnicalTestCibergestionBack.Domain.Entities;

namespace TechnicalTestCibergestionBack.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
