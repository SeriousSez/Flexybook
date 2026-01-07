using Flexybook.Domain.Entities.Restaurant;

namespace Flexybook.Infrastructure.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> GetAsync(Guid id);
        Task<Restaurant?> GetFullAsync(Guid id);
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<IEnumerable<Restaurant>> GetAllFullAsync();
        Task<bool> UpdateAsync(Restaurant restaurant);
    }
}
