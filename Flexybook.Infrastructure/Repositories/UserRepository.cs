using Flexybook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flexybook.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected internal readonly RestaurantContext _context;
        protected internal DbSet<UserEntity> _dbSet;

        public UserRepository(RestaurantContext context)
        {
            _context = context;
            _dbSet = _context.Set<UserEntity>();
        }

        public virtual async Task<UserEntity?> GetAsync(Guid id)
        {
            return await _dbSet
                .Include(u => u.FavouredRestaurants)
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id.ToString());
        }
    }
}
