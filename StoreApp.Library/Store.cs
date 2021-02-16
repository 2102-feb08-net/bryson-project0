using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace StoreApp.Library
{
    public class Store
    {
        List<IOrder> orderHistory = new List<IOrder>();

        public ReadOnlyCollection<IOrder> OrderHistory => orderHistory.AsReadOnly();

        public void AddCustomer(ICustomer customer)
        {

        }

        public void AddOrder(IOrder order)
        {

        }
    }
}
