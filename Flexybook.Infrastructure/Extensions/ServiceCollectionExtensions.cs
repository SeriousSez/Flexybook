using Microsoft.Extensions.DependencyInjection;
using Flexybook.Infrastructure.Repositories;

namespace Flexybook.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
