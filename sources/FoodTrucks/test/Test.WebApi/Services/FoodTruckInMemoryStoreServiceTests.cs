using System;
using FoodTrucks.Api.Services;
using NUnit.Framework;

namespace FoodTrucks.Api.Tests
{
    /// <summary>
    /// The Food Truck In-Memory Store Service Tests.
    /// </summary>
    [TestFixture]
    internal class FoodTruckInMemoryStoreServiceTests
    {
        /// <summary>
        /// Gets the Food Truck In-Memory Store Service.
        /// </summary>
        private static FoodTruckInMemoryStoreService FoodTruckInMemoryStoreService { get; set; }

        /// <summary>
        /// The Setup that is executed prior to each test.1
        /// </summary>
        [SetUp]
        public static void Initialize()
        {
            FoodTruckInMemoryStoreService = new FoodTruckInMemoryStoreService();
        }

        /// <summary>
        /// Asserts that an Argument Exception is thrown when creating a null food truck.
        /// </summary>
        [Test]
        public static void AssertArgumentExceptionForCreateNullFoodTruck()
        {
            Assert.Throws<ArgumentNullException>(() => FoodTruckInMemoryStoreService.CreateAsync(null, default));
        }

        /// <summary>
        /// Asserts that an Argument Exception is thrown when creating a food truck with an invalid location id.
        /// </summary>
        /// <param name="locationId"></param>
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(-50)]
        public static void AssertArgumentExceptionForCreateInvaidLocationId(int locationId)
        {
            var foodTruck = new FoodTruck { LocationId = locationId };

            Assert.Throws<ArgumentException>(() => FoodTruckInMemoryStoreService.CreateAsync(foodTruck, default));
        }

        /// <summary>
        /// Asserts that an Argument Exception is thrown when creating a duplicate food truck.
        /// </summary>
        [Test]
        public static void AssertArgumentExceptionForDuplicateFoodTruck()
        {
            var foodTruck = new FoodTruck { LocationId = 1 };

            Assert.DoesNotThrow(() => FoodTruckInMemoryStoreService.CreateAsync(foodTruck, default));
            Assert.Throws<ArgumentException>(() => FoodTruckInMemoryStoreService.CreateAsync(foodTruck, default));
        }


        /// <summary>
        /// Asserts that items can be successfully added to the store.
        /// </summary>
        [Test]
        public static void AssertFoodTrucksAdded()
        {
            var trucksToAdd = 100;

            Assert.Multiple(async () =>
            {
                for (int i = 1; i < trucksToAdd + 1; i++)
                {
                    var foodTruck = new FoodTruck { LocationId = i };
                    Assert.DoesNotThrow(() => FoodTruckInMemoryStoreService.CreateAsync(foodTruck, default));
                }

                var trucksAdded = await FoodTruckInMemoryStoreService.GetCountAsync(default);

                Assert.AreEqual(trucksToAdd, trucksAdded);
            });
        }
    }
}
