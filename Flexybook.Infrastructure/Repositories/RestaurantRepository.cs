using Flexybook.Domain.Entities.Restaurant;
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

        public async Task<Restaurant?> GetAsync(Guid id)
        {
            return await _context.Restaurants
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Restaurant?> GetFullAsync(Guid id)
        {
            return await _context.Restaurants
                .Include(r => r.Images)
                .Include(r => r.OpeningHours)
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            return await _context.Restaurants.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllFullAsync()
        {
            return await _context.Restaurants
                .Include(r => r.Images)
                .Include(r => r.OpeningHours)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> UpdateAsync(Restaurant restaurant)
        {
            var tracked = _context.ChangeTracker.Entries<Restaurant>()
                .FirstOrDefault(e => e.Entity.Id == restaurant.Id);

            if (tracked != null)
                tracked.State = EntityState.Detached;

            _context.Restaurants.Update(restaurant);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
