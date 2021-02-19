using System;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class OrderTests
    {
        ICustomer customer = new Customer() { FirstName = "John", LastName = "Doe" };
        
        IProduct product = new Product() { Name = "Apple", Category = "Food"};

        Location location = new Location();


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
            Action constructor = () => new Order(null, location);

            // assert
            Assert.Throws<NullReferenceException>(constructor);
        }

        [Fact]
        public void Order_NullLocation_Fail()
        {
            // arrange

            // act
            Action constructor = () => new Order(customer, null);

            // assert
            Assert.Throws<NullReferenceException>(constructor);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER)]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER)]
        public void Order_AddProductToOrder_Success(int quantity)
        {
            // arrange
            Order order = new Order(customer, location);
            ISaleItem saleItem = new SaleItem() { Product = product, UnitPrice = 5 };

            // act
            order.SetProductToOrder(saleItem, quantity);

            int quantityAdded;
            bool foundOrder = order.ShoppingCartQuantity.TryGetValue(saleItem, out quantityAdded);

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
            ISaleItem saleItem = new SaleItem() { Product = product, UnitPrice = 5 };

            // act
            Action addToOrder = () => order.SetProductToOrder(saleItem, quantity);

            // assert
            Assert.Throws<ArgumentException>(addToOrder);
        }

        [Theory]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER + 1)]
        [InlineData(100)]
        public void Order_AddExcessiveProductQuantity_Fail(int quantity)
        {
            // arrange
            Order order = new Order(customer, location);
            ISaleItem saleItem = new SaleItem() { Product = product, UnitPrice = 5 };

            // act
            Action addToOrder = () => order.SetProductToOrder(saleItem, quantity);

            // assert
            Assert.Throws<ExcessiveOrderException>(addToOrder);
        }
    }
}
