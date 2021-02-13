using System;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class OrderTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(15)]
        public void Order_AddProductToOrderSuccess(int quantity)
        {
            // arrange
            ICustomer customer = new Customer();
            IProduct product = new Product();
            Order order = new Order(customer);

            // act
            order.AddProductToOrder(product, quantity);

            int quantityAdded;
            bool foundOrder = order.ProductQuantity.TryGetValue(product, out quantityAdded);

            // assert
            Assert.True(foundOrder && quantity == quantityAdded);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-10)]
        public void Order_AddProductQuantityLessThanOne_Fail(int quantity)
        {
            // arrange
            ICustomer customer = new Customer();
            IProduct product = new Product();
            Order order = new Order(customer);

            // act
            Action addToOrder = () => order.AddProductToOrder(product, quantity);

            // assert
            Assert.Throws<ArgumentException>(addToOrder);
        }
    }
}
