using System;
using Xunit;
using StoreApp.Library.Model;
using System.Collections.Generic;

namespace StoreApp.Library.Tests
{
    public class OrderTests
    {
        private readonly ICustomer customer = new Customer(firstName: "John", lastName: "Doe", id: 1);

        private readonly IProduct product = new Product(name: "Apple", category: "Food", unitPrice: 1.29m, id: 1);

        [Fact]
        public void Order_Constructor_Success()
        {
            // arrange
            ILocation location = new Location("Location Name", "1234 Address", new Dictionary<IProduct, int>(), id: 1);

            // act
            Order order = new Order(customer, location);

            // assert
            Assert.NotNull(order);
        }

        [Fact]
        public void Order_NullCustomer_Fail()
        {
            // arrange
            ILocation location = new Location("Location Name", "1234 Address", new Dictionary<IProduct, int>(), id: 1);

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
        [InlineData(4)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER)]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER)]
        public void Order_AddProductToOrder_Pass(int quantity)
        {
            // arrange
            ILocation location = new Location("Location Name", "1234 Address", new Dictionary<IProduct, int>() { { product, quantity } }, id: 1);
            Order order = new Order(customer, location);

            // act
            bool success = order.TryAddProductToOrder(product, quantity);

            // assert
            Assert.True(success);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER)]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER)]
        public void Order_ShoppingCartQuantityEqualsAddProductToOrder_Pass(int quantity)
        {
            // arrange
            ILocation location = new Location("Location Name", "1234 Address", new Dictionary<IProduct, int>() { { product, quantity } }, id: 1);
            Order order = new Order(customer, location);
            order.TryAddProductToOrder(product, quantity);

            // act
            bool foundOrder = order.ShoppingCartQuantity.TryGetValue(product, out int quantityAdded);

            // assert
            Assert.True(foundOrder && quantity == quantityAdded);
        }

        [Theory]
        [InlineData(4)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER)]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER)]
        public void Order_InsufficientStockInLocation_Fail(int quantity)
        {
            // arrange
            ILocation location = new Location("Location Name", "1234 Address", new Dictionary<IProduct, int>() { { product, quantity - 1 } }, id: 1);
            Order order = new Order(customer, location);

            // act
            bool success = order.TryAddProductToOrder(product, quantity);

            // assert
            Assert.False(success);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER - 1)]
        public void Order_AddProductQuantityLessThanOne_Fail(int quantity)
        {
            // arrange
            ILocation location = new Location("Location Name", "1234 Address", new Dictionary<IProduct, int>() { { product, quantity } }, id: 1);
            Order order = new Order(customer, location);

            // act
            bool success = order.TryAddProductToOrder(product, quantity);

            // assert
            Assert.False(success);
        }

        [Theory]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER + 1)]
        public void Order_AddExcessiveProductQuantity_Fail(int quantity)
        {
            // arrange
            ILocation location = new Location("Location Name", "1234 Address", new Dictionary<IProduct, int>() { { product, quantity } }, id: 1);
            Order order = new Order(customer, location);

            // act
            bool success = order.TryAddProductToOrder(product, quantity);

            // assert
            Assert.False(success);
        }
    }
}