using Flexybook.Domain.Entities.Restaurant;

namespace Flexybook.Infrastructure.Repositories
{
    public interface IRestaurantRepository
    {
        Task<Restaurant?> Get(Guid id);
        Task<Restaurant?> GetFull(Guid id);
        Task<IEnumerable<Restaurant>> GetAll();
        Task<IEnumerable<Restaurant>> GetAllFull();
    }
}
