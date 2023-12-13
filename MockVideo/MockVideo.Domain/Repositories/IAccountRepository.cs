using MockVideo.Domain.Models;

namespace MockVideo.Domain.Repositories;

public interface IAccountRepository : IGenericRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}