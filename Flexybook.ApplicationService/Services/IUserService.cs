using Flexybook.Domain.Responses;

namespace Flexybook.ApplicationService.Services
{
    public interface IUserService
    {
        Task<UserResponse?> GetAsync(Guid id);
    }
}
