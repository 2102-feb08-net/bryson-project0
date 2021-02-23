using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// A store location that contains a product inventory
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// The display name of the location
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The formatted address of the location
        /// </summary>
        string Address { get; }

        /// <summary>
        /// The ID of the location
        /// </summary>
        int Id { get; }

        /// <summary>
        /// The inventory of the location with each product and its corresponding quantity in stock.
        /// </summary>
        IReadOnlyDictionary<IProduct, int> Inventory { get; }

        /// <summary>
        /// Checks whether there is available stock of said product and quantity.
        /// </summary>
        /// <param name="product">The product to check for</param>
        /// <param name="quantity">The number of the product</param>
        /// <returns>Whether the location has the product and at least said quantity in stock</returns>
        bool IsProductAvailable(IProduct product, int quantity);

        /// <summary>
        /// Gets the amount of available stock of the product at the location.
        /// </summary>
        /// <param name="product">The product you want to check the stock of.</param>
        /// <returns>Returns the quantity of the product available</returns>
        int GetAvailableStock(IProduct product);
    }
}