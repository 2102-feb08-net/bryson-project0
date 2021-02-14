using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class OrderHistory
    {
        List<IOrder> orders = new List<IOrder>();

        public IEnumerable<IOrder> SearchStoreLocation(Location location)
        {
            return orders.Where(o => o.StoreLocation == location);
        }


        public IEnumerable<IOrder> SearchByCustomer(ICustomer customer)
        {
            return orders.Where(o => o.Customer == customer);
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