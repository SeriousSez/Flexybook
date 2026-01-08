using Flexybook.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Flexybook.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for managing user data operations.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        protected internal readonly RestaurantContext _context;
        protected internal DbSet<UserEntity> _dbSet;

        public UserRepository(RestaurantContext context)
        {
            _context = context;
            _dbSet = _context.Set<UserEntity>();
        }

        /// <summary>
        /// Retrieves a user by ID.
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <returns>The user entity if found; otherwise, null.</returns>
        public virtual async Task<UserEntity?> GetAsync(Guid id)
        {
            return await _dbSet
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id.ToString());
        }

        /// <summary>
        /// Updates a user's information in the database.
        /// </summary>
        /// <param name="user">The user entity with updated information.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        public virtual async Task<bool> UpdateAsync(UserEntity user)
        {
            try
            {
                _dbSet.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
