using Flexybook.Domain.Entities;
namespace Flexybook.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetAsync(Guid id);
    }
}
