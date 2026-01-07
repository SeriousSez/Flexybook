using Flexybook.Domain.Responses.Restaurant;
using Flexybook.Infrastructure.Repositories;

namespace Flexybook.ApplicationService.Services
{
    public class RestaurantService(IRestaurantRepository restaurantRepository) : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

        public async Task<RestaurantResponse?> GetAsync(Guid id)
        {
            var restaurant = await _restaurantRepository.GetFullAsync(id);

            return restaurant?.ToResponse();
        }

        public async Task<IEnumerable<RestaurantResponse>> GetAllAsync()
        {
            var restaurants = await _restaurantRepository.GetAllFullAsync();

            return restaurants.Select(r => r.ToResponse());
        }

        public async Task<bool> UpdateAsync(RestaurantResponse restaurant)
        {
            var restaurantEntity = restaurant.ToEntity();
            return await _restaurantRepository.UpdateAsync(restaurantEntity);
        }
    }
}
