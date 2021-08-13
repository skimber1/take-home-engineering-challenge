using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        /// The Collection of Food Trucks mapped to Blocks.
        /// </summary>
        private ConcurrentDictionary<string, HashSet<int>> FoodTruckBlockMapping { get; } = new ConcurrentDictionary<string, HashSet<int>>();

        /// <summary>
        /// Gets the Count of the Food Trucks.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<int> GetCountAsync(CancellationToken cancellationToken)
            => Task.FromResult(FoodTrucks.Count);

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

            if (foodTruck.LocationId <= 0)
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

            // Adds the Block mapping for the food truck.
            // If an entry for the block doesn't exist, a new entry is added with a new HashSet containing the food truck.
            // If an existing entry exists, the HashSet is updated.
            FoodTruckBlockMapping.AddOrUpdate(
                foodTruck.Block,
                new HashSet<int>
                {
                    foodTruck.LocationId,
                },
                (key, old) =>
                {
                    old.Add(foodTruck.LocationId);
                    return old;
                });

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

        /// <summary>
        /// Gets the collection of food trucks for a given block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public Task<ICollection<FoodTruck>> GetByBlockIdAsync(string block, CancellationToken cancellationToken)
        {
            ICollection<FoodTruck> result = new List<FoodTruck>();

            if (FoodTruckBlockMapping.TryGetValue(block, out var foodTruckLocationIds))
            {
                foreach (var foodTruckLocationId in foodTruckLocationIds)
                {
                    if (FoodTrucks.TryGetValue(foodTruckLocationId, out var foodTruck))
                    {
                        result.Add(foodTruck);
                    }
                }
            }

            return Task.FromResult(result);
        }
    }
}
