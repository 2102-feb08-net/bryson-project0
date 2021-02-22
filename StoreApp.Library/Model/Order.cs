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

        public Order(ICustomer customer, ILocation storeLocation)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            StoreLocation = storeLocation ?? throw new ArgumentNullException(nameof(storeLocation), "Store location cannot be null.");
        }

        public AttemptResult TryAddProductToOrder(IProduct product, int quantity = 1)
        {
            if (quantity < MIN_QUANTITY_PER_ORDER)
                return AttemptResult.Fail("Quantity must be an integer greater than or equal to 1 for an order.");

            int totalProductQuantity = quantity;

            bool existsInCart;
            if (existsInCart = _shoppingCartQuantity.ContainsKey(product))
                totalProductQuantity += _shoppingCartQuantity[product];

            if (totalProductQuantity > MAX_QUANTITY_PER_ORDER)
            {
                return AttemptResult.Fail($"Cannot order more than {MAX_QUANTITY_PER_ORDER} in a single order, yet you are attempting to order {totalProductQuantity}.\nThe product may have already been added to your order previously.");
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