using Flexybook.Infrastructure;

namespace Flexybook___Restaurant_Opening_Hours.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void SeedDatabase(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<RestaurantContext>();
            DbSeeder.Seed(db);
        }
    }
}
