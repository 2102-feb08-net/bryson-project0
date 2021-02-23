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
    public class Location : ILocation
    {
        /// <summary>
        /// The display name of the location
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The formatted address of the location
        /// </summary>
        public string Address { get; }

        /// <summary>
        /// The ID of the location
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// The inventory of the location with each product and its corresponding quantity in stock.
        /// </summary>
        public IReadOnlyDictionary<IProduct, int> Inventory { get; } = new Dictionary<IProduct, int>();

        /// <summary>
        /// Checks whether there is available stock of said product and quantity.
        /// </summary>
        /// <param name="product">The product to check for</param>
        /// <param name="quantity">The number of the product</param>
        /// <returns>Whether the location has the product and at least said quantity in stock</returns>
        public bool IsProductAvailable(IProduct product, int quantity) => Inventory.ContainsKey(product) && Inventory[product] >= quantity;

        /// <summary>
        /// Gets the amount of available stock of the product at the location.
        /// </summary>
        /// <param name="product">The product you want to check the stock of.</param>
        /// <returns>Returns the quantity of the product available</returns>
        public int GetAvailableStock(IProduct product) => Inventory.ContainsKey(product) ? Inventory[product] : 0;

        /// <summary>
        /// Constructs a new Location with the specified name, address, and ID
        /// </summary>
        /// <param name="name">The name of the location.</param>
        /// <param name="address">The address of the location.</param>
        /// <param name="id">The ID of the location.</param>
        public Location(string name, string address, IDictionary<IProduct, int> inventory, int id)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));
            if (address is null)
                throw new ArgumentNullException(nameof(address));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(paramName: nameof(name), message: "Location name cannot be null or whitespace.");
            if (string.IsNullOrWhiteSpace(address))
                throw new ArgumentException(paramName: nameof(name), message: "Address cannot be null or whitespace.");
            if (id <= 0)
                throw new ArgumentException(paramName: nameof(id), message: "ID must be greater than 0.");

            Name = name;
            Address = address;
            Inventory = (IReadOnlyDictionary<IProduct, int>)inventory ?? throw new ArgumentNullException(nameof(inventory));
            Id = id;
        }
    }
}