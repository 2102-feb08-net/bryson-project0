using System;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class LocationTests
    {
        Location location = new Location();

        IProduct apple = new Product() { Name = "Apple", Category = "Food", ID = new Guid("c73af0c5-fa46-406a-bccc-f14b134dcb3c") };
        IProduct banana = new Product() { Name = "Banana", Category = "Food", ID = new Guid("d11591a8-aec2-4d71-859c-1eee4674dadf") };

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
        public void Location_IsProductAvailable_TooFew_Fail ()
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
