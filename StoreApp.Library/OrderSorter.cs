using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    /// <summary>
    /// Helper methods to allow IReadOnlyOrders to be easily sorted according to various methods.
    /// </summary>
    public static class OrderSorter
    {
        /// <summary>
        /// Extension method to return an IEnumerable of IReadOnlyOrders sorted by the earliest order date and time.
        /// </summary>
        /// <param name="orders">The orders to sort</param>
        /// <returns>Returns the sorted IEnumerable</returns>
        public static IEnumerable<IReadOnlyOrder> SortByEarliest(this IEnumerable<IReadOnlyOrder> orders) => orders.OrderBy(o => o.OrderTime);

        /// <summary>
        /// Extension method to return an IEnumerable of IReadOnlyOrders sorted by the latest order date and time.
        /// </summary>
        /// <param name="orders">The orders to sort</param>
        /// <returns>Returns the sorted IEnumerable</returns>
        public static IEnumerable<IReadOnlyOrder> SortByLatest(this IEnumerable<IReadOnlyOrder> orders) => orders.OrderByDescending(o => o.OrderTime);

        /// <summary>
        /// Extension method to return an IEnumerable of IReadOnlyOrders sorted by the cheapest total price.
        /// </summary>
        /// <param name="orders">The orders to sort</param>
        /// <returns>Returns the sorted IEnumerable</returns>
        public static IEnumerable<IReadOnlyOrder> SortByCheapest(this IEnumerable<IReadOnlyOrder> orders) => orders.OrderBy(o => o.CalculateTotalPrice());

        /// <summary>
        /// Extension method to return an IEnumerable of IReadOnlyOrders sorted by the most expensive total price.
        /// </summary>
        /// <param name="orders">The orders to sort</param>
        /// <returns>Returns the sorted IEnumerable</returns>
        public static IEnumerable<IReadOnlyOrder> SortByMostExpensive(this IEnumerable<IReadOnlyOrder> orders) => orders.OrderByDescending(o => o.CalculateTotalPrice());
    }
}