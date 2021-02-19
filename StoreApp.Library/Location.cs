using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class Location
    {
        public string Name { get; }
        public string Address { get; }

        public Dictionary<IProduct, int> Inventory = new Dictionary<IProduct, int>();

        /// <summary>
        /// Checks whether there is available stock of said product and quantity.
        /// </summary>
        /// <param name="product">The product to check for</param>
        /// <param name="quantity">The number of the product</param>
        /// <returns>Whether the location has the product and at least said quantity in stock</returns>
        public bool IsProductAvailable(IProduct product, int quantity)
        {
            return false;
        }
    }
}