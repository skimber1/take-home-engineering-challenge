namespace FoodTrucks.Api
{
    /// <summary>
    /// The Food Truck Model.
    /// </summary>
    /// <remarks>Represents a single food truck.</remarks>
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
