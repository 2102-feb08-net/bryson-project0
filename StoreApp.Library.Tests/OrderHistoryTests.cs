﻿using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using StoreApp.Library.Model;

namespace StoreApp.Library.Tests
{
    public class OrderHistoryTests
    {
        private readonly ICustomer customer = new Customer() { FirstName = "John", LastName = "Doe" };
        private readonly Location location = new Location();

        [Fact]
        public void OrderHistory_AddOrderToHistory_Successful()
        {
            // arrange
            OrderHistory history = new OrderHistory();
            IOrder order = new Order(customer, location);

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
            IOrder order = new Order(customer, location);

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
            List<IOrder> orders = new List<IOrder>(new Order[] {
                 new Order(customer, location)
            });

            foreach (var order in orders)
                history.TryAddOrderToHistory(order);

            // act
            List<IOrder> foundOrders = history.SearchByCustomer(customer);

            // assert
            Assert.Equal(orders.Count, foundOrders.Count);
        }
    }
}