using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StoreApp.Library
{
    public class Order : IOrder
    {
        public const int MIN_QUANTITY_PER_ORDER = 1;
        public const int MAX_QUANTITY_PER_ORDER = 20;

        public ICustomer Customer { get; }

        public Location StoreLocation { get; }


        private Dictionary<IProduct, int> _productQuantity = new Dictionary<IProduct, int>();
        public IReadOnlyDictionary<IProduct, int> ProductQuantity => _productQuantity;

        public DateTime? OrderTime { get; private set; } = null;

        public Order(ICustomer customer)
        {
            Customer = customer ?? throw new NullReferenceException();
        }


        public void AddProductToOrder(IProduct product, int quantity)
        {
            if (quantity < MIN_QUANTITY_PER_ORDER)
                throw new ArgumentException("Quantity must be great than 0 for an order.");

            if (quantity > MAX_QUANTITY_PER_ORDER)
                throw new ExcessiveOrderException($"Cannot order more than {MAX_QUANTITY_PER_ORDER} in a single order.");

            _productQuantity.Add(product, quantity);
        }

        public void ProcessOrder()
        {

            OrderTime = DateTime.Now;
        }
    }
}
