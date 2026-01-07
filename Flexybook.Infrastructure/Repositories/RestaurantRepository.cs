using Flexybook.Domain.Entities.Restaurant;
using Flexybook.Domain.Responses.Restaurant;
using Flexybook___Restaurant_Opening_Hours.Data;
using Microsoft.EntityFrameworkCore;

namespace Flexybook.Infrastructure.Repositories
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly RestaurantContext _context;

        public RestaurantRepository(RestaurantContext context)
        {
            _context = context;
        }

        public async Task<Restaurant?> Get(Guid id)
        {
            var Restaurant = await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == id);
            return Restaurant;
        }

        public async Task<Restaurant?> GetFull(Guid id)
        {
            var Restaurant = await _context.Restaurants
                .Include(r => r.Images)
                .Include(r => r.OpeningHours)
                .FirstOrDefaultAsync(r => r.Id == id);
            return Restaurant;
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            var restraurants = await _context.Restaurants.ToListAsync();
            return restraurants;
        }

        public async Task<IEnumerable<Restaurant>> GetAllFull()
        {
            var restraurants = await _context.Restaurants
                .Include(r => r.Images)
                .Include(r => r.OpeningHours)
                .ToListAsync();
            return restraurants;
        }
    }
}
