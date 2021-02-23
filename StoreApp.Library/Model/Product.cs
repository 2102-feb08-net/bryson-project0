using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    /// <summary>
    /// A product that can be added to an order
    /// </summary>
    public record Product : IProduct
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The category of the product.
        /// </summary>
        public string Category { get; }

        /// <summary>
        /// The unit price of the product in USD.
        /// </summary>
        public decimal UnitPrice { get; }

        /// <summary>
        /// The ID of the product.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Constructs a new ProductData
        /// </summary>
        /// <param name="name">The name of the product. Cannot be null or empty.</param>
        /// <param name="category">The category of the product. Cannot be null or empty.</param>
        /// <param name="unitPrice">The price in USD of the product. Must be greater than or equal to 0.</param>
        /// <param name="id">The product ID of the product. Must be greater than 0.</param>
        public Product(string name, string category, decimal unitPrice, int id)
        {
            if (name is null)
                throw new ArgumentNullException(paramName: nameof(name), message: "Product name cannot be null.");

            if (category is null)
                throw new ArgumentNullException(paramName: nameof(category), message: "Product category cannot be null.");

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(paramName: nameof(name), message: "Product name cannot be empty or whitespace.");

            if (string.IsNullOrWhiteSpace(category))
                throw new ArgumentException(paramName: nameof(category), message: "Product category cannot be empty or whitespace.");

            if (unitPrice < 0)
                throw new ArgumentException(paramName: nameof(unitPrice), message: "The price cannot be negative.");

            if (id <= 0)
                throw new ArgumentException(paramName: nameof(id), message: "The Product ID must be greater than 0.");

            Name = name;

            Category = category;

            UnitPrice = unitPrice;

            Id = id;
        }
    }
}