using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodTrucks.Api.Services
{
    /// <summary>
    /// The Food Truck Service Implementation.
    /// </summary>
    /// <remarks>This is the primary service used managed all CRUD operations related to food trucks.</remarks>
    internal sealed class FoodTruckService : IFoodTruckService
    {
        /// <summary>
        /// Gets the Food Truck Store.
        /// </summary>
        private IFoodTruckStore FoodTruckStore { get; }

        // FUTURE: Include a Cache Service for short term data caching.

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodTrucksController"/> class.
        /// </summary>
        /// <param name="foodTruckStore">The <see cref="IFoodTruckStore"/>.</param>
        public FoodTruckService(IFoodTruckStore foodTruckStore)
        {
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
        public Task<FoodTruck> GetAsync(int locationId, CancellationToken cancellationToken)
        {
            // FUTURE: Implement a caching strategy to bypass the store for frequent requests.
            return FoodTruckStore.GetAsync(locationId, cancellationToken);
        }

        /// <summary>
        /// Gets the collection of food trucks for a given block id.
        /// </summary>
        /// <param name="block">The block id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<ICollection<FoodTruck>> GetByBlockIdAsync(string block, CancellationToken cancellationToken)
        {
            // FUTURE: Implement a caching strategy to bypass the store for frequent requests.
            return FoodTruckStore.GetByBlockIdAsync(block, cancellationToken);
        }
    }
}
