using StoreApp.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    /// <summary>
    /// Repository for manipulation of Order data
    /// </summary>
    public interface IOrderRepository
    {
        /// <summary>
        /// Retrieves all of the orders from the specified customer.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns>Returns a list of all orders found</returns>
        Task<List<IReadOnlyOrder>> GetOrdersFromCustomer(ICustomer customer);

        /// <summary>
        /// Retrieves all of the orders from the specified location name.
        /// </summary>
        /// <param name="locationName"></param>
        /// <returns>Returns a list of all orders found</returns>
        Task<List<IReadOnlyOrder>> GetOrdersFromLocation(string locationName);

        /// <summary>
        /// Sends and process an order to the database.
        /// </summary>
        /// <param name="order">The order to process</param>
        Task SendOrderTransaction(IOrder order);
    }
}