using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library.Model
{
    public class Location : ILocation
    {
        public string Name { get; init; }
        public string Address { get; init; }

        public int Id { get; init; }

        public Dictionary<IProduct, int> Inventory { get; init; } = new Dictionary<IProduct, int>();

        /// <summary>
        /// Checks whether there is available stock of said product and quantity.
        /// </summary>
        /// <param name="product">The product to check for</param>
        /// <param name="quantity">The number of the product</param>
        /// <returns>Whether the location has the product and at least said quantity in stock</returns>
        public bool IsProductAvailable(IProduct product, int quantity)
        {
            return Inventory.ContainsKey(product) && Inventory[product] >= quantity;
        }

        /// <summary>
        /// Gets the amount of available stock of the product at the location.
        /// </summary>
        /// <param name="product">The product you want to check the stock of.</param>
        /// <returns>Returns the quantity of the product available</returns>
        public int GetAvailableStock(IProduct product)
        {
            return Inventory.ContainsKey(product) ? Inventory[product] : 0;
        }
    }
}