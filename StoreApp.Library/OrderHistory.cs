using System;
using System.Collections.Generic;
using StoreApp.Library.Model;

namespace StoreApp.Library
{
    public class OrderHistory
    {
        private List<IOrder> Orders { get; set; } = new List<IOrder>();

        public List<IOrder> SearchStoreLocation(Location location) => Orders.FindAll(o => o.StoreLocation == location);

        public List<IOrder> SearchByCustomer(ICustomer customer) => Orders.FindAll(o => o.Customer == customer);

        public bool TryAddOrderToHistory(IOrder order)
        {
            if (Orders.Exists(o => o.Id == order.Id))
                return false;

            Orders.Add(order);
            return true;
        }
    }
}