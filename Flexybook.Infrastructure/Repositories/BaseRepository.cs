using Microsoft.EntityFrameworkCore;
using Flexybook.Domain.Entities;

namespace Flexybook.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly RestaurantContext _context;
        protected internal DbSet<T> _dbSet;

        public BaseRepository(RestaurantContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T> Create(T baseEntity)
        {
            var entity = await _dbSet.AddAsync(baseEntity);
            await _context.SaveChangesAsync();
            return entity.Entity;
        }

        public async Task<T?> Get(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
