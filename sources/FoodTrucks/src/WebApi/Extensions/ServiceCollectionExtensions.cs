using FoodTrucks.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FoodTrucks.Api.Extensions
{
    /// <summary>
    /// The Service Collection Extensions.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds services to the service collection.
        /// </summary>
        /// <param name="serviceCollection">The Service Collection.</param>
        /// <returns>The updated service collection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton<IFoodTruckService, FoodTruckService>()
                .AddSingleton<IFoodTruckStore, FoodTruckInMemoryStoreService>();
    }
}
