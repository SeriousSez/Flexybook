using Flexybook.Domain.Entities;
using Flexybook.Infrastructure;
using Flexybook.Infrastructure.Seeders;
using Microsoft.AspNetCore.Identity;

namespace Flexybook.Extensions
{
    /// <summary>
    /// Extension methods for IApplicationBuilder to configure application startup.
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// Seeds the database with initial restaurant and user data.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        public static async Task SeedDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RestaurantContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<UserEntity>>();
            await DbSeeder.SeedAsync(db, userManager);
        }
    }
}
