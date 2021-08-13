namespace FoodTrucks.Api
{
    /// <summary>
    /// The Food Truck Model.
    /// </summary>
    /// <remarks>
    ///     Represents a single food truck.
    ///     The data model is minimal as part of the MVP but could easily be
    ///     extended without changing any other files within the project.
    /// </remarks>
    public sealed class FoodTruck
    {
        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the Block.
        /// </summary>
        public string Block { get; set; }

        /// <summary>
        /// Gets or sets the Location Id.
        /// </summary>
        public int LocationId { get; set; }
    }
}
