using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class OrderHistoryTests
    {
        ICustomer customer = new Customer() { FirstName = "John", LastName = "Doe", ID = new Guid("a79a5d93-c569-49b4-b82d-f2ad980a9041") };
        

        [Fact]
        public void OrderHistory_AddOrderToHistory_Successful()
        {
            // arrange
            OrderHistory history = new OrderHistory();
            IOrder order = new Order(customer);

            // act
            history.TryAddOrderToHistory(order);

            // assert
            Assert.Contains(order, history.SearchByCustomer(customer));

        }

        [Fact]
        public void OrderHistory_AddDuplicateOrder_Fail()
        {
            // arrange
            OrderHistory history = new OrderHistory();
            IOrder order = new Order(customer);

            // act
            history.TryAddOrderToHistory(order);
            bool addedDuplicate = history.TryAddOrderToHistory(order);

            // assert
            Assert.False(addedDuplicate);
        }

        [Fact]
        public void OrderHistory_SearchByCustomer_Success()
        {
            // arrange
            OrderHistory history = new OrderHistory();
            List<IOrder> orders = new List<IOrder>( new Order[] {
                 new Order(customer)
            });

            foreach(var order in orders)
                history.TryAddOrderToHistory(order);

            // act
            List<IOrder> foundOrders = history.SearchByCustomer(customer);

            // assert
            Assert.Equal(orders.Count, foundOrders.Count);
        }

    }
}
