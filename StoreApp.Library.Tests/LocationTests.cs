using System;
using Xunit;
using StoreApp.Library.Model;

namespace StoreApp.Library.Tests
{
    public class LocationTests
    {
        private readonly ILocation location = new Location();

        private readonly IProduct apple = new Product(name: "Apple", category: "Food", unitPrice: 1.29m, id: 1);
        private readonly IProduct banana = new Product(name: "Banana", category: "Food", unitPrice: 0.79m, id: 2);

        [Fact]
        public void Location_IsProductAvailable_Success()
        {
            // arrange
            location.Inventory.Add(apple, 3);

            // act
            bool isAvailable = location.IsProductAvailable(apple, 3);

            // assert
            Assert.True(isAvailable);
        }

        [Fact]
        public void Location_IsProductAvailable_TooFew_Fail()
        {
            // arrange
            location.Inventory.Add(apple, 3);

            // act
            bool isAvailable = location.IsProductAvailable(apple, 4);

            // assert
            Assert.False(isAvailable);
        }

        [Fact]
        public void Location_IsProductAvailable_ProductDoesNotExist_Fail()
        {
            // arrange
            location.Inventory.Add(apple, 3);

            // act
            bool isAvailable = location.IsProductAvailable(banana, 3);

            // assert
            Assert.False(isAvailable);
        }
    }
}