using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StoreApp.Library
{
    public class Order : IOrder
    {
        public const int NONE_QUANTITY = 0;
        public const int MIN_QUANTITY_PER_ORDER = 1;
        public const int MAX_QUANTITY_PER_ORDER = 20;

        public ICustomer Customer { get; }

        public Location StoreLocation { get; }

        private Dictionary<ISaleItem, int> _shoppingCartQuantity = new Dictionary<ISaleItem, int>();
        public IReadOnlyDictionary<ISaleItem, int> ShoppingCartQuantity => _shoppingCartQuantity;

        public DateTimeOffset? OrderTime { get; private set; } = null;

        public Guid ID { get; }

        public OrderState State { get; private set; }

        public decimal TotalPrice
        {
            get
            {
                decimal total = 0;
                foreach(var pair in _shoppingCartQuantity)
                {
                    ISaleItem saleItem = pair.Key;
                    int quantity = pair.Value;
                    total += saleItem.UnitPrice * quantity;
                }
                return total;
            }
        }

        public Order(ICustomer customer, Location storeLocation)
        {
            Customer = customer ?? throw new NullReferenceException();
            StoreLocation = storeLocation ?? throw new NullReferenceException();
            ID = Guid.NewGuid();
            State = OrderState.BeingBuilt;
        }

        public void SetProductToOrder(ISaleItem saleItem, int quantity)
        {
            if (quantity < MIN_QUANTITY_PER_ORDER)
                throw new ArgumentException("Quantity must be great than 0 for an order.");

            if (quantity > MAX_QUANTITY_PER_ORDER)
                throw new ExcessiveOrderException($"Cannot order more than {MAX_QUANTITY_PER_ORDER} in a single order.");

            _shoppingCartQuantity[saleItem] = quantity;
        }

        public void AddProductToOrder(ISaleItem saleItem)
        {

            if (_shoppingCartQuantity.ContainsKey(saleItem))
            {
                int quantity = _shoppingCartQuantity[saleItem];

                if (quantity + 1 > MAX_QUANTITY_PER_ORDER)
                    throw new ExcessiveOrderException($"Cannot order more than {MAX_QUANTITY_PER_ORDER} in a single order.");

                _shoppingCartQuantity[saleItem]++;
            }
            else
                _shoppingCartQuantity.Add(saleItem, 1);
        }

        public void ProcessOrder()
        {
            OrderTime = DateTime.Now;
            State = OrderState.Processed;
        }
    }
}
