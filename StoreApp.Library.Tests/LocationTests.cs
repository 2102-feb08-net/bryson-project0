using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class LocationTests
    {
        Location location = new Location();

        [Fact]
        public void Location_IsProductAvailable_Success()
        {
            // arrange
            IProduct product = new Product() { Name = "Apple", Category = "Food", ID = new Guid("c73af0c5-fa46-406a-bccc-f14b134dcb3c") };
            location.Inventory.Add(product, 3);

            // act
            bool isAvailable = location.IsProductAvailable(product, 3);

            // assert
            Assert.True(isAvailable);
        }

        [Fact]
        public void Location_IsProductAvailable_TooFew_Fail ()
        {
            // arrange
            IProduct product = new Product() { Name = "Apple", Category = "Food", ID = new Guid("c73af0c5-fa46-406a-bccc-f14b134dcb3c") };
            location.Inventory.Add(product, 3);

            // act
            bool isAvailable = location.IsProductAvailable(product, 4);

            // assert
            Assert.False(isAvailable);
        }

        [Fact]
        public void Location_IsProductAvailable_ProductDoesNotExist_Fail()
        {
            // arrange
            IProduct apple = new Product() { Name = "Apple", Category = "Food", ID = new Guid("c73af0c5-fa46-406a-bccc-f14b134dcb3c") };
            IProduct banana = new Product() { Name = "Banana", Category = "Food", ID = new Guid("d11591a8-aec2-4d71-859c-1eee4674dadf") };
            location.Inventory.Add(apple, 3);

            // act
            bool isAvailable = location.IsProductAvailable(banana, 3);

            // assert
            Assert.False(isAvailable);
        }

    }
}
