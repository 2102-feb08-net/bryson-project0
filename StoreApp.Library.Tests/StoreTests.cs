using System;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class StoreTests
    {
        [Fact]
        public void Store_AddEmptyOrderFail()
        {
            // arrange
            Store store = new Store();


            // act
            store.AddOrder(null);

            // assert
        }
    }
}
