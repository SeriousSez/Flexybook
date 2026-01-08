using Flexybook.Domain.Entities;
using Flexybook.Domain.Responses;

namespace Flexybook.ApplicationService.Services
{
    public interface IUserService
    {
        Task<UserResponse?> GetUserAsync(string userId);
        Task<bool> UpdateUserFavouritesAsync(string userId, Guid restaurantId, bool isFavourite);
    }
}
