using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class ProcessedOrder : IReadOnlyOrder
    {
        public ICustomer Customer { get; }

        public ILocation StoreLocation { get; }

        public IReadOnlyDictionary<IProduct, int> ShoppingCartQuantity { get; }

        public DateTimeOffset? OrderTime { get; }
        public int? Id { get; }

        /// <summary>
        /// Constructs a new Processed Order.
        /// </summary>
        /// <param name="customer">The customer in the order</param>
        /// <param name="storeLocation">The store location the order was sent to</param>
        /// <param name="productQuantities">The quantites of each product in the order</param>
        /// <param name="orderTime">The date and time of the order</param>
        /// <param name="id">The order number id</param>
        public ProcessedOrder(ICustomer customer, ILocation storeLocation, IDictionary<IProduct, int> productQuantities, DateTimeOffset orderTime, int id)
        {
            Customer = customer;
            StoreLocation = storeLocation;
            ShoppingCartQuantity = (IReadOnlyDictionary<IProduct, int>)productQuantities;
            OrderTime = orderTime;
            Id = id;
        }
    }
}