using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StoreApp.Library
{
    public class Order : IOrder
    {
        public ICustomer Customer { get; }

        public Location StoreLocation { get; }


        private Dictionary<IProduct, int> _productQuantity = new Dictionary<IProduct, int>();
        public IReadOnlyDictionary<IProduct, int> ProductQuantity => _productQuantity;

        public DateTime OrderTime => throw new NotImplementedException();

        public Order(ICustomer customer)
        {
            Customer = customer ?? throw new NullReferenceException();
        }


        public void AddProductToOrder(IProduct product, int quantity)
        {
            if (quantity < 1)
                throw new ArgumentException("Quantity must be great than 0 for an order.");

            _productQuantity.Add(product, quantity);
        }
    }
}
