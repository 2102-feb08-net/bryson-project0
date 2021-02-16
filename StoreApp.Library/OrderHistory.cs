using System;
using System.Collections.Generic;

namespace StoreApp.Library
{
    public class OrderHistory
    {
        List<IOrder> orders = new List<IOrder>();

        public List<IOrder> SearchStoreLocation(Location location)
        {
            return orders.FindAll(o => o.StoreLocation == location);
        }


        public List<IOrder> SearchByCustomer(ICustomer customer)
        {
            return orders.FindAll(o => o.Customer == customer);
        }

        public bool TryAddOrderToHistory(IOrder order)
        {
            if (orders.Exists(o => o.ID == order.ID))
                return false;

            orders.Add(order);
            return true;
        }
    }
}