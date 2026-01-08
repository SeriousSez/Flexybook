using Flexybook.Domain.Entities;
using Flexybook.Domain.Entities.Restaurant;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Flexybook.Infrastructure
{
    public class RestaurantContext : IdentityDbContext<UserEntity>
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    }
}
