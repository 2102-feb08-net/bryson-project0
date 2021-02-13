using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
