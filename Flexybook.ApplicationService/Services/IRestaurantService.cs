using Flexybook.Domain.Responses.Restaurant;
namespace Flexybook.ApplicationService.Services
{
    public interface IRestaurantService
    {
        Task<RestaurantResponse?> GetAsync(Guid id);
        Task<IEnumerable<RestaurantResponse>> GetAllAsync();
        Task<bool> UpdateAsync(RestaurantResponse restaurant);
    }
}
