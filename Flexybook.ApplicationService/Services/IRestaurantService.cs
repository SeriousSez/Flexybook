using Flexybook.Domain.Responses.Restaurant;
namespace Flexybook.ApplicationService.Services
{
    public interface IRestaurantService
    {
        Task<RestaurantResponse?> Get(Guid id);
        Task<IEnumerable<RestaurantResponse>> GetAll();
    }
}
