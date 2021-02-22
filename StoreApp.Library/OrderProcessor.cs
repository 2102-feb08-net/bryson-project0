using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library.Model;

namespace StoreApp.Library
{
    /// <summary>
    /// Helper class to provide additional functionality to orders
    /// </summary>
    public static class OrderProcessor
    {
        /// <summary>
        /// Calculates the total price of the order by summing up the unit prices of each product in the order and multiplied by their quantity.
        /// </summary>
        /// <param name="order">The order to get the total price of.</param>
        /// <returns>Returns the total price for the order.</returns>
        public static decimal CalculateTotalPrice(this IReadOnlyOrder order)
        {
            decimal total = 0;
            foreach (var pair in order.ShoppingCartQuantity)
            {
                IProduct saleItem = pair.Key;
                int quantity = pair.Value;
                total += saleItem.UnitPrice * quantity;
            }
            return total;
        }
    }
}