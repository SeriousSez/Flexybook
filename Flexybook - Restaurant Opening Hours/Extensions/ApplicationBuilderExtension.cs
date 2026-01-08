using Flexybook.Domain.Entities;
using Flexybook.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Flexybook___Restaurant_Opening_Hours.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RestaurantContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            await DbSeeder.SeedAsync(db, userManager);
        }
    }
}
