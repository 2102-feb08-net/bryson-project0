using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class ProcessedOrder : IReadOnlyOrder
    {
        public ICustomer Customer { get; init; }

        public ILocation StoreLocation { get; init; }

        public IReadOnlyDictionary<IProduct, int> ShoppingCartQuantity { get; init; }

        public DateTimeOffset? OrderTime { get; }
        public int? Id { get; init; }

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