using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// An order that has been created and yet to be submitted and proccessed.
    /// </summary>
    public class Order : IOrder
    {
        public const int NONE_QUANTITY = 0;
        public const int MIN_QUANTITY_PER_ORDER = 1;
        public const int MAX_QUANTITY_PER_ORDER = 99;

        /// <summary>
        /// The customer who made the order.
        /// </summary>
        public ICustomer Customer { get; }

        /// <summary>
        /// The location that the order was placed from.
        /// </summary>
        public ILocation StoreLocation { get; }

        /// <summary>
        /// The products in the order and their corresponding quantities.
        /// </summary>
        public IReadOnlyDictionary<IProduct, int> ShoppingCartQuantity => _shoppingCartQuantity;

        readonly private Dictionary<IProduct, int> _shoppingCartQuantity = new Dictionary<IProduct, int>();

        /// <summary>
        /// The time the order was proccessed. Will always be null for this type.
        /// </summary>
        public DateTimeOffset? OrderTime => null;

        /// <summary>
        /// The ID of the order. Will always be null for this type.
        /// </summary>
        public int? Id => null;

        /// <summary>
        /// Constructs a new order with the specified customer and location
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="storeLocation"></param>
        public Order(ICustomer customer, ILocation storeLocation)
        {
            Customer = customer ?? throw new ArgumentNullException(nameof(customer), "Customer cannot be null.");
            StoreLocation = storeLocation ?? throw new ArgumentNullException(nameof(storeLocation), "Store location cannot be null.");
        }

        /// <summary>
        /// Attempts to add the specified product and quantities to the order.
        /// </summary>
        /// <param name="product">The product to order.</param>
        /// <param name="quantity">The quantity to order.</param>
        /// <returns>Returns an AttemptResult with a message if the order failed. Can be treated like a bool.</returns>
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