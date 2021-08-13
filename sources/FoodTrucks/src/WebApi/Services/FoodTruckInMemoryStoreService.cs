using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace FoodTrucks.Api.Services
{
    /// <summary>
    /// The Food Truck In-Memory Store.
    /// </summary>
    /// <remarks>This is an in-memory store for the collection of food trucks.</remarks>
    internal sealed class FoodTruckInMemoryStoreService : IFoodTruckStore
    {
        /// <summary>
        /// The Collection of Food Trucks.
        /// </summary>
        private ConcurrentDictionary<int, FoodTruck> FoodTrucks { get; } = new ConcurrentDictionary<int, FoodTruck>();

        /// <summary>
        /// Creates a new food truck.
        /// </summary>
        /// <param name="foodTruck">The <see cref="FoodTruck"/> to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task CreateAsync(FoodTruck foodTruck, CancellationToken cancellationToken)
        {
            if (foodTruck == null)
            {
                throw new ArgumentNullException(nameof(foodTruck));
            }

            if (foodTruck.LocationId == default)
            {
                throw new ArgumentException("And invalid food truck location id was provided.");
            }

            if (FoodTrucks.ContainsKey(foodTruck.LocationId))
            {
                throw new ArgumentException($"A food truck with the same location id already exists [LocationId='{foodTruck.LocationId}'].");
            }

            // The key check above with catch duplicates.
            // Add the item to the collection simply overwriting if it already exists.
            FoodTrucks.AddOrUpdate(foodTruck.LocationId, foodTruck, (key, old) => foodTruck);

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets a food truck for a specific location.
        /// </summary>
        /// <param name="locationId">The location id for the food truck.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<FoodTruck> GetAsync(int locationId, CancellationToken cancellationToken)
        {
            FoodTrucks.TryGetValue(locationId, out var result);

            return Task.FromResult(result);
        }
    }
}
