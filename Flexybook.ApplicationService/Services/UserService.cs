using Flexybook.Domain.Entities;
using Flexybook.Domain.Responses;
using Flexybook.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Flexybook.ApplicationService.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<UserEntity> _userManager;

        public UserService(IUserRepository userRepository, UserManager<UserEntity> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }

        public async Task<UserResponse?> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user?.ToResponse();
        }

        public async Task<bool> UpdateUserFavouritesAsync(string userId, Guid restaurantId, bool isFavourite)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return false;

            var favourites = user.FavouredRestaurants?.ToList() ?? new List<Guid>();

            if (isFavourite && !favourites.Contains(restaurantId))
            {
                favourites.Add(restaurantId);
            }
            else if (!isFavourite && favourites.Contains(restaurantId))
            {
                favourites.Remove(restaurantId);
            }

            user.FavouredRestaurants = favourites;
            return await _userRepository.UpdateAsync(user);
        }
    }
}
