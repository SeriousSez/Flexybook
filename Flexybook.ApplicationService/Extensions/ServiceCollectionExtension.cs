using Microsoft.Extensions.DependencyInjection;
using Flexybook.ApplicationService.Services;
using Flexybook.ApplicationService.JwtFeatures;

namespace Flexybook.ApplicationService.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantService, RestaurantService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<JwtHandler>();
            return services;
        }
    }
}
