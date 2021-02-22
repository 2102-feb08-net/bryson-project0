using System;
using Xunit;
using StoreApp.Library.Model;

namespace StoreApp.Library.Tests
{
    public class OrderTests
    {
        private readonly ICustomer customer = new Customer(firstName: "John", lastName: "Doe", id: 1);

        private readonly IProduct product = new ProductData(name: "Apple", category: "Food", unitPrice: 1.29m);

        private readonly Location location = new Location();

        [Fact]
        public void Order_Constructor_Success()
        {
            // arrange

            // act
            Order order = new Order(customer, location);

            // assert
            Assert.NotNull(order);
        }

        [Fact]
        public void Order_NullCustomer_Fail()
        {
            // arrange

            // act
            Order constructor() => new Order(null, location);

            // assert
            Assert.Throws<ArgumentNullException>(constructor);
        }

        [Fact]
        public void Order_NullLocation_Fail()
        {
            // arrange

            // act
            Order constructor() => new Order(customer, null);

            // assert
            Assert.Throws<ArgumentNullException>(constructor);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER)]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER)]
        public void Order_AddProductToOrder_Pass(int quantity)
        {
            // arrange
            Order order = new Order(customer, location);

            // act
            order.SetProductToOrder(product, quantity);

            bool foundOrder = order.ShoppingCartQuantity.TryGetValue(product, out int quantityAdded);

            // assert
            Assert.True(foundOrder && quantity == quantityAdded);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER - 1)]
        public void Order_AddProductQuantityLessThanOne_Fail(int quantity)
        {
            // arrange
            Order order = new Order(customer, location);

            // act
            void addToOrder() => order.SetProductToOrder(product, quantity);

            // assert
            Assert.Throws<ArgumentException>(addToOrder);
        }

        [Theory]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER + 1)]
        public void Order_AddExcessiveProductQuantity_Fail(int quantity)
        {
            // arrange
            Order order = new Order(customer, location);

            // act
            void addToOrder() => order.SetProductToOrder(product, quantity);

            // assert
            Assert.Throws<ArgumentException>(addToOrder);
        }
    }
}