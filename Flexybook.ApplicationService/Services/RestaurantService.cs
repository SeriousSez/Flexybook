using Flexybook.Domain.Responses.Restaurant;
using Flexybook.Infrastructure.Repositories;

namespace Flexybook.ApplicationService.Services
{
    public class RestaurantService(IRestaurantRepository restaurantRepository) : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;

        public async Task<RestaurantResponse?> Get(Guid id)
        {
            var restaurant = await _restaurantRepository.GetFull(id);

            return restaurant?.ToResponse();
        }

        public async Task<IEnumerable<RestaurantResponse>> GetAll()
        {
            var restaurants = await _restaurantRepository.GetAllFull();

            return restaurants.Select(r => r.ToResponse());
        }
    }
}
