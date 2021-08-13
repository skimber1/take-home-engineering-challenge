using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodTrucks.Api
{
    /// <summary>
    /// The Truck Controller.
    /// </summary>
    /// <remarks>This controller is used to provide HTTP endpoints to consuming clients.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTrucksController : ControllerBase
    {
        /// <summary>
        /// Gets the Food Truck Service.
        /// </summary>
        private IFoodTruckService FoodTruckService { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FoodTrucksController"/> class.
        /// </summary>
        /// <param name="foodTruckService">The <see cref="IFoodTruckService"/>.</param>
        public FoodTrucksController(IFoodTruckService foodTruckService)
        {
            FoodTruckService = foodTruckService;
        }

        /// <summary>
        /// Creates a new food truck.
        /// </summary>
        /// <param name="foodTruck">The <see cref="FoodTruck"/> to create.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpPut]
        public async Task<IActionResult> CreateAsync(FoodTruck foodTruck)
        {
            IActionResult result;

            try
            {
                await FoodTruckService.CreateAsync(foodTruck, default);
                result = Ok();
            }
            catch (Exception e)
            {
                // TODO: Add Logging.

                result = Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    detail: e.Message);
            }

            return result;
        }

        /// <summary>
        /// Gets a food truck for a specific location.
        /// </summary>
        /// <param name="locationId">The location id for the food truck.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet("{locationId}")]
        public async Task<IActionResult> GetAsync(int locationId)
        {
            IActionResult result;

            try
            {
                var truckResult = await FoodTruckService.GetAsync(locationId, default);

                if (truckResult == null)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(truckResult);
                }
            }
            catch (Exception e)
            {
                // TODO: Add Logging.

                result = Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    detail: e.Message);
            }

            return result;
        }

        /// <summary>
        /// Gets the collection of food trucks for a given block.
        /// </summary>
        /// <param name="blockId">The block id.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [HttpGet("blocks/{block}/trucks")]
        public async Task<IActionResult> GetByBlockIdAsync(string block)
        {
            IActionResult result;

            try
            {
                var truckResult = await FoodTruckService.GetByBlockIdAsync(block, default);

                if (truckResult?.Count <= 0)
                {
                    result = NotFound();
                }
                else
                {
                    result = Ok(truckResult);
                }
            }
            catch (Exception e)
            {
                // TODO: Add Logging.

                result = Problem(
                    statusCode: StatusCodes.Status500InternalServerError,
                    detail: e.Message);
            }

            return result;
        }

    }
}
