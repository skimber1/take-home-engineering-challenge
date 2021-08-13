using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FoodTrucks.Api
{
    /// <summary>
    /// The Food Truck Service.
    /// </summary>
    /// <remarks>This is the primary service used managed all CRUD operations related to food trucks.</remarks>
    public interface IFoodTruckService
    {
        /// <summary>
        /// Creates a new food truck.
        /// </summary>
        /// <param name="foodTruck">The <see cref="FoodTruck"/> to create.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task CreateAsync(FoodTruck foodTruck, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a food truck for a specific location.
        /// </summary>
        /// <param name="locationId">The location id for the food truck.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<FoodTruck> GetAsync(int locationId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the collection of food trucks for a given block.
        /// </summary>
        /// <param name="block">The block.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<ICollection<FoodTruck>> GetByBlockIdAsync(string block, CancellationToken cancellationToken);
    }
}
