using Flexybook.Domain.Entities.Restaurant;
using Microsoft.EntityFrameworkCore;

namespace Flexybook.Infrastructure
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options) { }

        public DbSet<Restaurant> Restaurants => Set<Restaurant>();
    }
}
