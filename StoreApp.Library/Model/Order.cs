using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StoreApp.Library.Model
{
    public class Order : IOrder
    {
        public const int NONE_QUANTITY = 0;
        public const int MIN_QUANTITY_PER_ORDER = 1;
        public const int MAX_QUANTITY_PER_ORDER = 99;

        public ICustomer Customer { get; }

        public ILocation StoreLocation { get; }

        readonly private Dictionary<IProduct, int> _shoppingCartQuantity = new Dictionary<IProduct, int>();
        public IReadOnlyDictionary<IProduct, int> ShoppingCartQuantity => _shoppingCartQuantity;

        public DateTimeOffset? OrderTime => null;

        public int? Id => null;

        public Order(ICustomer customer, Location storeLocation)
        {
            Customer = customer ?? throw new NullReferenceException();
            StoreLocation = storeLocation ?? throw new NullReferenceException();
        }

        public void SetProductToOrder(IProduct product, int quantity)
        {
            if (quantity < MIN_QUANTITY_PER_ORDER)
                throw new ArgumentException("Quantity must be great than 0 for an order.");

            if (quantity > MAX_QUANTITY_PER_ORDER)
                throw new ArgumentException($"Cannot order more than {MAX_QUANTITY_PER_ORDER} in a single order.");

            _shoppingCartQuantity[product] = quantity;
        }

        public void AddProductToOrder(IProduct product)
        {
            if (_shoppingCartQuantity.ContainsKey(product))
            {
                int quantity = _shoppingCartQuantity[product];

                if (quantity + 1 > MAX_QUANTITY_PER_ORDER)
                    throw new ArgumentException($"Cannot order more than {MAX_QUANTITY_PER_ORDER} in a single order.");

                _shoppingCartQuantity[product]++;
            }
            else
                _shoppingCartQuantity.Add(product, 1);
        }

        public AttemptResult TryAddProductToOrder(IProduct product, int quantity = 1)
        {
            if (quantity < MIN_QUANTITY_PER_ORDER)
                return AttemptResult.Fail("Quantity must be great than 0 for an order.");

            int totalProductQuantity = quantity;

            bool existsInCart;
            if (existsInCart = _shoppingCartQuantity.ContainsKey(product))
                totalProductQuantity += _shoppingCartQuantity[product];

            if (totalProductQuantity > MAX_QUANTITY_PER_ORDER)
            {
                return AttemptResult.Fail($"Cannot order more than {MAX_QUANTITY_PER_ORDER} in a single order, yet you are attempting to order {totalProductQuantity}.\nYou may already the product in your cart.");
            }

            if (!StoreLocation.IsProductAvailable(product, totalProductQuantity))
                return AttemptResult.Fail($"The quantity is greater than the location's current stock of the product, which is {StoreLocation.GetAvailableStock(product)}");

            if (!existsInCart)
                _shoppingCartQuantity.Add(product, quantity);
            else
                _shoppingCartQuantity[product] += quantity;

            return AttemptResult.Success();
        }
    }
}