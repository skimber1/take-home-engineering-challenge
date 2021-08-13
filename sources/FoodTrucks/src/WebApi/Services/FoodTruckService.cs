using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace FoodTrucks.Api.Services
{
    /// <summary>
    /// The Food Truck Service Implementation.
    /// </summary>
    /// <remarks>This is the primary service used managed all CRUD operations related to food trucks.</remarks>
    internal sealed class FoodTruckService : IFoodTruckService
    {
        /// <summary>
        /// Gets the Cache.
        /// </summary>
        private IDistributedCache Cache { get; }

        /// <summary>
        /// Gets the Food Truck Store.
        /// </summary>
        private IFoodTruckStore FoodTruckStore { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodTrucksController"/> class.
        /// </summary>
        /// <param name="cache">The <see cref="IDistributedCache"/>.</param>
        /// <param name="foodTruckStore">The <see cref="IFoodTruckStore"/>.</param>
        public FoodTruckService(
            IDistributedCache cache,
            IFoodTruckStore foodTruckStore)
        {
            Cache = cache;
            FoodTruckStore = foodTruckStore;
        }

        /// <summary>
        /// Creates a new food truck.
        /// </summary>
        /// <param name="foodTruck">The <see cref="FoodTruck"/> to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task CreateAsync(FoodTruck foodTruck, CancellationToken cancellationToken)
            => FoodTruckStore.CreateAsync(foodTruck, cancellationToken);

        /// <summary>
        /// Gets a food truck for a specific location.
        /// </summary>
        /// <param name="locationId">The location id for the food truck.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task<FoodTruck> GetAsync(int locationId, CancellationToken cancellationToken)
        {
            var cacheKey = $"FoodTruck-LocationId-{locationId}";

            // Attempt to retrieve the item from the cache.
            var result = await Cache.GetStringAsync(cacheKey, cancellationToken);

            // If the item was not found, 
            if (result == null)
            {

            }

            return result;
        }
    }
}
