using System;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class OrderTests
    {
        ICustomer customer = new Customer("John", "Doe", new Guid("a79a5d93-c569-49b4-b82d-f2ad980a9041"));
        IProduct product = new Product();

        [Theory]
        [InlineData(2)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER)]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER)]
        public void Order_AddProductToOrder_Success(int quantity)
        {
            // arrange
            Order order = new Order(customer);

            // act
            order.SetProductToOrder(product, quantity);

            int quantityAdded;
            bool foundOrder = order.ProductQuantity.TryGetValue(product, out quantityAdded);

            // assert
            Assert.True(foundOrder && quantity == quantityAdded);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(Order.MIN_QUANTITY_PER_ORDER - 1)]
        public void Order_AddProductQuantityLessThanOne_Fail(int quantity)
        {
            // arrange
            Order order = new Order(customer);

            // act
            Action addToOrder = () => order.SetProductToOrder(product, quantity);

            // assert
            Assert.Throws<ArgumentException>(addToOrder);
        }

        [Theory]
        [InlineData(Order.MAX_QUANTITY_PER_ORDER + 1)]
        [InlineData(100)]
        public void Order_AddExcessiveProductQuantity_Fail(int quantity)
        {
            // arrange
            Order order = new Order(customer);

            // act
            Action addToOrder = () => order.SetProductToOrder(product, quantity);

            // assert
            Assert.Throws<ExcessiveOrderException>(addToOrder);
        }
    }
}
