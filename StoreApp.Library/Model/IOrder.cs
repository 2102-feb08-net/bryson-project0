using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// An order that supports adding new products to the order.
    /// </summary>
    public interface IOrder : IReadOnlyOrder
    {
        /// <summary>
        /// Attempts to add the specified product and quantities to the order.
        /// </summary>
        /// <param name="product">The product to order.</param>
        /// <param name="quantity">The quantity to order.</param>
        /// <returns>Returns an AttemptResult with a message if the order failed. Can be treated like a bool.</returns>
        AttemptResult TryAddProductToOrder(IProduct product, int quantity);
    }
}